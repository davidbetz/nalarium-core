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
    /// <summary>
    ///     Creates a pattern which allows interpolation with map data to create mass output.
    /// </summary>
    public class Template
    {
        //- @Common -//

        
        //- @Ctor -//
        /// <summary>
        ///     Initializes a new instance of the <see cref="Template" /> class.
        /// </summary>
        public Template()
            : this(string.Empty)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Template" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Template(string value)
        {
            Value = new StringBuilder(value);
        }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public StringBuilder Value { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Template" /> class.
        /// </summary>
        public static Template Create()
        {
            return new Template();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Template" /> class.
        /// </summary>
        /// <param name="value">Template text.</param>
        public static Template Create(string value)
        {
            return new Template(value);
        }

        
        //- @AppendText -//
        /// <summary>
        ///     Appends the text.
        /// </summary>
        /// <param name="text">Template text.</param>
        public void AppendText(string text)
        {
            Value.Append(text);
        }

        /// <summary>
        ///     Interpolates a dynamic object with a text template.
        /// </summary>
        /// <param name="text">text template</param>
        /// <param name="obj">dynamic object</param>
        /// <returns></returns>
        public static string Interpolate(string text, object obj)
        {
            if (String.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            if (obj == null)
            {
                return text;
            }
            var pr = obj.GetType().GetProperties();
            var map = new Map();
            foreach (var p in pr)
            {
                map.Add(p.Name, p.GetValue(obj).ToString());
            }
            return Interpolate(text, map);
        }

        //- @Interpolate -//
        /// <summary>
        ///     Interpolates the specified pair array.
        /// </summary>
        /// <param name="text">Template text.</param>
        /// <param name="map">The map.</param>
        /// <returns>Templated result string.</returns>
        public static string Interpolate(string text, Map map)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            if (map == null)
            {
                return string.Empty;
            }
            
            return Create(text).Interpolate(map);
        }

        /// <summary>
        ///     Interpolates the specified pair array.
        /// </summary>
        /// <param name="text">Template text.</param>
        /// <param name="parameterArray">A pair (i.e. "a=b") parameter array.</param>
        /// <returns>Templated result string.</returns>
        public static string Interpolate(string text, params MapEntry[] parameterArray)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            if (parameterArray == null)
            {
                return string.Empty;
            }
            
            return Create(text).Interpolate(new Map(parameterArray));
        }

        /// <summary>
        ///     Interpolates the specified map entry array.
        /// </summary>
        /// <param name="parameterArray">A MapEntry parameter array.</param>
        /// <returns>Templated result string.</returns>
        public string Interpolate(params MapEntry[] parameterArray)
        {
            if (parameterArray == null)
            {
                return string.Empty;
            }
            
            return Interpolate(new Map(parameterArray));
        }

        /// <summary>
        ///     Interpolates the specified map.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <returns>Templated result string.</returns>
        public string Interpolate(Map map)
        {
            var result = Value.ToString();
            foreach (var name in map.GetKeyList())
            {
                result = result.Replace("{" + name + "}", map[name]);
            }
            return result;
        }

        /// <summary>
        ///     Interpolates the specified pair array.
        /// </summary>
        /// <param name="pairArray">The pair array.</param>
        /// <returns>Templated result string.</returns>
        public string Interpolate(params string[] pairArray)
        {
            if (pairArray == null)
            {
                return string.Empty;
            }
            var result = Value.ToString();
            foreach (var mapping in pairArray)
            {
                var name = string.Empty;
                var value = string.Empty;
                var parts = mapping.Split('=');
                if (parts.Length == 2)
                {
                    name = parts[0];
                    value = parts[1];
                }
                else if (parts.Length == 1)
                {
                    name = parts[0];
                    value = parts[0];
                }
                
                if (!string.IsNullOrEmpty(name))
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        value = value.Trim();
                        result = result.Replace("{" + name + "}", value);
                    }
                }
            }
            
            return result;
        }

        #region Nested type: Common

        /// <summary>
        ///     Contains common templates for quick access
        /// </summary>
        public class Common
        {
            public const string Link = @"<a href=""{Link}"">{Text}</a>";
            public const string Image = @"<img src=""{Source}"" alt=""{Text}"" />";

            //- #Ctor -//
            protected Common()
            {
            }
        }

        #endregion
    }
}