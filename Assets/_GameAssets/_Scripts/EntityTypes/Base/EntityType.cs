using UnityEngine;

/// <summary>
/// Core of this project. Type Object Pattern and Asset Registry Pattern.
/// Giving <see cref="Entity"/> data from scriptables and registering themselves in <see cref="Registry{T}"/>
/// <seealso cref="BuildingType"/>, <seealso cref="UnitType"/>
/// <see href="https://bronsonzgeb.com/index.php/2021/09/11/the-scriptable-object-asset-registry-pattern/"/>
/// <see href="https://bronsonzgeb.com/index.php/2021/09/17/the-type-object-pattern-with-scriptable-objects/"/>
/// </summary>
public class EntityType: SerializableScriptableObject
{
    [Range(1,5)] public int StartWidth;
    [Range(1,5)] public int StartHeight;
    [Range(1,100)] public int StartHealth;
    public Sprite Sprite;
    public Entity Prefab;
}