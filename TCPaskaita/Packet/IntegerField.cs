using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packets
{
    /// <summary>
    ///     Represents an integer field in a byte packet.
    /// </summary>
    /// <remarks>
    ///     Internally values are represented by largest supported integer
    ///     type. It is assumed that multi-byte integers in packets are
    ///     stored int big-endian format.
    /// </remarks>
    class IntegerField: Field
    {
        /// <summary>
        ///     Represents underlying integer value. Truncated to
        ///     IntegerField.Length bytes.
        /// </summary>
        public ulong Long
        {
            get
            {
                return Bytes.ReadULongBE(Value, 0, Length);
            }
            set
            {
                Bytes.WriteULongBE(Value, 0, value, Length);
            }
        }

        /// <summary>
        ///     Constructs an IntegerField definition.
        /// </summary>
        /// <param name="length">Length of underlying Big-Endian integer.</param>
        /// <param name="value">
        ///     Current value of the field. Can be read from or written to buffer.
        /// </param>
        public IntegerField(int length, ulong value = 0)
        {
            if (length > sizeof(long))
            {
                throw new ArgumentOutOfRangeException();
            }
            Value = new byte[length];
            Length = length;
            Long = value;
        }

        public override void Write(byte[] buf, ref int at)
        {
            ulong val = Long;
            int b = Length - 1;
            if (Length + at >= buf.Length)
            {
                throw new ArgumentOutOfRangeException();
            }
            for (int i = at; i < buf.Length && i < at + Length; i++)
            {
                buf[i] = Bytes.GetByte(val, b--);
            }
            at += Length;
        }

        public override ReadState Read(byte[] buf, ref int at)
        {
            int start = at;
            if (at + Length >= buf.Length)
            {
                at = start;
                return ReadState.UnexpectedEndOfStream;
            }
            ULong = Bytes.ReadULongBE(buf, ref at, Length);
            at += Length;
            return ReadState.Success;
        }
    }
}
