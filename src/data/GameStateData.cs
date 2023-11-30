using Godot;

public partial class GameStateData : Resource
{
    [Export]
    public GameModeData GameMode = new();

    [Export]
    public CharacterData Character = new();
}
