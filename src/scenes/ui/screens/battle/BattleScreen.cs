using Godot;

public partial class BattleScreen : Screen
{
    public override void Enter()
    {
        GetNode<Control>("ExitMenu").Hide();
    }

    public override void Exit()
    {
        GetNode<Control>("ExitMenu").Visible = !GetNode<Control>("ExitMenu").Visible;
    }

    private void onExitMenuResumeButtonPressed()
    {
        GetNode<Control>("ExitMenu").Visible = false;
    }

    private void onExitMenuLoadButtonPressed()
    {
        ScreenController.ChangeScreen("LoadGameScreen");
    }

    private void onExitMenuExitButtonPressed()
    {
        GetTree().Quit();
    }
}
