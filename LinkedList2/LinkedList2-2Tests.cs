using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace AlgorithmsDataStructures
{
    [TestFixture]
    public class LinkedList2_2Tests
    {
        [Test]
        public void ReverseList_ListReverse()
        {
            LinkedList2 list = TestUtils.CreateFilledLinkedList2(5);
            Node expectedHead = list.tail;
            Node expectedTail = list.head;
            
            LinkedListTask2_2.ReverseList(list);

            List<int> result = new List<int>();

            for (Node node = list.head; node != null; node = node.next)
            {
                result.Add(node.value);
            }
            
            Assert.That(result, Is.Ordered.Descending);
            Assert.That(list.head, Is.EqualTo(expectedHead));
            Assert.That(list.tail, Is.EqualTo(expectedTail));
        }
        
        [Test]
        public void HasCycle_True()
        {
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(1));
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));
            list.AddInTail(new Node(5));

            list.tail.next = list.head;
            
            bool hasCycle = LinkedListTask2_2.HasCycle(list);

            Assert.That(hasCycle, Is.True);
        }
        
        [Test]
        public void HasCycle_False()
        {
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(1));
            list.AddInTail(new Node(2));
            list.AddInTail(new Node(3));
            list.AddInTail(new Node(5));
            
            bool hasCycle = LinkedListTask2_2.HasCycle(list);

            Assert.That(hasCycle, Is.False);
        }
        
        public static IEnumerable SortTestCases()
        {
            yield return new TestCaseData(new []{4, 2, 1, 3}).Returns(new []{1, 2, 3, 4}); 
            yield return new TestCaseData(new []{5, 2, 1, 3, 4}).Returns(new []{1, 2, 3, 4, 5}); 
        }
        
        [TestCaseSource(nameof(SortTestCases))]
        public int[] Sort_SeveralRanges_ExpectedValues(int [] notSortedRange)
        {
            LinkedList2 list = new LinkedList2();

            foreach (int number in notSortedRange)
            {
                list.AddInTail(new Node(number));
            }

            LinkedListTask2_2.Sort(list);
            
            List<int> sortedRange = new List<int>();
            
            for (Node node = list.head; node != null; node = node.next)
            {
                sortedRange.Add(node.value);
            }

            return sortedRange.ToArray();
        }
        
        public static IEnumerable UnionTestCases()
        {
            yield return new TestCaseData(new []{4, 2, 1, 3}, new []{5, 9, 6, 3}).Returns(new []{1, 2, 3, 3, 4, 5, 6, 9});
        }

        [TestCaseSource(nameof(UnionTestCases))]
        public int[] Union_SeveralRanges_ExpectedValues(int[] notSortedRange1, int[] notSortedRange2)
        {
            LinkedList2 list1 = new LinkedList2();
            LinkedList2 list2 = new LinkedList2();

            foreach (int number in notSortedRange1)
            {
                list1.AddInTail(new Node(number));
            }
            
            foreach (int number in notSortedRange2)
            {
                list2.AddInTail(new Node(number));
            }

            LinkedList2 resultList = LinkedListTask2_2.Union(list1, list2);
            
            List<int> sortedRange = new List<int>();
            
            for (Node node = resultList.head; node != null; node = node.next)
            {
                sortedRange.Add(node.value);
            }

            return sortedRange.ToArray();
        }
        
        [Test]
        public void Remove_ListDoesNotExistValue_ReturnFalse()
        {
            const int valueToRemove = 5;
            LinkedList2WithDummy list = new LinkedList2WithDummy();

            bool result = list.Remove(valueToRemove);
            
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void Remove_ListExistValue_ReturnTrue()
        {
            const int valueToRemove = 5;
            LinkedList2WithDummy list = new LinkedList2WithDummy();
            list.AddInTail(new Node(valueToRemove));

            bool result = list.Remove(valueToRemove);
            
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void Remove_NodeWithValueWasHead_HeadBecameValueFromNextNode()
        {
            const int valueToRemove = 5;
            Node expectedHead = new Node(7);
            LinkedList2WithDummy list = new LinkedList2WithDummy();
            list.AddInTail(new Node(valueToRemove));
            list.AddInTail(expectedHead);

            list.Remove(valueToRemove);
            
            Assert.That(list.Head?.value, Is.EqualTo(expectedHead?.value));
        }
        
        [Test]
        public void Remove_NodeWithValueWasTail_TailBecameValueFromPrevNode()
        {
            const int valueToRemove = 5;
            Node expectedTail = new Node(7);
            LinkedList2WithDummy list = new LinkedList2WithDummy();
            list.AddInTail(expectedTail);
            list.AddInTail(new Node(valueToRemove));

            list.Remove(valueToRemove);
            
            Assert.That(list.Tail?.value, Is.EqualTo(expectedTail?.value));
        }
        
        [Test]
        public void Remove_RemoveNodeOnListWithSeveralValues_PrevNodePointToNextNode()
        {
            const int valueToRemove = 2;
            
            LinkedList2WithDummy list = new LinkedList2WithDummy();
            list.AddInTail(new Node(1));
            list.AddInTail(new Node(valueToRemove));
            list.AddInTail(new Node(5));

            list.Remove(valueToRemove);
            
            Assert.That(list.Head.next, Is.EqualTo(list.Tail));
        }
        
        [Test]
        public void Remove_RemoveNodeOnListWithSeveralValues_NextNodePointToPrevNode()
        {
            const int valueToRemove = 2;
            
            LinkedList2WithDummy list = new LinkedList2WithDummy();
            list.AddInTail(new Node(1));
            list.AddInTail(new Node(valueToRemove));
            list.AddInTail(new Node(5));

            list.Remove(valueToRemove);
            
            
            Assert.That(list.Tail.prev, Is.EqualTo(list.Head));
        }
        
        [Test]
        public void Remove_HasSameValuesOnList_DeleteFirst()
        {
            const int valueToRemove = 2;
            
            LinkedList2WithDummy list = new LinkedList2WithDummy();
            list.AddInTail(new Node(valueToRemove));
            list.AddInTail(new Node(valueToRemove));

            list.Remove(valueToRemove);
            
            Assert.That(list.Tail.value, Is.EqualTo(valueToRemove));
        }
    }
}