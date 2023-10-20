using Godot;

public partial class NewGameScreen : Control
{
    private LineEdit _nameLineEdit;

    public override void _Ready()
    {
        _nameLineEdit = GetNode<LineEdit>("NameLineEdit");
    }

    private void onButtonPressed()
    {
        if (_nameLineEdit.Text.Length == 0)
        {
            GD.Print("NewGameScreen: onButtonPressed(): Failed, name is empty");
            return;
        }

        // create and save game
        GameData gameData = new GameData();
        gameData.ResourceName = _nameLineEdit.Text;
        gameData.ResourcePath = $"data/{gameData.GetInstanceId()}.tres";

        ResourceSaver.Save(gameData);
    }
}
