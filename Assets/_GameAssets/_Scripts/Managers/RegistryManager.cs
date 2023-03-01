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
        SaveEntityData saveEntityData = new SaveEntityData();
        var entities = FindObjectsOfType<Entity>();
        saveEntityData.SetEntities(entities);
        SaveHelper.SaveBinary($"{_saveFolderPath}/savedata01.dat", saveEntityData);
    }
    
    public void LoadGame()
    {
        var entities = FindObjectsOfType<Entity>();
        foreach (var entity in entities)
        {
            Destroy(entity.gameObject);
        }

        SaveEntityData saveEntityData = new SaveEntityData();
        SaveHelper.LoadBinary($"{_saveFolderPath}/savedata01.dat", saveEntityData);

        for (int i = 0; i < saveEntityData.EntityGuids.Length; ++i)
        {
            var entityGuid = saveEntityData.EntityGuids[i];
            var entityPosition = saveEntityData.EntityPositions[i];
            var entityHealth = saveEntityData.EntityHealths[i];
            
            var entityType = _entityRegistry.FindByGuid(entityGuid);
            
            var entity = Instantiate(entityType.EntityPrefab);
            entity.InitSave(entityType, entityPosition, entityHealth);
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

        PlayerPrefs.DeleteAll();

        if (Directory.GetFiles(Application.persistentDataPath, "*.dat").Length == 0)
        {
            Debug.Log("Saves Clear Succeed");
        }
    }
}