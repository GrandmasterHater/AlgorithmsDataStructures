using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures
{
    public class TestUtils
    {
        public static LinkedList2 CreateFilledLinkedList2(int requiredCount)
        {
            LinkedList2 list = new LinkedList2();
            
            IEnumerable<int> values = Enumerable.Range(0, requiredCount);

            foreach (int number in values)
            {
                list.AddInTail(new Node(number));
            }

            return list;
        }
    }
}