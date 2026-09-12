using Auto_Detect_VCP_Control;
using Hlk_Wifi_Wrapper_Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CymaLAB_Ver_1._0.Enums;
using System.Windows.Forms;

namespace CymaLAB_Ver_1._0
{
    class Communication
    {
        private CommunicationTransport active_Transport = CommunicationTransport.None;
        private HLK_Wifi_Wrapper WiFi;
        private Auto_Detect_VCP_Control.Auto_Detect_VCP_Control Vcp;

        private Connection_Mode connection_Mode = Connection_Mode.Disconnected;
        public Connection_Mode Connection_Mode
        {
            get { return connection_Mode; }
            set { connection_Mode = value; }
        }

        

        private Communication(HLK_Wifi_Wrapper wifi, Auto_Detect_VCP_Control.Auto_Detect_VCP_Control vcp)
        {
            WiFi = wifi;
            Vcp = vcp;
        }


        private void Initialize_VCP()
        {

            Vcp.Rx_Byte_Count = 8;          // receive  bytes every packet
            Vcp.Baud_Rate = 38400;
            Vcp.Is_Minimised = true;


            Vcp.Communication_Response = 0x55;
            Vcp.Communication_Response_Byte_Index = 2;

            Vcp.Start_Communication_Byte = 0x55;
            Vcp.Start_Communication_Byte_Index = 2;
            Vcp.Command_Bytes = (byte[])Constants.DETECTION_PC_COMMAND.Clone();


            Vcp.Connection_Ready += Vcp_Connection_Ready;
            Vcp.Normal_Operation_Starts += Vcp_Normal_Operation_Starts;
            Vcp.Connection_Failed += Vcp_Connection_Failed;
            Vcp.Received_Data_Ready += Vcp_Received_Data_Ready;

            Vcp.Start_VCP_Connection = true;
        }

        private void Vcp_Connection_Ready(object sender, EventArgs e)
        {

        }


        private void Vcp_Received_Data_Ready(object sender, EventArgs e)
        {
            if (active_Transport != CommunicationTransport.Usb)
            {
                return;
            }

            byte[] source = Vcp.Rx_Bytes;

            if (source == null || source.Length != Constants.RX_BUFFER_SIZE)
            {
                return;
            }

            byte[] receivedData = (byte[])source.Clone();

            Process_Complete_Rx_Packet(receivedData);
        }


        private void Send_Uc_Command(byte[] command, Connection_Mode connection_Mode)
        {
            if (command == null || command.Length != 8)
            {
                throw new ArgumentException(
                    "The MCU command must contain exactly 8 bytes.",
                    nameof(command));
            }

            if (connection_Mode != Connection_Mode.Connected)
            {
                return;
            }

            switch (active_Transport)
            {
                case CommunicationTransport.Wifi:
                    if (WiFi.IsConnected)
                    {
                        WiFi.Send_Data(command);
                    }
                    break;

                case CommunicationTransport.Usb:
                    if (Vcp.Is_Port_Open)
                    {
                        Vcp.Send_Data(command);
                    }
                    break;
            }
        }
    }
}
