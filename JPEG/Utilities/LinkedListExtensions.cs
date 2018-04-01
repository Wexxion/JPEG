using System.Collections.Generic;

namespace JPEG.Utilities
{
    public static class LinkedListExtensions
    {
        public static void SortedInsert(this LinkedList<HuffmanNode> lList, HuffmanNode value)
        {
            if (lList.Count == 0)
                lList.AddFirst(value);
            else if (value.Frequency <= lList.First.Value.Frequency)
                lList.AddFirst(value);
            else
            {
                var curr = lList.First;
                while (curr != null)
                {
                    if (value.Frequency < curr.Value.Frequency)
                    {
                        lList.AddBefore(curr, value);
                        return;
                    }
                    curr = curr.Next;
                }
            }
        }

        public static HuffmanNode Dequeue(this LinkedList<HuffmanNode> lList)
        {
            var res = lList.First;
            lList.RemoveFirst();
            return res.Value;
        }
    }
}