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

        saveCharacters(gameStateData);
        saveEnemies(gameStateData);
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

    private void saveCharacters(GameStateData gameStateData)
    {
        gameStateData.Characters.Clear();
        Node charactersContainer = GetNode("/root/Main/World/Characters");

        foreach (Character character in charactersContainer.GetChildren())
        {
            gameStateData.Characters.Add(CharacterData.FromCharacter(character));
        }
    }

    private void saveEnemies(GameStateData gameStateData)
    {
        gameStateData.Enemies.Clear();
        Node enemiesContainer = GetNode("/root/Main/World/Enemies");

        foreach (Character enemy in enemiesContainer.GetChildren())
        {
            gameStateData.Enemies.Add(CharacterData.FromCharacter(enemy));
        }
    }
}
