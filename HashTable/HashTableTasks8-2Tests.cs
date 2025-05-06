using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace AlgorithmsDataStructures
{
    [TestFixture]
    public class HashTableTasks8_2Tests
    {
        #region DynHashTable

        [Test]
        public void SeekSlot_PassValue_GetExpectedIndex()
        {
            DynHashTable DynHashTable = GetDynHashTable();
            int expectedIndex = 7;
            
            int result = DynHashTable.SeekSlot("Hello");
            
            Assert.That(result, Is.EqualTo(expectedIndex));
        }
        
        [TestCase("Hello", "World", 10)]
        public void SeekSlot_PutNonCollisionValues_GetExpectedIndexes(string initialTestData, string testData,  int expectedIndex)
        {
            DynHashTable DynHashTable = GetDynHashTable();

            DynHashTable.Put(initialTestData);
            int result = DynHashTable.SeekSlot(testData);
            
            Assert.That(result, Is.EqualTo(expectedIndex));
        }
        
        [TestCase("World", "dlroW", 1)]
        public void SeekSlot_PutCollisionValues_GetResolvedExpectedIndexes(string initialTestData, string testData,  int expectedIndex)
        {
            DynHashTable DynHashTable = GetDynHashTable();

            DynHashTable.Put(initialTestData);
            int result = DynHashTable.SeekSlot(testData);
            
            Assert.That(result, Is.EqualTo(expectedIndex));
        }
        
        [Test]
        public void SeekSlot_NotFoundFreeSlot_ExtendSize()
        {
            DynHashTable DynHashTable = GetDynHashTable();
            
            for (int i = 0; i < 12; i++)
            {
                DynHashTable.Put("test_key" + i);
            }

            int result = DynHashTable.SeekSlot("Hello");
            
            Assert.That(result, Is.EqualTo(19));
            Assert.That(DynHashTable._slots.Length, Is.EqualTo(37));
        }

        [Test]
        public void Put_PassValue_ValueSetInExpectedIndex()
        {
            DynHashTable DynHashTable = GetDynHashTable();
            int expectedIndex = 10;
            
            DynHashTable.Put("World");
            int result = DynHashTable.Find("World");
            
            Assert.That(result, Is.EqualTo(expectedIndex));
        }
        
        [TestCase(new []{"Hello", "World", "C# the best"}, new []{7, 10, 16})]
        [TestCase(new []{"Hello", "olleH", "Hlloe"}, new []{7, 12, 1})]
        public void Put_PassValues_ValueSetInExpectedIndexes(string[] testData, int[] expectedIndexes)
        {
            DynHashTable DynHashTable = GetDynHashTable(testData);
            
            int[] result = FindIndexes(DynHashTable, testData);
            
            Assert.That(result, Is.EqualTo(expectedIndexes));
        }
        
        [Test]
        public void Put_NotFoundFreeSlot_ExtendSizeФтвЗгеМфдгу()
        {
            DynHashTable DynHashTable = GetDynHashTable();
            
            for (int i = 0; i < 12; i++)
            {
                DynHashTable.Put("test_key" + i);
            }

            int result = DynHashTable.Put("Hello");
            
            Assert.That(result, Is.EqualTo(19));
            Assert.That(DynHashTable._slots.Length, Is.EqualTo(37));
        }
        
        [Test]
        public void Find_NotExistedSlot_ReturnMinusOne()
        {
            DynHashTable DynHashTable = GetDynHashTable(new []{"Hello", "World", "C# the best"});
            
            int result = DynHashTable.Find("some_key");
            
            Assert.That(result, Is.EqualTo(-1));
        }
        
        [TestCase(new []{"Hello", "World", "C# the best"}, "World", 10)]
        [TestCase(new []{"Hello", "olleH", "Hlloe"}, "Hlloe", 1)]
        public void Find_ExistedSlot_ReturnSlotIndex(string[] testData, string findData, int expectedIndexes)
        {
            DynHashTable DynHashTable = GetDynHashTable(testData);
            
            int result = DynHashTable.Find(findData);
            
            Assert.That(result, Is.EqualTo(expectedIndexes));
        }

        private static DynHashTable GetDynHashTable()
        {
            return new DynHashTable(17);
        }
        
        private static DynHashTable GetDynHashTable(string[] datas)
        {
            DynHashTable DynHashTable = new DynHashTable(17);

            foreach (string data in datas)
            {
                DynHashTable.Put(data);
            }
            
            return DynHashTable;
        }
        
        private static int[] FindIndexes(DynHashTable dynHashTable, string[] findDatas)
        {
            return findDatas.Select(dynHashTable.Find).ToArray();
        }

        #endregion

        #region SaltedHashTable

        // Exercise 8, task 5. All "indexes" are same that meaning that there is a collision and time complexity for Put
        // operation is O(n). All "saltedIndexes" are unique that meaning that there is no collision and time complexity
        // for Put operation is O(1). 
        // Add "Salt" for hashing operation reduce collision probability.
        [Test]
        public void HashTable_vs_SaltedHashTable_DDoSTest()
        {
            string[] collidedValues = new[] { "Hello", "olleH", "Hlloe", "elHol", "olelH" };

            HashTable hashTable = new HashTable(37, 3);
            SaltedHashTable saltedHashTable = new SaltedHashTable(37, 3);
            
            List<int> indexes = new List<int>();
            List<int> saltedIndexes = new List<int>();

            foreach (string value in collidedValues)
            {
                indexes.Add(hashTable.SeekSlot(value));
                saltedIndexes.Add(saltedHashTable.SeekSlot(value));
            }
            
            Assert.That(indexes, Is.All.EqualTo(indexes[0]));
            Assert.That(saltedIndexes, Is.Unique); 
        }

        [TestCase("Hello", 13)]
        [TestCase("World", 3)]
        [TestCase("C# the best", 9)]
        public void Salted_HashFun_PassNonCollisionValues_GetExpectedHash(string testData, int expectedHash)
        {
            SaltedHashTable hashTable = GetHashTable();

            int result = hashTable.HashFun(testData);
            
            Assert.That(result, Is.EqualTo(expectedHash));
        }

        [Test]
        public void Salted_SeekSlot_PassValue_GetExpectedIndex()
        {
            SaltedHashTable hashTable = GetHashTable();
            int expectedIndex = 13;
            
            int result = hashTable.SeekSlot("Hello");
            
            Assert.That(result, Is.EqualTo(expectedIndex));
        }
        
        [TestCase("Hello", "World", 3)]
        public void Salted_SeekSlot_PutNonCollisionValues_GetExpectedIndexes(string initialTestData, string testData,  int expectedIndex)
        {
            SaltedHashTable hashTable = GetHashTable();

            hashTable.Put(initialTestData);
            int result = hashTable.SeekSlot(testData);
            
            Assert.That(result, Is.EqualTo(expectedIndex));
        }
        
        [TestCase("World", "dlroW", 7)]
        public void Salted_SeekSlot_PutCollisionValues_GetResolvedExpectedIndexes(string initialTestData, string testData,  int expectedIndex)
        {
            SaltedHashTable hashTable = GetHashTable();

            hashTable.Put(initialTestData);
            int result = hashTable.SeekSlot(testData);
            
            Assert.That(result, Is.EqualTo(expectedIndex));
        }
        
        [Test]
        public void Salted_SeekSlot_NotFoundFreeSlot_ReturnMinusOne()
        {
            SaltedHashTable hashTable = GetHashTable();
            
            for (int i = 0; i < 17; i++)
            {
                hashTable.Put("test_key" + i);
            }

            int result = hashTable.SeekSlot("Hello");
            
            Assert.That(result, Is.EqualTo(-1));
        }
        
        [TestCase(new []{"Hello", "World", "C# the best"}, new []{13, 3, 9})]
        [TestCase(new []{"Hello", "olleH", "Hlloe"}, new []{13, 9, 14})]
        public void Salted_Put_PassValues_ValueSetInExpectedIndexes(string[] testData, int[] expectedIndexes)
        {
            SaltedHashTable hashTable = GetHashTable(testData);
            
            int[] result = FindIndexes(hashTable, testData);
            
            Assert.That(result, Is.EqualTo(expectedIndexes));
        }
        
        [Test]
        public void Salted_Find_NotExistedSlot_ReturnMinusOne()
        {
            SaltedHashTable hashTable = GetHashTable(new []{"Hello", "World", "C# the best"});
            
            int result = hashTable.Find("some_key");
            
            Assert.That(result, Is.EqualTo(-1));
        }
        
        [TestCase(new []{"Hello", "World", "C# the best"}, "World", 3)]
        [TestCase(new []{"Hello", "olleH", "Hlloe"}, "Hlloe", 14)]
        public void Salted_Find_ExistedSlot_ReturnSlotIndex(string[] testData, string findData, int expectedIndexes)
        {
            SaltedHashTable hashTable = GetHashTable(testData);
            
            int result = hashTable.Find(findData);
            
            Assert.That(result, Is.EqualTo(expectedIndexes));
        }

        #endregion
        
        private static SaltedHashTable GetHashTable()
        {
            return new SaltedHashTable(17, 3);
        }
        
        private static SaltedHashTable GetHashTable(string[] datas)
        {
            SaltedHashTable hashTable = new SaltedHashTable(17, 3);

            foreach (string data in datas)
            {
                hashTable.Put(data);
            }
            
            return hashTable;
        }
        
        private static int[] FindIndexes(SaltedHashTable hashTable, string[] findDatas)
        {
            return findDatas.Select(hashTable.Find).ToArray();
        }
    }
}