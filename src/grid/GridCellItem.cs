using Godot;

public partial class GridCellItem : Node3D
{
    [Export]
    public bool Attackable = false;

    private void onStaticBody3DMouseEntered()
    {
        GetNode<MeshInstance3D>("Highlight").Show();
    }

    private void onStaticBody3DMouseExited()
    {
        GetNode<MeshInstance3D>("Highlight").Hide();
    }
}
