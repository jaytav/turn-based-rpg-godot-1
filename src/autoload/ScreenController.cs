using Godot;
using Godot.Collections;

public partial class ScreenController : Node
{
    private Node _ui;
    private Screen _activeScreen;

    private Array<Screen> _activeScreenBuffer = new();
    private int _activeScreenBufferIndex = -1; // current screen (not in buffer)

    public override async void _Ready()
    {
        // wait for scene ready before changing screen
        await ToSignal(GetNode("/root/Main"), "ready");

        _ui = GetNode("/root/Main/UI");
        ChangeScreen("MainMenuScreen");
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (_activeScreen == null)
        {
            return;
        }

        if (@event.IsActionPressed("Exit"))
        {
            _activeScreen.Exit();
        }
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
            _activeScreenBuffer.Insert(0, _activeScreen);
            _activeScreen.Visible = false;
        }

        // reset buffer index, current screen not in buffer
        _activeScreenBufferIndex = -1;

        GD.Print($"ScreenController: ChangeScreen(): Changing screen [{screenName}]");

        // update current active screen, set visible and enter
        _activeScreen = _ui.GetNode<Screen>(screenName);
        _activeScreen.Visible = true;
        _activeScreen.Enter();
    }

    public void Back()
    {
        // increment active screen buffer index to track position in buffer
        _activeScreenBufferIndex++;

        _activeScreen.Visible = false;
        _activeScreen = _activeScreenBuffer[_activeScreenBufferIndex];

        GD.Print($"ScreenController: Back(): Going back to screen [{_activeScreen.Name}]");

        _activeScreen.Visible = true;
        _activeScreen.Enter();
    }
}
