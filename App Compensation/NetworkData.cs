using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace App_Compensation
{
    class NetworkData
    {
        public class UDPSocket
        {
            public class MessageEventArgs : EventArgs
            {
                public string MESSAGE { get; set; }
                public DateTime TIMESTAMP { get; set; }

                public MessageEventArgs(string MESSAGE, DateTime TIMESTAMP)
                {
                    this.MESSAGE = MESSAGE;
                    this.TIMESTAMP = TIMESTAMP;
                }
            }

            private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            private const int bufSize = 8 * 1024;
            private State state = new State();
            private EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0);
            private AsyncCallback recv = null;

            public class State
            {
                public byte[] buffer = new byte[bufSize];
            }

            public void Server(string address, int port)
            {
                _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
                _socket.Bind(new IPEndPoint(IPAddress.Parse(address), port));
                Receive();
            }

            public void Client(string address, int port)
            {
                _socket.Connect(IPAddress.Parse(address), port);
                Receive();
            }

            public void Send(string text)
            {
                byte[] data = Encoding.ASCII.GetBytes(text);
                _socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
                {
                    State so = (State)ar.AsyncState;
                    int bytes = _socket.EndSend(ar);
                }, state);
            }

            private void Receive()
            {
                _socket.BeginReceiveFrom(state.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv = (ar) =>
                {
                    State so = (State)ar.AsyncState;
                    int bytes = _socket.EndReceiveFrom(ar, ref epFrom);
                    _socket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv, so);

                    MessageEventArgs Message = new MessageEventArgs(Encoding.ASCII.GetString(so.buffer, 0, bytes), DateTime.Now);

                    EventValueChange(Message);
                }, state);
            }

            protected virtual void EventValueChange(MessageEventArgs e)
            {
                EventHandler<MessageEventArgs> handler = OnReciveMessage;

                if (handler != null)
                {
                    handler(this, e);
                }
            }

            public event EventHandler<MessageEventArgs> OnReciveMessage;
        }
    }
}
