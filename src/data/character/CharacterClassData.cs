using Godot;

public partial class CharacterClassData : Resource
{
    [Export]
    public CharacterStatsData Stats = new();

    [Export]
    public InventoryData Inventory = new();
}
