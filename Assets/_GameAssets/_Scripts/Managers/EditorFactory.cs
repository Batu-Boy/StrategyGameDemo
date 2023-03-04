using UnityEngine;

public class EditorFactory : MonoBehaviour
{
    [EditorButton]
    public void CreateEntity(EntityType type, Vector2Int position)
    {
        var unit = EntityFactory.CreateEntity<Entity>(type, position);
    }
}
