using Microsoft.SPOT;

namespace Robbo
{
    public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);

    public class MessageReceivedEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public MessageReceivedEventArgs(string message)
        {
            Message = message;
        }
    }
}