using Godot;

public partial class GridLoadController : Node
{
    private PackedScene _grid = GD.Load<PackedScene>("res://src/grid/Grid.tscn");
    private PackedScene _character = GD.Load<PackedScene>("res://src/grid/items/Character.tscn");

    public void Load(GameStateData gameStateData)
    {
        Grid grid = _grid.Instantiate<Grid>();

        // load player into grid
        GridCellItem player =_character.Instantiate<GridCellItem>();
        GetNode<PlayerController>("/root/PlayerController").Player = player;
        grid.GetChild(gameStateData.PlayerPosition).AddChild(player);

        // load enemy into grid
        grid.GetChild(gameStateData.EnemyPosition).AddChild(_character.Instantiate());

        GetNode("/root/Main/World").AddChild(grid);
    }
}
