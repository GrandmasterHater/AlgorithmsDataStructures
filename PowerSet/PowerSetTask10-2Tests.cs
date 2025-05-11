using System.Collections.Generic;
using NUnit.Framework;

namespace AlgorithmsDataStructures.PowerSet
{
    [TestFixture]
    public class PowerSetTask10_2Tests
    {
        #region Multiple intersection tests

        [TestCase(
            new[] { "s", "g", "z", "c", "f" },
            new[] { "c", "b", "p", "f", "i" },
            new[] { "t", "i", "f", "b", "c" },
            new[] { "t", "i", "f", "b", "c" },
            new[] { "t", "i", "f", "b", "c" },
            new[] { "f", "c" }
        )]
        public void Intersection_ReturnsCorrectCommonElements(
            string[] set1Items,
            string[] set2Items,
            string[] set3Items,
            string[] set4Items,
            string[] set5Items,
            string[] expectedItems)
        {
            var set1 = CreateSetStrings(set1Items);
            var set2 = CreateSetStrings(set2Items);
            var set3 = CreateSetStrings(set3Items);
            var set4 = CreateSetStrings(set4Items);
            var set5 = CreateSetStrings(set5Items);

            var result = set1.Intersection(set2, set3, set4, set5);

            var expected = CreateSetStrings(expectedItems);

            Assert.That(result.Equals(expected), Is.True);
        }

        #endregion

        #region Bag tests

        [Test]
        public void Add_WhenBagIsEmpty_AddsItemToBag()
        {
            Bag<string> bag = new Bag<string>();
            string item = "item1";
            KeyValuePair<string, int> expectedResult = new KeyValuePair<string, int>(item, 1);
            
            bag.Add(item);
            Assert.That(bag.Get(item), Is.EqualTo(expectedResult));
        }
        
        [Test]
        public void Add_WhenBagHasSameItem_ItemsCountIncreased()
        {
            Bag<string> bag = new Bag<string>();
            string item = "item1";
            KeyValuePair<string, int> expectedResult = new KeyValuePair<string, int>(item, 2);
            
            bag.Add(item);
            bag.Add(item);
            
            Assert.That(bag.Get(item), Is.EqualTo(expectedResult));
        }
        
        [Test]
        public void Remove_WhenBagExistsLast_ItemRemoved()
        {
            Bag<string> bag = new Bag<string>();
            string item = "item1";
            KeyValuePair<string, int> expectedResult = default(KeyValuePair<string, int>);
            bag.Add(item);
            
            bag.Remove(item);
            Assert.That(bag.Get(item), Is.EqualTo(expectedResult));
        }
        
        [Test]
        public void Remove_WhenBagExistsSeveralSameItems_OnlyOneItemRemoved()
        {
            Bag<string> bag = new Bag<string>();
            string item = "item1";
            KeyValuePair<string, int> expectedResult = new KeyValuePair<string, int>(item, 1);
            bag.Add(item);
            bag.Add(item);
            
            bag.Remove(item);
            Assert.That(bag.Get(item), Is.EqualTo(expectedResult));
        }
        
        [Test]
        public void Remove_WhenBagNotExistsItemForRemove_BagNotChanged()
        {
            Bag<string> bag = new Bag<string>();
            string item = "item1";
            string item2 = "item2";
            string item3 = "item3";
            string itemForRemove = "item4";
            
            bag.Add(item);
            bag.Add(item);
            bag.Add(item);
            bag.Add(item2);
            bag.Add(item2);
            bag.Add(item2);
            bag.Add(item3);
            bag.Add(item3);
            bag.Add(item3);
            IEnumerable<KeyValuePair<string, int>> expectedResult = bag.AllElements();
            
            bag.Remove(itemForRemove);
            Assert.That(bag.AllElements(), Is.EqualTo(expectedResult));
        }
        
        [Test]
        public void AllElements_WhenBagExistsItems_ReturnAllItems()
        {
            Bag<string> bag = new Bag<string>();
            string item = "item1";
            string item2 = "item2";
            string item3 = "item3";
            string itemForRemove = "item4";
            
            bag.Add(item);
            bag.Add(item);
            bag.Add(item);
            bag.Add(item2);
            bag.Add(item2);
            bag.Add(item2);
            bag.Add(item3);
            bag.Add(item3);
            bag.Add(item3);
            List<KeyValuePair<string, int>> expectedResult = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>(item, 3),
                new KeyValuePair<string, int>(item2, 3),
                new KeyValuePair<string, int>(item3, 3)
            };
            
            bag.Remove(itemForRemove);
            Assert.That(bag.AllElements(), Is.EqualTo(expectedResult));
        }

        #endregion

        #region Cartesian product tests

        [TestCase(new[] {1, 2}, new[] {3, 4}, new[] {1, 3, 1, 4, 2, 3, 2, 4})]
        [TestCase(new[] {1, 2}, new int[] { }, new int[] {})]
        [TestCase(new[] {1}, new[] {3}, new[] {1, 3})]
        [TestCase(new[] {1, 2}, new[] {1, 2}, new[] {1, 1, 1, 2, 2, 1, 2, 2})]
        public void CartesianProduct_Test(int[] set1Items, int[] set2Items, int[] expectedItems)
        {
            var set1 = CreateSetInt(set1Items);
            var set2 = CreateSetInt(set2Items);
            var expected = new PowerSet<Pair>();

            for (var index = 0; index < expectedItems.Length; index += 2)
            {
                var first = expectedItems[index];
                var second = expectedItems[index + 1];
                expected.Put(new Pair(first, second));
            }

            var result = set1.CartesianProduct(set2);
            
            Assert.That(result.Equals(expected), Is.True);
        }

        #endregion
        
        private PowerSet<int> CreateSetInt(int[] items)
        {
            var set = new PowerSet<int>();
            
            foreach (int item in items)
                set.Put(item);
            
            return set;
        }
        
        private PowerSet<string> CreateSetStrings(string[] items)
        {
            var set = new PowerSet<string>();
            
            foreach (string item in items)
                set.Put(item);
            
            return set;
        }
    }
}