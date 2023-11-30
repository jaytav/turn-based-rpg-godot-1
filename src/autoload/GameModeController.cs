using Godot;

public partial class GameModeController : Node
{
    public void LoadGameMode(GameModeData gameModeData)
    {
        GetNode<ScreenController>("/root/ScreenController").ChangeScreen(gameModeData.ScreenName);
    }

    public void ChangeGameMode(GameModeData gameModeData)
    {
        GetNode<ScreenController>("/root/ScreenController").ChangeScreen(gameModeData.ScreenName);

        // update game mode data in current game state data, then save
        GameDataLoadController gameDataLoadController = GetNode<GameDataLoadController>("/root/GameDataLoadController");
        gameDataLoadController.GameStateData.GameMode = gameModeData;
        GetNode<GameDataSaveController>("/root/GameDataSaveController").Save();
    }
}
