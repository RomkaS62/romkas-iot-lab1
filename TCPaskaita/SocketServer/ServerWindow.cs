using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Packets;

namespace MySocketServer
{
    /// <summary>
    ///     Server implementation. Responds to client packets. Response depends
    ///     on client packet ID.
    /// </summary>
    public partial class ServerWindow : Form
    {
        PacketServer packetServer;

        public delegate void Write(string text);

        public ServerWindow()
        {
            InitializeComponent();
        }

        public void ClientWriteLn(string text)
        {
            Invoke(new MethodInvoker(() =>
            {
                clientPacketTB.Text += text + "\r\n";
            }));
        }

        public void ServerWriteLn(string text)
        {
            Invoke(new MethodInvoker(() =>
            {
                serverPacketTB.Text += text + "\r\n";
            }));
        }

        private void StopButtonClick(object sender, EventArgs e)
        {
            packetServer?.Stop();
            packetServer = null;
        }

        private void Listen_Click(object sender, EventArgs e)
        {
            IPAddress ip;
            int port;

            if (packetServer != null)
            {
                packetServer.Stop();
                packetServer = null;
            }

            try
            {
                ip = IPAddress.Parse(textBoxIP.Text);
            }
            catch (Exception ex)
            {
                ServerWriteLn("Invalid IP address!");
                ServerWriteLn(ex.Message);
                return;
            }
            if (!int.TryParse(portTB.Text, out port))
            {
                ServerWriteLn("Invalid port!");
                return;
            }
            packetServer = new PacketServer(this, ip, port);
        }

        class PacketServer
        {
            ServerWindow GUI;
            Worker workerThread = new Worker();
            TcpListener listener;
            private volatile bool run = true;

            public PacketServer(ServerWindow gui, IPAddress ip, int port)
            {
                GUI = gui;
                listener = new TcpListener(ip, port);
                Thread t = new Thread(Run);
                t.IsBackground = true;
                t.Start();
            }

            ~PacketServer()
            {
                workerThread?.Kill();
                workerThread = null;
                listener?.Stop();
                listener = null;
            }

            public void Stop()
            {
                run = false;
            }

            public void Run()
            {
                GUI.ClientWriteLn("Listening for connections.");
                listener.Start();
                while (run)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    if (client == null)
                    {
                        continue;
                    }
                    GUI.ClientWriteLn("Incoming connection");
                    workerThread.Do(() =>
                    {
                        ProcessConnection(client);
                    });
                }
                listener.Stop();
            }

            private void ProcessConnection(TcpClient client)
            {
                byte[] buffer = new byte[1 << 10];
                int bytesRead = 0;
                int messageIndex = -1;
                ServerPacket reply = null;

                NetworkStream stream = client.GetStream();
                ClientPacket clientPacket = new ClientPacket(0);
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                messageIndex = clientPacket.FindStart(buffer);
                if (messageIndex < 0)
                {
                    GUI.ServerWriteLn("Invalid packet!");
                    Array.Resize(ref buffer, bytesRead);
                    GUI.ServerWriteLn(buffer.ToHexString());
                    return;
                }
                switch (clientPacket.Read(buffer, messageIndex))
                {
                    case ReadState.Success:
                        GUI.ClientWriteLn("Client packet:");
                        GUI.ClientWriteLn(buffer.ToHexString(0, bytesRead));
                        GUI.ClientWriteLn(clientPacket.ToString());
                        reply = new ServerPacket(clientPacket);
                        break;
                    case ReadState.UnexpectedEndOfStream:
                        GUI.ClientWriteLn("Could not receive entire packet in one go.");
                        break;
                    case ReadState.Fail:
                        GUI.ClientWriteLn("Malformed packet!");
                        GUI.ClientWriteLn(BitConverter.ToString(buffer, 0, clientPacket.SerialisedLength()));
                        GUI.ClientWriteLn(clientPacket.ToString());
                        break;
                }
                if (reply != null)
                {
                    reply.Write(buffer, 0);
                    stream.Write(buffer, 0, reply.SerialisedLength());
                    GUI.ServerWriteLn("Packet sent:");
                    GUI.ServerWriteLn(buffer.ToHexString(0, reply.SerialisedLength()));
                    GUI.ServerWriteLn(reply.ToString());
                }
                client.Close();
                stream.Close();
            }
        }
    }
}
