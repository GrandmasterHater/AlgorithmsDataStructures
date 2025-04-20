using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AlgorithmsDataStructures
{
    [TestFixture]
    public class LinkedListTests
    {
        private const int VALUE_TO_REMOVE = 1;

        #region FindAllMethodTests

        [Test]
        public void FindAll_MultipleMatchingValues_ReturnsAllMatchingNodes()
        {
            LinkedList list = CreateFilledLinkedList(5);

            int nodeValue = 42;
            int nodesCount = 5;

            for (int i = 0; i < nodesCount; i++)
            {
                list.AddInTail(new Node(nodeValue));
            }

            List<int> result = list.FindAll(nodeValue).Select(node => node.value).ToList();

            Assert.That(result, Is.All.EqualTo(nodeValue));
            Assert.That(result.Count, Is.EqualTo(nodesCount));
        }

        [Test]
        public void FindAll_ValueNotInList_ReturnsEmptyList()
        {
            LinkedList list = CreateFilledLinkedList(5);
            
            List<Node> result = list.FindAll(42);

            Assert.That(result, Is.Empty);
        }

        #endregion

        #region RemoveMethodTests
        
        [Test]
        public void Remove_EmptyList_ReturnsFalse()
        {
            LinkedList list = new LinkedList();
            
            bool result = list.Remove(VALUE_TO_REMOVE);
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void Remove_NotExistedNode_ReturnsFalse()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(VALUE_TO_REMOVE + 1));
            
            bool result = list.Remove(VALUE_TO_REMOVE);
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void Remove_RemoveNodeThatWasHead_HeadWasChangedToNext()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(VALUE_TO_REMOVE));
            list.AddInTail(new Node(3));
            
            Node newHead = list.head?.next;
            
            list.Remove(VALUE_TO_REMOVE);
            
            Assert.That(list.head, Is.EqualTo(newHead));
        }
        
        [Test]
        public void Remove_RemoveNodeThatWasTail_TailWasChangedToPreviousNode()
        {
            LinkedList list = new LinkedList();
            Node previousNode = new Node(3);
            list.AddInTail(previousNode);
            list.AddInTail(new Node(VALUE_TO_REMOVE));
            
            list.Remove(VALUE_TO_REMOVE);
            Assert.That(list.tail, Is.EqualTo(previousNode));
        }
        
        [Test]
        public void Remove_RemoveExistedNode_ReturnTrue()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(VALUE_TO_REMOVE));
            
            bool result = list.Remove(VALUE_TO_REMOVE);
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void Remove_RemoveExistedNode_PreviousNodeGetNewLinkToNextNode()
        {
            LinkedList list = new LinkedList();
            Node previousNode = new Node(VALUE_TO_REMOVE + 3);
            Node nextNode = new Node(VALUE_TO_REMOVE + 8);
            list.AddInTail(previousNode);
            list.AddInTail(new Node(VALUE_TO_REMOVE));
            list.AddInTail(nextNode);
            
            list.Remove(VALUE_TO_REMOVE);
            Assert.That(previousNode.next, Is.EqualTo(nextNode));
        }
        #endregion

        #region RemoveAllMethodTests
        
        [Test]
        public void RemoveAll_ExistsSeveralNodesForRemove_AllNodesRemoved()
        {
            LinkedList list = CreateFilledLinkedList(10);

            list.InsertAfter(new Node(2), new Node(VALUE_TO_REMOVE));
            list.InsertAfter(new Node(5), new Node(VALUE_TO_REMOVE));
            list.InsertAfter(new Node(7), new Node(VALUE_TO_REMOVE));
            list.InsertAfter(new Node(9), new Node(VALUE_TO_REMOVE));
            
            list.RemoveAll(VALUE_TO_REMOVE);

            Node node = list.head;
            bool hasAnyValueToRemove = false;

            while (node != null)
            {
                hasAnyValueToRemove |= node.value == VALUE_TO_REMOVE;
                
                node = node.next;
            }
            
            Assert.That(hasAnyValueToRemove, Is.False);
        }
        
        [Test]
        public void RemoveAll_RemoveNodeThatWasHead_HeadWasChangedToNext()
        {
            LinkedList list = new LinkedList();
            list.AddInTail(new Node(VALUE_TO_REMOVE));
            list.AddInTail(new Node(3));
            
            Node newHead = list.head?.next;
            
            list.RemoveAll(VALUE_TO_REMOVE);
            
            Assert.That(list.head, Is.EqualTo(newHead));
        }
        
        [Test]
        public void RemoveAll_RemoveNodeThatWasTail_TailWasChangedToPreviousNode()
        {
            LinkedList list = new LinkedList();
            Node previousNode = new Node(3);
            list.AddInTail(previousNode);
            list.AddInTail(new Node(VALUE_TO_REMOVE));
            
            list.RemoveAll(VALUE_TO_REMOVE);
            Assert.That(list.tail, Is.EqualTo(previousNode));
        }
        
        [Test]
        public void RemoveAll_RemoveExistedNode_PreviousNodeGetNewLinkToNextNode()
        {
            LinkedList list = new LinkedList();
            Node previousNode = new Node(VALUE_TO_REMOVE + 3);
            Node nextNode = new Node(VALUE_TO_REMOVE + 8);
            list.AddInTail(previousNode);
            list.AddInTail(new Node(VALUE_TO_REMOVE));
            list.AddInTail(nextNode);
            
            list.RemoveAll(VALUE_TO_REMOVE);
            Assert.That(previousNode.next, Is.EqualTo(nextNode));
        }

        #endregion

        #region ClearMethodTests

        [TestCase(5)]
        public void Clear_CleanLinkedListWithSeveralNodes_HeadAndTailIsEmpty(int requiredCount)
        {
            LinkedList list = CreateFilledLinkedList(requiredCount);
            
            list.Clear();
            
            Assert.That(list.head, Is.Null);
            Assert.That(list.tail, Is.Null);
        }

        #endregion
        
        #region CountMethodTests
        
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void Count_SeveralListSize_ExpectedCount(int requiredCount)
        {
            int expectedCount = requiredCount;

            LinkedList list = CreateFilledLinkedList(requiredCount);
            
            int count = list.Count();

            Assert.That(count, Is.EqualTo(expectedCount));
        }
        
        #endregion

        #region InsertAfterMethodTests

        [Test]
        public void InsertAfter_WhenNodeAfterNull_NodeBecameHeadAndOldHeadBecameNext()
        {
            LinkedList list = CreateFilledLinkedList(3);
            Node oldHead = list.head;
            Node newNode = new Node(15);
            
            list.InsertAfter(null, newNode);
            
            Assert.That(list.head, Is.EqualTo(newNode));
            Assert.That(list.head.next, Is.EqualTo(oldHead));
        }
        
        [Test]
        public void InsertAfter_InsertNewNodeAfterExisted_NodeInserted()
        {
            LinkedList list = CreateFilledLinkedList(3);
            Node nodeAfter = new Node(1);
            Node newNode = new Node(15);
            
            list.InsertAfter(nodeAfter, newNode);

            Node node = list.head;
            Node prevNode = null;

            while (node != null && node != newNode)
            {
                prevNode = node;
                node = node.next;
            }
            
            Assert.That(nodeAfter.value, Is.EqualTo(prevNode.value));
            Assert.That(newNode, Is.EqualTo(node));
        }

        [Test]
        public void InsertAfter_WhenNodeAfterIsTail_NewNodeBecameTail()
        {
            LinkedList list = CreateFilledLinkedList(3);
            Node nodeAfter = new Node(list.tail.value);
            Node newNode = new Node(15);
            
            list.InsertAfter(nodeAfter, newNode);
            
            Assert.That(list.tail, Is.EqualTo(newNode));
        }

        #endregion

        private LinkedList CreateFilledLinkedList(int requiredCount)
        {
            LinkedList list = new LinkedList();
            
            IEnumerable<int> values = Enumerable.Range(0, requiredCount);

            foreach (int number in values)
            {
                list.AddInTail(new Node(number));
            }

            return list;
        }
    }
}