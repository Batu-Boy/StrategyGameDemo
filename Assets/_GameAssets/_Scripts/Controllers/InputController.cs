using UnityEngine;

public class InputController : ControllerBase
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Camera mainCam;
    
    public override void Initialize()
    {
        base.Initialize();
        mainCam = Camera.main;
    }
    
    public override void ControllerUpdate(GameStates currentState)
    {
        //if (currentState != GameStates.Game) return;
        
        //if (!Input.GetMouseButtonDown(0)) return;
        
        var mousePos = Input.mousePosition;
        mousePos.z = 0;
        var mouseWorldPos = mainCam.ScreenToWorldPoint(mousePos);
        Vector3 roundInput = new Vector3(Mathf.RoundToInt(mouseWorldPos.x), Mathf.RoundToInt(mouseWorldPos.y));

        if(_transform)
            _transform.position = roundInput;
    }
}