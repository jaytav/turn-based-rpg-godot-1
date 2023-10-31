using Godot;

public partial class CharacterCreateScreen : Screen
{
    private CharacterData _characterData = new();

    public override void Enter()
    {
        GetNode<LineEdit>("NameLineEdit").Text = null;
        GetNode<LineEdit>("NameLineEdit").GrabFocus();
    }

    private void onCreateButtonPressed()
    {
        string name = GetNode<LineEdit>("NameLineEdit").Text;

        if (name.Length == 0)
        {
            GD.PushWarning("NewGameScreen: onCreateButtonPressed(): Failed, name is empty");
            return;
        }

        GD.Print($"CharacterCreateScreen: onCreateButtonPressed(): Creating character [{name}]");
        CharacterData characterData = new CharacterData();
        characterData.ResourceName = name;

        GameDataLoadController gameDataLoadController = GetNode<GameDataLoadController>("/root/GameDataLoadController");
        gameDataLoadController.GameStateData.Characters.Add(characterData);
        gameDataLoadController.GameStateData.GameMode = GD.Load<GameModeData>("res://src/data/game_modes/open_world.tres");
        GetNode<GameDataSaveController>("/root/GameDataSaveController").Save();

        ScreenController.ChangeScreen(gameDataLoadController.GameStateData.GameMode.Screen);
    }
}
