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

using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System;

namespace Nalarium.Cryptography
{
    [Reference("Ported from Python")]
    [Reference("https://gist.githubusercontent.com/shirriff/c9fb5d98e6da79d9a772/raw/18520930523f8e2f729b30033c2f90ee6a2bf4f0/merkle.py")]
    public static class Merkel
    {
        public static string Create(params string[] parameterArray)
        {
            using (var sha256 = new SHA256Managed())
            {
                return Create(sha256, parameterArray);
            }
        }

        private static string Create(SHA256Managed hash, params string[] parameterArray)
        {
            if (parameterArray == null)
            {
                return string.Empty;
            }
            var count = parameterArray.Length;
            if (count == 1)
            {
                return parameterArray[0];
            }
            var list = new List<string>();
            for (int i = 0; i < count - 1; i += 2)
            {
                list.Add(Leaf(hash, parameterArray[i], parameterArray[i + 1]));
            }
            if (count % 2 == 1)
            {
                //+ balance tree must have even # of nodes
                list.Add(Leaf(hash, parameterArray[count - 1], parameterArray[count - 1]));
            }
            return Create(hash, list.ToArray());
        }

        private static string Leaf(SHA256Managed hash, string node1, string node2)
        {
            var first = StringToByteArray(node1);
            Array.Reverse(first);
            var second = StringToByteArray(node2);
            Array.Reverse(second);
            
            byte[] merged = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, merged, 0, first.Length);
            Buffer.BlockCopy(second, 0, merged, first.Length, second.Length);
            
            var output = hash.ComputeHash(hash.ComputeHash(merged));
            Array.Reverse(output);
            
            return BitConverter.ToString(output).Replace("-", "").ToLower();
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length / 2).Select(x => Convert.ToByte(hex.Substring(x * 2, 2), 16)).ToArray();
        }

    }
}