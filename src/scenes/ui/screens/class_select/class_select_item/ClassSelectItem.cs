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
    }

    private void onButtonPressed()
    {
        EmitSignal(nameof(Pressed), CharacterClassData);
    }
}
