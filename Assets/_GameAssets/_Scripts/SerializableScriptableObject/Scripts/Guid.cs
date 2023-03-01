using System.IO;
using UnityEngine;

[System.Serializable]
public class Guid
{
    public const string VALUE_FIELDNAME = nameof(guid);
    
    [SerializeField, HideInInspector] private string guid;
    public string ID => guid;
    
    public Guid(string id)
    {
        guid = id;
    }

    public static bool operator ==(Guid x, Guid y) => x?.guid == y?.guid;

    public static bool operator !=(Guid x, Guid y) => x?.guid != y?.guid;
}

public static class BinaryReaderExtensions
{
    public static Guid ReadGuid(this BinaryReader reader)
    {
        return new Guid(reader.ReadString());
    }
}

public static class BinaryWriterExtensions
{
    public static void WriteGuid(this BinaryWriter writer, Guid guid)
    {
        writer.Write(guid.ID);
    }
}