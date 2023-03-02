using System.Collections.Generic;
using UnityEngine;

public class EntityType: SerializableScriptableObject
{
    [Range(1,5)] public int StartWidth;
    [Range(1,5)] public int StartHeight;
    [Range(1,100)] public int StartHealth;
    public Sprite Sprite;
    public Entity EntityPrefab;
}

