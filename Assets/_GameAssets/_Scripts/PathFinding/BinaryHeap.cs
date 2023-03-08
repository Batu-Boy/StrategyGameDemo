using System;
using UnityEngine;

/// <summary>
/// A min-type priority queue of Nodes
/// </summary>
public class BinaryHeap
{
    #region Instance variables

    HeapNode[] heap;
    int count;

    #endregion
    
    private struct HeapNode
    {
        public PathNode node;
        public int F;

        public HeapNode(int f, PathNode node)
        {
            this.F = f;
            this.node = node;
        }
    }

    /// <summary>
    /// Creates a new, empty priority queue with the specified capacity.
    /// </summary>
    /// <param name="capacity">The maximum number of nodes that will be stored in the queue.</param>
    public BinaryHeap(int capacity)
    {
        heap = new HeapNode[capacity];
        count = 0;
    }

    public bool Contains(PathNode node)
    {
        return node.heapIndex != ConstantValues.NotInHeap;
    }

    /// <summary>
    /// Adds an item to the queue.  Is position is determined by its priority relative to the other items in the queue.
    /// aka HeapInsert
    /// </summary>
    /// <param name="item">Item to add</param>
    /// <param name="priority">Priority value to attach to this item.  Note: this is a min heap, so lower priority values come out first.</param>
    public void Add(PathNode node)
    {
        if (node.heapIndex != ConstantValues.NotInHeap)
        {
            //MoveUp(node.heapIndex);
            return;
        }
        if (count >= heap.Length)
            Expand();

        var item = new HeapNode(node.f, node);
        // Add the item to the heap in the end position of the array (i.e. as a leaf of the tree)
        int position = count;
        count++;
        heap[position] = item;
        heap[position].node.heapIndex = position;
        // Move it upward into position, if necessary
        MoveUp(position);
    }
    
    /// <summary>Expands to a larger backing array when the current one is too small</summary>
    void Expand () {
        // 65533 == 1 mod 4 and slightly smaller than 1<<16 = 65536
        int newSize = heap.Length + 16;
        var newHeap = new HeapNode[newSize];
        heap.CopyTo(newHeap, 0);
        heap = newHeap;
    }

    /// <summary>
    /// Extracts the item in the queue with the minimal priority value.
    /// </summary>
    /// <returns></returns>
    public PathNode ExtractMin() // Probably THE most important function... Got everything working
    {
        PathNode minNode = heap[0].node;
        Swap(0, count - 1);
        count--;
        MoveDown(0);
        minNode.heapIndex = ConstantValues.NotInHeap;
        return minNode;
    }

    /// <summary>
    /// Moves the node at the specified position upward, it it violates the Heap Property.
    /// This is the while loop from the HeapInsert procedure in the slides.
    /// </summary>
    /// <param name="position"></param>
    void MoveUp(int position)
    {
        while ((position > 0) && (heap[Parent(position)].F > heap[position].F))
        {
            int original_parent_pos = Parent(position);
            Swap(position, original_parent_pos);
            position = original_parent_pos;
        }
    }

    /// <summary>
    /// Moves the node at the specified position down, if it violates the Heap Property
    /// aka Heapify
    /// </summary>
    /// <param name="position"></param>
    void MoveDown(int position)
    {
        int lchild = LeftChild(position);
        int rchild = RightChild(position);
        int largest = 0;
        if ((lchild < count) && (heap[lchild].F < heap[position].F))
        {
            largest = lchild;
        }
        else
        {
            largest = position;
        }

        if ((rchild < count) && (heap[rchild].F < heap[largest].F))
        {
            largest = rchild;
        }

        if (largest != position)
        {
            Swap(position, largest);
            MoveDown(largest);
        }
    }

    /// <summary>
    /// Number of items waiting in queue
    /// </summary>
    public int Count
    {
        get { return count; }
    }

    #region Utilities

    /// <summary>
    /// Swaps the nodes at the respective positions in the heap
    /// Updates the nodes' QueuePosition properties accordingly.
    /// </summary>
    void Swap(int position1, int position2)
    {
        (heap[position1].node.heapIndex, heap[position2].node.heapIndex) =
            (heap[position2].node.heapIndex, heap[position1].node.heapIndex);
        
        (heap[position1], heap[position2]) = (heap[position2], heap[position1]);
    }

    /// <summary>
    /// Gives the position of a node's parent, the node's position in the queue.
    /// </summary>
    static int Parent(int position)
    {
        return (position - 1) / 2;
    }

    /// <summary>
    /// Returns the position of a node's left child, given the node's position.
    /// </summary>
    static int LeftChild(int position)
    {
        return 2 * position + 1;
    }
    
    /// <summary>
    /// Returns the position of a node's right child, given the node's position.
    /// </summary>
    static int RightChild(int position)
    {
        return 2 * position + 2;
    }
    
    public void Clear ()
    {
        for (int i = 0; i < count; i++) {
            heap[i].node.heapIndex = ConstantValues.NotInHeap;
        }
        count = 0;
    }
    
    #endregion
}