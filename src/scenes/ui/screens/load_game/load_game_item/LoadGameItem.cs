using Godot;

public partial class LoadGameItem : VBoxContainer
{
    [Signal]
    public delegate void PressedEventHandler(GameData gameData, GameStateData gameStateData);

    public GameData GameData;

    private VBoxContainer _loadGameStateItems;
    private PackedScene _loadGameStateItem = GD.Load<PackedScene>("src/scenes/ui/screens/load_game/load_game_state_item/LoadGameStateItem.tscn");

    public override void _Ready()
    {
        GetNode<Button>("Button").ButtonGroup.Pressed += onButtonGroupPressed;
        GetNode<Label>("Button/Title").Text = GameData.ResourceName;

        _loadGameStateItems = GetNode<VBoxContainer>("LoadGameStateItems");

        foreach (Node loadGameStateItem in _loadGameStateItems.GetChildren())
        {
            loadGameStateItem.QueueFree();
        }

        ButtonGroup loadGameStateItemButtonGroup = new();
        loadGameStateItemButtonGroup.Pressed += onLoadGameStateButtonGroupPressed;

        foreach (GameStateData gameStateData in GameData.GameStates)
        {
            LoadGameStateItem loadGameStateItem = _loadGameStateItem.Instantiate<LoadGameStateItem>();
            loadGameStateItem.ButtonGroup = loadGameStateItemButtonGroup;
            loadGameStateItem.GameStateData = gameStateData;
            _loadGameStateItems.AddChild(loadGameStateItem);
        }

        _loadGameStateItems.GetChild<LoadGameStateItem>(0).SetPressedNoSignal(true);
        _loadGameStateItems.Hide();
    }

    private void onButtonGroupPressed(BaseButton button)
    {
        // toggle game state item visibility
        _loadGameStateItems.Visible = GetNode<Button>("Button") == button;

        // press first load game state item
        if (_loadGameStateItems.Visible)
        {
            _loadGameStateItems.GetChild<LoadGameStateItem>(0).ButtonPressed = true;
            onLoadGameStateButtonGroupPressed(_loadGameStateItems.GetChild<LoadGameStateItem>(0));
        }
    }

    private void onDeleteButtonPressed()
    {
        GetNode<GameDataController>("/root/GameDataController").DeleteGame(GameData);
        QueueFree();
    }

    private void onLoadGameStateButtonGroupPressed(BaseButton button)
    {
        EmitSignal(nameof(Pressed), GameData, ((LoadGameStateItem)button).GameStateData);
    }
}
