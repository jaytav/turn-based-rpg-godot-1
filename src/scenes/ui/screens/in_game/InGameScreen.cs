using Godot;

public partial class InGameScreen : Screen
{
    private void onBattleButtonPressed()
    {
        GetNode<BattleController>("/root/BattleController").StartBattle();
    }
}
