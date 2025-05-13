using System.Collections.Generic;
using NUnit.Framework;

namespace AlgorithmsDataStructures.BloomFilterTask11
{
    [TestFixture]
    public class BloomFilterTests
    {
        [Test]
        public void AddAndIsValue_AddRangeValues_AllValuesExists()
        {
            string[] testData = new[]
            {
                "0123456789", "1234567890", "2345678901", "3456789012", "4567890123",
                "5678901234", "6789012345", "7890123456", "8901234567", "9012345678"
            };
            
            BloomFilter bloomFilter = new BloomFilter(32);

            foreach (string data in testData)
            {
                bloomFilter.Add(data);
            }
            
            List<bool> results = new List<bool>(testData.Length);
            
            foreach (string data in testData)
            {
                results.Add(bloomFilter.IsValue(data));
            }
            
            Assert.That(results, Is.All.True);
        }
        
        [Test]
        public void IsValue_FindNotExistedValueInRange_AllValuesExists()
        {
            string[] testData = new[]
            {
                "0123456789", "1234567890", "2345678901", "3456789012", "4567890123",
                "5678901234", "6789012345", "7890123456", "8901234567", "9012345678"
            };
            
            BloomFilter bloomFilter = new BloomFilter(32);
            string findValue = "9992345678";

            foreach (string data in testData)
            {
                bloomFilter.Add(data);
            }
            
            Assert.That(bloomFilter.IsValue(findValue), Is.False);
        }
    }
}