using UnityEngine;

public class InputController : ControllerBase
{
    [SerializeField] private Camera mainCam;
    
    public override void Initialize()
    {
        base.Initialize();
        mainCam = Camera.main;
    }
    
    public override void ControllerUpdate(GameStates currentState)
    {
        if (currentState != GameStates.Game) return;

        if (!Input.GetMouseButtonDown(0)) return;
        
        var mousePos = Input.mousePosition;
        mousePos.z = 0;
        var mouseWorldPos = mainCam.ScreenToWorldPoint(mousePos);
        Vector2Int floorInput = new Vector2Int(Mathf.FloorToInt(mouseWorldPos.x), Mathf.FloorToInt(mouseWorldPos.y));
    }
}