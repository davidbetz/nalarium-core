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
using System.Globalization;
using System.Threading;

namespace Nalarium
{
    /// <summary>
    ///     Parses String, Int32, Double, and Boolean information from an object or string with overloads allowing for
    ///     providing default if parsing fails.
    /// </summary>
    /// <example>
    ///     Int32 int32 = Parser.ParseInt32("wrong", 15);
    ///     System.Console.WriteLine(Int32);
    ///     //+ Output: 15
    ///     
    ///     //+* There is also a generic Parse methed for anyone who prefers that style.
    ///     
    /// </example>
    public static class Parser
    {
        private static readonly Type BooleanType = typeof(bool);

        
        //- @Parse -//
        /// <summary>
        ///     Parses the string as a generic type
        /// </summary>
        /// <typeparam name="T">Type of return data</typeparam>
        /// <param name="data"> data to parse</param>
        /// <returns>Parsed value</returns>
        public static T Parse<T>(string data)
        {
            return Parse(data, default(T));
        }

        /// <summary>
        ///     Parses the string as a generic type
        /// </summary>
        /// <typeparam name="T">Type of return data</typeparam>
        /// <param name="data"> data to parse</param>
        /// <param name="defaultValue">Parsed default value to return is null or empty</param>
        /// <returns>Parsed value</returns>
        public static T Parse<T>(string data, T defaultValue)
        {
            try
            {
                if (typeof(T) == BooleanType)
                {
                    switch (data)
                    {
                        case "1":
                        case "1.0":
                        case "yes":
                        case "true":
                        case "active":
                        case "on":
                            data = "true";
                            break;
                        default:
                            data = "false";
                            break;
                    }
                }
                return (T)Convert.ChangeType(data, typeof(T), Thread.CurrentThread.CurrentCulture);
            }
            catch
            {
                return defaultValue;
            }
        }

        //- @ParseString -//
        /// <summary>
        ///     Parses the string.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>Parsed value.</returns>
        public static string ParseString(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            var valueString = value as string;
            if (valueString != null)
            {
                return valueString;
            }
            
            return value.ToString();
        }

        /// <summary>
        ///     Parses the string or sets default.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="defaultValue">Default value to return is null or empty</param>
        /// <returns>Parsed value or default.</returns>
        public static string ParseString(string value, string defaultValue)
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            
            return value;
        }

        /// <summary>
        ///     Parses the string or, if the string is longer than the max, only returns the portion upto the max.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="defaultValue">Default value to return is null or empty</param>
        /// <param name="max">The max length of the string.</param>
        /// <returns>Parsed value or default.</returns>
        public static string ParseMaxString(string value, string defaultValue = "", int max = 0)
        {
            if (string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(defaultValue))
            {
                value = defaultValue;
            }
            if (max > 0)
            {
                if (value.Length > max)
                {
                    value = value.Substring(0, max);
                }
            }
            
            return value;
        }

        //- @ParseByte -//
        /// <summary>
        ///     Parses the Byte.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>Parsed value.</returns>
        public static byte ParseByte(object value)
        {
            if (value != null)
            {
                return ParseByte(value.ToString());
            }
            
            return 0;
        }

        /// <summary>
        ///     Parses the Byte.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>Parsed value or 0.</returns>
        public static byte ParseByte(string value)
        {
            return ParseByte(value, 0);
        }

        /// <summary>
        ///     Parses the Byte or sets default.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="defaultValue">Default value to return is value is invalid</param>
        /// <returns>Parsed value or default.</returns>
        public static byte ParseByte(string value, byte defaultValue)
        {
            byte Byte;
            if (byte.TryParse(value, out Byte))
            {
                return Byte;
            }
            return defaultValue;
        }

        //- @ParseInt32 -//
        /// <summary>
        ///     Parses the int32.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>Parsed value.</returns>
        public static int ParseInt32(object value)
        {
            if (value != null)
            {
                return ParseInt32(value.ToString());
            }
            
            return 0;
        }

