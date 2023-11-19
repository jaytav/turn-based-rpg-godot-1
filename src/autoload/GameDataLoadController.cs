using Godot;

public partial class GameDataLoadController : Node
{
    public GameData GameData;
    public GameStateData GameStateData;

    public void Load(GameData gameData, GameStateData gameStateData)
    {
        GD.Print($"GameDataLoadController: Load(): Loading game [{gameData.ResourceName}], game state [{gameStateData.ResourceName}]");

        GameData = gameData;
        GameStateData = gameStateData;

        // set config game data and game state data
        GetNode<ConfigController>("/root/ConfigController").StoreGameData(GameData, GameStateData);

        // duplicate self so changes don't affect already saved state
        GameStateData = (GameStateData)GameStateData.Duplicate();

        GetNode<Node3D>("/root/Main/World").Visible = true;
        GetNode<BattleController>("/root/BattleController").Load(GameStateData);
    }
}
