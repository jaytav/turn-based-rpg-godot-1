using Godot;

public partial class MainMenuScreen : Screen
{
    public override void Enter()
    {
        // enable load game button only when game data exists
        GetNode<Button>("LoadGameButton").Disabled = DirAccess.GetFilesAt("res://data").Length == 0;
        GetNode<Button>("LoadGameButton").MouseDefaultCursorShape = GetNode<Button>("LoadGameButton").Disabled ? CursorShape.Arrow : CursorShape.PointingHand;
    }

    private void onNewGameButtonPressed()
    {
        ScreenController.ChangeScreen("NewGameScreen");
    }

    private void onLoadGameButtonPressed()
    {
        ScreenController.ChangeScreen("LoadGameScreen");
    }
}
