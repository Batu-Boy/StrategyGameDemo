using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
    private PathGrid _pathGrid;

    private BinaryHeap _openList;
    private List<PathNode> _closedList;

    public PathFinder(ref PathGrid pathGrid)
    {
        _pathGrid = pathGrid;
        _closedList = new List<PathNode>();
        _openList = new BinaryHeap(128);
    }
    
    public Path FindPath(Vector2Int startPos, Vector2Int endPos)
    {
        var startNode = _pathGrid.GetNode(startPos);
        var endNode = _pathGrid.GetNode(endPos);
        PathNode lowestHNode = startNode;
        
        _closedList.Clear();
        _openList.Clear();
        _openList.Add(startNode);

        for (int x = 0; x < _pathGrid.Width; x++)
        {
            for (int y = 0; y < _pathGrid.Height; y++)
            {
                var node = _pathGrid.GetNode(x, y);
                node.ResetValues();
            }
        }

        startNode.g = 0;
        startNode.h = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateF();

        while (_openList.Count > 0)
        {
            PathNode currentNode = _openList.ExtractMin();
            if (currentNode == endNode)
            {
               return CalculatePath(endNode);
            }

            _closedList.Add(currentNode);
            
            foreach (var neighborNode in currentNode.GetNeighbors())
            {
                if (_closedList.Contains(neighborNode)) continue;
                if(neighborNode == null) continue;
                if (!neighborNode.IsEmpty)
                {
                    _closedList.Add(neighborNode);
                    continue;
                }
                
                int tempG = currentNode.g + CalculateDistanceCost(currentNode, neighborNode);
                if (tempG < neighborNode.g)
                {
                    neighborNode.CameFromNode = currentNode;
                    neighborNode.g = tempG;
                    neighborNode.h = CalculateDistanceCost(neighborNode, endNode);
                    neighborNode.CalculateF();
                    if (neighborNode.h < lowestHNode.h)
                    {
                        lowestHNode = neighborNode;
                    }
                    _openList.Add(neighborNode) ;
                }
            }
        }
        
        return FindPath(startPos, lowestHNode.Position);
    }
     
    private Path CalculatePath(PathNode endNode)
    {
        List<Vector2Int> wayPoints = new();
        wayPoints.Add(endNode.Position);
        PathNode currentNode = endNode;

        while (currentNode.CameFromNode != null)
        {
            wayPoints.Add(currentNode.CameFromNode.Position); 
            currentNode = currentNode.CameFromNode;
        }

        //TODO change
        wayPoints.Reverse();
        wayPoints.RemoveAt(0);
        return new Path(wayPoints);
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDif = Mathf.Abs(a.x - b.x);
        int yDif = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDif - yDif);
        return Mathf.Min(xDif, yDif) * 14 + remaining * 10;
    }
}