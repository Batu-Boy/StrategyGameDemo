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
            var registryManager = GameObject.FindObjectOfType<RegistryManager>();
            registryManager.SaveGame();
        }
    }

    [MenuItem("Tools/Load Game (Binary)")]
    static void LoadBinaryGame()
    {
        if (Application.isPlaying)
        {
            var registryManager = GameObject.FindObjectOfType<RegistryManager>();
            registryManager.LoadGame();
        }
    }

    [MenuItem("Tools/Clear All Saves (Binary)")]
    static void ClearAllSaves()
    {
        if (Application.isPlaying)
        {
            var registryManager = GameObject.FindObjectOfType<RegistryManager>();
            registryManager.ClearData();
        }
    }
}