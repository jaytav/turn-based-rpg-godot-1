using Godot;

public partial class GameDataSaveController : Node
{
    public void Save(GameData gameData = null)
    {
        GameDataLoadController gameDataLoadController = GetNode<GameDataLoadController>("/root/GameDataLoadController");
        gameData = gameData ?? gameDataLoadController.GameData;

        if (gameData == null)
        {
            GD.PushError($"GameDataSaveController: SaveGame(): Failed to save, GameData is null");
            return;
        }

        GD.Print($"GameDataSaveController: SaveGame(): Saving game [{gameData.ResourceName}]");
        string gameDataDir = gameData.ResourcePath.Substring(0, gameData.ResourcePath.Length - 5);
        string gameStateDataDir = $"{gameDataDir}/game_states";

        GameStateData gameStateData = gameDataLoadController.GameStateData;
        gameStateData.ResourceName = DirAccess.GetFilesAt(gameStateDataDir).Length.ToString();
        gameStateData.ResourcePath = $"{gameStateDataDir}/{gameStateData.ResourceName}.tres";
        gameData.GameStates.Insert(0, gameStateData);

        ResourceSaver.Save(gameStateData);
        ResourceSaver.Save(gameData);

        // set config game data and game state data
        GetNode<ConfigController>("/root/ConfigController").StoreGameData(gameData, gameStateData);

        gameDataLoadController.GameData = gameData;
        gameDataLoadController.GameStateData = (GameStateData)gameStateData.Duplicate();
    }
}
