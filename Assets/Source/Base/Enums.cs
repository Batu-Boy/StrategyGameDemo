public enum GameStates
{
    Main,
    Game,
    End,
    WaitInput,
    Loading
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