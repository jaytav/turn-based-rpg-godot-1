using Godot;

public partial class GridCell : Node3D
{
    public bool HasSolidItem() {
        foreach (GridCellItem item in GetChildren())
        {
            if (item.Solid)
            {
                return true;
            }
        }

        return false;
    }
}
