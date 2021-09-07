using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packets
{
    public static class Bytes
    {
        public static void WriteWord(this byte[] arr, int idx, ushort num)
        {
            arr[idx] = (byte)(num & 0xFF);
            arr[idx] = (byte)(num & 0xFF);
        }

        public static ushort Bytes2Word(byte hb, byte lb)
        {
            ushort data = (ushort)(hb << 8 | lb);
            return data;
        }

        public static void Word2Bytes(ushort data, ref byte hb, ref byte lb)
        {
            hb = (byte)((data >> 8) & 0x000000FF);
            lb = (byte)(data & (ushort)0x00FF);
        }

        public static string Byte2Hexstr(byte data)
        {
            StringBuilder sb = new StringBuilder(4);
            sb.Append("0x");
            sb.AppendFormat("{0:x2}", data);
            return sb.ToString();
        }

        public static string Word2Hexstr(ushort data)
        {
            StringBuilder sb = new StringBuilder(6);
            byte hb = (byte)((data >> 8) & 0x000000FF);
            byte lb = (byte)(data & 0x00FF);
            sb.Append("0x");
            sb.AppendFormat("{0:X2}", hb);
            sb.AppendFormat("{0:X2}", lb);
            return sb.ToString();
        }

        public static byte GetByte(this ulong val, int byteNum)
        {
            if (byteNum > sizeof(long) - 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            return (byte)((val & (0xFFUL << (8 * byteNum))) >> (8 * byteNum));
        }

        public static void SetByte(ref this ulong ln, int byteNum, byte val)
        {
            if (byteNum > sizeof(long) - 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            ln |= (ln << (8 * byteNum));
        }

        public static ulong ReadULongBE(byte[] buf, int at, int length)
        {
            int copy = at;
            return ReadULongBE(buf, ref copy, length);
        }

        public static ulong ReadULongBE(byte[] buf, ref int at, int length)
        {
            ulong ret = 0;
            int i = 0;
            if (at + length > buf.Length)
            {
                throw new ArgumentOutOfRangeException();
            }
            while (true)
            {
                ret |= buf[at + i];
                i++;
                if (i >= length || i >= sizeof(long))
                    break;
                ret <<= 8;
            }
            return ret;
        }

        public static void WriteULongBE(byte[] buf, int at, ulong val, int length)
        {
            int copy = at;
            WriteULongBE(buf, ref copy, val, length);
        }

        public static void WriteULongBE(byte[] buf, ref int at, ulong val, int length)
        {
            if (at + length > buf.Length)
            {
                throw new ArgumentOutOfRangeException();
            }
            for (int i = 0; i < length && i < sizeof(long); i++)
            {
                buf[at + i] = GetByte(val, length - i - 1);
            }
        }

        public static string ToHexString(this byte[] bytes)
        {
            return BitConverter.ToString(bytes);
        }

        public static string ToHexString(this byte[] bytes, int from, int to)
        {
            return BitConverter.ToString(bytes, from, to - from + 1);
        }

        public static void Swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }

        public static ushort CRC(IEnumerable<byte> stream)
        {
            ushort crc = 0xFFFF;

            if (stream == null)
                throw new ArgumentNullException();
            foreach(byte b in stream)
            {
                CRCByte(ref crc, b);
            }

            return crc;
        }

        public static ushort CRC(byte[] bytes, int from, int to)
        {
            ushort crc = (ushort)0xffff;

            if (from >= bytes.Length || to >= bytes.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (from > to)
            {
                Swap(ref from, ref to);
            }

            for (int index = from; index < to; index++)
            {
                CRCByte(ref crc, bytes[index]);
            }
            return crc;
        }

        public static ushort CRCByte(ref ushort crc, byte b)
        {
            crc ^= ((ushort)((b << 8) & 0xFFFF));
            for (b = 0; b < 8; b++)
            {
                if ((crc & (ushort)0x8000) == (ushort)0x8000)
                    crc = (ushort)((ushort)((crc << 1) & 0xFFFF) ^ (ushort)0x1021);
                else
                    crc = (ushort)((crc << 1) & 0xFFFF);
            }
            return crc;
        }
    }
}
