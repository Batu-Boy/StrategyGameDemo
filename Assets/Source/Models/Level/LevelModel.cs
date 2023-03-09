[System.Serializable]
public class LevelModel
{
    public int index;
    public string name;
    public EntitySaveData saveData;

    public LevelModel(int index, string name, EntitySaveData saveData)
    {
        this.index = index;
        this.name = name;
        this.saveData = saveData;
    }
}
