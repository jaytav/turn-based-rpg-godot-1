using Godot;

public partial class NewGameScreen : Screen
{
    private LineEdit _nameLineEdit;

    private GameDataController _gameDataController;

    public override void _Ready()
    {
        base._Ready();
        _nameLineEdit = GetNode<LineEdit>("NameLineEdit");
        _gameDataController = GetNode<GameDataController>("/root/GameDataController");
    }

    public override void Enter()
    {
        _nameLineEdit.Clear();
    }

    private void onCreateButtonPressed()
    {
        if (_nameLineEdit.Text.Length == 0)
        {
            GD.PushWarning("NewGameScreen: onCreateButtonPressed(): Failed, name is empty");
            return;
        }

        // create and save game
        GameData gameData = new GameData();
        gameData.ResourceName = _nameLineEdit.Text;
        gameData.ResourcePath = $"data/{gameData.GetInstanceId()}.tres";
        _gameDataController.ActiveGameData = gameData;
        _gameDataController.SaveGame();

        ScreenController.ChangeScreen("LoadGameScreen");
    }
}
