using Godot;

public partial class GameModeData : Resource
{
    [Export]
    public string ScreenName;

    public static GameModeData ByName(string name)
    {
        return GD.Load<GameModeData>($"res://src/data/game_modes/{name}.tres");
    }
}
