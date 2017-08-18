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
    
    public class Parser
    {
        [Fact]
        public void ParseBoolean()
        {
            Assert.True(Nalarium.Parser.ParseBoolean(1));
            Assert.True(Nalarium.Parser.ParseBoolean("1"));
            Assert.True(Nalarium.Parser.ParseBoolean("1.0"));
            Assert.True(Nalarium.Parser.ParseBoolean("true"));
            Assert.True(Nalarium.Parser.ParseBoolean("True"));
            Assert.True(Nalarium.Parser.ParseBoolean("active"));
            Assert.True(Nalarium.Parser.ParseBoolean("on"));
            Assert.False(Nalarium.Parser.ParseBoolean("12"));
            Assert.False(Nalarium.Parser.ParseBoolean(12));
        }

        [Fact]
        public void ParseByte()
        {
            Assert.Equal((byte)1, Nalarium.Parser.ParseByte(1));
            Assert.Equal((byte)1, Nalarium.Parser.ParseByte("1"));
            Assert.Equal((byte)0, Nalarium.Parser.ParseByte(null));
            Assert.Equal((byte)0, Nalarium.Parser.ParseByte("burrito"));
        }

        [Fact]
        public void ParseDateTime()
        {
            Assert.Equal("2/3/2010 12:00:00 AM", Nalarium.Parser.ParseDateTime("2010-02-03").ToString());
        }

        [Fact]
        public void ParseDouble()
        {
            Assert.Equal(1d, Nalarium.Parser.ParseDouble(1));
            Assert.Equal(1d, Nalarium.Parser.ParseDouble("1"));
            Assert.Equal(0d, Nalarium.Parser.ParseDouble(null));
            Assert.Equal(0d, Nalarium.Parser.ParseDouble("burrito"));
        }

        [Fact]
        public void ParseInt32()
        {
            Assert.Equal(1, Nalarium.Parser.ParseInt32(1));
            Assert.Equal(1, Nalarium.Parser.ParseInt32("1"));
            Assert.Equal(0, Nalarium.Parser.ParseInt32(null));
            Assert.Equal(0d, Nalarium.Parser.ParseInt32("burrito"));
        }


        [Fact]
        public void ParseInt64()
        {
            Assert.Equal(1L, Nalarium.Parser.ParseInt64(1));
            Assert.Equal(1L, Nalarium.Parser.ParseInt64("1"));
            Assert.Equal(0L, Nalarium.Parser.ParseInt64(null));
            Assert.Equal(0L, Nalarium.Parser.ParseInt64("burrito"));
        }

        [Fact]
        public void ParseSingle()
        {
            Assert.Equal(1f, Nalarium.Parser.ParseSingle(1));
            Assert.Equal(1f, Nalarium.Parser.ParseSingle("1"));
            Assert.Equal(0f, Nalarium.Parser.ParseSingle(null));
            Assert.Equal(0f, Nalarium.Parser.ParseSingle("burrito"));
        }

        [Fact]
        public void ParseString()
        {
            Assert.Equal("1", Nalarium.Parser.ParseString(1));
            Assert.Equal(string.Empty, Nalarium.Parser.ParseString(null));
        }

        [Fact]
        public void ParseUInt16()
        {
            Assert.Equal((ushort)1, Nalarium.Parser.ParseUInt16(1));
            Assert.Equal((ushort)1, Nalarium.Parser.ParseUInt16("1"));
            Assert.Equal((ushort)0, Nalarium.Parser.ParseUInt16(null));
            Assert.Equal((ushort)0, Nalarium.Parser.ParseInt64("burrito"));
        }

        [Fact]
        public void ParseUInt32()
        {
            Assert.Equal((uint)1, Nalarium.Parser.ParseUInt32(1));
            Assert.Equal((uint)1, Nalarium.Parser.ParseUInt32("1"));
            Assert.Equal((uint)0, Nalarium.Parser.ParseUInt32(null));
            Assert.Equal((uint)0, Nalarium.Parser.ParseUInt32("burrito"));
        }

        [Fact]
        public void ParseUInt64()
        {
            Assert.Equal((ulong)1, Nalarium.Parser.ParseUInt64(1));
            Assert.Equal((ulong)1, Nalarium.Parser.ParseUInt64("1"));
            Assert.Equal((ulong)0, Nalarium.Parser.ParseUInt64(null));
            Assert.Equal((ulong)0, Nalarium.Parser.ParseUInt64("burrito"));
        }

        [Fact]
        public void ParseMaxString()
        {
            Assert.Equal("asdfasdfasdf", Nalarium.Parser.ParseMaxString("asdfasdfasdf"));
            Assert.Equal("this is my default", Nalarium.Parser.ParseMaxString(null, "this is my default"));
            Assert.Equal("asdfa", Nalarium.Parser.ParseMaxString("asdfasdfasdf", max: 5));
        }

        [Fact]
        public void ParseGenericInt32()
        {
            Assert.Equal(12, Nalarium.Parser.Parse<int>("12"));
            Assert.Equal(0, Nalarium.Parser.Parse<int>("burrito"));
        }
    }
}