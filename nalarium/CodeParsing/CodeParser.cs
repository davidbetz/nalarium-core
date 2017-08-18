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

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Nalarium.CodeParsing
{
    /// <summary>
    ///     Holds a series of code parsers to be processed in order.
    /// </summary>
    public class CodeParser
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CodeParser" /> class.
        /// </summary>
        public CodeParser()
        {
            CodeParserList = new Dictionary<string, CodeParserTemplate>();
        }

        /// <summary>
        ///     Gets or sets the code parser list.
        /// </summary>
        /// <value>The code parser list.</value>
        public Dictionary<string, CodeParserTemplate> CodeParserList { get; set; }

        /// <summary>
        ///     Adds the specified code parser.
        /// </summary>
        /// <param name="codeParser">The code parser.</param>
        public void Add(CodeParserTemplate codeParser)
        {
            if (codeParser == null)
            {
                return;
            }
            CodeParserList.Add(codeParser.Code, codeParser);
        }

        /// <summary>
        ///     Parses the code.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public string ParseCode(string content)
        {
            //if (string.IsNullOrEmpty(CodeParserId))
            //{
            //    throw new ArgumentNullException(ResourceAccessor.GetString("CodeParser_CodeParserIdRequired", AssemblyInfo.AssemblyName, Resource.ResourceManager));
            //}
            var matchPattern = @"{{(?<parser>[_\-a-z0-9]+):(?<code>[:= |;,_\-a-z0-9]+)}}";
            var matchCollection = Regex.Matches(content, matchPattern, RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            foreach (Match match in matchCollection)
            {
                //    var indexOf = content.IndexOf("=");
                //    var first = content.Substring(0, content.Length);
                //    var second = content.Substring(content.Length + 1, content.Length - indexOf - 1);
                var map = new Map();
                var parser = match.Groups["parser"].Value;
                var codeSeries = match.Groups["code"].Value;
                var codeArray = Text.TextSplitter.Split(codeSeries, Text.QuoteTypes.Single, ';');
                foreach (var code in codeArray)
                {
                    var indexOf = code.IndexOf("=");
                    var first = code.Substring(0, indexOf);
                    var second = code.Substring(indexOf + 1, code.Length - indexOf - 1);
                    map.Add(first.Trim(), second.Trim());
                }
                //    
                //    var parserName = parserGroup.Value.ToLower();
                var codeParser = CodeParserList[parser];
                if (codeParser == null)
                {
                    continue;
                }
                codeParser.MergeMap(map);
                var template = new Template(codeParser.Template);
                var replacement = template.Interpolate(map);

                content = Regex.Replace(content, matchCollection[0].Value, replacement, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

                //    var parsed = codeParser?.ParseCode(codeGroup.Value);
                //    if (parsed != null)
                //    {
                //        var replacementPattern = "{{" + parserName + "{{{" + codeGroup.Value + "}}}}}";
                //        replacementPattern = replacementPattern.Replace("|", "\\|");
                //        content = Regex.Replace(content, replacementPattern, parsed, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
                //    }
            }
            
            return content;
        }
    }
}