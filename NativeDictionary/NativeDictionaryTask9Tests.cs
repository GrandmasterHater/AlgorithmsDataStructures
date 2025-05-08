using System;
using NUnit.Framework;

namespace AlgorithmsDataStructures
{
    [TestFixture]
    public class NativeDictionaryTask9Tests
    {
        [Test]
        public void Put_PassValueWhenDictionaryEmpty_ValueSetInExpectedIndex()
        {
            NativeDictionary<int> nativeDictionary = GetNativeDictionary();
            string expectedKey = "Hello";
            int expectedValue = 5;
            
            nativeDictionary.Put(expectedKey, expectedValue);

            int resultValue = nativeDictionary.Get(expectedKey);
            
            Assert.That(resultValue, Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Put_PassValueWhenDictionaryExistValueWithSameKey_ValueWithKeyUpdatedToNew()
        {
            NativeDictionary<int> nativeDictionary = GetNativeDictionary();
            string expectedKey = "Hello";
            int oldValue = 5;
            int expectedValue = 7;
            
            nativeDictionary.Put(expectedKey, oldValue);
            nativeDictionary.Put(expectedKey, expectedValue);

            int resultValue = nativeDictionary.Get(expectedKey);
            
            Assert.That(resultValue, Is.EqualTo(expectedValue));
        }

        [Test]
        public void IsKey_WhenKeyExists_ReturnTrue()
        {
            NativeDictionary<int> nativeDictionary = GetNativeDictionary();
            string key = "Hello";
            int value = 7;
            
            nativeDictionary.Put(key, value);

            bool isKeyResult = nativeDictionary.IsKey(key);
            
            Assert.That(isKeyResult, Is.True);
        }
        
        [Test]
        public void IsKey_WhenKeyDoesNotExists_ReturnFalse()
        {
            NativeDictionary<int> nativeDictionary = GetNativeDictionary();
            string key = "Hello";
            string findingKey = "World";
            int value = 7;
            
            nativeDictionary.Put(key, value);

            bool isKeyResult = nativeDictionary.IsKey(findingKey);
            
            Assert.That(isKeyResult, Is.False);
        }
        
        [Test]
        public void Get_WhenKeyValueExists_ReturnValue()
        {
            NativeDictionary<int> nativeDictionary = GetNativeDictionary();
            string key = "Hello";
            int expectedValue = 7;
            
            nativeDictionary.Put(key, expectedValue);

            int valueResult = nativeDictionary.Get(key);
            
            Assert.That(valueResult, Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Get_WhenKeyValueDoesNotExists_ReturnNull()
        {
            NativeDictionary<object> nativeDictionary = new NativeDictionary<object>(17);
            string key = "Hello";
            string findingKey = "World";
            object value = new object();
            
            nativeDictionary.Put(key, value);

            object isKeyResult = nativeDictionary.Get(findingKey);
            
            Assert.That(isKeyResult, Is.Null);
        }
        
        private static NativeDictionary<int> GetNativeDictionary()
        {
            return new NativeDictionary<int>(17);
        }
    }
}