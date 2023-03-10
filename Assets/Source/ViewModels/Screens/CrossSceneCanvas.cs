using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossSceneCanvas : MonoSingleton<CrossSceneCanvas>, ICrossSceneObject
{
    public override void Initialize()
    {
        destroyGameObjectOnDuplicate = true;
        base.Initialize();
        if(destroyed) return;
        HandleDontDestroy();
    }

    public void HandleDontDestroy()
    {
        DontDestroyOnLoad(gameObject);
    }
}
