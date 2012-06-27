using System;
using Microsoft.SPOT.Hardware;

namespace Robbo.Devices
{
    public class Accelerometer
    {
        public struct SensorData
        {
            public float X;
            public float Y;
            public float Z;
        }

        private enum RegisterMap : byte
        {
            BandwidthRate = 0x2c,
            PowerControl = 0x2d,
            DataFormat = 0x31,
            DataX0 = 0x32,
            FifoControl = 0x38
        }

        private const byte fifoOff = 0x0f;
        private const byte bandwidth25HzLowPower = 0x18;
        private const byte fixedResolution4GRange = 0x01;
        private const byte enableMeasurement = 0x08;

        private readonly I2CDevice.Configuration configuration;
        private readonly I2CDevice bus;

        public Accelerometer(I2CDevice bus, ushort deviceAddress = 0x53, int clockRate = 400)
        {
            this.bus = bus;
            configuration = new I2CDevice.Configuration(deviceAddress, clockRate);

            ConfigureDevice();
        }

        private void ConfigureDevice()
        {
            WriteRegister(RegisterMap.FifoControl, fifoOff); // turn off FIFO
            WriteRegister(RegisterMap.DataFormat, fixedResolution4GRange); // set the data to fixed resolution
            WriteRegister(RegisterMap.BandwidthRate, bandwidth25HzLowPower); // set rate to low power 25Hz data rate
            WriteRegister(RegisterMap.PowerControl, enableMeasurement); // enable measurement
        }

        private void WriteRegister(RegisterMap register, byte value)
        {
            var data = new[] { (byte)register, value };
            var write = I2CDevice.CreateWriteTransaction(data);
            var writeTransaction = new I2CDevice.I2CTransaction[] { write };

            lock (bus)
            {
                bus.Config = configuration;
                bus.Execute(writeTransaction, 10);
            }
        }

        private Byte[] rawData;
        private I2CDevice.I2CWriteTransaction getDataWriteTransaction;
        private I2CDevice.I2CReadTransaction getDataReadTransaction;
        private I2CDevice.I2CTransaction[] getDataTransaction;
        private const float resolutionMultiplier = 0.0078125f; // Fixed resolution at +/-4g = 8 / 1024 = 0.0078125.

        public SensorData GetData()
        {
            lock (bus)
            {
                if (rawData == null)
                {
                    rawData = new Byte[6];
                    getDataWriteTransaction = I2CDevice.CreateWriteTransaction(new[] { (Byte)RegisterMap.DataX0 });
                    getDataReadTransaction = I2CDevice.CreateReadTransaction(rawData);
                    getDataTransaction = new I2CDevice.I2CTransaction[] { getDataWriteTransaction, getDataReadTransaction };
                }
                bus.Config = configuration;
                bus.Execute(getDataTransaction, 50);
            }

            // Convert the raw byte data into the raw acceleration data for each axis and convert it into Gs

            return new SensorData
                       {
                           X = resolutionMultiplier * (short)(rawData[1] << 8 | rawData[0]),
                           Y = resolutionMultiplier * (short)(rawData[3] << 8 | rawData[2]),
                           Z = resolutionMultiplier * (short)(rawData[5] << 8 | rawData[4])
                       };
        }
    }
}
