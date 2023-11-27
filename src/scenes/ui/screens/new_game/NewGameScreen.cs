using Godot;

public partial class NewGameScreen : Screen
{
    public override void Enter()
    {
        GetNode<LineEdit>("NameLineEdit").Clear();
        GetNode<LineEdit>("NameLineEdit").GrabFocus();
    }

    private void onCreateButtonPressed()
    {
        string name = GetNode<LineEdit>("NameLineEdit").Text;

        if (name.Length == 0)
        {
            GD.PushWarning("NewGameScreen: onCreateButtonPressed(): Failed, name is empty");
            return;
        }

        // create and load new game
        GameData gameData = GetNode<GameDataCreateController>("/root/GameDataCreateController").Create(name);
        GetNode<GameDataLoadController>("/root/GameDataLoadController").Load(gameData, gameData.GameStates[0]);
    }

    public override void Exit()
    {
        ScreenController.ChangeScreen("MainMenuScreen");
    }
}
