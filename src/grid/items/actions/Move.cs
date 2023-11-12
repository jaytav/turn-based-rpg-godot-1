using Godot;

public partial class Move : GridCellItemAction
{
    public override void Do(GridCellItemActionContext context)
    {
        // reparent grid cell item's grid cell to move
        context.GridCellFrom.RemoveChild(context.GridCellItem);
        context.GridCellTo.AddChild(context.GridCellItem);
    }
}
