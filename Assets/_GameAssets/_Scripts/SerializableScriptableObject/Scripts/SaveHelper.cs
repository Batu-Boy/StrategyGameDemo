using System.IO;
using UnityEngine;

public class SaveHelper
{
    public static void SaveBinary(string filePath, SaveEntityData saveEntityData)
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.OpenOrCreate)))
        {
            writer.Write(saveEntityData.EntityGuids.Length);
            for (int i = 0; i < saveEntityData.EntityGuids.Length; ++i)
            {
                writer.WriteGuid(saveEntityData.EntityGuids[i]);
                var position = saveEntityData.EntityPositions[i];
                writer.Write(position.x);
                writer.Write(position.y);
                writer.Write(saveEntityData.EntityHealths[i]);
            }
        }

        Debug.Log($"Saved Binary to {filePath}");
    }

    public static void LoadBinary(string filePath, SaveEntityData saveEntityData)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
        {
            var entityCount = reader.ReadInt32();
            saveEntityData.EntityGuids = new Guid[entityCount];
            saveEntityData.EntityPositions = new Vector2Int[entityCount];
            saveEntityData.EntityHealths = new int[entityCount];
            for (int i = 0; i < entityCount; ++i)
            {
                saveEntityData.EntityGuids[i] = reader.ReadGuid();
                saveEntityData.EntityPositions[i] = new Vector2Int(reader.ReadInt32(), reader.ReadInt32());
                saveEntityData.EntityHealths[i] = reader.ReadInt32();
            }
        }

        Debug.Log($"Loaded Binary from {filePath}");
    }
}