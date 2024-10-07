using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class ItemDetail
{
    public int ItemID;
    public string NameSong;
    public string NameArtist;
    public TypeDifficulty Difficulty;
    public int UnlockType;
    public Texture2D TextureCoverSong;
    public string LinkSound;
    public TypePromotion Promotion;
}

public enum TypePromotion
{
    None, New, Hot,
}

public enum TypeDifficulty
{
    tutorial, normal, hard, extreme
}

public class UIController : MonoBehaviour
{
    private ConfigManager configManager;
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private MusicView musicView;
    [SerializeField] private ItemMusic prefItemMusic;

    [SerializeField] private SongDetailPopup mSongDetailPopup;
    [SerializeField] private List<ConfigSongRecord> lstConfig = new List<ConfigSongRecord>();
    [SerializeField] private List<ItemDetail> lstItemDetail = new List<ItemDetail>();
    private int index = 0;
    private bool isDone = false;

    private void Awake()
    {
        LoadConfigFromCSV();
        StartCoroutine(PassParam());
        LoadItem();
    }

    private IEnumerator PassParam()
    {
        yield return new WaitUntil(() => isDone);
        musicView.Initialized(prefItemMusic, lstItemDetail);
    }

    public void BTN_PlaySound(int id)
    {
        string link = lstItemDetail.Find(x => x.ItemID == id).LinkSound;
        StartCoroutine(audioManager.LoadSound(link));
    }

    private void LoadConfigFromCSV()
    {
        configManager = new();
        configManager.LoadAllConfigLocal();
        lstConfig = configManager.configSong.GetAllConfigSong();
    }

    private void LoadItem()
    {
        index++;
        Texture2D coverTexture = null;

        //Load image
        StartCoroutine(LoadImage(lstConfig[index].LinkSongCover, (Texture2D texture) =>
        {
            if(texture == null)
            {
                LoadItem();
                return;
            }    
            coverTexture = texture;
            ItemDetail item = new ItemDetail
            {
                ItemID = lstConfig[index].ID,
                NameSong = lstConfig[index].SongName,
                NameArtist = lstConfig[index].Singer,
                Difficulty = (TypeDifficulty)lstConfig[index].Difficulty,
                UnlockType = lstConfig[index].UnlockType,
                LinkSound = lstConfig[index].Linkshort,
                TextureCoverSong = coverTexture,
                Promotion = (lstConfig[index].Promotion == null || lstConfig[index].Promotion == string.Empty) ? TypePromotion.None : (TypePromotion)Enum.Parse(typeof(TypePromotion), lstConfig[index].Promotion, true),
            };
            lstItemDetail.Add(item);

            if (index < lstConfig.Count - 1)
                LoadItem();
            else
                isDone = true;
        }));
    }

    private IEnumerator LoadImage(string imageUrl, Action<Texture2D> callback)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                //Debug.LogError(www.error);
                callback?.Invoke(null);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                callback?.Invoke(texture);
            }
        }
    }

    public void OpenPlayingPopup(int id, string nameSong)
    {
        mSongDetailPopup.gameObject.SetActive(true);
        mSongDetailPopup.Setup(id, nameSong, RandomPoint);
    }

    public void RandomPoint(int id)
    {
        int random = UnityEngine.Random.Range(0, 9);
        musicView.GetItemInList(id).ShowAchievement(random);
    }
}
