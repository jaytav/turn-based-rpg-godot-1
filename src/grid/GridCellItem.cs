using Godot;

public partial class GridCellItem : Node3D
{
    [Export]
    public bool Attackable = false;

    private Control _ui;

    public override void _Ready()
    {
        _ui = GetNode<Control>("UI");
        _ui.GetNode<Label>("Name").Text = Name;
    }

    public override void _Process(double delta)
    {
        _ui.Position = GetViewport().GetCamera3D().UnprojectPosition(GlobalPosition);
    }
}
