using Godot;
using System;

public partial class GameDataSaveController : Node
{
    public void Save(GameData gameData = null)
    {
        gameData = gameData ?? GetNode<GameDataLoadController>("/root/GameDataLoadController").GameData;

        if (gameData == null)
        {
            GD.PushError($"GameDataController: SaveGame(): Failed to save, GameData is null");
            return;
        }

        GD.Print($"GameDataController: SaveGame(): Saving game [{gameData.ResourceName}]");
        string gameDataDir = gameData.ResourcePath.Substring(0, gameData.ResourcePath.Length - 5);
        string gameStateDataDir = $"{gameDataDir}/game_states";

        // game data doesn't exist yet, initialise folders
        if (!FileAccess.FileExists(gameData.ResourcePath))
        {
            DirAccess.MakeDirRecursiveAbsolute(gameStateDataDir); // game state data directory
        }

        GameStateData gameStateData = new GameStateData();
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
    }
}
