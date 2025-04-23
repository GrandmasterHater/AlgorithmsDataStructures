using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AlgorithmsDataStructures
{
    [TestFixture]
    public class LinkedList2Tests
    {
        #region Find

        [Test]
        public void Find_EmptyList_ReturnsNull()
        {
            const int valueToAdd = 5;
            LinkedList2 list = new LinkedList2();

            Node foundNode = list.Find(valueToAdd);
            
            Assert.That(foundNode, Is.Null);
        }
        
        [Test]
        public void Find_ExistRequiredValue_ReturnExpectedNodeWithRequiredValue()
        {
            const int valueToAdd = 5;
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(valueToAdd));
            
            Node foundNode = list.Find(valueToAdd);
            
            Assert.That(foundNode, Is.Not.Null);
            Assert.That(foundNode.value, Is.EqualTo(valueToAdd));
        }

        #endregion
        
        #region FindAll
        
        [Test]
        public void FindAll_ListNotExistRequiredValue_ReturnsEmptyList()
        {
            const int valueToAdd = 5;
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(valueToAdd + 1));

            List<Node> foundNode = list.FindAll(valueToAdd);
            
            Assert.That(foundNode, Is.Not.Null.And.Empty);
        }
        
        [Test]
        public void FindAll_ExistRequiredValues_ReturnExpectedNodeWithRequiredValue()
        {
            const int valueToAdd = 5;
            const int itemsCount = 3;
            LinkedList2 list = TestUtils.CreateFilledLinkedList2(3);

            for (int i = 0; i < itemsCount; i++)
            {
                list.AddInTail(new Node(valueToAdd));
            }
            
            List<int> foundValues = list.FindAll(valueToAdd)?.Select(node => node.value).ToList();

            Assert.That(foundValues, Is.Not.Null.And.Count.EqualTo(itemsCount).And.All.EqualTo(valueToAdd));
        }

        #endregion

        #region Remove

        [Test]
        public void Remove_ListDoesNotExistValue_ReturnFalse()
        {
            const int valueToRemove = 5;
            LinkedList2 list = new LinkedList2();

            bool result = list.Remove(valueToRemove);
            
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void Remove_ListExistValue_ReturnTrue()
        {
            const int valueToRemove = 5;
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(valueToRemove));

            bool result = list.Remove(valueToRemove);
            
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void Remove_NodeWithValueWasHead_HeadBecameValueFromNextNode()
        {
            const int valueToRemove = 5;
            Node expectedHead = new Node(7);
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(valueToRemove));
            list.AddInTail(expectedHead);

            list.Remove(valueToRemove);
            
            Assert.That(list.head?.value, Is.EqualTo(expectedHead?.value));
        }
        
        [Test]
        public void Remove_NodeWithValueWasTail_TailBecameValueFromPrevNode()
        {
            const int valueToRemove = 5;
            Node expectedTail = new Node(7);
            LinkedList2 list = new LinkedList2();
            list.AddInTail(expectedTail);
            list.AddInTail(new Node(valueToRemove));

            list.Remove(valueToRemove);
            
            Assert.That(list.tail?.value, Is.EqualTo(expectedTail?.value));
        }
        
        [Test]
        public void Remove_RemoveNodeOnListWithSeveralValues_PrevNodePointToNextNode()
        {
            const int valueToRemove = 2;
            
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(1));
            list.AddInTail(new Node(valueToRemove));
            list.AddInTail(new Node(5));

            list.Remove(valueToRemove);
            
            Assert.That(list.head.next, Is.EqualTo(list.tail));
        }
        
        [Test]
        public void Remove_RemoveNodeOnListWithSeveralValues_NextNodePointToPrevNode()
        {
            const int valueToRemove = 2;
            
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(1));
            list.AddInTail(new Node(valueToRemove));
            list.AddInTail(new Node(5));

            list.Remove(valueToRemove);
            
            
            Assert.That(list.tail.prev, Is.EqualTo(list.head));
        }
        
        [Test]
        public void Remove_HasSameValuesOnList_DeleteFirst()
        {
            const int valueToRemove = 2;
            
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(valueToRemove));
            list.AddInTail(new Node(valueToRemove));

            list.Remove(valueToRemove);
            
            Assert.That(list.tail.value, Is.EqualTo(valueToRemove));
        }

        #endregion

        #region RemoveAll

        [Test]
        public void RemoveAll_NodeWithValueWasHead_HeadBecameValueFromNextNode()
        {
            const int valueToRemove = 5;
            Node expectedHead = new Node(7);
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(valueToRemove));
            list.AddInTail(expectedHead);

            list.RemoveAll(valueToRemove);
            
            Assert.That(list.head?.value, Is.EqualTo(expectedHead?.value));
        }
        
        [Test]
        public void RemoveAll_NodeWithValueWasTail_TailBecameValueFromPrevNode()
        {
            const int valueToRemove = 5;
            Node expectedTail = new Node(7);
            LinkedList2 list = new LinkedList2();
            list.AddInTail(expectedTail);
            list.AddInTail(new Node(valueToRemove));

            list.RemoveAll(valueToRemove);
            
            Assert.That(list.tail?.value, Is.EqualTo(expectedTail?.value));
        }
        
        [Test]
        public void RemoveAll_RemoveNodeOnListWithSeveralValues_PrevNodePointsToNextNode()
        {
            const int valueToRemove = 2;
            
            LinkedList2 list = new LinkedList2();
            list.AddInTail(new Node(1));
            list.AddInTail(new Node(valueToRemove));
            list.AddInTail(new Node(5));

            list.RemoveAll(valueToRemove);
            
            Assert.That(list.head.next, Is.EqualTo(list.tail));
        }
        
        [Test]
        public void RemoveAll_RemoveNodeOnListWithSeveralValues_DeleteOnlyAllRequestedValues()
        {
            const int valueToRemove = 2;
            Node head = new Node(1);
            Node tail = new Node(5);
            
            LinkedList2 list = new LinkedList2();
            list.AddInTail(head);
            list.AddInTail(new Node(valueToRemove));
            list.AddInTail(new Node(valueToRemove));
            list.AddInTail(new Node(valueToRemove));
            list.AddInTail(tail);

            list.RemoveAll(valueToRemove);
            List<Node> foundNotDeletedNodes = list.FindAll(valueToRemove);
            
            Assert.That(foundNotDeletedNodes, Is.Empty);
            Assert.That(list.head?.value, Is.EqualTo(head?.value));
            Assert.That(list.tail?.value, Is.EqualTo(tail?.value));
        }

        #endregion
        
        #region Count
        
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void Count_SeveralListSize_ExpectedCount(int requiredCount)
        {
            int expectedCount = requiredCount;

            LinkedList2 list = TestUtils.CreateFilledLinkedList2(requiredCount);
            
            int count = list.Count();

            Assert.That(count, Is.EqualTo(expectedCount));
        }
        
        #endregion
        
        #region Clear

        [Test]
        public void Clear_CleanLinkedListWithSeveralNodes_HeadAndTailIsEmpty()
        {
            LinkedList2 list = TestUtils.CreateFilledLinkedList2(5);
            
            list.Clear();
            
            Assert.That(list.head, Is.Null);
            Assert.That(list.tail, Is.Null);
        }

        #endregion
        
        #region InsertAfterMethodTests

        [Test]
        public void InsertAfter_WhenNodeAfterNull_NodeBecameHead()
        {
            LinkedList2 list = TestUtils.CreateFilledLinkedList2(3);
            Node oldHead = list.head;
            Node newNode = new Node(15);
            
            list.InsertAfter(null, newNode);
            
            Assert.That(list.head, Is.EqualTo(newNode));
            Assert.That(list.head.next, Is.EqualTo(oldHead));
        }
        
        [Test]
        public void InsertAfter_WhenNodeAfterNull_OldHeadPointToNewHeadOnPrev()
        {
            LinkedList2 list = TestUtils.CreateFilledLinkedList2(3);
            Node newNode = new Node(15);
            
            list.InsertAfter(null, newNode);
            
            Assert.That(list.head.next.prev, Is.EqualTo(newNode));
        }
        
        [Test]
        public void InsertAfter_InsertNewNodeOnMiddle_UpdateAllPoints()
        {
            LinkedList2 list = new LinkedList2();
            Node nodeAfter = new Node(0);
            Node newNode = new Node(15);
            Node nodeNext = new Node(1);
            list.AddInTail(nodeAfter);
            list.AddInTail(nodeNext);
            
            list.InsertAfter(nodeAfter, newNode);
            
            Assert.That(nodeAfter.next, Is.EqualTo(newNode));
            Assert.That(newNode.prev, Is.EqualTo(nodeAfter));
            Assert.That(newNode.next, Is.EqualTo(nodeNext));
            Assert.That(nodeNext.prev, Is.EqualTo(newNode));
        }

        [Test]
        public void InsertAfter_WhenNodeAfterIsTail_NewNodeBecameTail()
        {
            LinkedList2 list = TestUtils.CreateFilledLinkedList2(3);
            Node nodeAfter = list.tail;
            Node newNode = new Node(15);
            
            list.InsertAfter(nodeAfter, newNode);
            
            Assert.That(list.tail, Is.EqualTo(newNode));
        }

        #endregion
    }
}