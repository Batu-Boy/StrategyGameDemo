using System;
using UnityEngine;

public class InputHelper : MonoBase
{
    public static Vector3Int MouseMapPosition;
    private static Camera _mainCam;

    public override void Initialize()
    {
        base.Initialize();
        _mainCam = Camera.main;
    }
    
    public static Vector3Int GetMouseMapPosition()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 0;
        var mouseWorldPos = _mainCam.ScreenToWorldPoint(mousePos);
        Vector3Int roundInput = new Vector3Int(Mathf.RoundToInt(mouseWorldPos.x), Mathf.RoundToInt(mouseWorldPos.y));
        return roundInput;
    }
}