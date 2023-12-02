using Godot;

public partial class BattleController : Node
{
    public void StartBattle()
    {
        // change game mode to battle
        GameModeData changeGameMode = GameModeData.ByName("battle");
        GetNode<GameModeController>("/root/GameModeController").ChangeGameMode(changeGameMode);
    }
}
