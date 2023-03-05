public enum GameStates
{
    Main,
    Game,
    End,
    Loading
}

public enum InputStates
{
    Idle,
    BuildingPlacement,
    EntitySelect
}

public enum Team
{
    Green,
    Red,
    Blue
}

public enum VibrationTypes
{
    None,
    Light,
    Medium,
    Heavy,
    Succes,
    Fail,
    RigidImpact,
    Soft,
    Warning
}

//THE ORDER MUST DOWN>LEFT>UP>RIGHT
public enum Direction
{
    Down = 0,
    Left = 1,
    Up = 2,
    Right = 3
}