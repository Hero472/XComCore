using System;
using System.Collections.Generic;

namespace XComCore.Collections
{
    public sealed class PriorityQueue<T>
    {
        private readonly List<Node> _heap = new List<Node>();

        public int Count => _heap.Count;

        public void Enqueue(T item, int priority)
        {
            _heap.Add(new Node(item, priority));
            HeapifyUp(_heap.Count - 1);
        }

        public Node Dequeue()
        {
            if (_heap.Count == 0)
                throw new InvalidOperationException("The priority queue is empty.");

            Node node = _heap[0];

            int last = _heap.Count - 1;
            _heap[0] = _heap[last];
            _heap.RemoveAt(last);

            if (_heap.Count > 0)
                HeapifyDown(0);

            return node;
        }

        public Node Peek()
        {
            if (_heap.Count == 0)
                throw new InvalidOperationException("The priority queue is empty.");

            return _heap[0];
        }

        public void Clear()
        {
            _heap.Clear();
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;

                if (_heap[parent].Priority <= _heap[index].Priority)
                    break;

                Swap(parent, index);
                index = parent;
            }
        }

        private void HeapifyDown(int index)
        {
            while (true)
            {
                int left = index * 2 + 1;
                int right = left + 1;
                int smallest = index;

                if (left < _heap.Count &&
                    _heap[left].Priority < _heap[smallest].Priority)
                {
                    smallest = left;
                }

                if (right < _heap.Count &&
                    _heap[right].Priority < _heap[smallest].Priority)
                {
                    smallest = right;
                }

                if (smallest == index)
                    break;

                Swap(index, smallest);
                index = smallest;
            }
        }

        private void Swap(int a, int b)
        {
            Node temp = _heap[a];
            _heap[a] = _heap[b];
            _heap[b] = temp;
        }

        public readonly struct Node
        {
            public T Item { get; }
            public int Priority { get; }

            public Node(T item, int priority)
            {
                Item = item;
                Priority = priority;
            }
        }
    }
}