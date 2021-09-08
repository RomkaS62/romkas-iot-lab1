using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packets
{
    /// <summary>
    ///     Represents a server packet. It is made of the following:
    ///     <list type="bullet">
    ///         <item>Start magic. 3 bytes.</item>
    ///         <item>Data length. 3 bytes.</item>
    ///         <item>Packet sequence number. 3 bytes.</item>
    ///         <item>Packet ID. 3 bytes</item>
    ///         <item>Client packet ID. 3 bytes.</item>
    ///         <item>Client packet sequence number. 3 bytes.</item>
    ///         <item>Data. n bytes.</item>
    ///         <item>CRC. 2 bytes.</item>
    ///         <item>End magic. 'MAZ'.</item>
    ///     </list>
    /// </summary>
    public class ServerPacket : Packet
    {
        private const int DATA_LENGTH_IDX = 1;
        private const int SEQUENCE_NUM_IDX = 2;
        private const int PACKET_ID_IDX = 3;
        private const int CLIENT_PACKET_ID_IDX = 4;
        private const int CLIENT_PACKET_SEQUENCE_NUM_IDX = 5;
        private const int DATA_IDX = 6;
        private const int CRC_IDX = 7;

        protected uint NextSequenceNumber = 0;

        public uint DataLength
        {
            get { return (uint)Fields[DATA_LENGTH_IDX].ULong; }
            set { Fields[DATA_LENGTH_IDX].ULong = value; }
        }
        public uint SequenceNumber
        {
            get { return (uint)Fields[SEQUENCE_NUM_IDX].ULong; }
            set { Fields[SEQUENCE_NUM_IDX].ULong = value; }
        }
        public uint PacketID
        {
            get { return (uint)Fields[PACKET_ID_IDX].ULong; }
            set { Fields[PACKET_ID_IDX].ULong = value; }
        }
        public uint ClientPacketID
        {
            get { return (uint)Fields[CLIENT_PACKET_ID_IDX].ULong; }
            set { Fields[CLIENT_PACKET_ID_IDX].ULong = value; }
        }
        public uint ClientPacketSequenceNumber
        {
            get { return (uint)Fields[CLIENT_PACKET_SEQUENCE_NUM_IDX].ULong; }
            set { Fields[CLIENT_PACKET_SEQUENCE_NUM_IDX].ULong = value; }
        }
        public byte[] Data
        {
            get { return Fields[DATA_IDX].Value; }
            set { Fields[DATA_IDX].Value = value; }
        }

        public ushort CRC
        {
            get { return (ushort)Fields[CRC_IDX].ULong; }
            set { Fields[CRC_IDX].ULong = value; }
        }

        private void InitFields()
        {
            Fields = new Field[] {
                new BinaryConstField(Constants.BEGIN_MAGIC),
                new IntegerField(3),        // Data length
                new IntegerField(3),        // Sequence number
                new IntegerField(3),        // Packet ID
                new IntegerField(3),        // Client packet ID.
                new IntegerField(3),        // Client packet sequence number
                null,
                new IntegerField(2),        // CRC
                new BinaryConstField(Constants.END_MAGIC)
            };
            var dataField =
                new VariableLengthField(
                    (IntegerField)Fields[DATA_LENGTH_IDX],
                    3 + 3 + 3 + 2 + (uint)Constants.END_MAGIC.Length);
            Fields[DATA_IDX] = dataField;
        }

        public ServerPacket()
        {
            InitFields();
            Fields[PACKET_ID_IDX].ULong = NextSequenceNumber++;
        }

        public ServerPacket(uint clientPacketID, uint clientPacketSeqNum, byte[] data)
        {
            InitFields();
            ClientPacketID = clientPacketID;
            ClientPacketSequenceNumber = clientPacketSeqNum;
            Data = data;
        }

        public ServerPacket(ClientPacket cl, byte[] data)
        {
            InitFields();
            ClientPacketID = cl.PacketID;
            ClientPacketSequenceNumber = cl.SequenceNumber;
            Data = data;
        }

        public ServerPacket(ClientPacket cp)
        {
            InitFields();
            ClientPacketID = cp.PacketID;
            ClientPacketSequenceNumber = cp.SequenceNumber;
            Data = new byte[cp.DataLength];
            Array.Copy(cp.Data, Data, Data.Length);
        }

        public override ReadState Read(byte[] buf, ref int at)
        {
            ReadState state = base.Read(buf, ref at);

            if (Fields[CRC_IDX].ULong != Bytes.CRC(CRCStream()))
                return ReadState.Fail;

            return state;
        }

        public override void Write(byte[] buf, ref int at)
        {
            Fields[CRC_IDX].ULong = Bytes.CRC(CRCStream());
            base.Write(buf, ref at);
        }

        private IEnumerable<byte> CRCStream()
        {
            for (int i = 0; i < Fields.Length; i++)
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
                        "Data length            {0,4:D} {0,03:X}\r\n"
                    +   "Sequence number        {1,4:D} {1,03:X}\r\n"
                    +   "Packet ID              {2,4:D} {2,03:X}\r\n"
                    +   "Client ID              {3,4:D} {3,03:X}\r\n"
                    +   "Client sequence number {4,4:D} {4,03:X}\r\n"
                    +   "CRC                    {5,4:D} {5,02:X}\r\n"
                    +   "Data                   {6}\r\n"
                    ,
                        DataLength, SequenceNumber, PacketID,
                        ClientPacketID, ClientPacketSequenceNumber,
                        CRC, Data.ToHexString()
                );
        }
    }
}
