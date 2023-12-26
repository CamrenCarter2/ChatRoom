using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Chatroom
{
    internal class ConnectionHandler : ConnectionHandlerBase
    {
        private Socket socket;
        private readonly ManualResetEvent connectDone = new ManualResetEvent(false);
        private readonly ManualResetEvent sendDone = new ManualResetEvent(false);
        private readonly ManualResetEvent receiveDone = new ManualResetEvent(false);

        public void StartClient(string serverIP, int port)
        {

            try
            {
                IPAddress ipAddress = IPAddress.Parse(serverIP);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                socket.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), socket);
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.WriteLine("Stack Trace: " + e.StackTrace);
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);

                Console.WriteLine("Connected to peer");

                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void StartServer(int port)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Any;
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

                socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                socket.Bind(localEndPoint);
                socket.Listen(10);

                Console.WriteLine("Waiting for a connection...");

                socket.BeginAccept(new AsyncCallback(AcceptCallback), socket);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                Socket listener = (Socket)ar.AsyncState;
                Socket handler = listener.EndAccept(ar);

                Console.WriteLine("Peer connected to server");

                Receive(handler);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void SendMessage(string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
                sendDone.WaitOne();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
                Console.WriteLine($"Sent {bytesSent} bytes to peer");

                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Receive(Socket handler)
        {
            try
            {
                StateObject state = new StateObject { WorkSocket = handler };
                handler.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.WorkSocket;

                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    byte[] receivedData = new byte[bytesRead];
                    Array.Copy(state.Buffer, receivedData, bytesRead);

                    // Process received PictureBox data
                    PictureBox hostPictureBox = ConvertByteArrayToPictureBox(receivedData);

                    // Invoke an event or method to pass the received PictureBox to Form1
                    OnHostPictureBoxReceived(hostPictureBox);

                    handler.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // Add an event to pass the received PictureBox to Form1
        public event EventHandler<PictureBox> HostPictureBoxReceived;

        protected virtual void OnHostPictureBoxReceived(PictureBox hostPictureBox)
        {
            HostPictureBoxReceived?.Invoke(this, hostPictureBox);
        }

        public void CloseConnection()
        {
            try
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private class StateObject
        {
            public const int BufferSize = 1024;
            public byte[] Buffer = new byte[BufferSize];
            public Socket WorkSocket;
        }
        // Add these methods in the ConnectionHandler class

        public void SendHostPictureBox(PictureBox hostPictureBox)
        {
            try
            {
                // Convert the PictureBox data to a byte array
                byte[] data = ConvertPictureBoxToByteArray(hostPictureBox);

                // Send the PictureBox data
                socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendHostPictureBoxCallback), socket);
                sendDone.WaitOne();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void SendHostPictureBoxCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
                Console.WriteLine($"Sent {bytesSent} bytes of PictureBox data to client");

                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private byte[] ConvertPictureBoxToByteArray(PictureBox pictureBox)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                pictureBox.Image.Save(ms, pictureBox.Image.RawFormat);
                return ms.ToArray();
            }
        }

        private PictureBox ConvertByteArrayToPictureBox(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return new PictureBox
                {
                    Image = Image.FromStream(ms),
                    Size = new Size(50, 50),
                    BackColor = Color.Blue,
                    Visible = true,
                    Name = "Host"
                };
            }
        }

    }
}