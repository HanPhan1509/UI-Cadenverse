using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DetailMedal
{
    public Sprite spriteBG;
    public Sprite spriteIcon;
    public string name;
}

public class ItemMusic : MonoBehaviour
{
    [Header("DETAIL ITEM")]
    [SerializeField] private RawImage imgSong;
    [SerializeField] private TMP_Text txtNameSong;
    [SerializeField] private TMP_Text txtNameArtist;
    [SerializeField] private Image imgPromotion;
    [SerializeField] private Image imgTagDifficulty;
    [SerializeField] private TMP_Text txtTagDifficulty;

    [Header("BUTTONS")]
    [SerializeField] private GameObject[] btns = new GameObject[3];

    [Header("SPRITE")]
    [SerializeField] private Sprite[] spritePromotions; //New, hot
    [SerializeField] private Sprite[] spriteTags; //tuto, normal, hard, extreme

    [Space(2.0f)]
    [Header("ACHIVEMENT")]
    [SerializeField] private GameObject achievementsGroup;

    [SerializeField] private Image imgMedalBG;
    [SerializeField] private GameObject medal;
    [SerializeField] private Image iconMedal;
    [SerializeField] private TMP_Text txtMedal;
    [SerializeField] private DetailMedal[] detailMedals = new DetailMedal[3];

    [SerializeField] private GameObject starGroup;
    [SerializeField] private GameObject[] stars = new GameObject[5];


    private ItemDetail itemDetail;
    private Action<int> OnPlaySound;
    private Action<int, string> OnShowPopup;

    public ItemDetail ItemDetail { get => itemDetail; set => itemDetail = value; }

    public void SetUpItem(ItemDetail itemDetail, Action<int, string> OnShowPopup, Action<int> OnPlaySound)
    {
        this.OnPlaySound = OnPlaySound;
        this.OnShowPopup = OnShowPopup;
        this.itemDetail = itemDetail;
        imgMedalBG.gameObject.SetActive(false);
        this.txtNameSong.text = itemDetail.NameSong;
        this.txtNameArtist.text = itemDetail.NameArtist;
        //Genre...
        //Set type tag Difficulty
        imgTagDifficulty.sprite = spriteTags[(int)itemDetail.Difficulty];
        txtTagDifficulty.text = itemDetail.Difficulty.ToString();

        //Unlock item
        OnBTN((int)itemDetail.UnlockType);

        if (itemDetail.UnlockType == 0)
            imgPromotion.gameObject.SetActive(false);
        else
            imgPromotion.sprite = spritePromotions[itemDetail.UnlockType - 1];

        imgSong.texture = itemDetail.TextureCoverSong;

        //Set type promotion
        if (itemDetail.Promotion != TypePromotion.None)
        {
            imgPromotion.gameObject.SetActive(true);
            imgPromotion.sprite = spritePromotions[(int)itemDetail.Promotion - 1];
        }
        else
            imgPromotion.gameObject.SetActive(false);
    }

    public void ShowAchievement(int numberStar)
    {
        if (numberStar == 0)
        {
            achievementsGroup.SetActive(false);
        }
        else
        {
            achievementsGroup.SetActive(true);
            if (numberStar > 0 && numberStar < 6)
            {
                foreach (var star in stars)
                {
                    star.SetActive(false);
                }

                medal.SetActive(false);
                starGroup.SetActive(true);
                for (int i = 0; i < stars.Length; i++)
                {
                    if (i < numberStar - 1)
                        stars[i].SetActive(true);
                }
            }
            else
            {
                medal.SetActive(true);
                starGroup.SetActive(false);

                int indexDetail = 0;
                if (numberStar == 6) indexDetail = 0;
                if (numberStar == 7) indexDetail = 1;
                if (numberStar == 8) indexDetail = 2;

                imgMedalBG.gameObject.SetActive(true);
                imgMedalBG.sprite = detailMedals[indexDetail].spriteBG;
                iconMedal.sprite = detailMedals[indexDetail].spriteIcon;
                txtMedal.text = detailMedals[indexDetail].name;
                OnBTN(2);
            }
        }
    }

    public void OnBTN(int indexBTN)
    {
        foreach (var btn in btns)
        {
            btn.SetActive(false);
        }
        btns[indexBTN].SetActive(true);
    }

    public void BTN_Play()
    {
        OnShowPopup?.Invoke(itemDetail.ItemID, itemDetail.NameSong);
    }

    public void BTN_PlayAds()
    {
        OnBTN(0);
    }

    public void BTN_PlayPreviewSound()
    {
        OnPlaySound?.Invoke(itemDetail.ItemID);
    }
}