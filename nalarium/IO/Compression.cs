#region Copyright

// MIT License

// Copyright (c) 2007-2017 David Betz

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

#endregion

using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace Nalarium.IO
{
    /// <summary>
    ///     Compresses and decompresses.
    /// </summary>
    public static class Compression
    {
        public static byte[] Compress(string input)
        {
            return Compress(Encoding.UTF8.GetBytes(input));
        }

        public static byte[] Compress(byte[] buffer)
        {
            using (var output = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(output, CompressionMode.Compress))
                using (var input = new MemoryStream(buffer))
                {
                    input.CopyTo(gzipStream);
                }
                return output.ToArray();
            }
        }

        public static async Task<byte[]> CompressAsyc(byte[] buffer)
        {
            using (var output = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(output, CompressionMode.Compress))
                using (var input = new MemoryStream(buffer))
                {
                    await input.CopyToAsync(gzipStream);
                }
                return output.ToArray();
            }
        }

        public static byte[] DecompressUsingSegments(byte[] buffer)
        {
            using (var input = new MemoryStream(buffer))
            using (var memory = new MemoryStream())
            using (var stream = new GZipStream(input, CompressionMode.Decompress))
            {
                const int size = 4096;
                var buffer2 = new byte[size];
                int count;
                do
                {
                    count = stream.Read(buffer2, 0, size);
                    if (count > 0)
                    {
                        memory.Write(buffer2, 0, count);
                    }
                } while (count > 0);
                return memory.ToArray();
            }
        }

        public static byte[] Decompress(byte[] buffer)
        {
            using (var input = new MemoryStream(buffer))
            using (var gzipStream = new GZipStream(input, CompressionMode.Decompress))
            using (var output = new MemoryStream())
            {
                gzipStream.CopyTo(output);
                return output.ToArray();
            }
        }

        public static async Task<byte[]> DecompressAsync(byte[] buffer)
        {
            using (var input = new MemoryStream(buffer))
            using (var gzipStream = new GZipStream(input, CompressionMode.Decompress))
            using (var output = new MemoryStream())
            {
                await gzipStream.CopyToAsync(output);
                return output.ToArray();
            }
        }

        public static string DecompressToString(byte[] input)
        {
            return Encoding.UTF8.GetString(Decompress(input));
        }
    }
}