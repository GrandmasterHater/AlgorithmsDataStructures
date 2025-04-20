using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public static class LinkedListUtils
    {
        // Exercise 1, task 8, time complexity O(n), space complexity O(n)
        public static List<int> GetLinkedListsItemsSum(LinkedList firstList, LinkedList secondList)
        {
            List<int> result = new List<int>();
            
            if (firstList.Count() == secondList.Count())
            {
                Node firstListNode = firstList.head;
                Node secondListNode = secondList.head;
                int sum = 0;
                
                while (firstListNode != null)
                {
                    sum = firstListNode.value + secondListNode.value;
                    
                    result.Add(sum);

                    firstListNode = firstListNode.next;
                    secondListNode = secondListNode.next;
                }
            }

            return result;
        }
    }
}