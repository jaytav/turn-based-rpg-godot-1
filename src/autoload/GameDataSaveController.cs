using Godot;

public partial class GameDataSaveController : Node
{
    public void Save(GameData gameData = null)
    {
        GameDataLoadController gameDataLoadController = GetNode<GameDataLoadController>("/root/GameDataLoadController");
        gameData = gameData ?? gameDataLoadController.GameData;

        if (gameData == null)
        {
            GD.PushError($"GameDataController: SaveGame(): Failed to save, GameData is null");
            return;
        }

        GD.Print($"GameDataController: SaveGame(): Saving game [{gameData.ResourceName}]");
        string gameDataDir = gameData.ResourcePath.Substring(0, gameData.ResourcePath.Length - 5);
        string gameStateDataDir = $"{gameDataDir}/game_states";

        GameStateData gameStateData = gameDataLoadController.GameStateData ?? new GameStateData();

        // handle new game, initialise data
        if (!FileAccess.FileExists(gameData.ResourcePath))
        {
            // game state data directory
            DirAccess.MakeDirRecursiveAbsolute(gameStateDataDir);

            // game state data game mode
            gameStateData.GameMode = GD.Load<GameModeData>("res://src/data/game_modes/character_create.tres");
        }

        gameStateData.ResourceName = $"game_state_{DirAccess.GetFilesAt(gameStateDataDir).Length.ToString()}";
        gameStateData.ResourcePath = $"{gameStateDataDir}/{gameStateData.ResourceName}.tres";
        gameData.GameStates.Insert(0, gameStateData);

        ResourceSaver.Save(gameStateData);
        ResourceSaver.Save(gameData);

        // set config game data and game state data
        ConfigData configData = GetNode<ConfigController>("/root/ConfigController").ConfigData;
        configData.GameStateData = gameStateData;
        configData.GameData = gameData;
        ResourceSaver.Save(configData);

        gameDataLoadController.GameData = gameData;
        gameDataLoadController.GameStateData = (GameStateData)gameStateData.Duplicate();
    }
}
