using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Registry<T> : ScriptableObject where T : SerializableScriptableObject
{
    [SerializeField] protected List<T> registeringTypes = new ();

    public T FindByGuid(Guid guid)
    {
        foreach (var type in registeringTypes)
        {
            if (type.Guid == guid)
            {
                return type;
            }
        }

        return null;
    }
}