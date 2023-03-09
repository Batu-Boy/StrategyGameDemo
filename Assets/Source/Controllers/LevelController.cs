using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelController : MonoBase
{
    public bool initializeOnAwake;
#if UNITY_EDITOR
    public int forceLevelIndex = -1;
#endif
    [SerializeField] EntityRegistry _entityRegistry;
    [SerializeField] private Object jsonLevels;
    [SerializeField] private UnityEvent<LevelModel> onLevelLoaded;
    [SerializeField] private LevelList levelModels;
    [SerializeField] private LevelModel EditorLevel;

    private LevelModel activeLevel;
    
    private int maxLevelCount => levelModels.list.Count;
    private static readonly string LevelsPath = $"Assets/_GameAssets/Levels/Levels.json";
    private static string BinarySaveFilePath;

    private void Awake()
    {
        if (!initializeOnAwake) return;
        Init();
    }

    public override void Initialize()
    {
        if (initializeOnAwake) return;
        Init();
    }

    private void Init()
    {
        BinarySaveFilePath = $"{Application.persistentDataPath}/Saves";

        if (!Directory.Exists(BinarySaveFilePath))
            Directory.CreateDirectory(BinarySaveFilePath);
        
        EventManager.OnNewGame.AddListener(RestartLevel);
        EventManager.OnLoadGame.AddListener(LoadLevel);
        EventManager.OnSaveGame.AddListener(SaveLevel);
        EventManager.OnNextLevel.AddListener(NextLevel);
        
        DeserializeLevels();
        activeLevel = null;
        ClearScene();
/*#if UNITY_EDITOR
        LoadLevel(forceLevelIndex >= 0 ? forceLevelIndex : PlayerDataModel.Data.LevelIndex);
#else
        LoadLevel(PlayerDataModel.Data.LevelIndex);
#endif*/
    }
    
    private void NextLevel()
    {
        PlayerDataModel.Data.LevelIndex++;
        LoadLevel(PlayerDataModel.Data.LevelIndex);
    }
    
    private void RestartLevel()
    {
        LoadLevel(PlayerDataModel.Data.LevelIndex);
    }
    
    private void LoadLevel()
    {
        LoadLevelBinary();
    }
    
    private void LoadLevel(int levelIndex)
    {
        LoadLevelHelper(levelIndex);
    }

    private void SaveLevel()
    {
        SaveLevelBinary();
    }
    
    private void SaveLevelBinary()
    {
        EntitySaveData entitySaveData = new EntitySaveData();
        entitySaveData.SetEntities(RegistryManager.RegisteredEntities);
        SaveHelper.SaveBinary($"{BinarySaveFilePath}/savedata01.dat", entitySaveData);
        PlayerDataModel.Data.HasSaved = true;
    }
    
    private void LoadLevelBinary()
    {
        if(!PlayerDataModel.Data.HasSaved) return;

        ClearEntities();

        EntitySaveData entitySaveData = new EntitySaveData();
        SaveHelper.LoadBinary($"{BinarySaveFilePath}/savedata01.dat", entitySaveData);
        LoadAdaptor(entitySaveData);
    }
    
    #region UTILS
    private void LoadLevelHelper(int levelIndex)
    {
        if (levelIndex < 0) levelIndex = 0;

        levelIndex %= maxLevelCount;

        if (levelModels.list.Count <= 0)
        {
            Debug.LogWarning("LevelModels Empty");
            return;
        }
        DeserializeLevels();
        ClearScene();
        activeLevel = levelModels.list[levelIndex];
        
#if UNITY_EDITOR
        EditorLevel = activeLevel;
#endif
        LoadAdaptor(activeLevel.saveData);
        onLevelLoaded?.Invoke(activeLevel);
    }

    #endregion
    public void ClearScene()
    {
        ClearEntities();
        EditorLevel = null;
        activeLevel = null;
    }
    
    private void DeserializeLevels()
    {
        levelModels = JsonHelper.LoadJson<LevelList>(jsonLevels.ToString());
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
            
            if (entityType is BuildingType)
            {
                EntityFactory<Building>.LoadEntity(entityType, entityPosition, entityHealth, entityTeam);
            }
            else if (entityType is UnitType)
            {
                EntityFactory<Unit>.LoadEntity(entityType, entityPosition, entityHealth, entityTeam);
            }
        }
    }
    
    private void ClearEntities()
    {
        List<Entity> temp = new List<Entity>(RegistryManager.RegisteredEntities);
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
    }

    private void SaveToJson(LevelModel level, string path, bool _override = false)
    {
        if (_override)
        {
            levelModels.list.Insert(activeLevel.index, level);
            levelModels.list.Remove(activeLevel);
        }
        else levelModels.list.Add(level);
        
        JsonHelper.SaveJson(levelModels, path);
        
#if UNITY_EDITOR
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#endif
    }

