using Godot;

public partial class ClassSelectScreen : Screen
{
    private CharacterClassData _selectedCharacterClassData;
    private PackedScene _classSelectItem = GD.Load<PackedScene>("res://src/scenes/ui/screens/class_select/class_select_item/ClassSelectItem.tscn");

    public override void Enter()
    {
        // refresh ClassSelectItem
        foreach (Node classSelectItem in GetNode("ClassSelectItems").GetChildren()) {
            classSelectItem.QueueFree();
        }

        GetNode<Button>("StartButton").Disabled = true;
        ButtonGroup classSelectItemButtonGroup = new ButtonGroup();
        string[] files = DirAccess.GetFilesAt("res://src/data/character/classes");

        foreach (string file in files)
        {
            CharacterClassData characterClassData = GD.Load<CharacterClassData>($"res://src/data/character/classes/{file}");
            ClassSelectItem classSelectItem = _classSelectItem.Instantiate<ClassSelectItem>();
            classSelectItem.Pressed += onClassSelectItemPressed;
            classSelectItem.CharacterClassData = characterClassData;
            classSelectItem.GetNode<Button>("Button").ButtonGroup = classSelectItemButtonGroup;
            GetNode<HBoxContainer>("ClassSelectItems").AddChild(classSelectItem);
        }
    }

    public override void Exit()
    {
        ScreenController.ChangeScreen("MainMenuScreen");
    }

    private void onStartButtonPressed()
    {
        // update character class data to the selected
        GameDataLoadController gameDataLoadController = GetNode<GameDataLoadController>("/root/GameDataLoadController");
        gameDataLoadController.GameStateData.Character.Class = _selectedCharacterClassData;

        // change game mode to in_game
        GameModeData changeGameMode = GameModeData.ByName("in_game");
        GetNode<GameModeController>("/root/GameModeController").ChangeGameMode(changeGameMode);
    }

    private void onClassSelectItemPressed(CharacterClassData characterClassData)
    {
        _selectedCharacterClassData = characterClassData;
        GetNode<Button>("StartButton").Disabled = false;
    }
}
