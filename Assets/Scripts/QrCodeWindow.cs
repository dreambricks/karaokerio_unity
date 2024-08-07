using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class QrCodeWindow : MonoBehaviour
{
    [SerializeField] private GameObject cta;
    [SerializeField] private UDPReceiver udp;

    public Image image;

    public float totalTime;
    private float currentTime;

    private void OnEnable()
    {
        image.sprite = null;
        StartCoroutine(GetRequest("http://localhost:5000/render_terms"));
        currentTime = totalTime;

    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            cta.SetActive(true);
            gameObject.SetActive(false);
            
        }

        TryConnectApi();
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
    void TryConnectApi()
    {
        if (image.sprite == null)
        {
            GetNewQRCode();
        }
    }


    void GetNewQRCode()
    {
        string video_id = "";
        string[] messages = udp.GetLastestData().Split(" ");

        if (messages[0] == "video_id ")
        {
            video_id = messages[1];
        }


        string apiUrl = "http://localhost:5000";
        string url = apiUrl;
        string fullUrl = url + "/qr";

        WebRequests.GetTexture(fullUrl,
            (string error) => { Debug.Log("Error!\n" + error); },
            (Texture2D texture2D) =>
            {
                Debug.Log("Success getting the QRCode!\n");
                Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(.5f, .5f), 16f);
                image.sprite = sprite;
            });
    }

}