        /// <summary>
        ///     Parses the int32.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>Parsed value or 0.</returns>
        public static int ParseInt32(string value)
        {
            return ParseInt32(value, 0);
        }

        /// <summary>
        ///     Parses the int32 or sets default.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="defaultValue">Default value to return is value is invalid</param>
        /// <returns>Parsed value or default.</returns>
        public static int ParseInt32(string value, int defaultValue)
        {
            int int32;
            if (int.TryParse(value, out int32))
            {
                return int32;
            }
            return defaultValue;
        }

        //- @ParseInt64 -//
        /// <summary>
        ///     Parses the int64.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>Parsed value.</returns>
        public static long ParseInt64(object value)
        {
            if (value != null)
            {
                return ParseInt64(value.ToString());
            }
            
            return 0;
        }

        /// <summary>
        ///     Parses the int64.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>Parsed value or 0.</returns>
        public static long ParseInt64(string value)
        {
            return ParseInt64(value, 0);
        }

        /// <summary>
        ///     Parses the int64 or sets default.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="defaultValue">Default value to return is value is invalid</param>
        /// <returns>Parsed value or default.</returns>
        public static long ParseInt64(string value, long defaultValue)
        {
            long int64;
            if (long.TryParse(value, out int64))
            {
                return int64;
            }
            return defaultValue;
        }

        //- @ParseBoolean -//
        /// <summary>
        ///     Parses the boolean.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>true if "yes" or 1 (or variant of 1 like "1" or 1.0); otherwise false</returns>
        public static bool ParseBoolean(object value)
        {
            if (value != null)
            {
                return ParseBoolean(value.ToString());
            }
            return false;
        }

        /// <summary>
        ///     Parses the boolean.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>true if 1; false is not 1</returns>
        public static bool ParseBoolean(int value)
        {
            return value == 1;
        }

        /// <summary>
        ///     Parses the boolean.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>true if "yes" or 1; otherwise false</returns>
        public static bool ParseBoolean(string value)
        {
            return ParseBoolean(value, false);
        }

        /// <summary>
        ///     Parses the boolean or sets default.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="defaultValue">Default value to return is value is invalid</param>
        /// <returns>true if "yes" or 1; otherwise the specified default</returns>
        public static bool ParseBoolean(string value, bool defaultValue)
        {
            bool boolean;
            if (bool.TryParse(value, out boolean))
            {
                return boolean;
            }
            if (value != null)
            {
                if (value.ToLower(CultureInfo.CurrentCulture) == "yes" || value == "1" || value == "1.0" || value == "on" || value == "active")
                {
                    return true;
                }
            }
            return defaultValue;
        }

        //- @ParseSingle -//
        /// <summary>
        ///     Parses the single.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>Parsed value or 0F.</returns>
        public static float ParseSingle(object value)
        {
            if (value != null)
            {
                return ParseSingle(value.ToString());
            }
            return 0F;
        }

        /// <summary>
        ///     Parses the single.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>Parsed value or 0F.</returns>
        public static float ParseSingle(string value)
        {
            return ParseSingle(value, 0F);
        }

        /// <summary>
        ///     Parses the single or sets default.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="defaultValue">Default value to return is value is invalid</param>
        /// <returns>Parsed value or default.</returns>
        public static float ParseSingle(string value, float defaultValue)
        {
            float Single;
            if (float.TryParse(value, out Single))
            {
                return Single;
            }
            return defaultValue;
        }

        //- @ParseDouble -//
        /// <summary>
        ///     Parses the double.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>Parsed value or 0.0.</returns>
        public static double ParseDouble(object value)
        {
            if (value != null)
            {
                return ParseDouble(value.ToString());
            }
            return 0.0;
        }

        /// <summary>
        ///     Parses the double.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>Parsed value or 0.0.</returns>
        public static double ParseDouble(string value)
        {
            return ParseDouble(value, 0.0);
        }

