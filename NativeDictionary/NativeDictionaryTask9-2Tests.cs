using NUnit.Framework;

namespace AlgorithmsDataStructures
{
    [TestFixture]
    public class NativeDictionaryTask9_2Tests
    {
        #region NativeDictionaryWithOrderedKeys tests

        [Test]
        public void Put_PassValueWhenDictionaryEmpty_ValueSetInExpectedIndex()
        {
            NativeDictionaryWithOrderedKeys<int> nativeDictionary = GetNativeDictionaryWithOrderedKeys();
            string expectedKey = "Hello";
            int expectedValue = 5;
            
            nativeDictionary.Put(expectedKey, expectedValue);

            int resultValue = nativeDictionary.Get(expectedKey);
            
            Assert.That(resultValue, Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Put_PassValueWhenDictionaryExistValueWithSameKey_ValueWithKeyUpdatedToNew()
        {
            NativeDictionaryWithOrderedKeys<int> nativeDictionary = GetNativeDictionaryWithOrderedKeys();
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
            NativeDictionaryWithOrderedKeys<int> nativeDictionary = GetNativeDictionaryWithOrderedKeys();
            string key = "Hello";
            int value = 7;
            
            nativeDictionary.Put(key, value);

            bool isKeyResult = nativeDictionary.IsKey(key);
            
            Assert.That(isKeyResult, Is.True);
        }
        
        [Test]
        public void IsKey_WhenKeyDoesNotExists_ReturnFalse()
        {
            NativeDictionaryWithOrderedKeys<int> nativeDictionary = GetNativeDictionaryWithOrderedKeys();
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
            NativeDictionaryWithOrderedKeys<int> nativeDictionary = GetNativeDictionaryWithOrderedKeys();
            string key = "Hello";
            int expectedValue = 7;
            
            nativeDictionary.Put(key, expectedValue);

            int valueResult = nativeDictionary.Get(key);
            
            Assert.That(valueResult, Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Get_WhenKeyValueDoesNotExists_ReturnNull()
        {
            NativeDictionaryWithOrderedKeys<object> nativeDictionary = new NativeDictionaryWithOrderedKeys<object>();
            string key = "Hello";
            string findingKey = "World";
            object value = new object();
            
            nativeDictionary.Put(key, value);

            object isKeyResult = nativeDictionary.Get(findingKey);
            
            Assert.That(isKeyResult, Is.Null);
        }

        #endregion

        #region NativeDictionaryWithBitKeys tests

        [Test]
        public void Bit_Put_PassValueWhenDictionaryEmpty_ValueSetInExpectedIndex()
        {
            NativeDictionaryWithBitKeys<int> nativeDictionary = GetNativeDictionaryWithBitKeys(4);
            int expectedKey = 0b_0101;
            int expectedValue = 5;
            
            nativeDictionary.Put(expectedKey, expectedValue);

            int resultValue = nativeDictionary.Get(expectedKey);
            
            Assert.That(resultValue, Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Bit_Put_PassValueWhenDictionaryExistValueWithSameKey_ValueWithKeyUpdatedToNew()
        {
            NativeDictionaryWithBitKeys<int> nativeDictionary = GetNativeDictionaryWithBitKeys(4);
            int expectedKey = 0b_0101;
            int oldValue = 5;
            int expectedValue = 7;
            
            nativeDictionary.Put(expectedKey, oldValue);
            nativeDictionary.Put(expectedKey, expectedValue);

            int resultValue = nativeDictionary.Get(expectedKey);
            
            Assert.That(resultValue, Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Bit_Get_WhenKeyValueExists_ReturnValue()
        {
            NativeDictionaryWithBitKeys<int> nativeDictionary = GetNativeDictionaryWithBitKeys(4);
            int key = 0b_0101;
            int expectedValue = 7;
            
            nativeDictionary.Put(key, expectedValue);

            int valueResult = nativeDictionary.Get(key);
            
            Assert.That(valueResult, Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Bit_Get_WhenKeyValueDoesNotExists_ReturnNull()
        {
            NativeDictionaryWithBitKeys<object> nativeDictionary = new NativeDictionaryWithBitKeys<object>(4);
            int key = 0b_0101;
            int findingKey = 0b_1111;
            object value = new object();
            
            nativeDictionary.Put(key, value);

            object isKeyResult = nativeDictionary.Get(findingKey);
            
            Assert.That(isKeyResult, Is.Null);
        }
        
        [Test]
        public void Bit_Delete_RemoveExistedValue_ValueRemoved()
        {
            NativeDictionaryWithBitKeys<int> nativeDictionary = GetNativeDictionaryWithBitKeys(4);
            int key = 0b_0101;
            nativeDictionary.Put(key, 5);
            
            nativeDictionary.Delete(key);

            bool resultValue = nativeDictionary.IsKey(key);
            
            Assert.That(resultValue, Is.False);
        }
        
        [Test]
        public void Bit_Delete_ExistValuesRange_OnlyExpectedValueRemoved()
        {
            NativeDictionaryWithBitKeys<int> nativeDictionary = GetNativeDictionaryWithBitKeys(4);
            int keyForRemove = 0b_0101;
            int key = 0b_1111;
            nativeDictionary.Put(keyForRemove, 5);
            nativeDictionary.Put(key, 7);
            
            nativeDictionary.Delete(keyForRemove);

            bool existDeletedKey = nativeDictionary.IsKey(keyForRemove);
            bool existValueForNotDeletedKey = nativeDictionary.IsKey(key);
            
            Assert.That(existDeletedKey, Is.False);
            Assert.That(existValueForNotDeletedKey, Is.True);
        }

        #endregion
        
        private static NativeDictionaryWithOrderedKeys<int> GetNativeDictionaryWithOrderedKeys()
        {
            return new NativeDictionaryWithOrderedKeys<int>();
        }
        
        private static NativeDictionaryWithBitKeys<int> GetNativeDictionaryWithBitKeys(int maskLength)
        {
            return new NativeDictionaryWithBitKeys<int>(maskLength);
        }
    }
}