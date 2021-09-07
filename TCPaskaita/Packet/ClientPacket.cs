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

        private ushort prevCRC = 0;
        private int prevLength = 0;
        private bool dirty = true;

        public ulong DataLength {
            get { return Fields[LENGTH_IDX].ULong; }
            set
            {
                dirty = true;
                Fields[DATA_IDX].Value = new byte[value];
            }
        }
        public uint SequenceNumber {
            get { return (uint)Fields[SEQUENCE_NUMBER_IDX].ULong; }
            set
            {
                dirty = true;
                Fields[SEQUENCE_NUMBER_IDX].ULong = value;
            }
        }
        public uint PacketID {
            get { return (uint)Fields[PACKET_ID_IDX].ULong; }
            set
            {
                dirty = true;
                Fields[PACKET_ID_IDX].ULong = value;
            }
        }
        public byte[] Data {
            get { return Fields[DATA_IDX].Value; }
            set
            {
                dirty = true;
                Fields[DATA_IDX].Value = value;
            }
        }
        public ushort CRC
        {
            get
            {
                if (dirty)
                    Update();
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
            var dataField = new VariableLengthField((IntegerField)Fields[LENGTH_IDX]);
            Fields[DATA_IDX] = dataField;
            SequenceNumber = NextSequenceNumber++;
        }

        public ClientPacket(uint packetID)
        {
            InitFields();
            PacketID = PacketID;
            Data = Constants.NO_BYTES;
        }

        public ClientPacket(uint packetID, byte[] data)
        {
            InitFields();
            PacketID = packetID;
            Data = data;
        }

        private void Update()
        {
            prevLength = 0;
            for (int i = 0; i < Fields.Length; i++)
            {
                prevLength += Fields[i].Length;
            }
            prevCRC = Bytes.CRC(CRCStream());
            Fields[CRC_IDX].ULong = prevCRC;
            dirty = false;
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

        public int Length()
        {
            if (dirty)
                Update();
            return prevLength;
        }

        public override string ToString()
        {
            return String.Format(
                    "Size            {0,5:D} ({0:X3})\r\n"
                +   "Sqeuence number {1,5:D} ({1:X3})\r\n"
                +   "ID              {2,5:D} ({2:X3})\r\n"
                +   "CRC             {3:X4}\r\n"
                , DataLength, SequenceNumber, PacketID, CRC);
        }
    }
}
