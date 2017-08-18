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

using Xunit;

namespace Nalarium.Test
{
    
    public class QuickHash
    {
        [Fact]
        public void Hash_Object_MD5()
        {
            var results = Nalarium.Cryptography.QuickHash.Hash(123456789, Cryptography.HashMethod.MD5);

            string expected = "A5F4ED60F5CC379AEB207DAAF71E8219";

            Assert.Equal(expected, results);
        }

        [Fact]
        public void Hash_Text_MD5()
        {
            var results = Nalarium.Cryptography.QuickHash.Hash("testtesttest", Cryptography.HashMethod.MD5);

            string expected = "1FB0E331C05A52D5EB847D6FC018320D";

            Assert.Equal(expected, results);
        }

        [Fact]
        public void Hash_Bytes_MD5()
        {
            var results = Nalarium.Cryptography.QuickHash.Hash(System.Text.Encoding.UTF8.GetBytes("testtesttest"), Cryptography.HashMethod.MD5);

            string expected = "1FB0E331C05A52D5EB847D6FC018320D";

            Assert.Equal(expected, results);
        }

        [Fact]
        public void Hash_Object_SHA1()
        {
            var results = Nalarium.Cryptography.QuickHash.Hash(123456789, Cryptography.HashMethod.SHA1);

            string expected = "05A338ABAE191D659233B8A4F72F2E6D26A1B408";

            Assert.Equal(expected, results);
        }

        [Fact]
        public void Hash_Text_SHA1()
        {
            var results = Nalarium.Cryptography.QuickHash.Hash("testtesttest", Cryptography.HashMethod.SHA1);

            string expected = "0071877D20A65C02D9A1654F109B97DC61416D1A";

            Assert.Equal(expected, results);
        }

        [Fact]
        public void Hash_Bytes_SHA1()
        {
            var results = Nalarium.Cryptography.QuickHash.Hash(System.Text.Encoding.UTF8.GetBytes("testtesttest"), Cryptography.HashMethod.SHA1);

            string expected = "0071877D20A65C02D9A1654F109B97DC61416D1A";

            Assert.Equal(expected, results);
        }

        [Fact]
        public void Hash_Object_SHA256()
        {
            var results = Nalarium.Cryptography.QuickHash.Hash(123456789, Cryptography.HashMethod.SHA256);

            string expected = "D79F12481D6FEC45B4DA3FE145DB2374796BEEDC3C2F5D5DB211708543ED3091";

            Assert.Equal(expected, results);
        }

        [Fact]
        public void Hash_Text_SHA256()
        {
            var results = Nalarium.Cryptography.QuickHash.Hash("testtesttest", Cryptography.HashMethod.SHA256);

            string expected = "A2C96D518F1099A3B6AFE29E443340F9F5FDF1289853FC034908444F2BCB8982";

            Assert.Equal(expected, results);
        }

        [Fact]
        public void Hash_Bytes_SHA256()
        {
            var results = Nalarium.Cryptography.QuickHash.Hash(System.Text.Encoding.UTF8.GetBytes("testtesttest"), Cryptography.HashMethod.SHA256);

            string expected = "A2C96D518F1099A3B6AFE29E443340F9F5FDF1289853FC034908444F2BCB8982";

            Assert.Equal(expected, results);
        }

        [Fact]
        public void Hash_Object_SHA512()
        {
            var results = Nalarium.Cryptography.QuickHash.Hash(123456789, Cryptography.HashMethod.SHA512);

            string expected = "92E0795EE513F2E4EF806DA98B80562A5CEEF3ABCA268EA1EAF1AE6AE4F66FC7A2A54632C8DD29F5CB06B77E7AB981C8481155616AC7F830B318AD92EE51187B";

            Assert.Equal(expected, results);
        }

        [Fact]
        public void Hash_Text_SHA512()
        {
            var results = Nalarium.Cryptography.QuickHash.Hash("testtesttest", Cryptography.HashMethod.SHA512);

            string expected = "623C92D8C3E80A6963599E42AA37D43F8F4F4E84C742BFE5CF26B33B6E5A281599DD9E948691B5F76566E526375EF46CC5485AF55BAC2A198B69B40333AC92FB";

            Assert.Equal(expected, results);
        }

        [Fact]
        public void Hash_Bytes_SHA512()
        {
            var results = Nalarium.Cryptography.QuickHash.Hash(System.Text.Encoding.UTF8.GetBytes("testtesttest"), Cryptography.HashMethod.SHA512);

            string expected = "623C92D8C3E80A6963599E42AA37D43F8F4F4E84C742BFE5CF26B33B6E5A281599DD9E948691B5F76566E526375EF46CC5485AF55BAC2A198B69B40333AC92FB";

            Assert.Equal(expected, results);
        }

        [Fact]
        public void Hash_Object_DoubleSHA256()
        {
            var results = Nalarium.Cryptography.QuickHash.Hash(123456789, Cryptography.HashMethod.DoubleSHA256);

            string expected = "2DD8595E766C7A3BA8B723E9C9B5BDB3056C620D0AFEAAAE952AA5E43A75C81F";

            Assert.Equal(expected, results);
        }

        [Fact]
        public void Hash_Text_DoubleSHA256()
        {
            var results = Nalarium.Cryptography.QuickHash.Hash("testtesttest", Cryptography.HashMethod.DoubleSHA256);

            string expected = "9B110F8047643D5DFC30DFFE333B18D2D0ECD20425D72F9421A3BF1936BE604F";

            Assert.Equal(expected, results);
        }

        [Fact]
        public void Hash_Bytes_DoubleSHA256()
        {
            var results = Nalarium.Cryptography.QuickHash.Hash(System.Text.Encoding.UTF8.GetBytes("testtesttest"), Cryptography.HashMethod.DoubleSHA256);

            string expected = "9B110F8047643D5DFC30DFFE333B18D2D0ECD20425D72F9421A3BF1936BE604F";

            Assert.Equal(expected, results);
        }
    }
}
