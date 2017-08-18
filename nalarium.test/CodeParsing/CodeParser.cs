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
    public class AmazonAffiliateCodeParser : Nalarium.CodeParsing.CodeParserTemplate
    {
        public override string Code => "AmazonAffiliate";

        public override string Template => @"<a href=""http://www.amazon.com/gp/product/{ASIN}/{AffiliateCode}"">{Title}</a>";
    }

    public class YouTubeCodeParser : Nalarium.CodeParsing.CodeParserTemplate
    {
        public override string Code => "YouTube";

        public override string Template => @"https://www.youtube.com/watch?v={Code}";
    }

    
    public class CodeParser
    {
        [Fact]
        public void ParseCode_AmazonAffiliate()
        {
            var series = new Nalarium.CodeParsing.CodeParser();
            series.Add(new AmazonAffiliateCodeParser().AddCode("AffiliateCode", "my-amazon-code"));

            var input = "The book you should study is {{AmazonAffiliate:ASIN=B00SLXVBC4;Title=Elasticsearch: The Definitive Guide}}.";

            var results = series.ParseCode(input);

            var expected = @"The book you should study is <a href=""http://www.amazon.com/gp/product/B00SLXVBC4/my-amazon-code"">Elasticsearch: The Definitive Guide</a>.";

            Assert.Equal(expected, results);
        }

        [Fact]
        public void ParseCode_YouTube()
        {
            var series = new Nalarium.CodeParsing.CodeParser();
            series.Add(new YouTubeCodeParser());

            var input = "The book is {{YouTube:Code=XC2RYiaM6WU}}.";

            var results = series.ParseCode(input);

            var expected = "The book is https://www.youtube.com/watch?v=XC2RYiaM6WU.";

            Assert.Equal(expected, results);
        }
    }
}