using Godot;

public partial class CharacterData : Resource
{
    [Export]
    public Vector3 Position;

    public static CharacterData FromCharacter(Character character)
    {
        CharacterData characterData = new();
        characterData.ResourceName = character.Name;
        characterData.Position = character.Position;

        return characterData;
    }
}
