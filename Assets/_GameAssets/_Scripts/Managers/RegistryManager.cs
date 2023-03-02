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
            Destroy(entity.gameObject);
        }

        EntitySaveData entitySaveData = new EntitySaveData();
        SaveHelper.LoadBinary($"{_saveFolderPath}/savedata01.dat", entitySaveData);

        for (int i = 0; i < entitySaveData.EntityGuids.Length; ++i)
        {
            var entityGuid = entitySaveData.EntityGuids[i];
            var entityPosition = entitySaveData.EntityPositions[i];
            var entityHealth = entitySaveData.EntityHealths[i];
            
            var entityType = _entityRegistry.FindByGuid(entityGuid);

            var loadedEntity = EntityFactory.LoadEntity<Entity>(entityType, entityPosition, entityHealth);
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