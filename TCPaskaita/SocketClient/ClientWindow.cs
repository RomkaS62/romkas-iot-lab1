using System;
using System.IO;
using System.Windows.Forms;
using System.Net.Sockets;
using Packets;

namespace PacketClient
{
    public partial class Form1 : Form
    {
        Worker worker = new Worker();

        public Form1()
        {
            InitializeComponent();
        }

        public void WriteClientPacketTB(string data)
        {
            Invoke(new MethodInvoker(() => {
                clientPacketTB.AppendText(data + "\r\n");
            }));
        }

        public void WriteServerPacketTB(string data)
        {
            Invoke(new MethodInvoker(() => {
                serverPacketTB.AppendText(data + "\r\n");
            }));
        }

        private string PacketToString(Packet cp, byte[] serialisedPacket)
        {
            return
                "Packet: " + serialisedPacket.ToHexString() + "\r\n"
             +  "------------------------------------------" + "\r\n"
             +  cp.ToString()
             +  "------------------------------------------" + "\r\n"
             ;
        }

        private void Packet1Click(object sender, EventArgs e)
        {
            DispatchPacket(1, Constants.NO_BYTES);
        }

        private void Packet2Click(object sender, EventArgs e)
        {
            DispatchPacket(3, new byte[] { 0xAB });
        }

        private void Packet3Click(object sender, EventArgs e)
        {
            DispatchPacket(5, new byte[] { 1, 2, 3});
        }

        private void ClientP4Btn_Click(object sender, EventArgs e)
        {
            DispatchPacket(5, new byte[] { 0x56, 0x3b, 0x44, 0x63, 0x3e });
        }

        private void DispatchPacket(uint id, byte[] data)
        {
            ClientPacket cp = new ClientPacket(id, data);
            byte[] serialisedPacket = cp.ToByteArray();
            clientPacketTB.Text += PacketToString(cp, serialisedPacket);
            string hostname = hostnameTB.Text;
            int port;
            if (!int.TryParse(PortTB.Text, out port))
            {
                WriteClientPacketTB("Invalid port.");
                return;
            }
            worker.Do(() => {
                try
                {
                    byte[] receiveBuffer = new byte[1 << 10];
                    TcpClient client = new TcpClient(hostname, port);
                    NetworkStream stream = client.GetStream();
                    stream.Write(serialisedPacket, 0, serialisedPacket.Length);
                    int bytesRead = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                    ServerPacket sp = new ServerPacket();
                    switch (sp.Read(receiveBuffer, 0))
                    {
                        case ReadState.Success:
                            WriteServerPacketTB("Server packet:");
                            byte[] sps = new byte[sp.SerialisedLength()];
                            sp.Write(sps, 0);
                            WriteServerPacketTB(BitConverter.ToString(sps));
                            WriteServerPacketTB(sp.ToString());
                            break;
                        case ReadState.UnexpectedEndOfStream:
                            WriteServerPacketTB("Could not read entire packet in one go.");
                            break;
                        case ReadState.Fail:
                            WriteServerPacketTB("Malformed packet");
                            WriteServerPacketTB(BitConverter.ToString(receiveBuffer, 0, bytesRead));
                            WriteServerPacketTB(sp.ToString());
                            break;
                    }
                }
                catch (Exception ex)
                {
                    WriteClientPacketTB(ex.Message);
                    return;
                }
            });
        }
    }
}
