using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packets
{
    public class Packet
    {
        protected Field[] Fields;

        /// <summary>
        ///     Reads a packet from buffer.
        /// </summary>
        /// <param name="buf">Buffer to read from.</param>
        /// <param name="at">Start index.</param>
        /// <returns>Representation of read state.</returns>
        public ReadState Read(byte[] buf, int at)
        {
            int copy = at;
            return Read(buf, ref copy);
        }

        /// <summary>
        ///     Reads a packet from buffer.
        /// </summary>
        /// <param name="buf">Buffer to read from.</param>
        /// <param name="at">Start index. Incremented by bytes read.</param>
        /// <returns>Representation of read state.</returns>
        public virtual ReadState Read(byte[] buf, ref int at)
        {
            int start = at;
            for (int i = 0; i < Fields.Length; i++)
            {
                ReadState state = Fields[i].Read(buf, ref at);
                switch (state)
                {
                    case ReadState.Success:
                        continue;
                    case ReadState.UnexpectedEndOfStream:
                        at = start;
                        return state;
                    case ReadState.Fail:
#if DEBUG
                        Console.Error.WriteLine("Read failed. Malformed packet:");
                        Console.Error.WriteLine(BitConverter.ToString(buf, 0, at));
#endif
                        at = start;
                        return state;
                }
            }
            return ReadState.Success;
        }

        /// <summary>
        ///     Serialises the whole packet into a buffer.
        /// </summary>
        /// <param name="buf">Buffer to write to.</param>
        /// <param name="at">Start index. Not modified.</param>
        public void Write(byte[] buf, int at)
        {
            Write(buf, ref at);
        }

        /// <summary>
        ///     Serialises the whole packet into a buffer.
        /// </summary>
        /// <param name="buf">Buffer to write to.</param>
        /// <param name="at">
        ///     Start index. Incremented by the amount of bytes written.
        /// </param>
        public virtual void Write(byte[] buf, ref int at)
        {
            InitWrite();
            for (int i = 0; i < Fields.Length; i++)
            {
                Fields[i].Write(buf, ref at);
            }
        }

        public int SerialisedLength()
        {
            int ret = 0;
            for (int i = 0; i < Fields.Length; i++)
            {
                ret += Fields[i].Length;
            }
            return ret;
        }

        public byte[] ToByteArray()
        {
            byte[] ret = new byte[SerialisedLength()];
            Write(ret, 0);
            return ret;
        }

        /// <summary>
        ///     Finds a possible start of packet in a buffer.
        /// </summary>
        /// <param name="arr">Buffer to search in.</param>
        /// <param name="from">Index to search from.</param>
        /// <returns>Index of packet start. -1 if search failed.</returns>
        public int FindStart(byte[] arr, int from = 0)
        {
            if (from < 0 || from >= arr.Length)
            {
                throw new IndexOutOfRangeException();
            }
            for (int i = from; i < arr.Length; i++)
            {
                switch (Fields[0].Read(arr, i))
                {
                    case ReadState.Success:
                    case ReadState.UnexpectedEndOfStream:
                        return i;
                    case ReadState.Fail:
                        continue;
                }
            }
            return -1;
        }

        /// <Summary>
        ///     Override this if some fields depend on others non-trivially.
        /// </Summary>
        protected virtual void InitWrite()
        {
            return;
        }
    }
}
