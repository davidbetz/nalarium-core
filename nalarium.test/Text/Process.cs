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

namespace Nalarium.Test.Text
{
    
    public class Process
    {
        [Fact]
        public void Max()
        {
            var results = Nalarium.Text.Process.Max("asdfasdfasdasdfasfasdff", 10);

            Assert.Equal(results, "asdfasdfas");
        }

        [Fact]
        public void MaxWithEllipsis()
        {
            var results = Nalarium.Text.Process.Max("asdfasdfasdasdfasfasdff", 10, true);

            Assert.Equal(results, "asdfasd...");
        }

        [Fact] 
        public void MaxWithHtmlEllipsis()
        {
            var results = Nalarium.Text.Process.Max("asdfasdfasdasdfasfasdff", 10, true, true);

            Assert.Equal(results, "asdfasd&hellip;");
        }
    }
}