using System;
using System.Collections;

namespace CubeSatCommSim.Model
{
    //Modified from https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Queues/PriorityQueue.cs
    public class PriorityQueue<T> : IEnumerable where T : IComparable
    {
        private readonly BHeap<T> heap;

        public int Count
        {
            get { return heap.Count; }
        }

        public PriorityQueue()
        {
            heap = new BHeap<T>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return heap.GetEnumerator();
        }


        public void Enqueue(T item)
        {
            heap.Insert(item);
        }

        public T Dequeue()
        {
            return heap.Extract();
        }

        public T Peek()
        {
            return heap.Peek();
        }
    }
}
