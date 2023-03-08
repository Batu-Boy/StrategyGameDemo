using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuViewModel : ScreenElement
{
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _loadGameButton;
    [SerializeField] private Button _saveGameButton;
    
    public bool isClicked;
    private bool hasLoaded;
    
    public override void Initialize()
    {
        base.Initialize();
        _newGameButton.onClick.AddListener(NewGameButton);
        _loadGameButton.onClick.AddListener(LoadGameButton);
        _saveGameButton.onClick.AddListener(SaveButton);
    }

    private void SaveButton()
    {
        if(!hasLoaded) return;
        
        if(isClicked) return;
        isClicked = true;
        print("hasNTLOADED");
        EventManager.OnSaveGame?.Invoke();
    }

    public void NewGameButton()
    {
        if(isClicked) return;
        isClicked = true;
        hasLoaded = true;
print("newgame");
        EventManager.OnNewGame?.Invoke();
    }
    
    public void LoadGameButton()
    {
        if(isClicked) return;
        isClicked = true;
        hasLoaded = true;
        
        if (!PlayerDataModel.Data.HasSaved)
        {
            print("hasnt save");
            return;
        }
        
        EventManager.OnLoadGame?.Invoke();
    }
    
    private void OnEnable()
    {
        isClicked = false;
    }

    private void OnDestroy()
    {
        print("destroy");
        _newGameButton.onClick.RemoveAllListeners();
        _loadGameButton.onClick.RemoveAllListeners();
    }
}
