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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nalarium.Text
{
    /// <summary>
    ///     Splits text while respecting quotes and literals.
    /// </summary>
    public static class TextSplitter
    {
        //- @Split -//
        /// <summary>
        ///     Splits text.
        /// </summary>
        /// <param name="text">The text to split</param>
        /// <param name="delimiterArray">Array of text delimiters</param>
        /// <returns>Array of strings split from input text</returns>
        public static string[] Split(string text, params char[] delimiterArray)
        {
            return Split(text, QuoteTypes.Both, delimiterArray);
        }

        /// <summary>
        ///     Splits text.
        /// </summary>
        /// <param name="text">The text to split</param>
        /// <param name="delimiterArray">Array of text delimiters</param>
        /// <param name="allowedQuoteTypes">The type of quotes allowed</param>
        /// <returns>Array of strings split from input text</returns>
        public static string[] Split(string text, QuoteTypes allowedQuoteTypes, params char[] delimiterArray)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }
            if (delimiterArray == null || delimiterArray.Length == 0)
            {
                throw new ArgumentNullException("Delimiter required");
            }
            
            return InternalSplit(text, allowedQuoteTypes, delimiterArray);
        }

        //- ~InternalSplit -//
        internal static string[] InternalSplit(string text, QuoteTypes allowedQuoteTypes, params char[] delimiterArray)
        {
            var splitResult = new List<string>();
            var builder = new StringBuilder();
            var inQuote = false;
            var isLiteral = false;
            foreach (var c in text)
            {
                if (IsQuote(c, allowedQuoteTypes))
                {
                    if (isLiteral)
                    {
                        builder.Append(c);
                        isLiteral = false;
                    }
                    else
                    {
                        inQuote = !inQuote;
                    }
                }
                else if (c == '\\')
                {
                    isLiteral = true;
                }
                else if (inQuote)
                {
                    builder.Append(c);
                }
                else if (delimiterArray.Contains(c))
                {
                    if (isLiteral)
                    {
                        builder.Append(c);
                        isLiteral = false;
                    }
                    else
                    {
                        splitResult.Add(builder.ToString());
                        builder = new StringBuilder();
                    }
                }
                else
                {
                    builder.Append(c);
                }
            }
            //+ flush
            if (builder.Length > 0)
            {
                splitResult.Add(builder.ToString());
            }
            
            return splitResult.ToArray();
        }

        //- $IsQuote -//
        private static bool IsQuote(char c, QuoteTypes allowedQuoteTypes)
        {
            switch (allowedQuoteTypes)
            {
                case QuoteTypes.Double:
                    return c == '"';
                case QuoteTypes.Single:
                    return c == '\'';
                case QuoteTypes.Both:
                    return c == '"' | c == '\'';
            }
            
            return false;
        }
    }
}