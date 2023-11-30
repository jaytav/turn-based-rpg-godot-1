using Godot;

public partial class ClassSelectItem : Control
{
    [Signal]
    public delegate void PressedEventHandler(CharacterClassData characterClassData);

    public CharacterClassData CharacterClassData;

    public override void _Ready()
    {
        GetNode<Button>("Button").Pressed += onButtonPressed;
        GetNode<Label>("Button/Title").Text = CharacterClassData.ResourceName;
        GetNode<Label>("Button/HealthPoints").Text = CharacterClassData.Stats.HealthPoints.Value.ToString();
    }

    private void onButtonPressed()
    {
        EmitSignal(nameof(Pressed), CharacterClassData);
    }
}
