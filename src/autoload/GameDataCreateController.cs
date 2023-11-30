using Godot;

public partial class GameDataCreateController : Node
{
    public GameData Create(string name)
    {
        GD.Print($"GameDataCreateController: Create(): Creating game [{name}]");

        // create game data
        GameData gameData = new();
        gameData.ResourceName = name;
        gameData.ResourcePath = $"res://data/games/{gameData.GetInstanceId()}.tres";

        string gameDataDir = gameData.ResourcePath.Substring(0, gameData.ResourcePath.Length - 5);
        string gameStateDataDir = $"{gameDataDir}/game_states";

        // initialise game data directories
        DirAccess.MakeDirRecursiveAbsolute(gameStateDataDir);

        // create game state data
        GameStateData gameStateData = new();
        gameStateData.ResourceName = DirAccess.GetFilesAt(gameStateDataDir).Length.ToString();
        gameStateData.ResourcePath = $"{gameStateDataDir}/{gameStateData.ResourceName}.tres";
        gameStateData.GameMode = GameModeData.ByName("class_select");
        gameStateData.Character = new();
        gameData.GameStates.Insert(0, gameStateData);

        // save game data and state data files and into config
        ResourceSaver.Save(gameStateData);
        ResourceSaver.Save(gameData);

        return gameData;
    }
}
