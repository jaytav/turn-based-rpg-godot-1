using Godot;
using Godot.Collections;

public partial class CharacterOpenWorldController : Node
{
    private Array<Character> _activeCharacters = new();

    public override void _Ready()
    {
        GetNode("/root/Main/World/Characters").ChildEnteredTree += onCharactersContainerChildEnteredTree;
    }

    public override void _Input(InputEvent @event)
    {
        if (_activeCharacters.Count == 0)
        {
            return;
        }

        if (@event.IsActionPressed("ActionPrimary"))
        {
            Vector3 mousePositionInWorld = GetNode<MouseController>("/root/MouseController").PositionInWorld;

            foreach (Character character in _activeCharacters)
            {
                character.MoveTo(mousePositionInWorld);
            }
        }
    }

    private void onCharactersContainerChildEnteredTree(Node node)
    {
        Character character = (Character)node;

        if (_activeCharacters.Count == 0)
        {
            GetNode<CameraController>("/root/CameraController").FollowNode = character;
        }

        _activeCharacters.Add(character);
    }
}
