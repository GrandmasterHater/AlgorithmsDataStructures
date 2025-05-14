using System;
using System.Linq;
using NUnit.Framework;

namespace AlgorithmsDataStructures
{
    [TestFixture]
    public class NativeCacheTask12Tests
    {
        [Test]
        public void Put_PassValueWhenDictionaryEmpty_ValueSetInExpectedIndex()
        {
            NativeCache<int> nativeCache = GetNativeCache();
            string expectedKey = "Hello";
            int expectedValue = 5;
            
            nativeCache.Put(expectedKey, expectedValue);

            int resultValue = nativeCache.Get(expectedKey);
            
            Assert.That(resultValue, Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Put_PassValueWhenDictionaryExistValueWithSameKey_ValueWithKeyUpdatedToNew()
        {
            NativeCache<int> nativeCache = GetNativeCache();
            string expectedKey = "Hello";
            int oldValue = 5;
            int expectedValue = 7;
            
            nativeCache.Put(expectedKey, oldValue);
            nativeCache.Put(expectedKey, expectedValue);

            int resultValue = nativeCache.Get(expectedKey);
            
            Assert.That(resultValue, Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Put_CacheIsFull_DisplaceValueWithMinCount()
        {
            NativeCache<string> nativeCache = new NativeCache<string>(17);

            for (int i = 0; i < nativeCache.size; ++i)
                nativeCache.Put($"Key_{i}", $"Value_{i}");
            
            for (int i = 0; i < nativeCache.size - 1; ++i)
                nativeCache.Put($"Key_{i}", $"Value_{i}");

            int displacingKeyIndex = Array.IndexOf(nativeCache.hits, 1);
            string displacedKey = nativeCache.slots[displacingKeyIndex];
            string newKey = $"Key_{nativeCache.size + 1}";
            string newValue = "TestValue";
            
            nativeCache.Put(newKey, newValue);
            
            Assert.That(nativeCache.IsKey(displacedKey), Is.False);
            Assert.That(nativeCache.IsKey(newKey), Is.True);
            Assert.That(nativeCache.Get(newKey), Is.EqualTo(newValue));
        }
        
        [Test]
        public void Put_PassSameKeyAndValue_IncreaseHitsCount()
        {
            NativeCache<string> nativeCache = new NativeCache<string>(17);
            
            for (int i = 0; i < nativeCache.size; ++i)
                nativeCache.Put($"Key_{i}", $"Value_{i}");
            
            string keyForIncrease = $"Key_0";
            string valueForIncrease = $"Value_0";
            int expectedHitsCount = 2;
            
            nativeCache.Put(keyForIncrease, valueForIncrease);
            
            Assert.That(nativeCache.Get(keyForIncrease), Is.EqualTo(valueForIncrease));
            Assert.That(nativeCache.HitsCount(keyForIncrease), Is.EqualTo(expectedHitsCount));
        }

        [Test]
        public void IsKey_WhenKeyExists_ReturnTrue()
        {
            NativeCache<int> nativeCache = GetNativeCache();
            string key = "Hello";
            int value = 7;
            
            nativeCache.Put(key, value);

            bool isKeyResult = nativeCache.IsKey(key);
            
            Assert.That(isKeyResult, Is.True);
        }
        
        [Test]
        public void IsKey_WhenKeyDoesNotExists_ReturnFalse()
        {
            NativeCache<int> nativeCache = GetNativeCache();
            string key = "Hello";
            string findingKey = "World";
            int value = 7;
            
            nativeCache.Put(key, value);

            bool isKeyResult = nativeCache.IsKey(findingKey);
            
            Assert.That(isKeyResult, Is.False);
        }
        
        [Test]
        public void Get_WhenKeyValueExists_ReturnValue()
        {
            NativeCache<int> nativeCache = GetNativeCache();
            string key = "Hello";
            int expectedValue = 7;
            
            nativeCache.Put(key, expectedValue);

            int valueResult = nativeCache.Get(key);
            
            Assert.That(valueResult, Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Get_WhenKeyValueDoesNotExists_ReturnNull()
        {
            NativeCache<object> nativeCache = new NativeCache<object>(17);
            string key = "Hello";
            string findingKey = "World";
            object value = new object();
            
            nativeCache.Put(key, value);

            object isKeyResult = nativeCache.Get(findingKey);
            
            Assert.That(isKeyResult, Is.Null);
        }
        
        private static NativeCache<int>GetNativeCache()
        {
            return new NativeCache<int>(17);
        }
    }
}