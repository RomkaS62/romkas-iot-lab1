using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packets
{
    /// <summary>
    ///     Constants for packet-reading state machine.
    /// </summary>
    public enum ReadState
    {
        /// <summary>
        ///     The stream was read to the end successfully.
        /// </summary>
        Success,
        /// <summary>
        ///     Packet buffer ended unexpectedly but it is possible to continue
        ///     with more data. Not a format error per se.
        /// </summary>
        UnexpectedEndOfStream,
        /// <summary>
        ///     Malformed packet. Data is useless.
        /// </summary>
        Fail
    }

    /// <summary>
    ///     Represents a single field in a packet.
    /// </summary>
    /// <remarks>
    ///     Assumes big-endian format of multi-byte integers in byte packets.
    /// </remarks>
    public abstract class Field
    {
        protected byte[] buf;

        /// <summary>
        ///     Represents length of underlying buffer.
        /// </summary>
        public virtual int Length {
            get { return buf.Length; }
            protected set { buf = new byte[value]; }
        }

        /// <summary>
        ///     Representation of a value as it would appear serialised in a
        ///     buffer.
        /// </summary>
        public virtual byte[] Value {
            get { return buf; }
            set
            {
                byte[] newval = new byte[value.Length];
                value.CopyTo(newval, 0);
                buf = newval;
            }
        }

        /// <summary>
        ///     Writes an integer value to buffer in Big-Endian format truncated
        ///     to Field.Length bytes.
        /// </summary>
        /// <param name="val">Value to be written</param>
        public virtual ulong ULong{
            get { return Bytes.ReadULongBE(buf, 0, Length); }
            set { Bytes.WriteULongBE(buf, 0, value, Length); }
        }

        /// <summary>
        ///     Writes underlying value into a buffer without modifying write
        ///     index.
        /// </summary>
        /// <param name="buf">Buffer to write to.</param>
        /// <param name="at">Start offset for writing.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if reading at given starting offset would cause a buffer
        ///     overrun.
        /// </exception>
        public void Write(byte[] buf, int at)
        {
            Write(buf, ref at);
        }

        /// <summary>
        ///     Writes underlying value into a buffer and modifies write index.
        ///     Useful for chaining such writes together and writing the whole
        ///     packet into buffer from a loop.
        /// </summary>
        /// <param name="buf">Buffer to write to.</param>
        /// <param name="at">Start offset for writing.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if reading at given starting offset would cause a buffer
        ///     overrun.
        /// </exception>
        public abstract void Write(byte[] buf, ref int at);

        /// <summary>
        ///     Reads a field from buffer. Does not modify write index.
        /// </summary>
        /// <param name="buf">Buffer to read from.</param>
        /// <param name="at">Start offset for reading.</param>
        /// <returns>Representation of read state.</returns>
        /// <see cref="ReadState"/>
        public ReadState Read(byte[] buf, int at)
        {
            return Read(buf, ref at);
        }

        /// <summary>
        ///     Reads a field from buffer. Modifies write index. Useful for
        ///     chaining such method invocations and reading a whole packet in
        ///     one go from a loop.
        /// </summary>
        /// <param name="buf">Buffer to read from.</param>
        /// <param name="at">Start index for reading. Not modified if read fails.</param>
        /// <returns>Representation of read state.</returns>
        /// <see cref="ReadState"/>
        public abstract ReadState Read(byte[] buf, ref int at);
    }
}
