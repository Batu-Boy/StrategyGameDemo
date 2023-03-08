using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
#endif

public class GameController : ControllerBase
{
    public static GameController Instance;
    
    public bool IsPlayerWin { get; set; }
    
    public GameStates CurrentState => _currentState;
    [SerializeField] List<ControllerBase> controllers;
    [SerializeField] UnityEvent<GameStates> onGameStateChanged;
    [SerializeField] private bool autoMapControllers;

    private GameStates _currentState;
    private WaitForSeconds mainMenuToggleWait;
    private bool isStarted;

    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
        _currentState = GameStates.Main;
        mainMenuToggleWait = new WaitForSeconds(.5f);
        EventManager.OnLoadGame.AddListener(StartGame);
        EventManager.OnNewGame.AddListener(StartGame);
    }

    //Tap To Play Event Trigger
    public void StartGame()
    {
        isStarted = true;
        _currentState = GameStates.Game;
        onGameStateChanged?.Invoke(_currentState);
        OnStateChanged(_currentState);
    }
    
    public void ChangeState(GameStates state)
    {
        _currentState = state;
        onGameStateChanged?.Invoke(_currentState);
        OnStateChanged(_currentState);
    }
    
    public void EndState(bool isPlayerWin)
    {
        IsPlayerWin = isPlayerWin;
        ChangeState(GameStates.End);
        if (isPlayerWin)
        {
            PlayerDataModel.Data.LevelIndex++;
        }

        DOVirtual.DelayedCall(1, () => SceneController.Instance.RestartScene());
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isStarted)
        {
            ToggleMainMenuState();
        }
        
        foreach (var item in controllers)
        {
            item.ControllerUpdate(_currentState);
        }
    }

    private void ToggleMainMenuState()
    {
        ChangeState(_currentState == GameStates.Game ? GameStates.Main : GameStates.Game);
    }

    private void FixedUpdate()
    {
        foreach (var item in controllers)
        {
            item.ControllerFixedUpdate(_currentState);
        }
    }

    private void LateUpdate()
    {
        foreach (var item in controllers)
        {
            item.ControllerLateUpdate(_currentState);
        }
    }

    public override void OnStateChanged(GameStates state)
    {
        foreach (ControllerBase controllerBase in controllers)
        {
            controllerBase.OnStateChanged(state);
        }
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
    if(!autoMapControllers) return;
        AssemblyReloadEvents.afterAssemblyReload -= MapControllersInScene;
        AssemblyReloadEvents.afterAssemblyReload += MapControllersInScene;
    }
    [EditorButton]
    public void MapControllersInScene()
    {
        controllers.Clear();
        var controllersInScene = FindObjectsOfType<ControllerBase>();
        controllers.AddRange(controllersInScene);
        controllers.Remove(this);
    }
#endif
}