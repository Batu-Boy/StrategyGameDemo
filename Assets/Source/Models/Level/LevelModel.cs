using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

[System.Serializable]
public class LevelModel
{
    public int index;
    public string name;
    
    [Range(ConstantValues.MINROWS,ConstantValues.MAXROWS)] public int Width;   //Rows
    [Range(ConstantValues.MINCOLUMNS,ConstantValues.MAXCOLUMNS)] public int Height;   //Columns
    
    //Later feature
    [HideInInspector] public bool IsRandom = true;

    public LevelModel(int index, int width, int height)
    {
        this.index = index;
        Width = width;
        Height = height;
    }
    
    /*public void SetGrid(Grid grid)
    {
        if (grid != null)
        {
            M = grid.Rows;
            N = grid.Columns;
            
            Grid = grid;

            IsRandom = false;
        }
        else
        {
            IsRandom = true;
        }
    }*/
}
