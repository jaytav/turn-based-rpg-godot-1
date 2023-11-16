using Godot;

public partial class Attack : GridCellItemAction
{
    public override void Do()
    {
        GD.Print($"Attack: Do(): Attacking: {Context.GridCellItemTarget.Name}");

        // placeholder, handle health points here
        Context.GridCellItemTarget.QueueFree();
    }
}
