using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntitySaveData
{
    public Guid[] EntityGuids;
    public Vector2Int[] EntityPositions;
    public int[] EntityHealths;
    public Team[] EntityTeams;
    
    public void SetEntities(List<Entity> entities)
    {
        EntityGuids = new Guid[entities.Count];
        EntityPositions = new Vector2Int[entities.Count];
        EntityHealths = new int[entities.Count];
        EntityTeams = new Team[entities.Count];
        for (int i = 0; i < entities.Count; ++i)
        {
            EntityGuids[i] = entities[i].Type.Guid;
            EntityPositions[i] = entities[i].CurrentPosition;
            EntityHealths[i] = entities[i].Health;
            EntityTeams[i] = entities[i].Team;
        }
    }
}