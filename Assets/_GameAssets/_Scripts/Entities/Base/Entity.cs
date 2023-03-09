using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EntityVisual))]
public abstract class Entity: MonoBehaviour, IDamageable
{
    [Header("References")] 
    [SerializeField] private EntityVisual _entityVisual;

    [Header("Type")]
    public EntityType Type;
    
    [Space] 
    public Team Team;
    public int Health { get; set; }
    public Vector2Int CurrentPosition;

    public Action onPositionChange;
    
    private bool isDead;
    private Cell _currentCell;

    public virtual void InitType(EntityType type, Vector2Int position, Team team)
    {
        Type = type;
        Health = Type.StartHealth;
        Team = team;
        UpdatePosition(position);
        
        name = $"({Team}){Type.name}";
        transform.position = (Vector3Int)CurrentPosition;

        _currentCell = GridManager.GetCell(position);
        isDead = false;
        
        _entityVisual.InitVisual(Type.StartWidth, Type.StartHeight, Type.Sprite, Team);
    }
    
    public void InitSave(EntityType type, Vector2Int position, int health, Team team)
    {
        InitType(type, position, team);
        Health = health;
        _entityVisual.UpdateHpVisual(Health / (float)Type.StartHealth);
    }
    
    public void UpdatePosition(Vector2Int position)
    {
        if (position != CurrentPosition)
        {
            CurrentPosition = position;
            _currentCell?.Clear();
            _currentCell = GridManager.GetCell(position);
            _currentCell.Entity = this;
            onPositionChange?.Invoke();
        }
    }

    private void OnMouseUpAsButton()
    {
        //if(Team == PlayerDataModel.Data.PlayerTeam) Open later for only players unit controls
        if(!EventSystem.current.IsPointerOverGameObject())
            EventManager.OnMapEntitySelected?.Invoke(this);
    }
    
    private void OnDisable()
    {
        _currentCell?.Clear();
        _currentCell = null;
        DeInit();
    }
    
    public void TakeDamage(int amount)
    {
        Health -= amount;
        _entityVisual.UpdateHpVisual(Health / (float)Type.StartHealth);
        
        if (Health <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    protected abstract void DeInit();
    
    protected abstract void Die();
}
