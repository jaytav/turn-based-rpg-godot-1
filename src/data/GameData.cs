using Godot;
using Godot.Collections;

public partial class GameData : Resource
{
    [Signal]
    public delegate void DeletedEventHandler(GameData gameData);

    [Export]
    public Array<GameStateData> GameStates = new();
}
