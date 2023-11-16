using Godot;

public abstract partial class GridCellItemAction : GodotObject
{
    public GridCellItemActionContext Context;

    public abstract void Do();
}

public partial class GridCellItemActionContext
{
    public GridCellItem GridCellItem;
    public GridCell GridCellFrom;
    public GridCell GridCellTo;
}
