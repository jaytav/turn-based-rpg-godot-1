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
            getAction().Do();
        }
    }

    /** choose action based on context
        attack if attackable grid cell item
        move if no attackable grid cell item */
    private GridCellItemAction getAction()
    {
        GridCellItemActionContext context = new();
        context.GridCellTo = GetNode<MouseController>("/root/MouseController").GetGridCell();
        context.GridCellFrom = Player.GetParent<GridCell>();
        context.GridCellItem = Player;

        foreach (GridCellItem item in context.GridCellTo.GetChildren())
        {
            if (item.Attackable)
            {
                Attack attack = new();
                context.GridCellItemTarget = item;
                attack.Context = context;
                return attack;
            }
        }

        // move as default action
        Move move = new();
        move.Context = context;
        return move;
    }
}
