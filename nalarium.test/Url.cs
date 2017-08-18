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
using System;
using System.Globalization;

namespace Nalarium.Test
{
    
    public class Url
    {
        [Fact]
        public void Join()
        {
            var expected = "path/to/item";
            var path = @"path";
            var to = @"to";
            var item = @"item";

            var result = Nalarium.Url.Join(path, to, item);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void CleanTail()
        {
            var expected = "/path/with/useless/ending";
            var input = @"/path/with/useless/ending/";

            var result = Nalarium.Url.CleanTail(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void FromPath()
        {
            var expected = "myfolder";
            var baseFolder = @"E:\Some\Series\Of\Folders\content";
            var folder = @"E:\some\series\of\folders\content\" + expected;
            var result = Nalarium.Url.FromPath(Nalarium.Path.Clean(folder.Substring(baseFolder.Length, folder.Length - baseFolder.Length)).ToLower(CultureInfo.InvariantCulture));
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetParent()
        {
            var expected = "path/to/something/deep/with/lame";
            var input = "/path/to/something/deep/with/lame/ending/";
            var result = Nalarium.Url.GetParent(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetParent_Single()
        {
            var expected = "";
            var input = "path";
            var result = Nalarium.Url.GetParent(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetParent_AlreadyRoot()
        {
            var expected = "";
            var input = "";
            var result = Nalarium.Url.GetParent(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetUrlPartArray()
        {
            var result = Nalarium.Url.GetUrlPartArray(string.Empty);
            Assert.Equal(result.Length, 0);
        }
    }
}
