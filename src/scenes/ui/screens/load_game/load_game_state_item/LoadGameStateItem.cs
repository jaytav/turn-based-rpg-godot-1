using Godot;

public partial class LoadGameStateItem : Button
{
    public GameStateData GameStateData;

    public override void _Ready()
    {
        GetNode<Label>("Title").Text = GameStateData.ResourceName;
        GetNode<Label>("ModifiedTime").Text = Time.GetDatetimeStringFromUnixTime((long)FileAccess.GetModifiedTime(GameStateData.ResourcePath)).Replace('T', ' ');
    }
}
