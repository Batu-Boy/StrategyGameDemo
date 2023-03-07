using UnityEngine;

public class PathSeeker : MonoBehaviour
{
    public Path GetPath(Vector2Int from,Vector2Int targetPosition)
    {
        return GridManager.PathFinder.FindPath(from, targetPosition);
    }
}