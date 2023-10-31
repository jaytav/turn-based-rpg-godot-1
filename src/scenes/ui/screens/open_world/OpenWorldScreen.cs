using Godot;

public partial class OpenWorldScreen : Screen
{
    public override void Enter()
    {
        GetNode<AnimationPlayer>("AnimationPlayer").Play("title_fade_out");
    }
}
