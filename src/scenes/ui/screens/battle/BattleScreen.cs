using Godot;

public partial class BattleScreen : Screen
{
    public override void Exit()
    {
        // change game mode to class_select
        GameModeData changeGameMode = GameModeData.ByName("in_game");
        GetNode<GameModeController>("/root/GameModeController").ChangeGameMode(changeGameMode);
    }
}
