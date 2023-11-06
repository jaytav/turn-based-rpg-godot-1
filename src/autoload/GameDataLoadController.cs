using Godot;

public partial class GameDataLoadController : Node
{
    public GameData GameData;
    public GameStateData GameStateData;

    public void Load(GameData gameData, GameStateData gameStateData)
    {
        GD.Print($"GameDataLoadController: Load(): Loading game [{gameData.ResourceName}], game state [{gameStateData.ResourceName}]");

        GameData = gameData;
        GameStateData = (GameStateData)gameStateData.Duplicate();
    }
}
