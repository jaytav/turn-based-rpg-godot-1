using Godot;

public partial class Enemy : Character
{
    private void onArea3DBodyEntered(Node3D body)
    {
        if (body is Character)
        {
            GD.Print("Enemy: onBodyEntered(): Starting battle");
        }
    }
}
