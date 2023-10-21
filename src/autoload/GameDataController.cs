using Godot;

public partial class GameDataController : Node
{
    public GameData ActiveGameData;

    public void LoadGame()
    {
        GD.Print($"GameDataController: LoadGame(): {ActiveGameData.ResourceName}");
    }

    public void SaveGame()
    {
        GD.Print($"GameDataController: SaveGame(): {ActiveGameData.ResourceName}");
        ResourceSaver.Save(ActiveGameData);
    }

    public void DeleteGame()
    {
        GD.Print($"GameDataController: DeleteGame(): {ActiveGameData.ResourceName}");
        DirAccess.RemoveAbsolute(ActiveGameData.ResourcePath);
    }
}
