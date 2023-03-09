using UnityEngine;

public static class ConditionHelper
{
    public static void CheckEnd()
    {
        var entities = RegistryManager.RegisteredEntities;
        if (entities.Count <= 0) return;

        Entity firstEntity = entities[0];
        
        foreach (var registeredEntity in entities)
        {
            if (registeredEntity.Team != firstEntity.Team)
            {
                return;
            }
        }
        Debug.LogWarning("TEST!");
        GameController.Instance.EndState(firstEntity.Team);
    }
}