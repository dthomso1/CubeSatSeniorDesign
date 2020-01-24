using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CubeSatCommSim.Model
{
    //Modified from https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Heap/BHeap.cs
    public class BHeap<T> : IEnumerable<T> where T : IComparable
    {
        private T[] heapArray;

        public int Count { get; private set; }

        public BHeap()
        {
            heapArray = new T[2];
        }
        
        public void Insert(T newItem)
        {
            if (Count == heapArray.Length)
            {
                doubleArray();
            }

            heapArray[Count] = newItem;

            for (int i = Count; i > 0; i = (i - 1) / 2)
            {
                if (heapArray[i].CompareTo(heapArray[(i - 1) / 2]) < 0)
                {
                    var temp = heapArray[(i - 1) / 2];
                    heapArray[(i - 1) / 2] = heapArray[i];
                    heapArray[i] = temp;
                }
                else
                {
                    break;
                }
            }

            Count++;
        }
        
        public T Extract()
        {
            if (Count == 0)
            {
                throw new Exception("Empty heap");
            }

            var minMax = heapArray[0];

            delete(0);

            return minMax;
        }
        
        public T Peek()
        {
            if (Count == 0)
            {
                throw new Exception("Empty heap");
            }

            return heapArray[0];
        }
        
        public void Delete(T value)
        {
            var index = findIndex(value);

            if (index != -1)
            {
                delete(index);
                return;
            }

            throw new Exception("Item not found.");

        }
        
        public bool Exists(T value)
        {
            return findIndex(value) != -1;
        }

        private void delete(int parentIndex)
        {
            heapArray[parentIndex] = heapArray[Count - 1];
            Count--;

            //percolate down
            while (true)
            {
                var leftIndex = 2 * parentIndex + 1;
                var rightIndex = 2 * parentIndex + 2;

                var parent = heapArray[parentIndex];

                if (leftIndex < Count && rightIndex < Count)
                {
                    var leftChild = heapArray[leftIndex];
                    var rightChild = heapArray[rightIndex];

                    var leftIsMinMax = false;

                    if (leftChild.CompareTo(rightChild) < 0)
                    {
                        leftIsMinMax = true;
                    }

                    var minMaxChildIndex = leftIsMinMax ? leftIndex : rightIndex;

                    if (heapArray[minMaxChildIndex].CompareTo(parent) < 0)
                    {
                        var temp = heapArray[parentIndex];
                        heapArray[parentIndex] = heapArray[minMaxChildIndex];
                        heapArray[minMaxChildIndex] = temp;

                        if (leftIsMinMax)
                        {
                            parentIndex = leftIndex;
                        }
                        else
                        {
                            parentIndex = rightIndex;
                        }

                    }
                    else
                    {
                        break;
                    }
                }
                else if (leftIndex < Count)
                {
                    if (heapArray[leftIndex].CompareTo(parent) < 0)
                    {
                        var temp = heapArray[parentIndex];
                        heapArray[parentIndex] = heapArray[leftIndex];
                        heapArray[leftIndex] = temp;

                        parentIndex = leftIndex;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (rightIndex < Count)
                {
                    if (heapArray[rightIndex].CompareTo(parent) < 0)
                    {
                        var temp = heapArray[parentIndex];
                        heapArray[parentIndex] = heapArray[rightIndex];
                        heapArray[rightIndex] = temp;

                        parentIndex = rightIndex;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }

            }

            if (heapArray.Length / 2 == Count && heapArray.Length > 2)
            {
                halfArray();
            }
        }
        
        private int findIndex(T value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (heapArray[i].Equals(value))
                {
                    return i;
                }
            }
            return -1;
        }

        private void halfArray()
        {
            var smallerArray = new T[heapArray.Length / 2];

            for (int i = 0; i < Count; i++)
            {
                smallerArray[i] = heapArray[i];
            }

            heapArray = smallerArray;
        }

        private void doubleArray()
        {
            var biggerArray = new T[heapArray.Length * 2];

            for (int i = 0; i < Count; i++)
            {
                biggerArray[i] = heapArray[i];
            }

            heapArray = biggerArray;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return heapArray.Take(Count).GetEnumerator();
        }
    }
}