        /// <summary>
        ///     Parses the double or sets default.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="defaultValue">Default value to return is value is invalid</param>
        /// <returns>Parsed value or default.</returns>
        public static double ParseDouble(string value, double defaultValue)
        {
            double Double;
            if (double.TryParse(value, out Double))
            {
                return Double;
            }
            return defaultValue;
        }

        //- @ParseDateTime -//
        /// <summary>
        ///     Parses the date time.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>Parsed value or DateTime.MinValue.</returns>
        public static DateTime ParseDateTime(object value)
        {
            if (value != null)
            {
                return ParseDateTime(value.ToString());
            }
            return DateTime.MinValue;
        }

        /// <summary>
        ///     Parses the date time.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>Parsed value or DateTime.MinValue.</returns>
        public static DateTime ParseDateTime(string value)
        {
            return ParseDateTime(value, DateTime.MinValue);
        }

        /// <summary>
        ///     Parses the date time or sets default.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="defaultValue">Default value to return is value is invalid</param>
        /// <returns>Parsed value or default.</returns>
        /// <returns></returns>
        public static DateTime ParseDateTime(string value, DateTime defaultValue)
        {
            DateTime DateTime;
            if (DateTime.TryParse(value, out DateTime))
            {
                return DateTime;
            }
            return defaultValue;
        }

        //- @ParseUInt16 -//
        /// <summary>
        ///     Parses the UInt16.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>Parsed value.</returns>
        public static ushort ParseUInt16(object value)
        {
            if (value != null)
            {
                return ParseUInt16(value.ToString());
            }
            
            return 0;
        }

        /// <summary>
        ///     Parses the UInt16.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>Parsed value or 0.</returns>
        public static ushort ParseUInt16(string value)
        {
            return ParseUInt16(value, 0);
        }

        /// <summary>
        ///     Parses the UInt16 or sets default.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="defaultValue">Default value to return is value is invalid</param>
        /// <returns>Parsed value or default.</returns>
        public static ushort ParseUInt16(string value, ushort defaultValue)
        {
            ushort UInt16;
            if (ushort.TryParse(value, out UInt16))
            {
                return UInt16;
            }
            return defaultValue;
        }

        //- @ParseUInt32 -//
        /// <summary>
        ///     Parses the UInt32.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>Parsed value.</returns>
        public static uint ParseUInt32(object value)
        {
            if (value != null)
            {
                return ParseUInt32(value.ToString());
            }
            
            return 0;
        }

        /// <summary>
        ///     Parses the UInt32.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>Parsed value or 0.</returns>
        public static uint ParseUInt32(string value)
        {
            return ParseUInt32(value, 0);
        }

        /// <summary>
        ///     Parses the UInt32 or sets default.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="defaultValue">Default value to return is value is invalid</param>
        /// <returns>Parsed value or default.</returns>
        public static uint ParseUInt32(string value, uint defaultValue)
        {
            uint UInt32;
            if (uint.TryParse(value, out UInt32))
            {
                return UInt32;
            }
            return defaultValue;
        }

        //- @ParseUInt64 -//
        /// <summary>
        ///     Parses the UInt64.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>Parsed value.</returns>
        public static ulong ParseUInt64(object value)
        {
            if (value != null)
            {
                return ParseUInt64(value.ToString());
            }
            
            return 0;
        }

        /// <summary>
        ///     Parses the UInt64.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>Parsed value or 0.</returns>
        public static ulong ParseUInt64(string value)
        {
            return ParseUInt64(value, 0);
        }

        /// <summary>
        ///     Parses the UInt64 or sets default.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="defaultValue">Default value to return is value is invalid</param>
        /// <returns>Parsed value or default.</returns>
        public static ulong ParseUInt64(string value, ulong defaultValue)
        {
            ulong UInt64;
            if (ulong.TryParse(value, out UInt64))
            {
                return UInt64;
            }
            return defaultValue;
        }
    }
}