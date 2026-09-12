using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CymaLAB_Ver_1._0.Enums;

namespace CymaLAB_Ver_1._0
{
    static class Commands
    {


        private static readonly object pending_Command_Lock = new object();

        private static readonly List<byte[]> pending_Commands = new List<byte[]>(Constants.MAX_PENDING_COMMANDS);

        private static bool packet_Response_Mode_Active;


        // ============================================================
        // General command helpers
        // ============================================================

        private static byte[] Create_Command(DeviceCommand command)
        {
            byte[] packet = new byte[8];
            packet[0] = (byte)command;

            return packet;
        }

        private static void Finalize_And_Send_Command(byte[] packet, bool forceImmediate = false)
        {
            if (packet == null || packet.Length != 8)
            {
                throw new ArgumentException("The MCU command must contain exactly 8 bytes.", nameof(packet));
            }

            packet[7] = Utils.Calculate_Checksum8(packet, packet.Length);

            Submit_Uc_Command(packet, forceImmediate);
        }


        public static byte[] Submit_Uc_Command(byte[] command, bool forceImmediate)
        {
            byte[] commandCopy = (byte[])command.Clone();

            /*
             * Startup commands must be sent immediately because the MCU has
             * not started sending measurement packets yet.
             *
             * Commands issued while capture is stopped must also be immediate,
             * because no received packet exists to release a queued command.
             */
            if (forceImmediate || !packet_Response_Mode_Active || !capture_In_Progress)
            {
                return commandCopy;
            }

            Queue_Pending_Command(commandCopy);
            return null;
        }

        private static void Queue_Pending_Command(byte[] command)
        {
            if (command == null || command.Length != 8)
            {
                return;
            }

            lock (pending_Command_Lock)
            {
                /*
                 * Replace an older pending command having the same command ID.
                 *
                 * Example:
                 * Several zoom operations may generate several command 0x02
                 * packets. Only the newest sampling parameters are useful.
                 */
                for (int i = 0; i < pending_Commands.Count; i++)
                {
                    if (pending_Commands[i][0] == command[0])
                    {
                        pending_Commands[i] = (byte[])command.Clone();

                        return;
                    }
                }

                if (pending_Commands.Count >= Constants.MAX_PENDING_COMMANDS)
                {
                    pending_Commands.RemoveAt(0);
                }

                pending_Commands.Add((byte[])command.Clone());
            }
        }

        private static byte[] Take_Next_Pending_Command()
        {
            lock (pending_Command_Lock)
            {
                if (pending_Commands.Count == 0)
                {
                    return null;
                }

                byte[] command = pending_Commands[0];

                pending_Commands.RemoveAt(0);

                return command;
            }
        }

        private static void Clear_Pending_Commands()
        {
            lock (pending_Command_Lock)
            {
                pending_Commands.Clear();
            }
        }

        public static byte[] Send_Pending_Command_Or_Heartbeat(Connection_Mode connection_Mode)
        {
            if (!packet_Response_Mode_Active || connection_Mode != Connection_Mode.Connected)
            {
                return null;
            }

            byte[] response = Take_Next_Pending_Command();

            if (response == null)
            {
                response = (byte[])Constants.HEARTBEAT_COMMAND.Clone();
            }

            /*
             * Send directly here. Do not queue this response again.
             */
            return response;
        }

    }
}
