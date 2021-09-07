using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packets
{
    class BinaryConstField : Field
    {
        /// <summary>
        ///     Constructs a binary constant field for a packet.
        /// </summary>
        /// <param name="val">Constant value to check for in a packet.</param>
        public BinaryConstField(byte[] val)
        {
            if (val == null)
            {
                throw new ArgumentNullException();
            }
            this.Value = val;
        }

        public override void Write(byte[] buf, ref int at)
        {
            if (at + Length > buf.Length)
            {
                throw new ArgumentOutOfRangeException();
            }
            for (int i = 0; i < Length; i++)
            {
                buf[at + i] = Value[i];
            }
            at += Length;
        }

        public override ReadState Read(byte[] buf, ref int at)
        {
            int start = at;
            for (int i = 0; i < Length; i++)
            {
                if (i + at >= buf.Length)
                {
                    at = start;
                    return ReadState.UnexpectedEndOfStream;
                }
                if (buf[i + at] != Value[i])
                {
                    at = start;
                    return ReadState.Fail;
                }
            }
            at += Length;
            return ReadState.Success;
        }
    }
}
