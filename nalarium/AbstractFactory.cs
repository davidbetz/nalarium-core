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

namespace Nalarium
{
    public static class AbstractFactory
    {
        private static readonly Dictionary<Type, object> ProviderFactories = new Dictionary<Type, object>();

        public static void Set<T>(IProviderFactory<T> factory) where T : class
        {
            ProviderFactories.Add(typeof(T), factory);
        }

        public static void Remove<T>() where T : class
        {
            ProviderFactories.Remove(typeof(T));
        }

        public static T Resolve<T>(params string[] parameterArray) where T : class
        {
            var type = typeof(T);
            if (!ProviderFactories.ContainsKey(type))
            {
                return null;
            }
            var factory = ProviderFactories[type] as IProviderFactory<T>;
            return factory?.Create(parameterArray);
        }
    }
}