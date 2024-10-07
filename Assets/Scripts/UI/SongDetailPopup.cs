using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SongDetailPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text txtNameSong;
    private int id = 0;
    private Action<int> OnClose;

    public void Setup(int id, string nameSong, Action<int> OnClose)
    {
        this.OnClose = OnClose;
        this.id = id;
        this.txtNameSong.text = nameSong;
    }    

    public void BTN_Close()
    {
        OnClose?.Invoke(id);
        this.gameObject.SetActive(false);
    }    
}
