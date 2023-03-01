using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SerializableScriptableObject : ScriptableObject
{
    //using reference for inherited members
    [SerializeReference] Guid _guid = new ("");
    public Guid Guid => _guid;

#if UNITY_EDITOR
    void OnValidate()
    {
        var path = AssetDatabase.GetAssetPath(this);
        _guid = new Guid(AssetDatabase.AssetPathToGUID(path));
    }
#endif
}