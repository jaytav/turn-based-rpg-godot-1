using Godot;

public partial class Main : Node
{
    public override void _Ready()
    {
        GD.Print("Main: _Ready()");
        GetNode<Node3D>("/root/Main/World").Visible = false;
    }
}
