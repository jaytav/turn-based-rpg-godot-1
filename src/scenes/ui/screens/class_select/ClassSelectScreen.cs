using Godot;

public partial class ClassSelectScreen : Screen
{
    public override void Enter()
    {
        string[] files = DirAccess.GetFilesAt("res://src/data/character/classes");

        foreach (string file in files)
        {
            CharacterClassData characterClassData = GD.Load<CharacterClassData>($"res://src/data/character/classes/{file}");
            // use characterClassData to create selectable items for player to choose class here
        }
    }

    public override void Exit()
    {
        ScreenController.ChangeScreen("MainMenuScreen");
    }
}
