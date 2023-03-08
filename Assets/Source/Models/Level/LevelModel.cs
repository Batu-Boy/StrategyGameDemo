using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

[System.Serializable]
public class LevelModel
{
    public int index;
    public string name;
    public EntitySaveData defaultLevel;

    public LevelModel(int index, string name, EntitySaveData defaultLevel)
    {
        this.index = index;
        this.name = name;
        this.defaultLevel = defaultLevel;
    }
}
