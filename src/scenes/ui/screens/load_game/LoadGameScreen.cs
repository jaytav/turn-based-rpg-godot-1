using Godot;

public partial class LoadGameScreen : Screen
{
    private GameData _selectedGameData;
    private GameStateData _selectedGameStateData;
    private PackedScene _loadGameItem = GD.Load<PackedScene>("res://src/scenes/ui/screens/load_game/load_game_item/LoadGameItem.tscn");

    public override void Enter()
    {
        // refresh LoadGameDataItems
        foreach (LoadGameItem loadGameDataItem in GetNode<VBoxContainer>("LoadGameDataItems/ScrollContainer/VBoxContainer").GetChildren())
        {
            loadGameDataItem.QueueFree();
        }

        // add to same button group so that they know when each other are pressed
        ButtonGroup loadGameItemButtonGroup = new ButtonGroup();

        string[] files = DirAccess.GetFilesAt("res://data/games");

        // sort game data files by modified date desc
        System.Array.Sort(files, (string a, string b) =>
            FileAccess.GetModifiedTime($"res://data/games/{a}") < FileAccess.GetModifiedTime($"res://data/games/{b}") ? 1 : 0
        );

        string firstFile = files[0];

        foreach (string file in files)
        {
            LoadGameItem loadGameItem = _loadGameItem.Instantiate<LoadGameItem>();
            loadGameItem.GameData = GD.Load<GameData>($"res://data/games/{file}");
            loadGameItem.Pressed += onLoadGameItemPressed;
            loadGameItem.GameData.Deleted += onLoadGameItemGameDataDeleted;
            loadGameItem.GetNode<Button>("Button").ButtonGroup = loadGameItemButtonGroup;
            GetNode<VBoxContainer>("LoadGameDataItems/ScrollContainer/VBoxContainer").AddChild(loadGameItem);

            if (file == firstFile)
            {
                loadGameItem.GetNode<Button>("Button").ButtonPressed = true;
            }
        }
    }

    private void onLoadGameItemPressed(GameData gameData, GameStateData gameStateData)
    {
        _selectedGameData = gameData;
        _selectedGameStateData = gameStateData;

        // enable load game button
        GetNode<Button>("LoadButton").Disabled = false;

        // display load game state details
        Control loadGameStateDetailsContainer = GetNode<Control>("LoadGameStateDetails/Container");
        loadGameStateDetailsContainer.GetNode<Label>("Title").Text = gameData.ResourceName;
        loadGameStateDetailsContainer.GetNode<Label>("Subtitle").Text = gameStateData.GameMode.ResourceName;
        loadGameStateDetailsContainer.GetNode<Label>("ModifiedTime").Text = Time.GetDatetimeStringFromUnixTime((long)FileAccess.GetModifiedTime(gameStateData.ResourcePath)).Replace('T', ' ');
        loadGameStateDetailsContainer.Visible = true;
    }

    private void onLoadGameItemGameDataDeleted(GameData gameData)
    {
        if (gameData != _selectedGameData)
        {
            return;
        }

        // selected game data deleted, unset variables and hide load game state details
        _selectedGameData = null;
        _selectedGameStateData = null;
        GetNode<Control>("LoadGameStateDetails/Container").Visible = false;
        GetNode<Button>("LoadButton").Disabled = true;
    }

    private void onLoadButtonPressed()
    {
        GetNode<GameDataLoadController>("/root/GameDataLoadController").Load(_selectedGameData, _selectedGameStateData);
    }
}
