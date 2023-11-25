using Godot;

public partial class Move : GridCellItemAction
{
    public override void Do()
    {
        // reparent grid cell item's grid cell to move
        Context.GridCellFrom.GetNode("Items").RemoveChild(Context.GridCellItem);
        Context.GridCellTo.GetNode("Items").AddChild(Context.GridCellItem);
    }
}
