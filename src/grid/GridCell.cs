using Godot;

public partial class GridCell : Node3D
{
    public Character Character;

    public override void _Ready()
    {
        foreach (GridCellItem item in GetNode("Items").GetChildren())
        {
            onItemsChildEnteredTree(item);
        }
    }

    private void onItemsChildEnteredTree(Node node)
    {
        if (node is Character)
        {
            Character = (Character)node;
        }
    }

    private void onItemsChildExitedTree(Node node)
    {
        if (node is Character)
        {
            Character = null;
        }
    }

    private void onStaticBody3dMouseEntered()
    {
        if (Character != null)
        {
            if (Character.Name == "Enemy")
            {
                Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);
                GetNode<MeshInstance3D>("Highlight/Attack").Show();
            }
        }
        else
        {
            Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);
            GetNode<MeshInstance3D>("Highlight/Move").Show();
        }
    }

    private void onStaticBody3dMouseExited()
    {
        Input.SetDefaultCursorShape(Input.CursorShape.Arrow);

        foreach (MeshInstance3D highlight in GetNode("Highlight").GetChildren())
        {
            highlight.Hide();
        }
    }
}
