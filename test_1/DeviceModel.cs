using System;

namespace zad1
{
    public class DeviceModel
    {
        private string _serialNumber;
        private string SerialNumber
        {
            get
            {
                return _serialNumber;
            }
            set
            {
                _serialNumber = value;
                _type = Convert.ToUInt16(_serialNumber[0].ToString(), 16);
                _model = Convert.ToUInt16(_serialNumber[7].ToString(), 16);
            }
        }

        private int _type;
        private int _model;
        private float[] _output = new float[8];
        private float[] _input = new float[4];

        /// <summary>
        /// Sets data to the device.
        /// </summary>
        public void SetOutput(float[] outputs)
        {
            Hardware.SetOutputs(outputs);
        }

        /// <summary>
        /// Reads data from the device.
        /// </summary>
        /// <param name="all">true: Retrieve serial number</param>
        public void ReadData(bool all = false)
        {
            if (all)
            {
                SerialNumber = Hardware.GetSerial();
            }

            _input = Hardware.ReadInputs();
            _output = Hardware.ReadOutputs();
        }

        /// <summary>
        /// Prints all known info about the device.
        /// </summary>
        public void PrintInfo()
        {
            Console.WriteLine($"Serial number:\t{_serialNumber}");
            Console.WriteLine($"Type:\t{_type}");
            Console.WriteLine($"Model:\t{_model}");
            Console.WriteLine($"Input:\t{_input[0]}\t{_input[1]}\t{_input[2]}\t{_input[3]}");
            Console.WriteLine($"Output:\t{_output[0]}\t{_output[1]}\t{_output[2]}\t{_output[3]}\t{_output[4]}\t{_output[5]}\t{_output[6]}\t{_output[7]}");
        }
    }
}