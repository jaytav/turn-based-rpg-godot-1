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

    private void onStaticBody3dMouseEntered()
    {
        foreach (GridCellItem item in GetNode("Items").GetChildren())
        {
            item.GetNode<AnimationPlayer>("UI/Name/AnimationPlayer").Play("fade_in");
        }
    }

    private void onStaticBody3dMouseExited()
    {
        foreach (GridCellItem item in GetNode("Items").GetChildren())
        {
            item.GetNode<AnimationPlayer>("UI/Name/AnimationPlayer").PlayBackwards("fade_in");
        }
    }
}
