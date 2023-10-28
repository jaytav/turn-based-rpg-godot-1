using Godot;

public partial class MainMenuScreen : Screen
{
    public override void Enter()
    {
        // enable load game button only when game data exists
        GetNode<Button>("LoadGameButton").Disabled = !DirAccess.DirExistsAbsolute("res://data/games")
            || DirAccess.GetFilesAt("res://data/games").Length == 0;
        GetNode<Button>("LoadGameButton").MouseDefaultCursorShape = GetNode<Button>("LoadGameButton").Disabled ? CursorShape.Arrow : CursorShape.PointingHand;

        // enable continue only when config game data and game state data exists
        ConfigData configData = GetNode<ConfigController>("/root/ConfigController").ConfigData;
        GetNode<Button>("ContinueButton").Disabled = configData.GameData == null || configData.GameStateData == null;
        GetNode<Button>("ContinueButton").MouseDefaultCursorShape = GetNode<Button>("ContinueButton").Disabled ? CursorShape.Arrow : CursorShape.PointingHand;
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
        GetNode<GameDataController>("/root/GameDataController").LoadGame(configData.GameData, configData.GameStateData);
    }
}
