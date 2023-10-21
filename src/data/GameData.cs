using Godot;
using Godot.Collections;

public partial class GameData : Resource
{
    [Export]
    public Array<CharacterData> Characters = new();
}
