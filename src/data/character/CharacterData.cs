using Godot;

public partial class CharacterData : Resource
{
    [Export]
    public CharacterClassData Class = new();

    [Export]
    public CharacterStatsData Stats = new();
}
