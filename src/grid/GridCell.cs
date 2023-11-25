using Godot;

public partial class GridCell : Node3D
{
    public GridCellItem GetAttackable()
    {
        foreach (GridCellItem item in GetNode("Items").GetChildren())
        {
            if (item.Attackable)
            {
                return item;
            }
        }

        return null;
    }
}
