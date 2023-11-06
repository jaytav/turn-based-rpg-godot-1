using Godot;

public partial class MainMenuScreen : Screen
{
    public override void Enter()
    {
        // enable load game button only when game data exists
        bool gameDataExists = DirAccess.DirExistsAbsolute("res://data/games") && DirAccess.GetFilesAt("res://data/games").Length > 0;

        GetNode<Button>("LoadGameButton").Disabled = !gameDataExists;
        GetNode<Button>("LoadGameButton").MouseDefaultCursorShape = gameDataExists ? CursorShape.PointingHand : CursorShape.Arrow;

        // enable continue only when config game data and game state data exists
        ConfigData configData = GetNode<ConfigController>("/root/ConfigController").ConfigData;
        bool configGameDataExists = configData.GameData != null && configData.GameStateData != null;

        GetNode<Button>("ContinueButton").Disabled = !configGameDataExists;
        GetNode<Button>("ContinueButton").MouseDefaultCursorShape = configGameDataExists ? CursorShape.PointingHand : CursorShape.Arrow;
        GetNode<Label>("ContinueButtonDetail").Text = configGameDataExists ? configData.GameData.ResourceName : "";
        GetNode<Label>("ContinueButtonDetail").Visible = false;
    }

    private void onNewGameButtonPressed()
    {
        ScreenController.ChangeScreen("NewGameScreen");
    }

    private void onLoadGameButtonPressed()
    {
        ScreenController.ChangeScreen("LoadGameScreen");
    }

    private void onContinueButtonPressed()
    {
        ConfigData configData = GetNode<ConfigController>("/root/ConfigController").ConfigData;
        GetNode<GameDataLoadController>("/root/GameDataLoadController").Load(configData.GameData, configData.GameStateData);
    }

    private void onContinueButtonMouseEntered()
    {
        GetNode<AnimationPlayer>("AnimationPlayer").Play("fade_in_continue_button_detail");
    }

    private void onContinueButtonMouseExited()
    {
        GetNode<AnimationPlayer>("AnimationPlayer").PlayBackwards("fade_in_continue_button_detail");
    }
}
