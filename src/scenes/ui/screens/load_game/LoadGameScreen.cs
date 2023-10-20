using Godot;

public partial class LoadGameScreen : Control
{
    private LoadGameDataItem _activeLoadGameDataItem;
    private PackedScene _loadGameDataItem = GD.Load<PackedScene>("src/scenes/ui/screens/load_game/LoadGameDataItem.tscn");
    private VBoxContainer _loadGameDataItems;

    private Button _deleteButton;
    private Button _loadButton;

    public override void _Ready()
    {
        _loadGameDataItems = GetNode<VBoxContainer>("LoadGameDataItems/ScrollContainer/VBoxContainer");

        foreach (string file in DirAccess.GetFilesAt("data"))
        {
            LoadGameDataItem loadGameDataItem = _loadGameDataItem.Instantiate<LoadGameDataItem>();
            loadGameDataItem.GameData = GD.Load<GameData>($"data/{file}");
            loadGameDataItem.Pressed += onLoadGameDataItemPressed;
            _loadGameDataItems.AddChild(loadGameDataItem);
        }

        _deleteButton = GetNode<Button>("DeleteButton");
        _loadButton = GetNode<Button>("LoadButton");
    }

    public override void _Process(double delta)
    {
        _deleteButton.Disabled = !GodotObject.IsInstanceValid(_activeLoadGameDataItem);
        _loadButton.Disabled = !GodotObject.IsInstanceValid(_activeLoadGameDataItem);
    }

    private void onLoadGameDataItemPressed(LoadGameDataItem loadGameDataItem)
    {
        _activeLoadGameDataItem = loadGameDataItem;
    }

    private void onLoadButtonPressed()
    {
        if (_activeLoadGameDataItem == null)
        {
            GD.Print("LoadGameScreen: onLoadButtonPressed(): Failed to load, no _activeLoadGameDataItem");
            return;
        }

        GD.Print($"LoadGameScreen: onLoadButtonPressed(): Loading game data {_activeLoadGameDataItem.GameData.ResourceName}");
        // load game here
    }

    private void onDeleteButtonPressed()
    {
        if (_activeLoadGameDataItem == null)
        {
            GD.Print("LoadGameScreen: onDeleteButtonPressed(): Failed to delete, no _activeLoadGameDataItem");
            return;
        }

        GD.Print($"LoadGameScreen: onDeleteButtonPressed(): Deleting game data ({_activeLoadGameDataItem.GameData.ResourceName})");
        DirAccess.RemoveAbsolute(_activeLoadGameDataItem.GameData.ResourcePath);
        _activeLoadGameDataItem.QueueFree();
    }
}
