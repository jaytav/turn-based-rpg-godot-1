using Godot;

public partial class GameDataLoadController : Node
{
    public GameData GameData;
    public GameStateData GameStateData;

    public void Load(GameData gameData, GameStateData gameStateData)
    {
        GD.Print($"GameDataController: Load(): Loading game [{gameData.ResourceName}], game state [{gameStateData.ResourceName}]");
        GameData = gameData;
        GameStateData = gameStateData;

        // set config game data and game state data on load
        ConfigData configData = GetNode<ConfigController>("/root/ConfigController").ConfigData;
        configData.GameStateData = GameStateData;
        configData.GameData = GameData;
        ResourceSaver.Save(configData);
    }
}
