[System.Serializable]
public class PlayerDataModel : DataModel
{
    public static PlayerDataModel Data;
    public Team PlayerTeam = Team.Green;
    public int LevelIndex;
    public bool HasSaved;
    
    public PlayerDataModel Load()
    {
        if (Data == null)
        {
            Data = this;
            object data = LoadData();

            if (data != null)
            {
                Data = (PlayerDataModel)data;
            }
        }

        return Data;
    }


    public void Save()
    {
        Save(Data);
    }
}