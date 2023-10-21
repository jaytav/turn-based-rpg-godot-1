using Godot;

public partial class LoadGameScreen : Screen
{
    private LoadGameDataItem _activeLoadGameDataItem;
    private PackedScene _loadGameDataItem = GD.Load<PackedScene>("src/scenes/ui/screens/load_game/LoadGameDataItem.tscn");
    private VBoxContainer _loadGameDataItems;

    private Button _deleteButton;
    private Button _loadButton;

    private GameDataController _gameDataController;

    public override void _Ready()
    {
        base._Ready();
        _loadGameDataItems = GetNode<VBoxContainer>("LoadGameDataItems/ScrollContainer/VBoxContainer");
        _deleteButton = GetNode<Button>("DeleteButton");
        _loadButton = GetNode<Button>("LoadButton");
        _gameDataController = GetNode<GameDataController>("/root/GameDataController");
    }

    public override void Enter()
    {
        // refresh LoadGameDataItems
        foreach (LoadGameDataItem loadGameDataItem in _loadGameDataItems.GetChildren())
        {
            loadGameDataItem.QueueFree();
        }

        foreach (string file in DirAccess.GetFilesAt("data"))
        {
            LoadGameDataItem loadGameDataItem = _loadGameDataItem.Instantiate<LoadGameDataItem>();
            loadGameDataItem.GameData = GD.Load<GameData>($"data/{file}");
            loadGameDataItem.Pressed += onLoadGameDataItemPressed;
            _loadGameDataItems.AddChild(loadGameDataItem);
        }
    }

    public override void _Process(double delta)
    {
        // disable delete and load buttons when _activeLoadGameDataItem is not set
        _deleteButton.Disabled = !GodotObject.IsInstanceValid(_activeLoadGameDataItem);
        _loadButton.Disabled = !GodotObject.IsInstanceValid(_activeLoadGameDataItem);
    }

    private void onLoadGameDataItemPressed(LoadGameDataItem loadGameDataItem)
    {
        _activeLoadGameDataItem = loadGameDataItem;
    }

    private void onLoadButtonPressed()
    {
        _gameDataController.ActiveGameData = _activeLoadGameDataItem.GameData;
        _gameDataController.LoadGame();
    }

    private void onDeleteButtonPressed()
    {
        _gameDataController.ActiveGameData = _activeLoadGameDataItem.GameData;
        _gameDataController.DeleteGame();
        _activeLoadGameDataItem.QueueFree();
    }
}
