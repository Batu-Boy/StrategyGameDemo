using UnityEngine;

public static class ConstantValues
{
    public const int NotInHeap = int.MaxValue;
    public const int MAXROWS = 20;
    public const int MINROWS = 10;
    public const int MAXCOLUMNS = 20;
    public const int MINCOLUMNS = 10;
}

public static class Directions
{
    public static readonly Vector2Int Down = new (0, -1);
    public static readonly Vector2Int DownLeft = new (-1, -1);
    public static readonly Vector2Int Left = new (-1, 0);
    public static readonly Vector2Int UpLeft = new (1, -1);
    public static readonly Vector2Int Up = new (0, 1);
    public static readonly Vector2Int UpRight = new (1, 1);
    public static readonly Vector2Int Right = new (1, 0);
    public static readonly Vector2Int DownRight = new (1, -1);
}