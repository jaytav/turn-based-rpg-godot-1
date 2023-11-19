using Godot;

public partial class BattleController : Node
{
    private PackedScene _grid = GD.Load<PackedScene>("res://src/grid/Grid.tscn");
    private PackedScene _character = GD.Load<PackedScene>("res://src/grid/items/Character.tscn");

    public void Load(GameStateData gameStateData)
    {
        Grid grid = _grid.Instantiate<Grid>();

        // load player
        GridCellItem player = _character.Instantiate<GridCellItem>();
        GetNode<PlayerController>("/root/PlayerController").Player = player;
        grid.GetChild(gameStateData.PlayerPosition).AddChild(player);

        // load enemy
        GridCellItem enemy = _character.Instantiate<GridCellItem>();
        grid.GetChild(gameStateData.EnemyPosition).AddChild(enemy);
        enemy.TreeExited += onEnemyTreeExited;

        GetNode("/root/Main/World").AddChild(grid);
        GetNode<ScreenController>("/root/ScreenController").ChangeScreen("BattleScreen");
    }

    private void onEnemyTreeExited()
    {
        GetNode("/root/Main/World/Grid").QueueFree();
        GetNode<ScreenController>("/root/ScreenController").ChangeScreen("MainMenuScreen");
        GD.Print("You Win!");
    }
}
