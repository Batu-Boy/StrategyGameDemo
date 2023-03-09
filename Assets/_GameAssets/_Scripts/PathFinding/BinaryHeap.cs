/// <summary>
/// Classic binary heap with some tricks.
/// Like heap index is in the nodes himselfs. fastest for Contains checks and clearing the heap.
/// </summary>
public class BinaryHeap
{
    HeapNode[] heap;
    int count;
    
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
    
    public BinaryHeap(int capacity)
    {
        heap = new HeapNode[capacity];
        count = 0;
    }

    public bool Contains(PathNode node)
    {
        return node.heapIndex != ConstantValues.NotInHeap;
    }
    
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
        int position = count;
        count++;
        heap[position] = item;
        heap[position].node.heapIndex = position;
        MoveUp(position);
    }
    
    /// <summary>if heap was small for pathfinding then expand</summary>
    void Expand () {
        int newSize = heap.Length + 16;
        var newHeap = new HeapNode[newSize];
        heap.CopyTo(newHeap, 0);
        heap = newHeap;
    }

    public PathNode ExtractMin()
    {
        PathNode minNode = heap[0].node;
        Swap(0, count - 1);
        count--;
        MoveDown(0);
        minNode.heapIndex = ConstantValues.NotInHeap;
        return minNode;
    }

    /// <summary>
    /// Classic binary heap moving least element to the top.
    /// </summary>
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
    /// Classic downing an element with big value
    /// </summary>
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
    
    public int Count
    {
        get { return count; }
    }
    
    /// <summary>
    /// Changes also heap indexes from nodes. 
    /// </summary>
    void Swap(int position1, int position2)
    {
        (heap[position1].node.heapIndex, heap[position2].node.heapIndex) =
            (heap[position2].node.heapIndex, heap[position1].node.heapIndex);
        
        (heap[position1], heap[position2]) = (heap[position2], heap[position1]);
    }
    
    static int Parent(int position)
    {
        return (position - 1) / 2;
    }

    static int LeftChild(int position)
    {
        return 2 * position + 1;
    }
    
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
}