using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packets
{
    class VariableLengthField : Field
    {
        /// <summary>
        ///     Reference to field in packet that will contain length of data.
        ///     Refrain from manually setting length there.
        /// </summary>
        private IntegerField lengthField;
        private uint bias;

        public override byte[] Value {
            set
            {
                byte[] newVal = new byte[value.Length];
                value.CopyTo(newVal, 0);
                buf = newVal;
                lengthField.ULong = (uint)value.Length + bias;
            }
        }

        /// <summary>
        ///     Gets and sets new packet length. Setting a new length
        ///     creates a new empty buffer.
        /// </summary>
        public override int Length
        {
            get { return buf.Length; }
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                lengthField.ULong = (ulong)value + bias;
                buf = new byte[value];
            }
        }

        public VariableLengthField(IntegerField field, uint bias = 0)
        {
            if (field == null)
            {
                throw new ArgumentNullException();
            }
            lengthField = field;
            this.bias = bias;
            buf = Constants.NO_BYTES;
        }

        public override void Write(byte[] buf, ref int at)
        {
            if (at + Length >= buf.Length)
            {
                throw new ArgumentOutOfRangeException();
            }
            for (int i = 0; i < Length; i++)
            {
                buf[i + at] = Value[i];
            }
            at += Length;
        }

        public override ReadState Read(byte[] buf, ref int at)
        {
            int dataLength = (int)(lengthField.ULong - bias);
            if (at + Length >= buf.Length)
            {
                return ReadState.UnexpectedEndOfStream;
            }
            Value = new byte[dataLength];
            for (int i = 0; i < (int)dataLength; i++)
            {
                Value[i] = buf[at + i];
            }
            at += dataLength;
            return ReadState.Success;
        }
    }
}
