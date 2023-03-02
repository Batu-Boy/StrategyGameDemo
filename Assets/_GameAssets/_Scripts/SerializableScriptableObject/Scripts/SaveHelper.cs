using System.IO;
using UnityEngine;

public class SaveHelper
{
    public static void SaveBinary(string filePath, EntitySaveData entitySaveData)
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.OpenOrCreate)))
        {
            writer.Write(entitySaveData.EntityGuids.Length);
            for (int i = 0; i < entitySaveData.EntityGuids.Length; ++i)
            {
                writer.WriteGuid(entitySaveData.EntityGuids[i]);
                var position = entitySaveData.EntityPositions[i];
                writer.Write(position.x);
                writer.Write(position.y);
                writer.Write(entitySaveData.EntityHealths[i]);
            }
        }

        Debug.Log($"Saved Binary to {filePath}");
    }

    public static void LoadBinary(string filePath, EntitySaveData entitySaveData)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
        {
            var entityCount = reader.ReadInt32();
            entitySaveData.EntityGuids = new Guid[entityCount];
            entitySaveData.EntityPositions = new Vector2Int[entityCount];
            entitySaveData.EntityHealths = new int[entityCount];
            for (int i = 0; i < entityCount; ++i)
            {
                entitySaveData.EntityGuids[i] = reader.ReadGuid();
                entitySaveData.EntityPositions[i] = new Vector2Int(reader.ReadInt32(), reader.ReadInt32());
                entitySaveData.EntityHealths[i] = reader.ReadInt32();
            }
        }

        Debug.Log($"Loaded Binary from {filePath}");
    }
}