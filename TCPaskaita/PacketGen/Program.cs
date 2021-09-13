using System;
using System.Collections.Generic;
using Packets;

namespace PacketGen
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] data = new byte[] { 0xAA, 0xAB, 0xAC };
            for (int i = 0; i < args.Length; i++)
            {
                data = ReadHex(args[i]).ToArray();
                ClientPacket cp = new ClientPacket(1, data);
                cp.SequenceNumber = 0xBBBBBB;
                cp.PacketID = 0xCCCCCC;
                byte[] clientData = new byte[cp.SerialisedLength()];
                cp.Write(clientData, 0);
                if (cp.Read(clientData, 0) != ReadState.Success)
                {
                    Console.Error.WriteLine("Malformed client packet:");
                    Console.Error.WriteLine(BitConverter.ToString(clientData));
                    return;
                }
                string hexString = BitConverter.ToString(clientData);
                Console.WriteLine("Client packet");
                Console.WriteLine(hexString);
                Console.WriteLine(cp.ToString());
                ServerPacket sp = new ServerPacket(cp);
                sp.PacketID = 0xAAAAAA;
                sp.SequenceNumber = 0xFFFFFF;
                byte[] serverData = new byte[sp.SerialisedLength()];
                sp.Write(serverData, 0);
                if (sp.Read(serverData, 0) != ReadState.Success)
                {
                    Console.Error.WriteLine("Malformed server packet:");
                    Console.Error.WriteLine(BitConverter.ToString(serverData));
                    return;
                }
                hexString = BitConverter.ToString(serverData);
                Console.WriteLine("\r\nServer packet:");
                Console.WriteLine(hexString);
                Console.WriteLine(sp.ToString());
            }
        }

        private static List<byte> ReadHex(string text)
        {
            int idx = 0;
            byte b = 0;
            List<byte> ret = new List<byte>();
            while (idx < text.Length)
            {
                ReadByte(text, ref idx, ref b);
                ret.Add(b);
            }
            return ret;
        }

        private static int ReadByte(string text, ref int idx, ref byte b)
        {
            int ret = 0;
            SkipWS(text, ref idx);
            b = (byte)(CharToNibble(text[idx++]) << 4);
            if (idx >= text.Length)
                return 0;
            SkipWS(text, ref idx);
            b |= CharToNibble(text[idx++]);

            return ret;
        }

        private static void SkipWS(string text, ref int idx)
        {
            while (char.IsWhiteSpace(text[idx]))
                idx++;
        }

        private static byte CharToNibble(char ch)
        {
            ch = char.ToUpper(ch);
            if (ch >= 'A' && ch <= 'F')
            {
                return (byte)((byte)(ch - 'A') + 10);
            }
            else if (ch >= '0' && ch <= '9')
            {
                return (byte)(ch - '0');
            }
            return 0;
        }
    }
}
