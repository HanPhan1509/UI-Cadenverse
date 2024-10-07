using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    //[SerializeField] private Dictionary<int, AudioClip> dicAudioData = new Dictionary<int, AudioClip>();
    // Start is called before the first frame update
    void Start()
    {
        if(audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.Play();
    }

    public IEnumerator LoadSound(string audioUrl)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(audioUrl, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                //Debug.LogError(www.error);
            }
            else
            {
                AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
                if (audioClip != null)
                {
                    PlaySound(audioClip);
                }
            }
        }
    }
}
