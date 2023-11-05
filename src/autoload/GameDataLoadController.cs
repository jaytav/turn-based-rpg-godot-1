using Godot;

public partial class GameDataLoadController : Node
{
    public GameData GameData;
    public GameStateData GameStateData;

    private PackedScene _character = GD.Load<PackedScene>("res://src/character/Character.tscn");
    private PackedScene _enemy = GD.Load<PackedScene>("res://src/enemy/Enemy.tscn");

    public void Load(GameData gameData, GameStateData gameStateData)
    {
        GD.Print($"GameDataController: Load(): Loading game [{gameData.ResourceName}], game state [{gameStateData.ResourceName}]");

        GameData = gameData;
        GameStateData = gameStateData;
    }
}
