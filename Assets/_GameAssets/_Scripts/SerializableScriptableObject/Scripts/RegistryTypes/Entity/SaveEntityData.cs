using UnityEngine;

public class SaveEntityData
{
    public Guid[] EntityGuids;
    public Vector2Int[] EntityPositions;
    public int[] EntityHealths;
    
    public void SetEntities(Entity[] entities)
    {
        EntityGuids = new Guid[entities.Length];
        EntityPositions = new Vector2Int[entities.Length];
        EntityHealths = new int[entities.Length];
        for (int i = 0; i < entities.Length; ++i)
        {
            EntityGuids[i] = entities[i].Type.Guid;
            EntityPositions[i] = entities[i].CurrentPosition;
            EntityHealths[i] = entities[i].Health;
        }
    }
}