using System.IO;
using UnityEditor;
using UnityEngine;

public class SaveDataDebugTools
{
    [MenuItem("Tools/Save Game (Binary)")]
    static void SaveBinaryGame()
    {
        if (Application.isPlaying)
        {
            var levelController = GameObject.FindObjectOfType<LevelController>();
            levelController.E_SaveLevel();
        }
    }

    [MenuItem("Tools/Load Game (Binary)")]
    static void LoadBinaryGame()
    {
        if (Application.isPlaying)
        {
            var levelController = GameObject.FindObjectOfType<LevelController>();
            levelController.E_LoadLevel(0);
        }
    }
}