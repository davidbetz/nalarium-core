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
using System.Reflection;

namespace Nalarium.Reflection
{
    public class PropertyReader
    {
        private readonly object _object;
        private readonly Type _type;

        
        //- @Ctor -//
        public PropertyReader(object obj)
        {
            if (obj == null)
            {
                return;
            }
            _object = obj;
            _type = _object.GetType();
        }

        //- @ReadAsString -//
        public string ReadAsString(string propertyName)
        {
            return Read(propertyName) as string;
        }

        //- @Read -//
        public object Read(string propertyName)
        {
            var pi = _type?.GetProperty(propertyName);
            return pi?.GetValue(_object, null);
        }

        public static List<PropertyInfo> GetPropertyList(Type type, BindingFlags flags = BindingFlags.Public | BindingFlags.Instance)
        {
            var fn = type.FullName;
            lock (Lock)
            {
                if (!TypePropertyInfoDictionary.ContainsKey(fn))
                {
                    TypePropertyInfoDictionary.Add(fn, new List<PropertyInfo>(type.GetProperties(flags)));
                }
                if (TypePropertyInfoDictionary.ContainsKey(fn))
                {
                    return TypePropertyInfoDictionary[fn];
                }
            }
            return new List<PropertyInfo>();
        }

        private static readonly Dictionary<string, List<PropertyInfo>> TypePropertyInfoDictionary = new Dictionary<string, List<PropertyInfo>>();
        private static readonly object Lock = new object();
    }
}