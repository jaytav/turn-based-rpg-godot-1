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
            GridCellItemAction action = getAction();
            GD.Print($"PlayerController: _UnhandledInput(): ActionPrimary: {action.GetType()}");
            action.Do();
        }
    }

    /** choose action based on context
        attack if attackable grid cell item
        move if no attackable grid cell item */
    private GridCellItemAction getAction()
    {
        GridCellItemActionContext context = new();
        context.GridCellTo = GetNode<MouseController>("/root/MouseController").GetGridCell();
        context.GridCellFrom = Player.GetParent().GetParent<GridCell>();
        context.GridCellItem = Player;

        GridCellItem attackableItem = context.GridCellTo.GetAttackable();

        if (attackableItem != null && context.GridCellItem != attackableItem)
        {
            context.GridCellItemTarget = attackableItem;

            Attack attack = new();
            attack.Context = context;
            return attack;
        }

        // move as default action
        Move move = new();
        move.Context = context;
        return move;
    }
}
