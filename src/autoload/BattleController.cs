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
        player.Name = "Player";
        grid.GetChild(gameStateData.PlayerPosition).GetNode("Items").AddChild(player);

        // load enemy
        GridCellItem enemy = _character.Instantiate<GridCellItem>();
        enemy.Name = "Enemy";
        grid.GetChild(gameStateData.EnemyPosition).GetNode("Items").AddChild(enemy);
        enemy.TreeExited += onEnemyTreeExited;

        // if grid exists, remove
        Node existingGrid = GetNodeOrNull("/root/Main/World/Grid");

        if (existingGrid != null)
        {
            existingGrid.QueueFree();
        }

        GetNode("/root/Main/World").AddChild(grid);
        GetNode<ScreenController>("/root/ScreenController").ChangeScreen("BattleScreen");
    }

    private void onEnemyTreeExited()
    {
        Node grid = GetNodeOrNull("/root/Main/World/Grid");

        // handle when enemy exits tree when game loaded while in game
        if (grid != null)
        {
            GetNode("/root/Main/World/Grid").QueueFree();
            GetNode<ScreenController>("/root/ScreenController").ChangeScreen("MainMenuScreen");
            GD.Print("You Win!");
        }
    }
}
