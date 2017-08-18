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
using System.Globalization;
using System.Text;

namespace Nalarium.Text
{
    /// <summary>
    ///     Manipulates text by setting the case to either PascalCasing or camelCasing.
    /// </summary>
    public static class Case
    {
        //- @GetPascalCase -//
        /// <summary>
        ///     Gets the pascal case.
        /// </summary>
        /// <param name="parameterArray">The parameter array.</param>
        /// <returns></returns>
        public static string GetPascalCase(params string[] parameterArray)
        {
            var builder = new StringBuilder();
            foreach (var text in parameterArray)
            {
                builder.Append(InternalGetPascalCase(text));
            }
            
            return builder.ToString();
        }

        //- @GetCamelCase -//
        /// <summary>
        ///     Gets the camel case.
        /// </summary>
        /// <param name="parameterArray">The parameter array.</param>
        /// <returns></returns>
        public static string GetCamelCase(params string[] parameterArray)
        {
            if (parameterArray != null && parameterArray.Length > 0)
            {
                var first = parameterArray[0].ToLower(CultureInfo.CurrentCulture);
                if (parameterArray.Length > 1)
                {
                    var destinationArray = new string[parameterArray.Length - 1];
                    Array.Copy(parameterArray, 1, destinationArray, 0, parameterArray.Length - 1);
                    
                    return first + GetPascalCase(destinationArray);
                }
                return first;
            }
            
            return string.Empty;
        }

        
        //- $InternalGetPascalCase -//
        /// <summary>
        ///     Internals the get pascal case.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        private static string InternalGetPascalCase(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.ToLower(CultureInfo.CurrentCulture);
                var first = text[0].ToString();
                
                return first.ToUpper(CultureInfo.CurrentCulture) + text.Substring(1, text.Length - 1);
            }
            
            return string.Empty;
        }
    }
}