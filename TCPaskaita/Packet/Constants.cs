using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packets
{
    public static class Constants
    {
        public static readonly byte[] BEGIN_MAGIC
                = new byte[] { (byte)'R', (byte)'A', (byte)'M' };
        public static readonly byte[] END_MAGIC
                = new byte[] { (byte)'M', (byte)'A', (byte)'Z' };
        public static readonly byte[] NO_BYTES = new byte[0];
    }
}
