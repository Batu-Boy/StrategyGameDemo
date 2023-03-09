using System.Collections.Generic;
using System.IO;
using UnityEngine;

//TODO: save version
public class RegistryManager : MonoBase
{
    [Header("References")]
    [SerializeField] private Object _defaultLevel;
    [SerializeField] EntityRegistry _entityRegistry;
    
    public static List<Entity> RegisteredEntities;
    
    private static string _saveFolderPath;
    private static string _savedGameFolderPath;
#if UNITY_EDITOR
    private static string _editorSavePath;
#endif
    
    private bool _isLoaded;
    
    public override void Initialize()
    {
        base.Initialize();
        
        RegisteredEntities = new List<Entity>();
        
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
    
    private void SaveGame()
    {
        EntitySaveData entitySaveData = new EntitySaveData();
        entitySaveData.SetEntities(RegisteredEntities);
        SaveHelper.SaveBinary(_savedGameFolderPath, entitySaveData);
        PlayerDataModel.Data.HasSaved = true;
    }
    
    private void LoadGame()
    {
        if(!PlayerDataModel.Data.HasSaved) return;

        List<Entity> temp = new List<Entity>(RegisteredEntities);
        foreach (var entity in temp)
        {
            if (entity is Building building)
            {
                EntityDestroyer.DestroyEntityImmediate<Building>(building);
            }
            else if (entity is Unit unit)
            { 
                EntityDestroyer.DestroyEntityImmediate<Unit>(unit);
            }
        }

        EntitySaveData entitySaveData = new EntitySaveData();
        SaveHelper.LoadBinary(_savedGameFolderPath, entitySaveData);
        LoadAdaptor(entitySaveData);
    }
    
    private void LoadDefaultGame()
    {
        var entities = FindObjectsOfType<Entity>();
        foreach (var entity in entities)
        {
            if (entity is Building building)
            {
                EntityDestroyer.DestroyEntityImmediate<Building>(building);
            }
            else if (entity is Unit unit)
            { 
                EntityDestroyer.DestroyEntityImmediate<Unit>(unit);
            }
        }
        
        EntitySaveData defaultLevel = new EntitySaveData();
        var levelList = JsonHelper.LoadJson<LevelList>(_defaultLevel.ToString());
        defaultLevel = levelList.list[0].defaultLevel;
        LoadAdaptor(defaultLevel);
    }
    
    public static void RegisterEntity(Entity entity)
    {
        if(!RegisteredEntities.Contains(entity))
            RegisteredEntities.Add(entity);
    }
    
    public static void RemoveEntity(Entity entity)
    {
        RegisteredEntities.Remove(entity);
    }
    
    private void LoadAdaptor(EntitySaveData data)
    {
        for (int i = 0; i < data.EntityGuids.Length; ++i)
        {
            var entityGuid = data.EntityGuids[i];
            var entityPosition = data.EntityPositions[i];
            var entityHealth = data.EntityHealths[i];
            var entityTeam = data.EntityTeams[i];
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
        entitySaveData.SetEntities(RegisteredEntities);
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
        LoadAdaptor(entitySaveData);
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