using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NOT USED!.
/// for later can be used for level editor.
/// </summary>
public class EditorFactory : MonoBehaviour
{
    [SerializeField] private Transform _entityParent;
    
    private List<Entity> _createdEntities;

    [EditorButton]
    public void CreateEntity(EntityType type, Team team, Vector2Int position)
    {
        Entity entity = Instantiate(type.Prefab, _entityParent);
        entity.InitType(type, position, team);
        _createdEntities.Add(entity);
    }
}