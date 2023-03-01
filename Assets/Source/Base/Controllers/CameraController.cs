using UnityEngine;
using Cinemachine;

//TODO: implement camera controls
public class CameraController : ControllerBase
{
    public void SetPositionByGrid(Grid grid)
    {
        //Camera.main.transform.position =
            //new Vector3(grid.Width / 2f, grid.Height / 2f, Camera.main.transform.position.z);
        
        //Camera.main.orthographicSize = Mathf.Max(grid.Width, grid.Height * 3f / 4f) + 2f;
    }
}
