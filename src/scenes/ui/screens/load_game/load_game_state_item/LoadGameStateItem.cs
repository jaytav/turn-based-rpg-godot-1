using Godot;

public partial class LoadGameStateItem : Button
{
    public GameStateData GameStateData;

    public override void _Ready()
    {
        string title = GameStateData.ResourceName;

        if (GameStateData.Characters.Count > 0)
        {
            title += $" - {GameStateData.Characters[0].ResourceName}";
        }

        GetNode<Label>("Title").Text = title;
        GetNode<Label>("ModifiedTime").Text = Time.GetDatetimeStringFromUnixTime((long)FileAccess.GetModifiedTime(GameStateData.ResourcePath)).Replace('T', ' ');
    }
}
