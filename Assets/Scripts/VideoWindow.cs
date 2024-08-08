using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

public class VideoWindow : MonoBehaviour
{
    public VideoPlayer player;
    [SerializeField] private GameObject qrcodewindow;

    private void OnEnable()
    {
        player.Play();
        Invoke("TriggerRequest", 30f);
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

    private void TriggerRequest()
    {
        StartCoroutine(GetRequest("http://localhost:5000/save_video"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Erro na requisição: " + webRequest.error);
            }
            else
            {
                Debug.Log("Requisição bem-sucedida!");
            }
        }
    }
}
