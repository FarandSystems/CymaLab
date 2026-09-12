using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CymaLAB_Ver_1._0
{
    public static class Enums
    {
        public enum Connection_Mode
        {
            Disconnected = 0,
            Connected
        }

        public enum DeviceCommand : byte
        {
            CaptureControl = 0x01,
            SamplingParameters = 0x02,
            FilterParameters = 0x03,
            SignalIntensity = 0x04,
            FilterMode = 0x05,
            MeasurementOptions = 0x06,
            AveragingCount = 0x07,
            SelectChannel = 0x08,
            HighVoltageControl = 0x09,
            FirmwareUpdate = 0xFA
        }

        public enum CommunicationTransport
        {
            None,
            Wifi,
            Usb
        }
    }
}
