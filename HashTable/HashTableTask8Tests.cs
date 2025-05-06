using System.Linq;
using NUnit.Framework;

namespace AlgorithmsDataStructures
{
    [TestFixture]
    public class HashTableTask8Tests
    {
        [TestCase("Hello", 7)]
        [TestCase("World", 10)]
        [TestCase("C# the best", 16)]
        public void HashFun_PassNonCollisionValues_GetExpectedHash(string testData, int expectedHash)
        {
            HashTable hashTable = GetHashTable();

            int result = hashTable.HashFun(testData);
            
            Assert.That(result, Is.EqualTo(expectedHash));
        }

        [Test]
        public void SeekSlot_PassValue_GetExpectedIndex()
        {
            HashTable hashTable = GetHashTable();
            int expectedIndex = 7;
            
            int result = hashTable.SeekSlot("Hello");
            
            Assert.That(result, Is.EqualTo(expectedIndex));
        }
        
        [TestCase("Hello", "World", 10)]
        public void SeekSlot_PutNonCollisionValues_GetExpectedIndexes(string initialTestData, string testData,  int expectedIndex)
        {
            HashTable hashTable = GetHashTable();

            hashTable.Put(initialTestData);
            int result = hashTable.SeekSlot(testData);
            
            Assert.That(result, Is.EqualTo(expectedIndex));
        }
        
        [TestCase("World", "dlroW", 13)]
        public void SeekSlot_PutCollisionValues_GetResolvedExpectedIndexes(string initialTestData, string testData,  int expectedIndex)
        {
            HashTable hashTable = GetHashTable();

            hashTable.Put(initialTestData);
            int result = hashTable.SeekSlot(testData);
            
            Assert.That(result, Is.EqualTo(expectedIndex));
        }
        
        [Test]
        public void SeekSlot_NotFoundFreeSlot_ReturnMinusOne()
        {
            HashTable hashTable = GetHashTable();
            
            for (int i = 0; i < 17; i++)
            {
                hashTable.Put("test_key" + i);
            }

            int result = hashTable.SeekSlot("Hello");
            
            Assert.That(result, Is.EqualTo(-1));
        }
        
        [TestCase(new []{"Hello", "World", "C# the best"}, new []{7, 10, 16})]
        [TestCase(new []{"Hello", "olleH", "Hlloe"}, new []{7, 10, 13})]
        public void Put_PassValues_ValueSetInExpectedIndexes(string[] testData, int[] expectedIndexes)
        {
            HashTable hashTable = GetHashTable(testData);
            
            int[] result = FindIndexes(hashTable, testData);
            
            Assert.That(result, Is.EqualTo(expectedIndexes));
        }
        
        [Test]
        public void Find_NotExistedSlot_ReturnMinusOne()
        {
            HashTable hashTable = GetHashTable(new []{"Hello", "World", "C# the best"});
            
            int result = hashTable.Find("some_key");
            
            Assert.That(result, Is.EqualTo(-1));
        }
        
        [TestCase(new []{"Hello", "World", "C# the best"}, "World", 10)]
        [TestCase(new []{"Hello", "olleH", "Hlloe"}, "Hlloe", 13)]
        public void Find_ExistedSlot_ReturnSlotIndex(string[] testData, string findData, int expectedIndexes)
        {
            HashTable hashTable = GetHashTable(testData);
            
            int result = hashTable.Find(findData);
            
            Assert.That(result, Is.EqualTo(expectedIndexes));
        }

        private static HashTable GetHashTable()
        {
            return new HashTable(17, 3);
        }
        
        private static HashTable GetHashTable(string[] datas)
        {
            HashTable hashTable = new HashTable(17, 3);

            foreach (string data in datas)
            {
                hashTable.Put(data);
            }
            
            return hashTable;
        }
        
        private static int[] FindIndexes(HashTable hashTable, string[] findDatas)
        {
            return findDatas.Select(hashTable.Find).ToArray();
        }
    }
}