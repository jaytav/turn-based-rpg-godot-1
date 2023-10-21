using Godot;

public partial class MainMenuScreen : Screen
{
    private void onNewGameButtonPressed()
    {
        ScreenController.ChangeScreen("NewGameScreen");
    }

    private void onLoadGameButtonPressed()
    {
        ScreenController.ChangeScreen("LoadGameScreen");
    }
}
