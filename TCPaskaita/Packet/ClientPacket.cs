using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Packets
{
    /// <summary>
    ///     Represents a client packet. Packet structure:
    ///     <list type="bullet">
    ///          <item>Start magic: 'RAM'.</item>
    ///          <item>Data length. 3 bytes.</item>
    ///          <item>Sequence number. 3 bytes.</item>
    ///          <item>Packet ID. 3 bytes.</item>
    ///          <item>Data. n bytes.</item>
    ///          <item>CRC. 2 bytes.</item>
    ///          <item>End magic: 'MAZ'.</item>
    ///     </list>
    /// </summary>
    public class ClientPacket : Packet
    {
        private static uint NextSequenceNumber = 0;

        private const int START_MAGIC_IDX = 0;
        private const int LENGTH_IDX = 1;
        private const int SEQUENCE_NUMBER_IDX = 2;
        private const int PACKET_ID_IDX = 3;
        private const int DATA_IDX = 4;
        private const int CRC_IDX = 5;
        private const int END_MAGIC_IDX = 6;

        public ulong DataLength {
            get { return (ulong)Fields[DATA_IDX].Length; }
            set
            {
                Fields[DATA_IDX].Value = new byte[value];
            }
        }
        public uint SequenceNumber {
            get { return (uint)Fields[SEQUENCE_NUMBER_IDX].ULong; }
            set
            {
                Fields[SEQUENCE_NUMBER_IDX].ULong = value;
            }
        }
        public uint PacketID {
            get { return (uint)Fields[PACKET_ID_IDX].ULong; }
            set
            {
                switch (value)
                {
                    case 1:
                    case 3:
                    case 5:
                        Fields[PACKET_ID_IDX].ULong = value;
                        break;
                    default:
                        throw new ArgumentException("Invalid packet ID: " + value);
                }
            }
        }
        public byte[] Data {
            get { return Fields[DATA_IDX].Value; }
            set { Fields[DATA_IDX].Value = value; }
        }
        public ushort CRC
        {
            get
            {
                Fields[CRC_IDX].ULong = Bytes.CRC(CRCStream());
                return (ushort)Fields[CRC_IDX].ULong;
            }
            set { Fields[CRC_IDX].ULong = value; }
        }

        private void InitFields()
        {
            Fields = new Field[] {
                new BinaryConstField(Constants.BEGIN_MAGIC),
                new IntegerField(3, 0),
                new IntegerField(3, 0),
                new IntegerField(3, 0),
                null,
                new IntegerField(2, 0),
                new BinaryConstField(Constants.END_MAGIC)
            };
            var dataField = new VariableLengthField((IntegerField)Fields[LENGTH_IDX],
                    3 + 3 + 2 + (uint)Constants.END_MAGIC.Length);
            Fields[DATA_IDX] = dataField;
            SequenceNumber = NextSequenceNumber++;
        }

        public ClientPacket(uint packetID)
        {
            InitFields();
            PacketID = packetID;
            Data = Constants.NO_BYTES;
        }

        public ClientPacket(uint packetID, byte[] data)
        {
            InitFields();
            PacketID = packetID;
            Data = data;
        }

        public override void Write(byte[] buf, ref int at)
        {
            base.Write(buf, ref at);
        }

        public override ReadState Read(byte[] buf, ref int at)
        {
            ReadState state =  base.Read(buf, ref at);

#if DEBUG
            if (state != ReadState.Success)
            {
                Console.Error.WriteLine("Malformed packet:");
                Console.Error.WriteLine(BitConverter.ToString(buf, at));
            }
#endif

            ushort expectedCRC = Bytes.CRC(CRCStream());
            if (Fields[CRC_IDX].ULong != expectedCRC)
            {
#if DEBUG
                Console.Error.WriteLine("Read failed. CRC does not match. Malformed client packet:");
                Console.Error.WriteLine(String.Format("Expected CRC: {0:X}", expectedCRC));
                Console.Error.WriteLine(String.Format("Received CRC: {0:X}", Fields[CRC_IDX].ULong));
                Console.Error.WriteLine(BitConverter.ToString(buf, 0, at));
#endif
                return ReadState.Fail;
            }

            return state;
        }

        public IEnumerable<byte> CRCStream()
        {
            for (int i = 2; i < Fields.Length - 2; i++)
            {
                for (int j = 0; j < Fields[i].Length; j++)
                {
                    yield return Fields[i].Value[j];
                }
            }
        }

        public override string ToString()
        {
            return String.Format(
                    "Data            {1}\r\n"
                +   "Size            {0,12:D} ({0,6:X6})\r\n"
                +   "Sqeuence number {2,12:D} ({2,6:X6})\r\n"
                +   "ID              {3,12:D} ({3,6:X6})\r\n"
                +   "CRC             {4,12:D} ({4,12:X})\r\n"
                , DataLength, Data.ToHexString(), SequenceNumber, PacketID, CRC);
        }

        protected override void InitWrite()
        {
            CRC = Bytes.CRC(CRCStream());
        }
    }
}
