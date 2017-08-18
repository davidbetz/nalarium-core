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

namespace Nalarium
{
    /// <summary>
    ///     Manages a Path.
    /// </summary>
    public static class Path
    {
        //- @CleanPath -//
        /// <summary>
        ///     Removes slashes from the beginning and end of a path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The fixed path.</returns>
        public static string Clean(string path)
        {
            return CleanHead(CleanTail(path));
        }

        //- @CleanPathHead -//
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
            if (path.StartsWith("\\", StringComparison.OrdinalIgnoreCase))
            {
                path = path.Substring(1, path.Length - 1);
            }
            
            return path;
        }

        //- @CleanPathTail -//
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
            if (path.EndsWith("\\", StringComparison.OrdinalIgnoreCase))
            {
                path = path.Substring(0, path.Length - 1);
            }
            
            return path;
        }

        /// <summary>
        ///     Converts a / url into a Windows \ relative path.
        /// </summary>
        /// <param name="url">Internet-style relative url</param>
        /// <returns>Windows-style path</returns>
        public static string FromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }
            
            return url.Replace("/", "\\");
        }

        /// <summary>
        ///     Cleanly joins two paths.
        /// </summary>
        /// <param name="parameterArray">Path array.</param>
        /// <returns>The merged path.</returns>
        public static string Join(params string[] parameterArray)
        {
            if (parameterArray == null)
            {
                return string.Empty;
            }
            
            return Clean(string.Join("\\", new List<string>(parameterArray.Select(p => Clean(FromUrl(p))).Where(p => !string.IsNullOrWhiteSpace(p)))));
        }

        /// <summary>
        ///     Removed drive and colon from path.
        /// </summary>
        /// <param name="path">Path, possibly including colon.</param>
        /// <returns>Path without drive or colon.</returns>
        public static string ToRelative(string path)
        {
            if (String.IsNullOrEmpty(path))
            {
                return string.Empty;
            }
            if (path.Contains(":"))
            {
                path = path.Substring(path.IndexOf(":") + 1);
            }
            if (path.Length < 3)
            {
                return string.Empty;
            }
            
            return Clean(path.Substring(path.IndexOf(":") + 1));
        }
    }
}