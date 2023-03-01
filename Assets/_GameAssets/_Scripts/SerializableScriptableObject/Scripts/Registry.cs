using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Registry<T> : ScriptableObject where T : SerializableScriptableObject
{
    [SerializeField] protected List<T> registeringTypes = new ();

    public T FindByGuid(Guid guid)
    {
        return registeringTypes.FirstOrDefault(type => type.Guid == guid);
    }
}