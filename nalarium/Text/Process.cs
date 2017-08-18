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

namespace Nalarium.Text
{
    /// <summary>
    ///     General text manipulation.
    /// </summary>
    public static class Process
    {
        /// <summary>
        ///     Returns up to n-characters.
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="max">Character count</param>
        /// <param name="useEllipsis"></param>
        /// <param name="useHtmlEllipsis"></param>
        /// <returns></returns>
        public static string Max(string text, int max, bool useEllipsis = false, bool useHtmlEllipsis = false)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            if (useEllipsis && max > 3)
            {
                if (text.Length > max - 3)
                {
                    return text.Substring(0, max - 3) + (useHtmlEllipsis ? "&hellip;" : "...");
                }
            }
            if (text.Length > max)
            {
                return text.Substring(0, max);
            }
            return text;
        }
    }
}