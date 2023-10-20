using Godot;

public partial class LoadGameDataItem : Control
{
    [Signal]
    public delegate void PressedEventHandler(LoadGameDataItem loadGameDataItem);

    public GameData GameData;

    public override void _Ready()
    {
        GetNode<Button>("Button").Text = GameData.ResourceName;
    }

    private void onButtonPressed()
    {
        EmitSignal(nameof(Pressed), this);
    }
}
