using Godot;

public partial class LoadGameScreen : Screen
{
    private GameData _selectedGameData;
    private GameStateData _selectedGameStateData;
    private PackedScene _loadGameItem = GD.Load<PackedScene>("src/scenes/ui/screens/load_game/load_game_item/LoadGameItem.tscn");

    public override void _Process(double delta)
    {
        // disable delete and load buttons when _selectedGameStateData is not set
        GetNode<Button>("LoadButton").Disabled = !GodotObject.IsInstanceValid(_selectedGameData);
    }

    public override void Enter()
    {
        // refresh LoadGameDataItems
        foreach (LoadGameItem loadGameDataItem in GetNode<VBoxContainer>("LoadGameDataItems/ScrollContainer/VBoxContainer").GetChildren())
        {
            loadGameDataItem.QueueFree();
        }

        // add to same button group so that they know when each other are pressed
        ButtonGroup loadGameItemButtonGroup = new ButtonGroup();

        string[] files = DirAccess.GetFilesAt("res://data");

        // sort game data files by modified date desc
        System.Array.Sort(files, (string a, string b) =>
            FileAccess.GetModifiedTime($"res://data/{a}") < FileAccess.GetModifiedTime($"res://data/{b}") ? 1 : 0
        );

        foreach (string file in files)
        {
            LoadGameItem loadGameItem = _loadGameItem.Instantiate<LoadGameItem>();
            loadGameItem.GameData = GD.Load<GameData>($"res://data/{file}");
            loadGameItem.Pressed += onLoadGameItemPressed;
            loadGameItem.GetNode<Button>("Button").ButtonGroup = loadGameItemButtonGroup;
            GetNode<VBoxContainer>("LoadGameDataItems/ScrollContainer/VBoxContainer").AddChild(loadGameItem);
        }
    }

    private void onLoadGameItemPressed(GameData gameData, GameStateData gameStateData)
    {

        _selectedGameData = gameData;
        _selectedGameStateData = gameStateData;

        GetNode<Label>("LoadGameStateDetails/Title").Text = gameData.ResourceName;
        GetNode<Label>("LoadGameStateDetails/Subtitle").Text = gameStateData.ResourceName;
        GetNode<Label>("LoadGameStateDetails/ModifiedTime").Text = Time.GetDatetimeStringFromUnixTime((long)FileAccess.GetModifiedTime(gameStateData.ResourcePath)).Replace('T', ' ');
    }

    private void onLoadButtonPressed()
    {
        GetNode<GameDataController>("/root/GameDataController").LoadGame(_selectedGameData, _selectedGameStateData);
    }
}
