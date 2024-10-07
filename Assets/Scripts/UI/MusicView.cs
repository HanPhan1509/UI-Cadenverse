using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class MusicView : MonoBehaviour
{
    [SerializeField] private Transform parentItem;
    [SerializeField] private TMP_InputField ipSearch;

    private List<ItemDetail> lstItemDetail = new List<ItemDetail>();
    private List<ItemMusic> lstItemMusic = new List<ItemMusic>();

    [SerializeField] private UnityEvent<int> OnPlaySound;
    [SerializeField] private UnityEvent<int, string> OnShowPlayingPopup;

    public void Initialized(ItemMusic prefItem, List<ItemDetail> lstItemDetail)
    {
        this.lstItemDetail = lstItemDetail;
        CreateItemInList(prefItem);
    }

    private void CreateItemInList(ItemMusic prefItem)
    {
        for (int i = 0; i < lstItemDetail.Count; i++)
        {
            ItemMusic item = Instantiate(prefItem, parentItem);
            item.SetUpItem(lstItemDetail[i], (int id, string nameSong) => OnShowPlayingPopup?.Invoke(id, nameSong), (int id) => OnPlaySound?.Invoke(id));
            lstItemMusic.Add(item);
        }
    }

    public ItemMusic GetItemInList(int id)
    {
        ItemMusic itemMusic = null;
        foreach (var item in lstItemMusic)
        {
            if(item.ItemDetail.ItemID == id)
            {
                itemMusic = item;
            }    
        }
        return itemMusic;
    }    
}
