using UnityEngine;

public class EntitySaveData
{
    public Guid[] EntityGuids;
    public Vector2Int[] EntityPositions;
    public int[] EntityHealths;
    public Team[] EntityTeams;
    
    public void SetEntities(Entity[] entities)
    {
        EntityGuids = new Guid[entities.Length];
        EntityPositions = new Vector2Int[entities.Length];
        EntityHealths = new int[entities.Length];
        EntityTeams = new Team[entities.Length];
        for (int i = 0; i < entities.Length; ++i)
        {
            EntityGuids[i] = entities[i].Type.Guid;
            EntityPositions[i] = entities[i].CurrentPosition;
            EntityHealths[i] = entities[i].Health;
            EntityTeams[i] = entities[i].Team;
        }
    }
}