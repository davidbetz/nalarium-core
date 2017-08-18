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
using System.Globalization;
using System.Linq;

namespace Nalarium
{
    /// <summary>
    ///     Manages a Url
    /// </summary>
    public static class Url
    {
        //- @Join -//
        /// <summary>
        ///     Cleanly joins two paths.
        /// </summary>
        /// <param name="parameterArray">Segment array.</param>
        /// <returns>The merged path.</returns>
        public static string Join(params string[] parameterArray)
        {
            if (parameterArray == null)
            {
                return string.Empty;
            }
            
            return Clean(string.Join("/", new List<string>(parameterArray.Select(Clean).Where(p => !string.IsNullOrWhiteSpace(p)))));
        }

        //- @FixUrl -//
        /// <summary>
        ///     Removes slashes from the beginning and end of a path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The fixed path.</returns>
        public static string Clean(string path)
        {
            return CleanHead(CleanTail(path));
        }

        //- @CleanHead -//
        /// <summary>
        ///     Removes slashes from the beginning of a path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The fixed path.</returns>
        public static string CleanHead(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }
            while (path.StartsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                path = path.Substring(1, path.Length - 1);
            }
            
            return path;
        }

        /// <summary>
        /// Gets a/b/c from /a/b/c/d/, /a/b/c/d, and a/b/c/d
        /// </summary>
        /// <param name="path">A url path</param>
        /// <returns>Parent url to url path</returns>
        public static string GetParent(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }
            return Join(ArrayModifier.Strip<string>(Split(path)));
        }

        //- @CleanTail -//
        /// <summary>
        ///     Removes slashes from the end of a path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The fixed path.</returns>
        public static string CleanTail(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }
            while (path.EndsWith("/"))
            {
                path = path.Substring(0, path.Length - 1);
            }
            
            return path;
        }

        //- @RemoveEndingQuestionMark -//
        /// <summary>
        ///     Removes the ending question mark of a path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns> without question mark.</returns>
        public static string RemoveEndingQuestionMark(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }
            var index = path.IndexOf("?", StringComparison.Ordinal);
            if (index > -1)
            {
                path = path.Substring(0, index);
            }
            
            return path;
        }

        /// <summary>
        ///     Converts a \ path into a / relative url.
        /// </summary>
        /// <param name="path">Windows-style path</param>
        /// <returns>Internet-style relative url</returns>
        public static string FromPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }
            
            return Clean(Path.ToRelative(path).Replace("\\", "/"));
        }

        /// <summary>
        ///     Splits a url into a string array; paths are converted into urls.
        ///     Use Nalarum.Collection.GetArrayPart to get a specific part.
        /// </summary>
        /// <param name="url">Url</param>
        /// <returns>url part array</returns>
        public static string[] Split(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return new string[] { };
            }
            url = Clean(FromPath(url));
            
            return url.Split('/');
        }

        /// <summary>
        ///     Shifts a URL left; a/b/c/d -> b/c/d
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="count">number of places</param>
        /// <returns>fixed url</returns>
        public static string Shift(string url, int count = 1)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }
            var partArray = Split(url);
            return string.Join("/", ArrayModifier.Shift<string>(partArray, count));
        }

        /// <summary>
        ///     Shifts a URL right; a/b/c/d -> a/b/c
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="count">number of places</param>
        /// <returns>fixed url</returns>
        public static string Strip(string url, int count = 1)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }
            var partArray = Split(url);
            return string.Join("/", ArrayModifier.Strip<string>(partArray, count));
        }

        //- @UrlPartArray -//
        /// <summary>
        ///     Gets the URL part array.
        /// </summary>
        public static string[] GetUrlPartArray(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return new string[] { };
            }
            return url.ToLower(CultureInfo.CurrentCulture).Split('/').Where(p => !string.IsNullOrEmpty(p)).ToArray();
        }

        /// <summary>
        ///     Gets url part
        /// </summary>
        /// <param name="url"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static string GetPart(string url, Position position)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }
            return Collection.GetArrayPart(GetUrlPartArray(url), position);
        }

        /// <summary>
        ///     Gets url part
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static string GetPart(string[] array, Position position)
        {
            if (array == null)
            {
                return string.Empty;
            }
            return Collection.GetArrayPart(array, position);
        }
    }
}