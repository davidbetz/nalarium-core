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
    public static class AttributeReader
    {
        //- @ReadAssemblyAttribute -//
        public static T ReadAssemblyAttribute<T>(Type type) where T : Attribute
        {
            return ReadAssemblyAttribute(type, typeof(T)) as T;
        }

        public static T ReadAssemblyAttribute<T>(object obj) where T : Attribute
        {
            return ReadAssemblyAttribute(obj, typeof(T)) as T;
        }

        public static Attribute ReadAssemblyAttribute(Type type, Type attributeType)
        {
            var array = ReadAssemblyAttributeArray(type, attributeType);
            if (!Collection.IsNullOrEmpty(array))
            {
                return array[0] as Attribute;
            }
            
            return null;
        }

        public static Attribute ReadAssemblyAttribute(object obj, Type attributeType)
        {
            var array = ReadAssemblyAttributeArray(obj, attributeType);
            if (!Collection.IsNullOrEmpty(array))
            {
                return array[0] as Attribute;
            }
            
            return null;
        }

        //- @ReadTypeAttributeArray -//
        public static object[] ReadAssemblyAttributeArray<T>(object obj)
        {
            return ReadAssemblyAttributeArray(obj, typeof(T));
        }

        public static object[] ReadAssemblyAttributeArray<T>(Type type)
        {
            return ReadAssemblyAttributeArray(type, typeof(T));
        }

        public static object[] ReadAssemblyAttributeArray(object obj, Type attributeType)
        {
            return ReadAssemblyAttributeArray(obj.GetType(), attributeType);
        }

        public static object[] ReadAssemblyAttributeArray(Type type, Type attributeType)
        {
            var assembly = type.Assembly;
            object[] objectArray;
            if (attributeType == null)
            {
                objectArray = assembly.GetCustomAttributes(true);
            }
            else
            {
                objectArray = assembly.GetCustomAttributes(attributeType, true);
            }
            
            return objectArray;
        }


        //- @ReadTypeAttribute -//
        public static T ReadTypeAttribute<T>(Type type) where T : Attribute
        {
            return ReadTypeAttribute(type, type) as T;
        }

        public static T ReadTypeAttribute<T>(object obj) where T : Attribute
        {
            return ReadTypeAttribute(obj, typeof(T)) as T;
        }

        public static Attribute ReadTypeAttribute(object obj, Type attributeType)
        {
            return ReadTypeAttribute(obj.GetType(), attributeType);
        }

        public static Attribute ReadTypeAttribute(Type type, Type attributeType)
        {
            var array = ReadTypeAttributeArray(type, attributeType);
            if (!Collection.IsNullOrEmpty(array))
            {
                return array[0] as Attribute;
            }
            
            return null;
        }

        //- @ReadTypeAttributeArray -//
        public static object[] ReadTypeAttributeArray<T>(object obj)
        {
            return ReadTypeAttributeArray(obj.GetType(), typeof(T));
        }

        public static object[] ReadTypeAttributeArray<T>(Type type)
        {
            return ReadTypeAttributeArray(type, typeof(T));
        }

        public static object[] ReadTypeAttributeArray(object obj, Type attributeType)
        {
            return ReadTypeAttributeArray(obj.GetType(), attributeType);
        }

        public static object[] ReadTypeAttributeArray(Type type, Type attributeType)
        {
            object[] objectArray;
            if (attributeType == null)
            {
                objectArray = type.GetCustomAttributes(true);
            }
            else
            {
                objectArray = type.GetCustomAttributes(attributeType, true);
            }
            
            return objectArray;
        }

        //- @FindPropertiesWithAttribute -//
        public static List<PropertyAttributeInformation<TAttribute>> FindPropertiesWithAttribute<TAttribute>(object obj) where TAttribute : Attribute
        {
            var type = obj.GetType();
            var propertyInfoList = new List<PropertyAttributeInformation<TAttribute>>();
            var propertyInfoArray = type.GetProperties();
            foreach (var pi in propertyInfoArray)
            {
                var attribute = ReadPropertyAttribute<TAttribute>(pi);
                if (attribute == null)
                {
                    continue;
                }
                propertyInfoList.Add(new PropertyAttributeInformation<TAttribute>
                {
                    PropertyInfo = pi,
                    Attribute = attribute
                });
            }
            
            return propertyInfoList;
        }

        //- @FindMethodsWithAttribute -//
        public static List<MethodAttributeInformation<TAttribute>> FindMethodsWithAttribute<TAttribute>(object obj) where TAttribute : Attribute
        {
            var type = obj.GetType();
            var methodInfoList = new List<MethodAttributeInformation<TAttribute>>();
            var methodInfoArray = type.GetMethods();
            foreach (var mi in methodInfoArray)
            {
                var attribute = ReadMethodAttribute<TAttribute>(mi);
                if (attribute == null)
                {
                    continue;
                }
                methodInfoList.Add(new MethodAttributeInformation<TAttribute>
                {
                    MethodInfo = mi,
                    Attribute = attribute
                });
            }
            
            return methodInfoList;
        }

        //- @ReadTypeAttribute -//
        public static T ReadMethodAttribute<T>(MethodInfo methodInfo) where T : Attribute
        {
            return ReadMethodAttribute(methodInfo, typeof(T)) as T;
        }

        public static Attribute ReadMethodAttribute(MethodInfo methodInfo, Type attributeType)
        {
            var array = ReadMethodAttributeArray(methodInfo, attributeType);
            if (!Collection.IsNullOrEmpty(array))
            {
                return array[0] as Attribute;
            }
            
            return null;
        }

        //- @ReadMethodAttributeArray -//
        public static object[] ReadMethodAttributeArray<T>(MethodInfo methodInfo)
        {
            return ReadMethodAttributeArray(methodInfo, typeof(T));
        }

        public static object[] ReadMethodAttributeArray(MethodInfo methodInfo, Type attributeType)
        {
            object[] objectArray;
            if (attributeType == null)
            {
                objectArray = methodInfo.GetCustomAttributes(true);
            }
            else
            {
                objectArray = methodInfo.GetCustomAttributes(attributeType, true);
            }
            
            return objectArray;
        }

        //- @ReadTypeAttribute -//
        public static T ReadPropertyAttribute<T>(PropertyInfo propertyInfo) where T : Attribute
        {
            return ReadPropertyAttribute(propertyInfo, typeof(T)) as T;
        }

        public static Attribute ReadPropertyAttribute(PropertyInfo propertyInfo, Type attributeType)
        {
            var array = ReadPropertyAttributeArray(propertyInfo, attributeType);
            if (!Collection.IsNullOrEmpty(array))
            {
                return array[0] as Attribute;
            }
            
            return null;
        }

        //- @ReadPropertyAttributeArray -//
        public static object[] ReadPropertyAttributeArray<T>(PropertyInfo propertyInfo)
        {
            return ReadPropertyAttributeArray(propertyInfo, typeof(T));
        }

        public static object[] ReadPropertyAttributeArray(PropertyInfo propertyInfo, Type attributeType)
        {
            object[] objectArray;
            if (attributeType == null)
            {
                objectArray = propertyInfo.GetCustomAttributes(true);
            }
            else
            {
                objectArray = propertyInfo.GetCustomAttributes(attributeType, true);
            }
            
            return objectArray;
        }
    }
}