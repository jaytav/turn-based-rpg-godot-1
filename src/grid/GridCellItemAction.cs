using Godot;

public abstract partial class GridCellItemAction : GodotObject
{
    public abstract void Do(GridCellItemActionContext context);
}

public partial class GridCellItemActionContext
{
    public GridCellItem GridCellItem;
    public GridCell GridCellFrom;
    public GridCell GridCellTo;
}
