using Godot;
using Godot.Collections;

public partial class GameStateData : Resource
{
    [Export]
    public Array<CharacterData> Characters = new();

    [Export]
    public GameModeData GameMode = new();
}
