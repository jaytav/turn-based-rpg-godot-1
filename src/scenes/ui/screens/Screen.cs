using Godot;

public abstract partial class Screen : Control
{
    protected ScreenController ScreenController;

    public override void _Ready()
    {
        ScreenController = GetNode<ScreenController>("/root/ScreenController");
    }

    public virtual void Enter() { }

    public virtual void Exit() { }
}
