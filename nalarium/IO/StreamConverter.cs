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

using System.IO;

namespace Nalarium.IO
{
    public static class StreamConverter
    {
        //- @CreateStream -//
        /// <summary>
        ///     Creates the stream.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static T CreateStream<T>(string text) where T : Stream, new()
        {
            var stream = new T();
            var writer = new BinaryWriter(stream);
            writer.Write(text);
            
            return stream;
        }

        /// <summary>
        ///     Creates the stream.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static T CreateStream<T>(byte[] data) where T : Stream, new()
        {
            var stream = new T();
            var writer = new BinaryWriter(stream);
            writer.Write(data);
            
            return stream;
        }

        //- @GetStreamByteArray -//
        /// <summary>
        ///     Gets the stream byte array.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public static byte[] GetStreamByteArray(Stream stream)
        {
            if (stream == null)
            {
                return null;
            }
            stream.Seek(0, SeekOrigin.Begin);
            var r = new BinaryReader(stream);
            
            return r.ReadBytes((int) stream.Length);
        }

        //- @GetStreamText -//
        /// <summary>
        ///     Gets the stream text.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public static string GetStreamText(Stream stream)
        {
            if (stream == null)
            {
                return string.Empty;
            }
            stream.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(stream);
            
            return reader.ReadToEnd();
        }
    }
}