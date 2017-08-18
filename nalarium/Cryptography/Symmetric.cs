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
using System.Security.Cryptography;
using Nalarium.IO;

namespace Nalarium.Cryptography
{
    /// <summary>
    ///     Used to work with Symmetric cryptography.
    /// </summary>
    public static class Symmetric
    {

        public static string Encrypt(string text, byte[] iv, byte[] key, SymmetricMethod method = SymmetricMethod.Rijndael)
        {
            var memoryStream = StreamConverter.CreateStream<MemoryStream>(text);
            memoryStream.Seek(0, SeekOrigin.Begin);
            
            var output = new MemoryStream();
            SymmetricAlgorithm symm = null;
            switch (method)
            {
                case SymmetricMethod.Rijndael:
                    symm = new RijndaelManaged();
                    break;
                case SymmetricMethod.Aes:
                    symm = new AesManaged();
                    break;
                default:
                    return string.Empty;
            }
            symm.BlockSize = 128;
            symm.KeySize = 256;
            symm.IV = iv;
            symm.Key = key;
            var transform = symm.CreateEncryptor();
            using (var cstream = new CryptoStream(output, transform, CryptoStreamMode.Write))
            {
                var br = new BinaryReader(memoryStream);
                cstream.Write(br.ReadBytes((int)memoryStream.Length), 0, (int)memoryStream.Length);
                cstream.FlushFinalBlock();
                
                return Convert.ToBase64String(output.ToArray());
            }
        }
        public static string Decrypt(string text, byte[] iv, byte[] key, SymmetricMethod method = SymmetricMethod.Rijndael)
        {
            var memoryStream = new MemoryStream(Convert.FromBase64String(text));
            
            SymmetricAlgorithm symm = null;
            switch (method)
            { 
                case SymmetricMethod.Rijndael:
                    symm = new RijndaelManaged();
                    break;
                case SymmetricMethod.Aes:
                    symm = new AesManaged();
                    break;
                default:
                    return string.Empty;
            }
            symm.BlockSize = 128;
            symm.KeySize = 256;
            symm.IV = iv;
            symm.Key = key;
            var transform = symm.CreateDecryptor();
            using (var cstream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read))
            {
                var output = new StreamReader(cstream).ReadToEnd();
                return output.Substring(1, output.Length - 1);
            }
        }
    }
}