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
    ///     Represents a position.  May be used forward (first, second, third) or backward (ultima, penultima, antepenultima).
    /// </summary>
    public enum Position
    {
        /// <summary>
        ///     The first position
        /// </summary>
        First = 1,

        /// <summary>
        ///     The second position
        /// </summary>
        Second = 2,

        /// <summary>
        ///     The third position
        /// </summary>
        Third = 3,

        /// <summary>
        ///     The last position
        /// </summary>
        Ultima = 4,

        /// <summary>
        ///     The position before the ultima
        /// </summary>
        Penultima = 5,

        /// <summary>
        ///     The position before the penultima
        /// </summary>
        Antepenultima = 6
    }
}