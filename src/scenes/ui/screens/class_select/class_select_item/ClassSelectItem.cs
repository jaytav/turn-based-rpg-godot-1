using Godot;

public partial class ClassSelectItem : Control
{
    [Signal]
    public delegate void PressedEventHandler(CharacterClassData characterClassData);

    public CharacterClassData CharacterClassData;

    public override void _Ready()
    {
        // allows keeping in scene to view in editor
        if (CharacterClassData == null) {
            QueueFree();
            return;
        }

        GetNode<Button>("Button").Pressed += onButtonPressed;
        GetNode<Label>("Button/Title").Text = CharacterClassData.ResourceName;

        // clear placeholder health point texture rects
        foreach (Node node in GetNode("Button/HealthPointsContainer").GetChildren())
        {
            node.QueueFree();
        }

        for (int i = 0; i < CharacterClassData.Stats.HealthPoints.Value; i++)
        {
            TextureRect healthPointTextureRect = new();
            healthPointTextureRect.Texture = CharacterClassData.Stats.HealthPoints.Icon;
            healthPointTextureRect.SizeFlagsVertical = SizeFlags.ShrinkCenter;
            healthPointTextureRect.Modulate = CharacterClassData.Stats.HealthPoints.Modulate;
            GetNode("Button/HealthPointsContainer").AddChild(healthPointTextureRect);
        }
    }

    private void onButtonPressed()
    {
        EmitSignal(nameof(Pressed), CharacterClassData);
    }
}
