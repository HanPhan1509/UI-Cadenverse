using System.Collections.Generic;
using System.Linq;

public class ConfigSong : SgConfigDataTable<ConfigSongRecord>
{
    protected override void RebuildIndex()
    {
        RebuildIndexByField<int>("ID");
    }

    public ConfigSongRecord GetConfigLevelById(int id)
    {
        ConfigSongRecord record = Records.FirstOrDefault(x => x.ID == id);
        return record;
    }

    public List<ConfigSongRecord> GetAllConfigSong()
    {
        return Records;
    }
}

public class ConfigSongRecord
{
    public int ID;
    public string SongName;
    public string Singer;
    public string Genre;
    public int Difficulty;
    public int UnlockType;
    public string LinkSongCover;
    public string Linkshort;
    public string Promotion;
}
