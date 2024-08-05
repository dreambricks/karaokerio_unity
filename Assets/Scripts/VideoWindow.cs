using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoWindow : MonoBehaviour
{

    [SerializeField] private VideoPlayer player;
    [SerializeField] private GameObject qrcodewindow;



    private void OnEnable()
    {
        player.Play();
    }

    private void Start()
    {
        if (player != null)
        {
            player.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("VideoPlayer não está atribuído.");
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        player.clip = null;
        qrcodewindow.SetActive(true);
        gameObject.SetActive(false);
    }
}
