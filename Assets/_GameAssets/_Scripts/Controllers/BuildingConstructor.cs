using UnityEngine;

//TODO: seperate visual
public class BuildingConstructor : MonoBase
{
    [SerializeField] private Transform _silhouette;
    [SerializeField] private SpriteRenderer _silhouetteRenderer;
    
    private BuildingType _currentSelectedType;
    private Vector3Int _currentPosition;
    private bool _isValidPosition;
    private bool _hasSelection;
    
    public override void Initialize()
    {
        base.Initialize();
        ResetValues();
        EventManager.OnBuildingUISelected.AddListener(OnBuildingUISelected);
    }
    
    private void OnBuildingUISelected(BuildingType selectedType)
    {
        ArrangeSilhouette(selectedType);
    }
    
    private void ArrangeSilhouette(BuildingType selectedType)
    {
        _hasSelection = true;
        _currentSelectedType = selectedType;
        _silhouetteRenderer.sprite = selectedType.Sprite;
        bool isWidthEven = selectedType.StartWidth % 2 == 0;
        bool isHeightEven = selectedType.StartHeight % 2 == 0;
        float localX = isWidthEven ? .5f : 0;
        float localY = isHeightEven ? .5f : 0;
        _silhouetteRenderer.transform.localPosition = new Vector3(localX, localY);
        _silhouette.SetActiveGameObject(true);
    }
    
    private void Update()
    {
        if(!_hasSelection) return;
        
        PositionHandler();
        
        if (Input.GetMouseButtonDown(0))
        {
            if(_isValidPosition)
                ConstructBuilding();
            else
                Debug.LogWarning("Not a Valid Position! Cannot Construct");
        }
    }
    
    private void ConstructBuilding()
    {
        var newBuilding = EntityFactory.CreateEntity<Building>(_currentSelectedType, _currentPosition);
        
        ResetValues();
    }
    
    private void PositionHandler()
    {
        var mouseMapPos = InputHelper.GetMouseMapPosition();
        if (mouseMapPos != _currentPosition)
        {
            UpdatePosition(mouseMapPos);
            CheckPositionValid(mouseMapPos);
        }
    }
    
    private void CheckPositionValid(Vector3Int mouseMapPos)
    {
        _isValidPosition =
            GridManager.IsSettlementValid(_currentSelectedType, new Vector2Int(mouseMapPos.x, mouseMapPos.y));
        var color = _isValidPosition ? Color.white : Color.red;
        color.a = .6f;
        _silhouetteRenderer.color = color;
    }
    
    private void UpdatePosition(Vector3Int mouseMapPos)
    {
        _currentPosition = mouseMapPos;
        _silhouette.position = _currentPosition;
    }
    
    private void ResetValues()
    {
        _silhouette.SetActiveGameObject(false);
        _currentSelectedType = null;
        _currentPosition = Vector3Int.zero;
        _hasSelection = false;
        _isValidPosition = false;
    }
}