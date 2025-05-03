using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace AlgorithmsDataStructures
{
    [TestFixture]
    public class OrderedListTask7_2Tests
    {
        [TestCase(new int[] { 1, 1, 2, 2, 3, 4, 4 }, new int[] { 1, 2, 3, 4 }, true)]
        [TestCase(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 }, true)]
        [TestCase(new int[] { 1, 1, 1, 1 }, new int[] { 1 }, true)]
        [TestCase(new int[] { }, new int[] { }, true)]
        [TestCase(new int[] { 5, 5, 4, 4, 3, 3 }, new int[] { 5, 4, 3 }, false)]
        [TestCase(new int[] { 6, 6, 5, 5, 5, 5, 4, 3, 3, 2, 1 }, new int[] {6, 5, 4, 3, 2, 1 }, false)]
        public void DistinctTest(int[] data, int[] expectedResult, bool ascending)
        {
            var list = new OrderedList<int>(ascending);

            foreach (int i in data)
                list.Add(i);

            list.Distinct();

            List<int> result = new List<int>();
            
            for(Node<int> node = list.head; node != null; node = node.next)
            {
                result.Add(node.value);
            }
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }
        
        [TestCase(new[] { 1, 3, 5 }, new[] { 2, 4, 6 }, true, new[] { 1, 2, 3, 4, 5, 6 })]
        [TestCase(new[] { 5, 3, 1 }, new[] { 6, 4, 2 }, false, new[] { 6, 5, 4, 3, 2, 1 })]
        [TestCase(new[] { 1, 3, 5, 5 }, new[] { 2, 5, 6 }, true, new[] { 1, 2, 3, 5, 5, 5, 6 })]
        [TestCase(new[] { 5, 3, 1, 1 }, new[] { 6, 5, 2 }, false, new[] { 6, 5, 5, 3, 2, 1, 1 })]
        [TestCase(new[] { 5, 5, 3, 3 }, new[] { 5, 5, 4, 4 }, true, new[] { 3, 3, 4, 4, 5, 5, 5, 5 })]
        public void UnionTests(int[] firstValues, int[] secondValues, bool ascending, int[] expected)
        {
            OrderedList<int> first = OrderedListTask7Tests.CreateListWithData(firstValues, IsAscending(firstValues));
            OrderedList<int> second = OrderedListTask7Tests.CreateListWithData(secondValues, IsAscending(secondValues));

            OrderedList<int> union = first.Union(second, ascending);

            Assert.That(OrderedListTask7Tests.GetValues(union), Is.EqualTo(expected));
        }

        [TestCase(new []{1, 3, 4, 5, 6}, new []{3, 4, 5}, true, true)]
        [TestCase(new []{1, 3, 4, 5, 6}, new []{1, 3, 4}, true, true)]
        [TestCase(new []{1, 3, 4, 5, 6}, new []{4, 5, 6}, true, true)]
        [TestCase(new []{1, 3, 4, 4, 5, 6}, new []{4, 5, 6}, true, true)]
        [TestCase(new []{1, 3, 4, 5, 6}, new []{1, 3, 4, 5, 6}, true, true)]
        [TestCase(new []{1, 3, 4, 5, 6}, new []{2, 3, 4}, true, false)]
        [TestCase(new []{1, 3, 4, 5, 6}, new []{5, 6, 7}, true, false)]
        [TestCase(new []{1, 3, 4, 5, 6}, new []{0, 1, 3}, true, false)]
        [TestCase(new []{6, 5, 4, 3, 2}, new []{5, 4, 3}, false, true)]
        [TestCase(new []{6, 5, 4, 3, 2}, new []{7, 4, 3}, false, false)]
        public void ExistSubOrderedListTest(int[] data, int[] subListData, bool ascending, bool expectedResult)
        {
            OrderedList<int> list = OrderedListTask7Tests.CreateListWithData(data, ascending);
            OrderedList<int> subList = OrderedListTask7Tests.CreateListWithData(subListData, ascending);

            bool result = list.ExistSubOrderedList(subList);
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }
        
        [TestCase(new []{1, 3, 4, 5, 6}, true, 1)]
        [TestCase(new []{1, 1, 3, 4, 5, 6}, true, 1)]
        [TestCase(new []{1, 1, 3, 4, 4, 4, 5, 6}, true, 4)]
        [TestCase(new []{5, 5, 5, 5, 4, 4, 4, 3}, false, 5)]
        public void GetMostFrequentlyOccuredValueTest(int[] data, bool ascending, int expectedResult)
        {
            OrderedList<int> list = OrderedListTask7Tests.CreateListWithData(data, ascending);
            
            int result = list.GetMostFrequentlyOccuredValue();
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        #region OrderedListWithIndexes

        #region Compare

        [TestCase(1, 2, ExpectedResult = -1)]
        [TestCase(5, 5, ExpectedResult = 0)]
        [TestCase(10, 3, ExpectedResult = 1)]
        public int Compare_Integers_ShouldWorkCorrectly(int a, int b)
        {
            var list = new OrderedListWithIndexes<int>(true);
            return Math.Sign(list.Compare(a, b));
        }

        [TestCase(1.1, 2.2, ExpectedResult = -1)]
        [TestCase(3.14, 3.14, ExpectedResult = 0)]
        [TestCase(10.5, 9.5, ExpectedResult = 1)]
        public int Compare_Doubles_ShouldWorkCorrectly(double a, double b)
        {
            var list = new OrderedListWithIndexes<double>(true);
            return Math.Sign(list.Compare(a, b));
        }

        [TestCase(" test ", "TEST", ExpectedResult = 0)]
        [TestCase("abc", "BCD", ExpectedResult = -1)]
        [TestCase("zeta", "Alpha", ExpectedResult = 1)]
        [TestCase("   ", "", ExpectedResult = 0)]
        [TestCase("abc", " ", ExpectedResult = 1)]
        [TestCase(" ", "xyz", ExpectedResult = -1)]
        public int Compare_Strings_CompleteWithExpectedResult(string a, string b)
        {
            var list = new OrderedListWithIndexes<string>(true);
            return Math.Sign(list.Compare(a, b));
        }

        #endregion

        #region Add

        [TestCase(true, new int[] { }, 42, new[] { 42 })]
        [TestCase(false, new int[] { }, 42, new[] { 42 })]
        public void Add_ToEmptyList_ValueShouldBeHeadAndTail(bool ascending, int[] data, int valueToAdd, int[] expected)
        {
            var list = CreateListWithDataWithIndexes(data, ascending);

            list.Add(valueToAdd);

            Assert.That(GetValues(list), Is.EqualTo(expected));
        }

        [TestCase(true, new[] { 10, 20 }, 30, new[] { 10, 20, 30 })]
        [TestCase(false, new[] { 30, 20 }, 10, new[] { 30, 20, 10 })]
        public void Add_ToEndOfList_ShouldBecomeTail(bool ascending, int[] data, int valueToAdd, int[] expected)
        {
            var list = CreateListWithDataWithIndexes(data, ascending);

            list.Add(valueToAdd);

            Assert.That(GetValues(list), Is.EqualTo(expected));
        }

        [TestCase(true, new[] { 10, 30 }, 20, new[] { 10, 20, 30 })]
        [TestCase(false, new[] { 30, 10 }, 20, new[] { 30, 20, 10 })]
        public void Add_ToMiddle_ShouldBeInsertedCorrectly(bool ascending, int[] data, int valueToAdd, int[] expected)
        {
            var list = CreateListWithDataWithIndexes(data, ascending);

            list.Add(valueToAdd);

            Assert.That(GetValues(list), Is.EqualTo(expected));
        }

        [TestCase(true, new[] { 10, 20 }, 10, new[] { 10, 10, 20 })]
        [TestCase(false, new[] { 20, 10 }, 10, new[] { 20, 10, 10 })]
        public void Add_Duplicates_ShouldRetainOrder(bool ascending, int[] data, int valueToAdd, int[] expected)
        {
            var list = CreateListWithDataWithIndexes(data, ascending);

            list.Add(valueToAdd);

            Assert.That(GetValues(list), Is.EqualTo(expected));
        }

        [TestCase(true, new[] { 0 }, int.MinValue, new[] { int.MinValue, 0 })]
        [TestCase(true, new[] { 0 }, int.MaxValue, new[] { 0, int.MaxValue })]
        [TestCase(false, new[] { 0 }, int.MinValue, new[] { 0, int.MinValue })]
        [TestCase(false, new[] { 0 }, int.MaxValue, new[] { int.MaxValue, 0 })]
        public void Add_ExtremeValues_ShouldBeOrderedCorrectly(bool ascending, int[] data, int valueToAdd, int[] expected)
        {
            var list = CreateListWithDataWithIndexes(data, ascending);

            list.Add(valueToAdd);

            Assert.That(GetValues(list), Is.EqualTo(expected));
        }

        #endregion
        
        #region Delete

        [TestCase(true, new[] { 10, 20, 30 }, 40, new[] { 10, 20, 30 })]
        [TestCase(false, new[] { 30, 20, 10 }, 5, new[] { 30, 20, 10 })]
        public void Delete_ElementNotInList_ListRemainsUnchanged(bool ascending, int[] data, int valueToDelete, int[] expected)
        {
            var list = CreateListWithDataWithIndexes(data, ascending);

            list.Delete(valueToDelete);

            Assert.That(GetValues(list), Is.EqualTo(expected));
        }

        [TestCase(true, new[] { 10, 20, 30 }, 20, new[] { 10, 30 })]
        [TestCase(false, new[] { 30, 20, 10 }, 20, new[] { 30, 10 })]
        [TestCase(true, new[] { 10 }, 10, new int[0])]
        public void Delete_ElementExists_ElementIsRemoved(bool ascending, int[] data, int valueToDelete, int[] expected)
        {
            var list = CreateListWithDataWithIndexes(data, ascending);

            list.Delete(valueToDelete);

            Assert.That(GetValues(list), Is.EqualTo(expected));
        }

        [TestCase(true, new[] { 10, 20, 20, 30 }, 20, new[] { 10, 20, 30 })]
        [TestCase(false, new[] { 30, 20, 20, 10 }, 20, new[] { 30, 20, 10 })]
        public void Delete_DuplicatesValuesForDelete_OnlyFirstOccurrenceIsRemoved(bool ascending, int[] data, int valueToDelete, int[] expected)
        {
            var list = CreateListWithDataWithIndexes(data, ascending);

            list.Delete(valueToDelete);

            Assert.That(GetValues(list), Is.EqualTo(expected));
        }
        
        [TestCase(true, new[] { 10, 20, 30 }, 10, new[] { 20, 30 }, 20)]
        [TestCase(false, new[] { 30, 20, 10 }, 30, new[] { 20, 10 }, 20)]
        [TestCase(false, new[] { 30, 20 }, 20, new[] { 30 }, 30)]
        public void Delete_HeadElement_ShouldUpdateHead(bool ascending, int[] data, int toDelete, int[] expected, int expectedHead)
        {
            var list = CreateListWithDataWithIndexes(data, ascending);

            list.Delete(toDelete);

            Assert.That(GetValues(list), Is.EqualTo(expected));
        }

        [TestCase(true, new[] { 10, 20, 30 }, 30, new[] { 10, 20 },  20)]
        [TestCase(false, new[] { 30, 20, 10 }, 10, new[] { 30, 20 },  20)]
        [TestCase(false, new[] { 30, 20 }, 20, new[] { 30 },  30)]
        public void Delete_TailElement_ShouldUpdateTail(bool ascending, int[] data, int toDelete, int[] expected, int expectedTail)
        {
            var list = CreateListWithDataWithIndexes(data, ascending);

            list.Delete(toDelete);

            Assert.That(GetValues(list), Is.EqualTo(expected));
        }

        [TestCase(true, new[] { 42 }, 42)]
        [TestCase(false, new[] { 42 }, 42)]
        public void Delete_ListHasOnlyOneElement_ListBecomesEmpty(bool ascending, int[] data, int toDelete)
        {
            var list = CreateListWithDataWithIndexes(data, ascending);

            list.Delete(toDelete);

            Assert.That(GetValues(list), Is.Empty);
        }

        #endregion

        #region Count

        [TestCase(true)]
        [TestCase(false)]
        public void Count_EmptyList_ShouldReturnZero(bool ascending)
        {
            var list = new OrderedListWithIndexes<int>(ascending);

            Assert.That(list.Count(), Is.EqualTo(0));
        }

        [TestCase(true, new[] { 1, 2, 3 })]
        [TestCase(false, new[] { 10, 5, 0 })]
        public void Count_NonEmptyList_ShouldReturnCorrectCount(bool ascending, int[] items)
        {
            var list = new OrderedListWithIndexes<int>(ascending);
            foreach (var item in items)
                list.Add(item);

            Assert.That(list.Count(), Is.EqualTo(items.Length));
        }

        #endregion


        #region Clear

        [TestCase(true)]
        [TestCase(false)]
        public void Clear_ListExistsValues_AllValesAreRemoved(bool ascending)
        {
            var list = new OrderedListWithIndexes<int>(ascending);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            list.Clear(ascending); // Очищаем, не меняя направление

            Assert.That(list.Count(), Is.EqualTo(0));
        }
        
        [TestCase(true, false, new[] { 1, 2, 3 }, new[] { 3, 2, 1 })]
        [TestCase(false, true, new[] { 3, 2, 1 }, new[] { 1, 2, 3 })]
        public void Clear_ChangesSortingOrder_ValuesAddWithNewOrder(bool initialAscending, bool newAscending, int[] data, int[] expected)
        {
            var list = CreateListWithDataWithIndexes(data, initialAscending);
            
            list.Clear(newAscending);

            foreach (var val in data)
                list.Add(val);

            Assert.That(GetValues(list), Is.EqualTo(expected));
        }

        #endregion

        #region GetIndex
        
        [TestCase(new[] { 1, 2, 3, 4, 5 }, true, 3, 2)]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, true, 6, -6)]
        [TestCase(new[] { 5, 4, 3, 2, 1 }, false, 3, 2)]
        [TestCase(new[] { 5, 4, 3, 2, 1 }, false, 0, -6)]
        [TestCase(new[] { 10, 20, 30, 40 }, true, 20, 1)]
        [TestCase(new[] { 40, 30, 20, 10 }, false, 30, 1)]
        public void GetIndex_ShouldReturnCorrectIndexOrInsertionPoint(int[] data, bool ascending, int searchValue, int expected)
        {
            var list = CreateListWithDataWithIndexes(data, ascending);

            int index = list.GetIndex(searchValue);

            Assert.That(index, Is.EqualTo(expected));
        }
        
        #endregion

        #region GetValue

        [TestCase(new[] { 10, 20, 30 }, 0, 10)]
        [TestCase(new[] { 10, 20, 30 }, 1, 20)]
        [TestCase(new[] { 10, 20, 30 }, 2, 30)]
        public void GetValue_ShouldReturnCorrectValue(int[] data, int index, int expected)
        {
            var list = CreateListWithDataWithIndexes(data, true);
            var result = list.GetValue(index);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(new[] { 10, 20, 30 }, -1)]
        [TestCase(new[] { 10, 20, 30 }, 3)]
        [TestCase(new int[0], 0)]
        public void GetValue_ShouldThrow_WhenIndexOutOfRange(int[] data, int index)
        {
            var list = CreateListWithDataWithIndexes(data, true);
            Assert.Throws<ArgumentOutOfRangeException>(() => list.GetValue(index));
        }

        #endregion
        
        #endregion
        
        public static OrderedListWithIndexes<int> CreateListWithDataWithIndexes(int[] data, bool ascending)
        {
            OrderedListWithIndexes<int> list = new OrderedListWithIndexes<int>(ascending);
            
            foreach (var number in data)
                list.Add(number);
            
            return list;
        }
        
        public static int[] GetValues(OrderedListWithIndexes<int> list)
        {
            var result = new List<int>();
            
            for (int i = 0; i < list.Count(); ++i)
                result.Add(list.GetValue(i));
            
            return result.ToArray();
        }
        
        public static OrderedList<int> CreateListWithData(int[] data, bool ascending)
        {
            OrderedList<int> list = new OrderedList<int>(ascending);
            
            foreach (var number in data)
                list.Add(number);
            
            return list;
        }
        
        public static int[] GetValues(OrderedList<int> list)
        {
            var result = new List<int>();
            for (var node = list.head; node != null; node = node.next)
                result.Add(node.value);
            return result.ToArray();
        }
        
        private static bool IsAscending(int[] array)
        {
            return array.Length < 2 || array[0] < array[1];
        }
    }
}