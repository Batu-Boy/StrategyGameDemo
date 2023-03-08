using System.IO;
using System.Linq;
using UnityEngine;

//TODO: save version
public class RegistryManager : MonoBase
{
    [SerializeField] private Object _defaultLevel;
    [SerializeField] EntityRegistry _entityRegistry;
    
    private static string _saveFolderPath;
    private static string _savedGameFolderPath;
#if UNITY_EDITOR
    private static string _editorSavePath;
#endif

    private bool _isLoaded;
    
    public override void Initialize()
    {
        base.Initialize();

        _saveFolderPath = $"{Application.persistentDataPath}/Saves";
        _savedGameFolderPath = $"{_saveFolderPath}/savedata01.dat";
        
        EventManager.OnNewGame.AddListener(LoadDefaultGame);
        EventManager.OnLoadGame.AddListener(LoadGame);
        EventManager.OnSaveGame.AddListener(SaveGame);
        
        if (!Directory.Exists(_saveFolderPath))
            Directory.CreateDirectory(_saveFolderPath);
#if UNITY_EDITOR
        _editorSavePath = $"{Application.persistentDataPath}/Editor";
        if (!Directory.Exists(_editorSavePath))
            Directory.CreateDirectory(_editorSavePath);
#endif
    }

    public void SaveGame()
    {
        EntitySaveData entitySaveData = new EntitySaveData();
        var entities = FindObjectsOfType<Entity>();
        entitySaveData.SetEntities(entities);
        SaveHelper.SaveBinary(_savedGameFolderPath, entitySaveData);
        PlayerDataModel.Data.HasSaved = true;
    }
    
    public void LoadGame()
    {
        if(!PlayerDataModel.Data.HasSaved) return;
        
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
        SaveHelper.LoadBinary(_savedGameFolderPath, entitySaveData);

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

    public void LoadDefaultGame()
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
        var levelList = JsonHelper.LoadJson<LevelList>(_defaultLevel.ToString());
        entitySaveData = levelList.list[0].defaultLevel;
        
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

    #region EditorUtils

#if UNITY_EDITOR
    
    public void E_SaveGame()
    {
        EntitySaveData entitySaveData = new EntitySaveData();
        var entities = FindObjectsOfType<Entity>();
        entitySaveData.SetEntities(entities);
        SaveHelper.SaveBinary($"{_editorSavePath}/editorsave.dat", entitySaveData);
    }
    
    public void E_LoadGame()
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
        SaveHelper.LoadBinary($"{_editorSavePath}/editorsave.dat", entitySaveData);

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
#endif
    
    #endregion
    

}