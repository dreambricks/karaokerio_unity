using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
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

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                // Successfully received a response
                Debug.Log("Received: " + webRequest.downloadHandler.text);
            }
        }
    }

}
