using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace AlgorithmsDataStructures
{
    [TestFixture]
    public class OrderedListTask7Tests
    {
        #region Compare

        [TestCase(1, 2, ExpectedResult = -1)]
        [TestCase(5, 5, ExpectedResult = 0)]
        [TestCase(10, 3, ExpectedResult = 1)]
        public int Compare_Integers_ShouldWorkCorrectly(int a, int b)
        {
            var list = new OrderedList<int>(true);
            return Math.Sign(list.Compare(a, b));
        }

        [TestCase(1.1, 2.2, ExpectedResult = -1)]
        [TestCase(3.14, 3.14, ExpectedResult = 0)]
        [TestCase(10.5, 9.5, ExpectedResult = 1)]
        public int Compare_Doubles_ShouldWorkCorrectly(double a, double b)
        {
            var list = new OrderedList<double>(true);
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
            var list = new OrderedList<string>(true);
            return Math.Sign(list.Compare(a, b));
        }

        #endregion

        #region Add

        [TestCase(true, new int[] { }, 42, new[] { 42 })]
        [TestCase(false, new int[] { }, 42, new[] { 42 })]
        public void Add_ToEmptyList_ValueShouldBeHeadAndTail(bool ascending, int[] data, int valueToAdd, int[] expected)
        {
            var list = CreateListWithData(data, ascending);

            list.Add(valueToAdd);

            Assert.That(GetValues(list), Is.EqualTo(expected));
            Assert.That(list.head, Is.Not.Null.And.EqualTo(list.tail));
        }

        [TestCase(true, new[] { 10, 20 }, 30, new[] { 10, 20, 30 })]
        [TestCase(false, new[] { 30, 20 }, 10, new[] { 30, 20, 10 })]
        public void Add_ToEndOfList_ShouldBecomeTail(bool ascending, int[] data, int valueToAdd, int[] expected)
        {
            var list = CreateListWithData(data, ascending);

            list.Add(valueToAdd);

            Assert.That(GetValues(list), Is.EqualTo(expected));
            Assert.That(list.tail.value, Is.EqualTo(valueToAdd));
        }

        [TestCase(true, new[] { 10, 30 }, 20, new[] { 10, 20, 30 })]
        [TestCase(false, new[] { 30, 10 }, 20, new[] { 30, 20, 10 })]
        public void Add_ToMiddle_ShouldBeInsertedCorrectly(bool ascending, int[] data, int valueToAdd, int[] expected)
        {
            var list = CreateListWithData(data, ascending);

            list.Add(valueToAdd);

            Assert.That(GetValues(list), Is.EqualTo(expected));
        }

        [TestCase(true, new[] { 10, 20 }, 10, new[] { 10, 10, 20 })]
        [TestCase(false, new[] { 20, 10 }, 10, new[] { 20, 10, 10 })]
        public void Add_Duplicates_ShouldRetainOrder(bool ascending, int[] data, int valueToAdd, int[] expected)
        {
            var list = CreateListWithData(data, ascending);

            list.Add(valueToAdd);

            Assert.That(GetValues(list), Is.EqualTo(expected));
        }

        [TestCase(true, new[] { 0 }, int.MinValue, new[] { int.MinValue, 0 })]
        [TestCase(true, new[] { 0 }, int.MaxValue, new[] { 0, int.MaxValue })]
        [TestCase(false, new[] { 0 }, int.MinValue, new[] { 0, int.MinValue })]
        [TestCase(false, new[] { 0 }, int.MaxValue, new[] { int.MaxValue, 0 })]
        public void Add_ExtremeValues_ShouldBeOrderedCorrectly(bool ascending, int[] data, int valueToAdd, int[] expected)
        {
            var list = CreateListWithData(data, ascending);

            list.Add(valueToAdd);

            Assert.That(GetValues(list), Is.EqualTo(expected));
        }

        #endregion

        #region Find

        [TestCase(true)]
        [TestCase(false)]
        public void Find_EmptyList_ShouldReturnNull(bool ascending)
        {
            var list = new OrderedList<int>(ascending);
            
            var result = list.Find(42);

            Assert.That(result, Is.Null);
        }

        [TestCase(true, new[] { 10, 20, 30 }, 25)]
        [TestCase(false, new[] { 30, 20, 10 }, 25)]
        [TestCase(true, new[] { 10, 20, 30 }, 5)]
        [TestCase(false, new[] { 30, 20, 10 }, 35)]
        public void Find_ValueNotInList_ShouldReturnNull(bool ascending, int[] data, int valueToFind)
        {
            var list = CreateListWithData(data, ascending);

            var result = list.Find(valueToFind);

            Assert.That(result, Is.Null);
        }

        [TestCase(true, new[] { 10, 20, 30 }, 20)]
        [TestCase(false, new[] { 30, 20, 10 }, 20)]
        [TestCase(true, new[] { 5, 10, 15 }, 5)]
        [TestCase(false, new[] { 15, 10, 5 }, 5)]
        public void Find_ValueInList_ShouldReturnCorrectNode(bool ascending, int[] data, int valueToFind)
        {
            var list = CreateListWithData(data, ascending);

            var result = list.Find(valueToFind);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.value, Is.EqualTo(valueToFind));
        }

        #endregion

        #region Delete

        [TestCase(true, new[] { 10, 20, 30 }, 40, new[] { 10, 20, 30 })]
        [TestCase(false, new[] { 30, 20, 10 }, 5, new[] { 30, 20, 10 })]
        public void Delete_ElementNotInList_ListRemainsUnchanged(bool ascending, int[] data, int valueToDelete, int[] expected)
        {
            var list = CreateListWithData(data, ascending);

            list.Delete(valueToDelete);

            Assert.That(GetValues(list), Is.EqualTo(expected));
        }

        [TestCase(true, new[] { 10, 20, 30 }, 20, new[] { 10, 30 })]
        [TestCase(false, new[] { 30, 20, 10 }, 20, new[] { 30, 10 })]
        [TestCase(true, new[] { 10 }, 10, new int[0])]
        public void Delete_ElementExists_ElementIsRemoved(bool ascending, int[] data, int valueToDelete, int[] expected)
        {
            var list = CreateListWithData(data, ascending);

            list.Delete(valueToDelete);

            Assert.That(GetValues(list), Is.EqualTo(expected));
        }

        [TestCase(true, new[] { 10, 20, 20, 30 }, 20, new[] { 10, 20, 30 })]
        [TestCase(false, new[] { 30, 20, 20, 10 }, 20, new[] { 30, 20, 10 })]
        public void Delete_DuplicatesValuesForDelete_OnlyFirstOccurrenceIsRemoved(bool ascending, int[] data, int valueToDelete, int[] expected)
        {
            var list = CreateListWithData(data, ascending);

            list.Delete(valueToDelete);

            Assert.That(GetValues(list), Is.EqualTo(expected));
        }
        
        [TestCase(true, new[] { 10, 20, 30 }, 10, new[] { 20, 30 }, 20)]
        [TestCase(false, new[] { 30, 20, 10 }, 30, new[] { 20, 10 }, 20)]
        [TestCase(false, new[] { 30, 20 }, 20, new[] { 30 }, 30)]
        public void Delete_HeadElement_ShouldUpdateHead(bool ascending, int[] data, int toDelete, int[] expected, int expectedHead)
        {
            var list = CreateListWithData(data, ascending);

            list.Delete(toDelete);

            Assert.That(GetValues(list), Is.EqualTo(expected));
            Assert.That(list.head.value, Is.EqualTo(expectedHead));
        }

        [TestCase(true, new[] { 10, 20, 30 }, 30, new[] { 10, 20 },  20)]
        [TestCase(false, new[] { 30, 20, 10 }, 10, new[] { 30, 20 },  20)]
        [TestCase(false, new[] { 30, 20 }, 20, new[] { 30 },  30)]
        public void Delete_TailElement_ShouldUpdateTail(bool ascending, int[] data, int toDelete, int[] expected, int expectedTail)
        {
            var list = CreateListWithData(data, ascending);

            list.Delete(toDelete);

            Assert.That(GetValues(list), Is.EqualTo(expected));
            Assert.That(list.tail.value, Is.EqualTo(expectedTail));
        }

        [TestCase(true, new[] { 42 }, 42)]
        [TestCase(false, new[] { 42 }, 42)]
        public void Delete_ListHasOnlyOneElement_ListBecomesEmpty(bool ascending, int[] data, int toDelete)
        {
            var list = CreateListWithData(data, ascending);

            list.Delete(toDelete);

            Assert.That(GetValues(list), Is.Empty);
            Assert.That(list.head, Is.Null);
            Assert.That(list.tail, Is.Null);
        }

        #endregion

        #region Count

        [TestCase(true)]
        [TestCase(false)]
        public void Count_EmptyList_ShouldReturnZero(bool ascending)
        {
            var list = new OrderedList<int>(ascending);

            Assert.That(list.Count(), Is.EqualTo(0));
        }

        [TestCase(true, new[] { 1, 2, 3 })]
        [TestCase(false, new[] { 10, 5, 0 })]
        public void Count_NonEmptyList_ShouldReturnCorrectCount(bool ascending, int[] items)
        {
            var list = new OrderedList<int>(ascending);
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
            var list = new OrderedList<int>(ascending);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            list.Clear(ascending); // Очищаем, не меняя направление

            Assert.That(list.Count(), Is.EqualTo(0));
            Assert.That(list.head, Is.Null);
            Assert.That(list.tail, Is.Null);
        }
        
        [TestCase(true, false, new[] { 1, 2, 3 }, new[] { 3, 2, 1 })]
        [TestCase(false, true, new[] { 3, 2, 1 }, new[] { 1, 2, 3 })]
        public void Clear_ChangesSortingOrder_ValuesAddWithNewOrder(bool initialAscending, bool newAscending, int[] data, int[] expected)
        {
            var list = CreateListWithData(data, initialAscending);
            
            list.Clear(newAscending);

            foreach (var val in data)
                list.Add(val);

            Assert.That(GetValues(list), Is.EqualTo(expected));
        }

        #endregion
        
        
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
    }
}