using System.IO;
using UnityEngine;

//TODO: save version
public class RegistryManager : MonoBase
{
    [SerializeField] EntityRegistry _entityRegistry;
    
    static string _saveFolderPath;
    
    public override void Initialize()
    {
        base.Initialize();
        
        _saveFolderPath = $"{Application.persistentDataPath}/Saves";
        
        if (!Directory.Exists(_saveFolderPath))
            Directory.CreateDirectory(_saveFolderPath);
    }
    
    public void SaveGame()
    {
        EntitySaveData entitySaveData = new EntitySaveData();
        var entities = FindObjectsOfType<Entity>();
        entitySaveData.SetEntities(entities);
        SaveHelper.SaveBinary($"{_saveFolderPath}/savedata01.dat", entitySaveData);
    }
    
    public void LoadGame()
    {
        var entities = FindObjectsOfType<Entity>();
        foreach (var entity in entities)
        {
            if (entity is Building building)
            {
                EntityDestroyer.DestroyEntity<Building>(building);
            }
            else if (entity is Unit unit)
            { 
                EntityDestroyer.DestroyEntity<Unit>(unit);
            }
        }

        EntitySaveData entitySaveData = new EntitySaveData();
        SaveHelper.LoadBinary($"{_saveFolderPath}/savedata01.dat", entitySaveData);

        for (int i = 0; i < entitySaveData.EntityGuids.Length; ++i)
        {
            var entityGuid = entitySaveData.EntityGuids[i];
            var entityPosition = entitySaveData.EntityPositions[i];
            var entityHealth = entitySaveData.EntityHealths[i];
            var entityTeam = entitySaveData.EntityTeams[i];
            var entityType = _entityRegistry.FindByGuid(entityGuid);
            
            if (entityType is BuildingType building)
            {
                EntityFactory<Building>.LoadEntity(entityType, entityPosition, entityHealth, entityTeam);
            }
            else if (entityType is UnitType unit)
            {
                EntityFactory<Unit>.LoadEntity(entityType, entityPosition, entityHealth, entityTeam);
            }
        }
    }

    public void ClearData()
    {
        string[] files = Directory.GetFiles(_saveFolderPath, "*.dat");
        for (int i = 0; i < files.Length; i++)
        {
            print($"{files[i]} Deleted");
            File.Delete(files[i]);
        }
        
        if (Directory.GetFiles(Application.persistentDataPath, "*.dat").Length == 0)
        {
            Debug.Log("Saves Clear Succeed");
        }
    }
}