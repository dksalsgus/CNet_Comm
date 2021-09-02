using System;
using System.Collections.Generic;
using System.Text;

namespace HPLC.Net.TCP
{
    public static class ByteArrayExtension
    {
        public static byte[] ToHexBytes(this string hex)
        {
            if ((hex.Length % 2) != 0)
                throw new ArgumentException();
            byte[] data = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length / 2; i++)
                data[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            return data;
        }

        public static string ToHexString(this byte[] buffer, int count)
        {
            var sb = new StringBuilder(count * 5);
            for (int i = 0; i < count; i++)
                sb.AppendFormat("{0:X2}", buffer[i]);
            return sb.ToString();
        }

        public static string ToHexString(this byte[] buffer)
        {
            var sb = new StringBuilder(buffer.Length * 5);
            for (int i = 0; i < buffer.Length; i++)
                sb.AppendFormat("{0:X2}", buffer[i]);
            return sb.ToString();
        }

        public static string GetAsciiString(this byte[] buffer, int count)
        {
            return Encoding.ASCII.GetString(buffer, 0, count);
        }
    }
}
