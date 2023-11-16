using Godot;

public partial class Move : GridCellItemAction
{
    public override void Do()
    {
        if (Context.GridCellTo.HasSolidItem())
        {
            return;
        }

        // reparent grid cell item's grid cell to move
        Context.GridCellFrom.RemoveChild(Context.GridCellItem);
        Context.GridCellTo.AddChild(Context.GridCellItem);
    }
}
