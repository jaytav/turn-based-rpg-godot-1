using Godot;

public partial class ScreenController : Node
{
    private Node _ui;
    private Screen _activeScreen;

    public override void _Ready()
    {
        _ui = GetNode("/root/Main/UI");
        ChangeScreen("MainMenuScreen");
    }

    public void ChangeScreen(string screenName)
    {
        if (_ui.GetNodeOrNull(screenName) == null)
        {
            GD.PushError($"ScreenController: ChangeScreen(): Failed to change screen to [{screenName}], doesn't exist");
            return;
        }

        // set current active screen invisible
        if (_activeScreen != null)
        {
            _activeScreen.Visible = false;
        }

        GD.Print($"ScreenController: ChangeScreen(): Changing screen [{screenName}]");

        // update current active screen, set visible and enter
        _activeScreen = _ui.GetNode<Screen>(screenName);
        _activeScreen.Visible = true;
        _activeScreen.Enter();
    }
}