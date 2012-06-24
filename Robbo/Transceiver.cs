using System;
using System.IO.Ports;
using System.Text;
using Microsoft.SPOT;

namespace Robbo
{
    public class Transceiver : IDisposable
    {
        private readonly SerialPort port;

        public event MessageReceivedEventHandler MessageReceived;

        public Transceiver(string portName, int baudRate = 96000, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            port = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            port.Open();
            port.DataReceived += PortOnDataReceived;
            Debug.Print("Created transceiver on port " + portName);
        }

        private void PortOnDataReceived(object sender, SerialDataReceivedEventArgs serialDataReceivedEventArgs)
        {
            var bytesReceived = ((SerialPort)sender).BytesToRead;
            var bytes = new byte[bytesReceived];
            ((SerialPort)sender).Read(bytes, 0, bytes.Length);
            Debug.Print("Received raw message on transceiver: " + bytes);
            var message = new string(Encoding.UTF8.GetChars(bytes));
            Debug.Print("Received message on transceiver: " + message);
            OnMessageReceived(message);
        }

        private void OnMessageReceived(string message)
        {
            if (MessageReceived != null) MessageReceived(this, new MessageReceivedEventArgs(message));
        }

        public void Send(string message)
        {
            Debug.Print("Sending message on transceiver: " + message);
            Send(Encoding.UTF8.GetBytes(message));
        }

        public void Send(byte[] bytes)
        {
            port.Write(bytes, 0, bytes.Length);
            port.Flush();
        }

        public void Dispose()
        {
            port.Close();
            port.DataReceived -= PortOnDataReceived;
            port.Dispose();
        }
    }
}