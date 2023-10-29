using Godot;

public partial class NewGameScreen : Screen
{
    public override void Enter()
    {
        GetNode<LineEdit>("NameLineEdit").Clear();
        GetNode<LineEdit>("NameLineEdit").GrabFocus();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_text_submit"))
        {
            GetNode<Button>("CreateButton").EmitSignal("pressed");
        }
    }

    private void onCreateButtonPressed()
    {
        if (GetNode<LineEdit>("NameLineEdit").Text.Length == 0)
        {
            GD.PushWarning("NewGameScreen: onCreateButtonPressed(): Failed, name is empty");
            return;
        }

        // create and save game
        GameData gameData = new GameData();
        gameData.ResourceName = GetNode<LineEdit>("NameLineEdit").Text;
        gameData.ResourcePath = $"res://data/games/{gameData.GetInstanceId()}.tres";
        GetNode<GameDataSaveController>("/root/GameDataSaveController").Save(gameData);

        ScreenController.ChangeScreen("LoadGameScreen");
    }
}
