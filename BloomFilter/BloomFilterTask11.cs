using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

namespace AlgorithmsDataStructures
{
    public class BloomFilter
    {
        public int filter_len;
        public int filter;

        public BloomFilter(int f_len)
        {
            filter_len = f_len;
            filter = 0;
        }
        
        // Exercise 11, task 1, time complexity O(n), space complexity O(1), where n depend on input string length
        public int Hash1(string str1)
        {
            return str1.Aggregate(0, (currCode, s) => (currCode * 17 + s) % filter_len);
        }
        
        // Exercise 11, task 1, time complexity O(n), space complexity O(1)
        public int Hash2(string str1)
        {
            return str1.Aggregate(0, (currCode, s) => (currCode * 223 + s) % filter_len);
        }

        // Exercise 11, task 1, time complexity O(n), space complexity O(1)
        public void Add(string str1)
        {
            int bit = Hash1(str1);
            SetBit(bit);
            
            bit = Hash2(str1);
            SetBit(bit);
        }

        // Exercise 11, task 1, time complexity O(n), space complexity O(1)
        public bool IsValue(string str1)
        {
            int bit = Hash1(str1);
            bool bit1 = GetBit(bit);
            
            bit = Hash2(str1);
            bool bit2 = GetBit(bit);
            
            return bit1 && bit2;
        }

        private void SetBit(int index) => filter |= 1 << index;

        private bool GetBit(int index) => (filter & 1 << index) != 0;
    }
}