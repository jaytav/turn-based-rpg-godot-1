using Godot;

public partial class PlayerController : Node
{
    public GridCellItem Player;

    public override void _UnhandledInput(InputEvent @event)
    {
        if (Player == null)
        {
            return;
        }

        if (@event.IsActionPressed("ActionPrimary"))
        {
            GD.Print($"PlayerController: _UnhandledInput(): ActionPrimary");
            GridCellItemAction move = new Move();
            move.Context = getGridCellItemActionContext();
            move.Do();
        }
    }

    private GridCellItemActionContext getGridCellItemActionContext()
    {
        GridCellItemActionContext context = new();
        context.GridCellItem = Player;
        context.GridCellFrom = Player.GetParent<GridCell>();
        context.GridCellTo = GetNode<MouseController>("/root/MouseController").GetGridCell();

        return context;
    }
}
