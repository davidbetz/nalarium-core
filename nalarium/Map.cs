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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Nalarium
{
    /// <summary>
    ///     A dictionary-like structure with allows for easy interaction.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <example>See Map type</example>
    [DataContract]
    public class Map<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Map&lt;T1, T2&gt;" /> class with an optional generic MapEntry
        ///     parameter array
        /// </summary>
        /// <param name="parameterArray">The parameter array.</param>
        public Map(IEnumerable<MapEntry<TKey, TValue>> parameterArray)
        {
            if (parameterArray != null)
            {
                foreach (var mapEntry in parameterArray)
                {
                    AddMapEntry(mapEntry);
                }
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Map&lt;T1, T2&gt;" /> class with an optional generic MapEntry
        ///     parameter array
        /// </summary>
        /// <param name="parameterArray">The parameter array.</param>
        public Map(params MapEntry<TKey, TValue>[] parameterArray)
        {
            if (parameterArray != null)
            {
                foreach (var mapEntry in parameterArray)
                {
                    AddMapEntry(mapEntry);
                }
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Map&lt;T1, T2&gt;" /> class with another Map instance.
        /// </summary>
        /// <param name="initMap">The initialization map.</param>
        public Map(Map<TKey, TValue> initMap)
        {
            ImportMap(initMap);
        }

        [DataMember]
        protected Dictionary<TKey, TValue> Data { get; set; } = new Dictionary<TKey, TValue>();

        public ICollection<TKey> Keys
        {
            get { return Data.Keys; }
        }

        public ICollection<TValue> Values
        {
            get { return Data.Values; }
        }

        public virtual TValue this[TKey key]
        {
            get
            {
                if (ContainsKey(key))
                {
                    return Data[key];
                }
                
                return default(TValue);
            }
            set { Data[key] = value; }
        }

        public int Count
        {
            get { return Data.Count; }
        }

        /// <summary>
        ///     The map data as a property (same as the GetDataSource Method)
        /// </summary>
        public List<MapEntry<TKey, TValue>> DataSource
        {
            get { return GetDataSource(); }
        }

        
        //-  @IsNotNullOrEmpty -//
        public static bool IsNotNullOrEmpty(Map<TKey, TValue> dictionary)
        {
            if (dictionary == null)
            {
                return false;
            }
            if (dictionary.Count == 0)
            {
                return false;
            }
            
            return true;
        }

        
        //- @Add -//
        public void Add(TKey key, TValue value)
        {
            if (!Data.ContainsKey(key))
            {
                Data.Add(key, value);
            }
        }

        public void Add(TKey key, TValue value, MapDuplicateMode mode)
        {
            if (!Data.ContainsKey(key))
            {
                Data.Add(key, value);
            }
            switch (mode)
            {
                case MapDuplicateMode.Ignore:
                    Add(key, value);
                    break;
                case MapDuplicateMode.Replace:
                    Data[key] = value;
                    break;
                case MapDuplicateMode.Throw:
                    Data.Add(key, value);
                    break;
                default:
                    break;
            }
        }

        //- @AddIfNotPresent -//
        public bool AddIfNotPresent(TKey key, TValue value)
        {
            if (!Data.ContainsKey(key))
            {
                Data.Add(key, value);
                
                return true;
            }
            
            return false;
        }

        //- @ContainsKey -//
        public bool ContainsKey(TKey key)
        {
            if (key == null)
            {
                return false;
            }
            return Data.ContainsKey(key);
        }

        //- @Keys -//

        //- @Remove -//
        public bool Remove(TKey key)
        {
            return Data.Remove(key);
        }

        //- @TryGetValue -//
        public bool TryGetValue(TKey key, out TValue value)
        {
            return Data.TryGetValue(key, out value);
        }

        //- @Values -//

        //- @FindKeyForIndex -//
        public TKey FindKeyForIndex(int index)
        {
            var currentIndex = 0;
            foreach (var key in Data.Keys)
            {
                if (currentIndex++ == index)
                {
                    return key;
                }
            }
            
            return default(TKey);
        }

        //- @[] -//

        //- @Clear -//
        public void Clear()
        {
            Data.Clear();
        }

        //- @Count -//

        //+ static
        //- @IsNullOrEmpty -//
        public static bool IsNullOrEmpty(Map<TKey, TValue> map)
        {
            if (map == null || map.Count == 0)
            {
                return true;
            }
            
            return false;
        }

        
        //- @Ctor -//

        
        //- @AddMapEntry -//
        /// <summary>
        ///     Adds the map entry.
        /// </summary>
        /// <param name="mapEntry">The map entry.</param>
        public void AddMapEntry(MapEntry<TKey, TValue> mapEntry)
        {
            if (mapEntry != null)
            {
                Add(mapEntry.Key, mapEntry.Value);
            }
        }

        //- @GetKeyList -//
        /// <summary>
        ///     Gets the key list.
        /// </summary>
        /// <returns></returns>
        public List<TKey> GetKeyList()
        {
            return Data.Keys.ToList();
        }

        //- @GetValueList -//
        /// <summary>
        ///     Gets the value list.
        /// </summary>
        /// <returns></returns>
        public List<TValue> GetValueList()
        {
            return Data.Values.ToList();
        }

        //- @GetValueArray -//
        /// <summary>
        ///     Gets the value array.
        /// </summary>
        /// <returns></returns>
        public TValue[] GetValueArray()
        {
            return Data.Values.ToArray();
        }

        //- @ImportMap -//
        /// <summary>
        ///     Appends the specific map to the current one.
        /// </summary>
        /// <param name="map">The map to import</param>
        public void ImportMap(Map<TKey, TValue> map)
        {
            if (map != null)
            {
                var keyList = map.GetKeyList();
                foreach (var key in keyList)
                {
                    Add(key, map[key]);
                }
            }
        }

        //- @PeekSafely -//
        /// <summary>
        ///     Returns a the value associated with a specific key or the default value of the type is the value doens't exist.
        ///     It's safe because an exception is not thrown.
        /// </summary>
        /// <param name="key">The key to look up.</param>
        /// <returns>The associated value of the key or, if there isn't one, the default value of the value generic type.</returns>
        [Obsolete("Use indexer instead.")]
        public TValue PeekSafely(TKey key)
        {
            if (key != null)
            {
                if (ContainsKey(key))
                {
                    return this[key];
                }
            }
            
            return default(TValue);
        }

        //- @PeekSafely -//
        /// <summary>
        ///     Returns a the value associated with a specific key or the default value of the type is the value doens't exist.
        ///     It's safe because an exception is not thrown.
        /// </summary>
        /// <typeparam name="T">The type to which the returning value should be cast.</typeparam>
        /// <param name="key">The key to look up.</param>
        /// <returns>The associated value of the key or, if there isn't one, the default value of the value generic type.</returns>
        [Obsolete("Use Get instead.")]
        public T PeekSafely<T>(TKey key) where T : TValue
        {
            if (key != null)
            {
                if (ContainsKey(key))
                {
                    return (T) this[key];
                }
            }
            return default(T);
        }

        //- @Get -//
        /// <summary>
        ///     Returns a the value associated with a specific key or the default value of the type is the value doens't exist.
        ///     It's safe because an exception is not thrown.
        /// </summary>
        /// <typeparam name="T">The type to which the returning value should be cast.</typeparam>
        /// <param name="key">The key to look up.</param>
        /// <returns>The associated value of the key or, if there isn't one, the default value of the value generic type.</returns>
        public T Get<T>(TKey key) where T : TValue
        {
            if (key != null)
            {
                if (ContainsKey(key))
                {
                    return (T) this[key];
                }
            }
            return default(T);
        }

        //- @Pull - //
        /// <summary>
        ///     Pulls the value associated with the key and removes the key/value pair from the map.
        /// </summary>
        /// <param name="key">The key to look up.</param>
        /// <returns>The associated value of the key or, if there isn't one, the default value of the value generic type.</returns>
        public TValue Pull(TKey key)
        {
            if (key != null)
            {
                if (ContainsKey(key))
                {
                    var value = this[key];
                    Remove(key);
                    return value;
                }
            }
            
            return default(TValue);
        }

        //- @GetDataSource -//
        /// <summary>
        ///     Gets the datasource from the map as a map entry list.
        /// </summary>
        /// <returns></returns>
        public List<MapEntry<TKey, TValue>> GetDataSource()
        {
            var mapEntryList = new List<MapEntry<TKey, TValue>>();
            var keyList = GetKeyList();
            foreach (var key in keyList)
            {
                mapEntryList.Add(new MapEntry<TKey, TValue>(key, this[key]));
            }
            
            return mapEntryList;
        }

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        #endregion
    }

    
    /// <summary>
    ///     A dictionary-like structure with allows for easy interaction for string keys and values.
    /// </summary>
    /// <example>
    ///     Map map = new Map("FirstName=John", "LastName=Doe");
    ///     //+ add single pair, pair series, and comma-separated series
    ///     map.AddPair("UserName=JohnDoe");
    ///     map.AddPairSeries("Email=johndoe@tempuri.org", "WebSite=www.tempuri.org");
    ///     map.AddCommaSeries("SpouseName=Jane Doe,Birthdate=04/14/1974");
    ///     //+ template with pattern
    ///     Template person = new Template("My name is {FirstName} {LastName}; {Email}; {WebSite}; {UserName}; {SpouseName};
    ///     {Birthdate}");
    ///     //+ interpolate with data with string
    ///     Console.WriteLine(person.Interpolate(map));
    /// </example>
    [DataContract]
    public class Map : Map<string, string>
    {
        //- @Ctor -//
        /// <summary>
        ///     Initializes a new instance of the <see cref="Map" /> class.
        /// </summary>
        public Map()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Map" /> class with an optional MapEntry parameter array
        /// </summary>
        /// <param name="parameterArray">An optional MapEntry parameter array.</param>
        public Map(params MapEntry[] parameterArray)
        {
            AddMapEntrySeries(parameterArray);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Map" /> class with an optional pair parameter array
        /// </summary>
        /// <param name="parameterArray">An optional pair (i.e. "a=b") parameter array.</param>
        public Map(params string[] parameterArray)
        {
            AddPairSeries(parameterArray);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Map" /> class with another Map instance.
        /// </summary>
        /// <param name="initMap">The init map.</param>
        public Map(Map initMap)
            : base(initMap)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Map" /> class with an IDictionary instance
        /// </summary>
        /// <param name="dictionary">The IDictionary instance used to initialize the map</param>
        public Map(IDictionary dictionary)
        {
            if (dictionary != null)
            {
                foreach (string key in dictionary.Keys)
                {
                    var k = key;
                    var v = dictionary[key] as string;
                    
                    if (k != null && v != null)
                    {
                        Add(k, v);
                    }
                }
            }
        }

        
        //- @AddQueryString -//
        /// <summary>
        ///     Adds the pair series (a query string looks like "a=b&c=d&e=f")
        /// </summary>
        /// <param name="seriesMapping">A queryString.</param>
        public void AddQueryString(string queryString)
        {
            if (!string.IsNullOrEmpty(queryString))
            {
                var parts = queryString.Split('&');
                if (parts.Length > 0)
                {
                    AddPairSeries(parts);
                }
            }
        }

        //- @AddMapEntrySeries -//
        /// <summary>
        ///     Adds the map entry series.
        /// </summary>
        /// <param name="mapEntryArray">The map entry array.</param>
        public void AddMapEntrySeries(MapEntry[] mapEntryArray)
        {
            if (mapEntryArray != null)
            {
                foreach (var mapEntry in mapEntryArray)
                {
                    AddMapEntry(mapEntry);
                }
            }
        }

        //- @AddPairSeries -//
        /// <summary>
        ///     Adds the pair series (a pair is a "a=b" pattern)
        /// </summary>
        /// <param name="parameterArray">The parameter array.</param>
        public void AddPairSeries(params string[] parameterArray)
        {
            if (parameterArray != null)
            {
                foreach (var mapping in parameterArray)
                {
                    AddPair(mapping);
                }
            }
        }

        //- @AddPair -//
        /// <summary>
        ///     Adds the pair (a pair is a "a=b" pattern)
        /// </summary>
        /// <param name="singleMapping">The single mapping.</param>
        public void AddPair(string singleMapping)
        {
            var name = string.Empty;
            var value = string.Empty;
            var parts = singleMapping.Split('=');
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
                }
                
                Add(name.Trim(), value);
            }
        }

        //- @Get -//
        /// <summary>
        ///     Returns a case sensitive value from the map.
        /// </summary>
        /// <param name="key">The key to look up.</param>
        /// <returns>The associated value of the key or String.Empty is the key doens't exist</returns>
        public string Get(string key)
        {
            return Get(key, StringComparison.Ordinal);
        }

        /// <summary>
        ///     Returns a case insensitive value from the map.
        /// </summary>
        /// <param name="key">The key to look up.</param>
        /// <param name="stringComparison">The StringComparison value used to control string comparison.</param>
        /// <returns>The associated value of the key or String.Empty is the key doens't exist</returns>
        public string Get(string key, StringComparison stringComparison)
        {
            Func<KeyValuePair<string, string>, bool> keyExists = p => p.Key.Equals(key, stringComparison);
            if (Data.Any(keyExists))
            {
                return Data.First(keyExists).Value;
            }
            
            return string.Empty;
        }

        //- @PeekSafely -//
        /// <summary>
        ///     Returns a the value associated with a specific key or the default value of the type is the value doens't exist.
        ///     It's safe because an exception is not thrown.
        /// </summary>
        /// <typeparam name="T">The type to which the returning value should be cast.</typeparam>
        /// <param name="key">The key to look up.</param>
        /// <returns>The associated value of the key or, if there isn't one, the default value of the value generic type.</returns>
        [Obsolete("Use indexer instead.")]
        public new T PeekSafely<T>(string key)
        {
            if (key != null)
            {
                if (ContainsKey(key))
                {
                    return Parser.Parse<T>(this[key]);
                }
            }
            return default(T);
        }
    }
}