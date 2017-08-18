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

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace Nalarium.Cryptography
{
    public static class QuickHash
    {
        public static string Hash(string text, HashMethod method = HashMethod.MD5)
        {
            byte[] hash;
            switch (method)
            {
                case HashMethod.MD5:
                    using (var md5 = MD5.Create())
                        hash = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
                    break;
                case HashMethod.SHA1:
                    using (var sha = new SHA1Managed())
                        hash = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
                    break;
                case HashMethod.SHA256:
                    using (var sha = new SHA256Managed())
                        hash = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
                    break;
                case HashMethod.SHA512:
                    using (var sha = new SHA512Managed())
                        hash = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
                    break;
                case HashMethod.DoubleSHA256:
                    using (var sha = new SHA256Managed())
                        hash = sha.ComputeHash(sha.ComputeHash(Encoding.UTF8.GetBytes(text)));
                    break;
                default:
                    return string.Empty;
            }
            return BitConverter.ToString(hash).Replace("-", "");
        }

        public static string HashFile(string filename, HashMethod method = HashMethod.MD5)
        {
            byte[] hash;
            switch (method)
            {
                case HashMethod.MD5:
                    using (var md5 = MD5.Create())
                    using (var stream = File.OpenRead(filename))
                        hash = md5.ComputeHash(stream);
                    break;
                case HashMethod.SHA1:
                    using (var sha = new SHA1Managed())
                    using (var stream = File.OpenRead(filename))
                        hash = sha.ComputeHash(stream);
                    break;
                case HashMethod.SHA256:
                    using (var sha = new SHA256Managed())
                    using (var stream = File.OpenRead(filename))
                        hash = sha.ComputeHash(stream);
                    break;
                case HashMethod.SHA512:
                    using (var sha = new SHA512Managed())
                    using (var stream = File.OpenRead(filename))
                        hash = sha.ComputeHash(stream);
                    break;
                case HashMethod.DoubleSHA256:
                    using (var sha = new SHA256Managed())
                    using (var stream = File.OpenRead(filename))
                        hash = sha.ComputeHash(sha.ComputeHash(stream));
                    break;
                default:
                    return string.Empty;
            }
            return BitConverter.ToString(hash).Replace("-", "");
        }

        public static string Hash(object obj, HashMethod method = HashMethod.MD5)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                return Hash(stream.ToArray(), method);
            }
        }

        public static string Hash(byte[] buffer, HashMethod method = HashMethod.MD5)
        {
            byte[] hash;
            switch (method)
            {
                case HashMethod.MD5:
                    using (var md5 = MD5.Create())
                        hash = md5.ComputeHash(buffer);
                    break;
                case HashMethod.SHA1:
                    using (var sha = new SHA1Managed())
                        hash = sha.ComputeHash(buffer);
                    break;
                case HashMethod.SHA256:
                    using (var sha = new SHA256Managed())
                        hash = sha.ComputeHash(buffer);
                    break;
                case HashMethod.SHA512:
                    using (var sha = new SHA512Managed())
                        hash = sha.ComputeHash(buffer);
                    break;
                case HashMethod.DoubleSHA256:
                    using (var sha = new SHA256Managed())
                        hash = sha.ComputeHash(sha.ComputeHash(buffer));
                    break;
                default:
                    return string.Empty;
            }
            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}