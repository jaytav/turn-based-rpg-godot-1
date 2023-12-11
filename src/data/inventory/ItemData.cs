using Godot;

public partial class ItemData : Resource
{
    [Export]
    public Texture2D Icon = GD.Load<Texture2D>("res://assets/textures/rectangle.png");

    [Export]
    public int StackSize = 1;

    [Export]
    public Color Modulate = Colors.White;

    [Export]
    public string Description;
}
