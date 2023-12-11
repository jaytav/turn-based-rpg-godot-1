using Godot;
using Godot.Collections;

public partial class InventoryData : Resource
{
    [Export]
    public Array<InventoryItemData> Items = new();
}
