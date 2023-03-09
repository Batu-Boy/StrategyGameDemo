using UnityEngine;
using UnityEngine.UI;

public class MainMenuViewModel : ScreenElement
{
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _loadGameButton;
    [SerializeField] private Button _saveGameButton;
    
    private bool _isClicked;
    private bool _hasLoaded;
    
    public override void Initialize()
    {
        base.Initialize();
        _newGameButton.onClick.AddListener(NewGameButton);
        _loadGameButton.onClick.AddListener(LoadGameButton);
        _saveGameButton.onClick.AddListener(SaveButton);
    }

    private void SaveButton()
    {
        if(!_hasLoaded) return;
        
        if(_isClicked) return;
        _isClicked = true;
        print("hasNTLOADED");
        PlayerDataModel.Data.HasSaved = true;
        EventManager.OnSaveGame?.Invoke();
    }

    private void NewGameButton()
    {
        if(_isClicked) return;
        _isClicked = true;
        _hasLoaded = true;
        print("newgame");
        EventManager.OnNewGame?.Invoke();
    }
    
    private void LoadGameButton()
    {
        if(_isClicked) return;
        _isClicked = true;
        _hasLoaded = true;
        
        if (!PlayerDataModel.Data.HasSaved)
        {
            print("hasnt save");
            _isClicked = false;
            _hasLoaded = false;
            return;
        }
        
        EventManager.OnLoadGame?.Invoke();
    }
    
    private void OnEnable()
    {
        _isClicked = false;
    }

    private void OnDestroy()
    {
        print("destroy");
        _newGameButton.onClick.RemoveAllListeners();
        _loadGameButton.onClick.RemoveAllListeners();
    }
}