#if UNITY_EDITOR
    /*public void E_SaveGame()
    {
        EntitySaveData entitySaveData = new EntitySaveData();
        entitySaveData.SetEntities(RegistryManager.RegisteredEntities);
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
    }*/
    
    public void E_LoadLevel(int levelIndex)
    {
        DeserializeLevels();

        LoadLevelHelper(levelIndex);
    }
    
    public void E_Test()
    {
        //JsonHelper.SaveJson(levelModels, LevelsPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    
    public void E_SaveLevel()
    {
        if(EditorLevel == null)
        {
            Debug.LogWarning("You don't have any level to Save.");
            return;
        }
        if (activeLevel != null)
        {
            Debug.LogWarning("You have an active level. Try OverrideLevel instead.");
            return;
        }
        DeserializeLevels();
        EditorLevel.name = $"Level {levelModels.list.Count}";
        EditorLevel.index = levelModels.list.Count;
        EntitySaveData entitySaveData = EditorLevel.saveData;
        entitySaveData.SetEntities(RegistryManager.RegisteredEntities);
        SaveToJson(EditorLevel, LevelsPath);
        var asset = AssetDatabase.LoadAssetAtPath(LevelsPath, typeof(Object));
        jsonLevels = asset;
        ClearScene();
    }
    
    public void E_OverrideLevel()
    {
        if(levelModels == null) DeserializeLevels();
        if (EditorLevel == null) return;
        if (activeLevel == null) return;
        
        SaveToJson(activeLevel, LevelsPath, true);
        E_LoadLevel(activeLevel.index);
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(LevelController))]
public class LevelControllerEditor : Editor
{
    private LevelController levelController;
    private int editorLevelIndex = -1;
    private GUIContent saveContent, loadContent, clearSceneContent, overrideContent,TestContent;

    private void OnEnable()
    {
        saveContent = new GUIContent();
        saveContent.text = "Save Level";

        loadContent = new GUIContent();
        loadContent.text = "Load Level";

        overrideContent = new GUIContent();
        overrideContent.text = "Override Level";

        clearSceneContent = new GUIContent();
        clearSceneContent.text = "Clear Scene";
        
        TestContent = new GUIContent();
        TestContent.text = "Test Method";
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorUtils.DrawUILine(Color.white);

        levelController = target as LevelController;

        EditorGUILayout.BeginVertical();

        if (GUILayout.Button(saveContent))
        {
            levelController.E_SaveLevel();
        }
        if (GUILayout.Button(overrideContent))
        {
            levelController.E_OverrideLevel();
        }
        editorLevelIndex = EditorGUILayout.IntField("Loaded Level Index", editorLevelIndex);
        if (GUILayout.Button(loadContent))
        {
            levelController.E_LoadLevel(editorLevelIndex);
        }

        if (GUILayout.Button(clearSceneContent))
        {
            editorLevelIndex = -1;
            levelController.ClearScene();
        }
        
        if (GUILayout.Button(TestContent))
        {
            levelController.E_Test();
        }
        
        EditorGUILayout.EndVertical();
    }
}

#endif


