using UnityEngine;
/// <summary>
/// Mouse position input helper class.
/// Translates mouse pos to grid pos, or map pos.
/// </summary>
public static class InputHelper
{
    public static Vector3Int MouseMapPosition;
    private static Camera _mainCam;
    
    public static void Initialize()
    {
        _mainCam = Camera.main;
    }

    private static Vector3Int CalculateRoundPosition()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 0;
        var mouseWorldPos = _mainCam.ScreenToWorldPoint(mousePos);
        Vector3Int roundInput = new Vector3Int(Mathf.RoundToInt(mouseWorldPos.x), Mathf.RoundToInt(mouseWorldPos.y));
        return roundInput;
    }
    
    public static Vector3Int GetMouseMapPosition()
    {
        return CalculateRoundPosition();
    }

    public static Vector2Int GetMouseGridPosition()
    {
        return CalculateRoundPosition().ToGridPos();
    }

    public static Vector3 ToMapPos(this Vector2Int pos)
    {
        return new Vector3(pos.x, pos.y);
    }
    
    public static Vector2Int ToGridPos(this Vector3 pos)
    {
        return new Vector2Int((int)pos.x, (int)pos.y);
    }
    
    public static Vector2Int ToGridPos(this Vector3Int pos)
    {
        return new Vector2Int(pos.x, pos.y);
    }
}