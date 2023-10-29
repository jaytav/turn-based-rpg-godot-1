using Godot;

public partial class GameDataDeleteController : Node
{
    public void Delete(GameData gameData = null)
    {
        gameData = gameData ?? GetNode<GameDataLoadController>("/root/GameDataLoadController").GameData;

        if (gameData == null)
        {
            GD.PushError($"GameDataController: DeleteGame(): Failed to delete, GameData is null");
            return;
        }

        GD.Print($"GameDataController: DeleteGame(): Deleting game [{gameData.ResourceName}]");
        string gameDataDir = gameData.ResourcePath.Substring(0, gameData.ResourcePath.Length - 5);
        string gameStateDataDir = $"{gameDataDir}/game_states";

        foreach (string file in DirAccess.GetFilesAt(gameStateDataDir))
        {
            DirAccess.RemoveAbsolute($"{gameStateDataDir}/{file}"); // game state data
        }

        ConfigData configData = GetNode<ConfigController>("/root/ConfigController").ConfigData;

        // if config has deleted game data referenced, unset game data and game state data
        if (configData.GameData == gameData)
        {
            configData.GameData = null;
            configData.GameStateData = null;
            ResourceSaver.Save(configData);
        }

        DirAccess.RemoveAbsolute(gameStateDataDir); // game states data directory
        DirAccess.RemoveAbsolute(gameDataDir); // game data directory
        DirAccess.RemoveAbsolute(gameData.ResourcePath); // game data
    }
}
