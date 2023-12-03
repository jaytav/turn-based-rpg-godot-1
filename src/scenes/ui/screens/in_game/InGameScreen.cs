using Godot;

public partial class InGameScreen : Screen
{
    public override void Exit()
    {
        // change game mode to class_select
        GameModeData changeGameMode = GameModeData.ByName("class_select");
        GetNode<GameModeController>("/root/GameModeController").ChangeGameMode(changeGameMode);
    }

    private void onBattleButtonPressed()
    {
        GetNode<BattleController>("/root/BattleController").StartBattle();
    }
}
