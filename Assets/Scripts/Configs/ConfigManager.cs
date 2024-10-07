using Unity.VisualScripting;

public class ConfigManager /* : Singleton<ConfigManager>*/
{
    private const string ConfigSharePath = "Configs/";

    #region GAME_CONFIG

    public ConfigSong configSong;

    #endregion

    //======================================================

    public void LoadAllConfigLocal()
    {
        if (isLoadedConfigLocal)
            return;

        configSong = new ConfigSong();
        configSong.LoadFromAssetPath(ConfigSharePath + "ConfigSong");

        isLoadedConfigLocal = true;
    }

    private static bool isLoadedConfigLocal = false;
    public static bool IsLoadedConfigLocal
    {
        set { isLoadedConfigLocal = value; }
        get { return isLoadedConfigLocal; }
    }

}