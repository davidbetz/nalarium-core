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

namespace Nalarium
{
    /// <summary>
    ///     A key/value-like structure for use with Map types.
    /// </summary>
    /// <typeparam name="T1">The type of the key.</typeparam>
    /// <typeparam name="T2">The type of the value.</typeparam>
    public class MapEntry<T1, T2>
    {
        //- @Source -//

        
        //- @Ctor -//
        /// <summary>
        ///     Initializes a new instance of the <see cref="MapEntry&lt;T1, T2&gt;" /> class.
        /// </summary>
        public MapEntry()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MapEntry&lt;T1, T2&gt;" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public MapEntry(T1 key, T2 value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        ///     Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public T1 Key { get; set; }

        //- @Target -//
        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public T2 Value { get; set; }

        //- @Create -//
        /// <summary>
        ///     Initializes a new instance of the <see cref="MapEntry&lt;T1, T2&gt;" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>Initialized map entry.</returns>
        public static MapEntry<T1, T2> Create(T1 key, T2 value)
        {
            return new MapEntry<T1, T2>(key, value);
        }
    }

    public class MapEntry : MapEntry<string, string>
    {
        //- @Ctor -//
        /// <summary>
        ///     Initializes a new instance of the <see cref="MapEntry" /> class.
        /// </summary>
        public MapEntry()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MapEntry" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public MapEntry(string key, string value)
            : base(key, value)
        {
            //+ blank
        }

        //- @Create -//
        /// <summary>
        ///     Initializes a new instance of the <see cref="MapEntry&lt;T1, T2&gt;" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>Initialized map entry.</returns>
        public new static MapEntry Create(string key, string value)
        {
            return new MapEntry(key, value);
        }
    }
}