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
using System.Text;

namespace Nalarium
{
    public static class Base64
    {
        public static string To(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        }

        public static string From(string base64Text)
        {
            if (string.IsNullOrEmpty(base64Text))
            {
                return string.Empty;
            }
            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(base64Text));
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string Merge(params string[] parameterArray)
        {
            return To(String.Join(String.Empty, parameterArray));
        }

        public static string Merge(char separator, params string[] parameterArray)
        {
            if (Collection.IsNullOrEmpty(parameterArray))
            {
                return string.Empty;
            }
            return To(String.Join(separator.ToString(), parameterArray));
        }
    }
}