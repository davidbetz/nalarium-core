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

namespace Nalarium.CodeParsing
{
    public abstract class CodeParserTemplate
    {
        public abstract string Code { get; }

        public abstract string Template { get; }

        public CodeParserTemplate AddCode(string key, string value)
        {
            if (CodeData == null)
            {
                CodeData = new Dictionary<string, string>();
            }
            CodeData.Add(key, value);

            return this;
        }

        public void MergeMap(Map map)
        {
            if (CodeData == null)
            {
                return;
            }
            foreach (var key in CodeData.Keys)
            {
                map.Add(key, CodeData[key]);
            }
        }

        public Dictionary<string, string> CodeData { get; set; }

        public virtual Map Setup()
        {
            return new Map();
        }
    }
}