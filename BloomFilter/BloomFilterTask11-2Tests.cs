using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AlgorithmsDataStructures.BloomFilterTask11
{
    [TestFixture]
    public class BloomFilterTask11_2Tests
    {
        [Test]
        public void UnionTest()
        {
            var bloom1 = new BloomFilter(32);
            var bloom2 = new BloomFilter(32);

            string[] set1 = {"0123456789", "1234567890", "2345678901", "3456789012", "4567890123"};
            string[] set2 = {"5678901234", "6789012345", "7890123456", "8901234567", "9012345678"};
            string[] expectedResults = set1.Union(set2).ToArray();

            foreach (var item in set1) 
                bloom1.Add(item);
            
            foreach (var item in set2) 
                bloom2.Add(item);

            BloomFilter unionFilter = bloom1.Union(bloom2);

            List<bool> results = new List<bool>(expectedResults.Length);
            
            foreach (string data in expectedResults)
            {
                results.Add(unionFilter.IsValue(data));
            }
            
            Assert.That(results, Is.All.True);
        }

        #region BloomFilter with removes tests

        [Test]
        public void AddAndIsValue_AddRangeValues_AllValuesExists()
        {
            string[] testData = new[]
            {
                "0123456789", "1234567890", "2345678901", "3456789012", "4567890123",
                "5678901234", "6789012345", "7890123456", "8901234567", "9012345678"
            };
            
            BloomFilterWithRemoves bloomFilter = new BloomFilterWithRemoves(32);

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
            
            BloomFilterWithRemoves bloomFilter = new BloomFilterWithRemoves(32);
            string findValue = "9992345678";

            foreach (string data in testData)
            {
                bloomFilter.Add(data);
            }
            
            Assert.That(bloomFilter.IsValue(findValue), Is.False);
        }
        
        [Test]
        public void Remove_WhenSeveralExistedValues_OneValueRemoved()
        {
            string valueForRemove = "0123456789";
            string[] testData = { valueForRemove, valueForRemove, valueForRemove};
            
            BloomFilterWithRemoves bloomFilter = new BloomFilterWithRemoves(32);

            foreach (string data in testData)
            {
                bloomFilter.Add(data);
            }
            
            bloomFilter.Remove(valueForRemove);
            
            Assert.That(bloomFilter.IsValue(valueForRemove), Is.True);
        }
        
        [Test]
        public void Remove_WhenLastValue_FilterIsEmpty()
        {
            string valueForRemove = "0123456789";
            
            BloomFilterWithRemoves bloomFilter = new BloomFilterWithRemoves(32);

            bloomFilter.Add(valueForRemove);
            
            bloomFilter.Remove(valueForRemove);
            
            Assert.That(bloomFilter.IsValue(valueForRemove), Is.False);
        }

        #endregion

        
        /*
        [Test]
        public void RecoverTest()
        {
            BloomFilter bloomFilter = new BloomFilter(32);
            
            bloomFilter.Add("0123456789");
            bloomFilter.Add("1234567890");
            bloomFilter.Add("6789012345");
            bloomFilter.Add("7890123456");
            bloomFilter.Add("8901234567");

            List<string> data = bloomFilter.RecoverData();
            
            Assert.That(data, Is.All.Count.EqualTo(5));
        }
        */
    }
}