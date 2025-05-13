using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures.BloomFilterTask11
{
    public static class BloomFilterTask11_2
    {
        // Exercise 11, task 2, time complexity O(1), space complexity O(1)
        public static BloomFilter Union(this BloomFilter firstBloomFilter, BloomFilter secondBloomFilter)
        {
            if (firstBloomFilter.filter_len != secondBloomFilter.filter_len)
            {
                throw new ArgumentException("Bloom filters have different lengths.");
            }
            
            BloomFilter unionBloomFilter = new BloomFilter(firstBloomFilter.filter_len);
            
            unionBloomFilter.filter = firstBloomFilter.filter | secondBloomFilter.filter;
            
            return unionBloomFilter;
        }

        // Exercise 11, task 4
        // Set the criteria so that input values contain characters from 0 to 9 and their length is always 10.
        // Reflection:
        // I made a "head-on" version, just brute-force until we get a value. As a conditional preliminary
        // analysis of the data, I set the criteria that the input values are characters 0-9, and there are
        // always 10 of them. The current filter produces a huge number of false positives. With the amount
        // of input data (for 10 characters 0-9, this is 10 billion options) that participate in brute-force
        // for this implementation, the probability of a false positive is about 100 percent. If we do not
        // consider changes in the filter itself, such as increasing the filter length and using a different
        // hash function, then we need to somehow analyze the input data in order to minimize the list of
        // possible data options. For example, by accepting the limitations of the input values, that the
        // characters in the line do not repeat (3 628 800 combines) and go sequentially (10 combines). But to get such limitations, a very
        // extensive analysis of the input data is needed.
        public static List<string> RecoverData(this BloomFilter filter)
        {
            List<string> result = new List<string>();
            
            string charSet = "0123456789";

            for (char[] currentCharSet = GetInitialCharSet(charSet); IsAllCombinesUsed(currentCharSet); IncrementCharSetValue(currentCharSet))
            {
                string data = new string(currentCharSet);
                
                if (filter.IsValue(data))
                    result.Add(data);
            }
            
            return result;

            char[] GetInitialCharSet(string chars)
            {
                char[] initialCharSet = new char[chars.Length];
            
                for (int i = 0; i < charSet.Length; ++i)
                    initialCharSet[i] = '0';

                return initialCharSet;
            }

            bool IsAllCombinesUsed(char[] chars)
            {
                return chars[0] != 'c';
            }

            void IncrementCharSetValue(char[] chars)
            {
                for (int i = 9; i < chars.Length; --i)
                {
                    chars[i] = chars[i] == '9' ? '0' : (char)(chars[i] + 1);

                    if (chars[i] != '0')
                    {
                        return;
                    }
                }

                for (int i = 9; i >= 0; --i)
                {
                    if (i == 0)
                        chars[i] = 'c';
                    else
                        chars[i] = '0';
                }
            }
        }
    }
    
    // Exercise 11, task 3
    public class BloomFilterWithRemoves
    {
        public int filter_len;
        public int[] filter;

        public BloomFilterWithRemoves(int f_len)
        {
            filter_len = f_len;
            filter = new int[filter_len];
        }
        
        // Exercise 11, task 3, time complexity O(n), space complexity O(1), where n depend on input string length
        public int Hash1(string str1)
        {
            return str1.Aggregate(0, (currCode, s) => (currCode * 17 + s) % filter_len);
        }
        
        // Exercise 11, task 3, time complexity O(n), space complexity O(1), where n depend on input string length
        public int Hash2(string str1)
        {
            return str1.Aggregate(0, (currCode, s) => (currCode * 223 + s) % filter_len);
        }

        // Exercise 11, task 3, time complexity O(n), space complexity O(1)
        public void Add(string str1)
        {
            int index = Hash1(str1);
            ++filter[index];
            
            index = Hash2(str1);
            ++filter[index];
        }
        
        // Exercise 11, task 3, time complexity O(n), space complexity O(1)
        public void Remove(string str1)
        {
            int index = Hash1(str1);
            filter[index] = filter[index] > 0 ? filter[index] - 1 : 0;
            
            index = Hash2(str1);
            filter[index] = filter[index] > 0 ? filter[index] - 1 : 0;
        }

        // Exercise 11, task 3, time complexity O(n), space complexity O(1)
        public bool IsValue(string str1)
        {
            int index = Hash1(str1);
            int firstCount = filter[index];
            
            index = Hash2(str1);
            int secondCount = filter[index];
            
            return firstCount > 0 && secondCount > 0;
        }
    }
}