using Godot;

public partial class GameDataLoadController : Node
{
    public GameData GameData;
    public GameStateData GameStateData;

    private PackedScene _character = GD.Load<PackedScene>("res://src/character/Character.tscn");
    private PackedScene _enemy = GD.Load<PackedScene>("res://src/enemy/Enemy.tscn");

    public void Load(GameData gameData, GameStateData gameStateData)
    {
        GD.Print($"GameDataLoadController: Load(): Loading game [{gameData.ResourceName}], game state [{gameStateData.ResourceName}]");
        GameData = gameData;
        GameStateData = gameStateData;

        // set config game data and game state data on load
        ConfigData configData = GetNode<ConfigController>("/root/ConfigController").ConfigData;
        configData.GameStateData = GameStateData;
        configData.GameData = GameData;
        ResourceSaver.Save(configData);

        // duplicate game state on load, prevent altering already saved game state
        GameStateData = (GameStateData)gameStateData.Duplicate();
        LoadCharacters();
        LoadEnemies();

        // set screen based on game mode data
        GetNode<ScreenController>("/root/ScreenController").ChangeScreen(GameStateData.GameMode.Screen);
    }

    public void LoadCharacters()
    {
        // load characters
        Node charactersContainer = GetNode("/root/Main/World/Characters");

        foreach (CharacterData characterData in GameStateData.Characters)
        {
            Character character = _character.Instantiate<Character>();
            character.Position = characterData.Position;
            character.Name = characterData.ResourceName;
            charactersContainer.AddChild(character);

            GD.Print($"GameDataLoadController: LoadCharacters(): {character.Name}");
        }
    }

    public void LoadEnemies()
    {
        // load characters
        Node enemiesContainer = GetNode("/root/Main/World/Characters");

        foreach (CharacterData enemyData in GameStateData.Enemies)
        {
            Enemy enemy = _enemy.Instantiate<Enemy>();
            enemy.Position = enemyData.Position;
            enemy.Name = enemyData.ResourceName;
            enemiesContainer.AddChild(enemy);

            GD.Print($"GameDataLoadController: LoadEnemies(): {enemy.Name}");
        }
    }
}
