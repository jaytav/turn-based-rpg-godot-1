using Godot;

public partial class ConfigController : Node
{
    public ConfigData ConfigData;

    private string _configFilePath = "res://data/config.tres";

    public override void _Ready()
    {
        DirAccess.MakeDirRecursiveAbsolute("res://data");

        // if _configFilePath doesn't exist, create
        if (!FileAccess.FileExists(_configFilePath))
        {
            ConfigData configData = new ConfigData();
            configData.ResourcePath = _configFilePath;
            ResourceSaver.Save(configData);
        }

        ConfigData = GD.Load<ConfigData>(_configFilePath);
    }
}
