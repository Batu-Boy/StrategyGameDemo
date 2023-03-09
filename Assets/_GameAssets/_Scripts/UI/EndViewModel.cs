using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndViewModel : ScreenElement
{
    [SerializeField] private TextMeshProUGUI _teamText;
    [SerializeField] private Button _button;
    
    private bool _isClicked;
    
    public override void Initialize()
    {
        base.Initialize();
        _button.onClick.AddListener(OnNextLevelButton);
        EventManager.OnGameEnd.AddListener(SetWinnerText);
    }
    
    private void SetWinnerText(Team winnerTeam)
    {
        _teamText.text = $"{winnerTeam} Team";
        switch (winnerTeam)
        {
            case Team.Green:
                _teamText.color = Color.green;
                break;
            case Team.Red:
                _teamText.color = Color.red;
                break;
            case Team.Blue:
                _teamText.color = Color.blue;
                break;
            default:
                _teamText.color = Color.green;
                break;
        }
    }
    
    private void OnNextLevelButton()
    {
        if(_isClicked) return;
        _isClicked = true;
        
        EventManager.OnNextLevel?.Invoke();
    }

    private void OnEnable()
    {
        _isClicked = false;
    }
}
