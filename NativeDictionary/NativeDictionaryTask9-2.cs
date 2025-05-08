using System;
using System.Linq;

namespace AlgorithmsDataStructures
{
    public class KeyValue<T> : IComparable<KeyValue<T>>
    {
        public string Key { get; set; }
        public T Value { get; set; }
            
        public int CompareTo(KeyValue<T> other)
        {
            return String.CompareOrdinal(Key, other.Key);
        }
    }
    
    public class NativeDictionaryWithOrderedKeys<T>
    {
        public OrderedList<KeyValue<T>> slots;

        public NativeDictionaryWithOrderedKeys()
        {
            slots = new OrderedList<KeyValue<T>>(true);
        }

        // Exercise 9, task 5, time complexity O(n), Omega(1), space complexity O(1), where n depend on slots count
        public bool IsKey(string key)
        {
            KeyValue<T> findingKey = new KeyValue<T>() { Key = key };
            Node<KeyValue<T>> result = slots.Find(findingKey);
            
            return result != null;
        }

        // Exercise 9, task 5, time complexity O(n), Omega(1), space complexity O(1), where n depend on slots count
        public void Put(string key, T value)
        {
            KeyValue<T> findingKey = new KeyValue<T>() { Key = key };
            Node<KeyValue<T>> result = slots.Find(findingKey);
            
            if (result == null)
            {
                KeyValue<T> keyValue = new KeyValue<T>() { Key = key, Value = value };
                slots.Add(keyValue);
            }
            else
            {
                result.value.Value = value;
            }
        }

        // Exercise 9, task 5, time complexity O(n), Omega(1), space complexity O(1), where n depend on slots count
        public T Get(string key)
        {
            KeyValue<T> findingKey = new KeyValue<T>() { Key = key };
            Node<KeyValue<T>> result = slots.Find(findingKey);
            
            return result != null ? result.value.Value : default(T);
        }
    } 
    
    public class NativeDictionaryWithBitKeys<T>
    {
        public T [] values;
        public bool [] hasValue;
        private int _mask;

        public NativeDictionaryWithBitKeys(int maskLength)
        {
            int capacity = 1 << maskLength;
            _mask = capacity - 1;
            values = new T[capacity];
            hasValue = new bool[capacity];
        }

        // Exercise 9, task 6, time complexity O(1), space complexity O(1)
        public void Put(int key, T value)
        {
            int index = ConvertKeyToIndex(key);
            
            values[index] = value;
            hasValue[index] = true;
        }
        
        // Exercise 9, task 6, time complexity O(1), space complexity O(1)
        public bool IsKey(int key)
        {
            int index = ConvertKeyToIndex(key);
            
            return hasValue[index];
        }

        // Exercise 9, task 6, time complexity O(1), space complexity O(1)
        public T Get(int key)
        {
            int index = ConvertKeyToIndex(key);
            
            return values[index];
        }
        
        // Exercise 9, task 6, time complexity O(1), space complexity O(1)
        public void Delete(int key)
        {
            int index = ConvertKeyToIndex(key);
            
            values[index] = default;
            hasValue[index] = false;
        }
        
        private int ConvertKeyToIndex(int key)
        {
            return key & _mask;
        }
    } 
}