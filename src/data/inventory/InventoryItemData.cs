using Godot;

public partial class InventoryItemData : Resource
{
    [Export]
    public ItemData Item = new();

    [Export]
    public int Quantity = 1;
}
