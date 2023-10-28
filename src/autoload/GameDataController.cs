using Godot;

public partial class GameDataController : Node
{
    public GameData GameData;
    public GameStateData GameStateData;

    public void LoadGame(GameData gameData, GameStateData gameStateData)
    {
        GD.Print($"GameDataController: LoadGame(): Loading game [{gameData.ResourceName}], game state [{gameStateData.ResourceName}]");

        GameData = gameData;
        GameStateData = gameStateData;
    }

    public void SaveGame(GameData gameData = null)
    {
        gameData = gameData ?? GameData;

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
            DirAccess.MakeDirAbsolute("res://data"); // data directory
            DirAccess.MakeDirAbsolute(gameDataDir); // game data directory
            DirAccess.MakeDirAbsolute(gameStateDataDir); // game state data directory
        }

        GameStateData gameStateData = new GameStateData();
        gameStateData.ResourceName = $"game_state_{DirAccess.GetFilesAt(gameStateDataDir).Length.ToString()}";
        gameStateData.ResourcePath = $"{gameStateDataDir}/{gameStateData.ResourceName}.tres";
        gameData.GameStates.Insert(0, gameStateData);

        ResourceSaver.Save(gameStateData);
        ResourceSaver.Save(gameData);
    }

    public void DeleteGame(GameData gameData = null)
    {
        gameData = gameData ?? GameData;

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

        DirAccess.RemoveAbsolute(gameStateDataDir); // game states data directory
        DirAccess.RemoveAbsolute(gameDataDir); // game data directory
        DirAccess.RemoveAbsolute(gameData.ResourcePath); // game data
    }

    public void DeleteGameState(GameStateData gameStateData)
    {
        if (gameStateData == null)
        {
            GD.PushError($"GameDataController: DeleteGame(): Failed to delete, GameStateData is null");
            return;
        }

        GD.Print($"GameDataController: DeleteGameState(): Deleting game state [{gameStateData.ResourceName}]");
        DirAccess.RemoveAbsolute(gameStateData.ResourcePath);
    }
}
