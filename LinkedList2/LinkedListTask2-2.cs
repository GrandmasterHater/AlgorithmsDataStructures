namespace AlgorithmsDataStructures
{
    public static class LinkedListTask2_2
    {
        // Exercise 2, task 9, time complexity O(n), space complexity O(1)
        public static void ReverseList(LinkedList2 list)
        {
            Node tempNode;
            
            for (Node node = list.head; node != null; node = node.prev)
            {
                tempNode = node.prev;
                node.prev = node.next;
                node.next = tempNode;
            }

            tempNode = list.head;
            list.head = list.tail;
            list.tail = tempNode;
        }
        
        // Exercise 2, task 10, time complexity O(n), space complexity O(1), 
        public static bool HasCycle(LinkedList2 list)
        {
            Node slow = list.head;
            Node fast = list.head;

            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;

                if (slow == fast)
                    return true;
            }

            return false;
        }
        
        // Exercise 2, task 12, time complexity O(n log n), space complexity O(1)
        public static LinkedList2 Union(LinkedList2 firstList, LinkedList2 secondList)
        {
            Sort(firstList);
            Sort(secondList);

            NodeRange mergedParts = MergeParts(firstList.head, secondList.head);

            return new LinkedList2()
            {
                head = mergedParts.head,
                tail = mergedParts.tail
            };
        }

        // Exercise 2, task 11, time complexity O(n log n), space complexity O(1)
        public static void Sort(LinkedList2 list)
        {
            int count = list.Count();
            
            for (int size = 1; size < count; size *= 2)
            {
                Node currentHead = list.head;
                NodeRange sortedSequence = new NodeRange()
                {
                    head = list.head,
                    tail = list.head
                };
                
                for (int step = 0; currentHead != null; step += size * 2)
                {
                    Node secondPartHead = Split(currentHead, size);
                    Node nextPartHead = Split(secondPartHead, size);
                    
                    NodeRange mergedParts = MergeParts(currentHead, secondPartHead);
                    
                    if (step == 0)
                    {
                        sortedSequence.head = mergedParts.head;
                    }
                    else
                    {
                        sortedSequence.tail.next = mergedParts.head;
                        mergedParts.head.prev = sortedSequence.tail;
                    }
                    
                    sortedSequence.tail = mergedParts.tail;
                    currentHead = nextPartHead;
                }

                list.head = sortedSequence.head;
                list.tail = sortedSequence.tail;
            }
        }

        private static Node Split(Node head, int size)
        {
            if (head == null) return null;
            
            Node tail = head; 
            
            for (int i = 1; tail.next != null && i < size; i++, tail = tail.next) { }

            Node next = tail.next;
            tail.next = null;
            if (next != null) next.prev = null;

            return next;
        }

        private static NodeRange MergeParts(Node headFirst, Node headSecond)
        {
            Node dummy = new Node(0);
            Node current = dummy;
            
            Node currentFirst;
            Node currentSecond;

            for (currentFirst = headFirst, currentSecond = headSecond; currentFirst != null && currentSecond != null; current = current.next)
            {
                if (currentFirst.value <= currentSecond.value)
                {
                    current.next = currentFirst;
                    currentFirst.prev = current;

                    currentFirst = currentFirst.next;
                }
                else
                {
                    current.next = currentSecond;
                    currentSecond.prev = current;

                    currentSecond = currentSecond.next;
                }
            }
            
            if (currentFirst != null)
            {
                current.next = currentFirst;
                currentFirst.prev = current;
            }
            
            if (currentSecond != null)
            {
                current.next = currentSecond;
                currentSecond.prev = current;
            }

            for ( ; current.next != null; current = current.next) { }

            return new NodeRange
            {
                head = dummy.next,
                tail = current
            };
        }
    }

    public struct NodeRange
    {
        public Node head;
        public Node tail;
    }

    // Exercise 2, task 13
    public class LinkedList2WithDummy
    {
        public Node Head => head.next == tail ? null : head.next;
        public Node Tail => tail.prev == head ? null : tail.prev;
        
        private readonly Node head;
        private readonly Node tail;

        public LinkedList2WithDummy()
        {
            head = null;
            tail = null;
            head = new Node(0);
            tail = new Node(0);
            
            head.next = tail;
            tail.prev = head;
        }
        
        public void AddInTail(Node _item)
        {
            _item.prev = tail.prev;
            _item.next = tail;
            tail.prev.next = _item;
            tail.prev = _item;
        }
        
        public bool Remove(int _value)
        {
            Node node;
                
            for (node = head.next; node != tail && node.value != _value; node = node.next) { }

            bool isSuccess = node != tail;
            
            if (isSuccess)
            {
                node.prev.next = node.next;
                node.next.prev = node.prev;
            }
            
            return isSuccess; 
        }
    }
}