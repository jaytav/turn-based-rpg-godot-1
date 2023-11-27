using Godot;

public partial class GameStateData : Resource
{
    [Export]
    public GameModeData GameMode;

    [Export]
    public CharacterData Character;
}
