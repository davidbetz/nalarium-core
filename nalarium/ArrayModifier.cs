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

namespace Nalarium
{
    /// <summary>
    ///     Modifies and array by allowing shifting or stripping of elements.
    /// </summary>
    public static class ArrayModifier
    {
        //- @Shift -//
        /// <summary>
        ///     Shifts an array left by 1.
        /// </summary>
        /// <typeparam name="T">Type of the array.</typeparam>
        /// <param name="original">The array to shift left.</param>
        /// <returns>The shifted array or null if original array had one element.</returns>
        public static T[] Shift<T>(Array original)
        {
            return Shift<T>(original, 1);
        }

        /// <summary>
        ///     Shifts an array left by 1.
        /// </summary>
        /// <typeparam name="T">Type of the array.</typeparam>
        /// <param name="original">The array to shift left.</param>
        /// <param name="count">Number of places to shift the array left.</param>
        /// <returns>The shifted array or null if original array had one element.</returns>
        public static T[] Shift<T>(Array original, int count)
        {
            if (original.Length > 0)
            {
                var shifted = new T[original.Length - count];
                Array.Copy(original, count, shifted, 0, original.Length - count);
                
                return shifted;
            }
            return null;
        }

        //- @Strip -//
        /// <summary>
        ///     Strips one element off the array.
        /// </summary>
        /// <typeparam name="T">Type of the array.</typeparam>
        /// <param name="original">The array to strip left.</param>
        /// <returns>The striped array or null if original array had one element.</returns>
        public static T[] Strip<T>(Array original)
        {
            return Strip<T>(original, 1);
        }

        /// <summary>
        ///     Strips a certain number of elements off the array.
        /// </summary>
        /// <typeparam name="T">Type of the array.</typeparam>
        /// <param name="original">The array to strip left.</param>
        /// <param name="count">Number of places to strip the array left.</param>
        /// <returns>The striped array or null if original array had one element.</returns>
        public static T[] Strip<T>(Array original, int count)
        {
            if (original.Length > 0)
            {
                var shifted = new T[original.Length - count];
                Array.Copy(original, shifted, original.Length - count);
                
                return shifted;
            }
            return null;
        }
    }
}