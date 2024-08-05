using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.Networking;

public class QrCodeWindow : MonoBehaviour
{
    [SerializeField] private GameObject cta;

    public float totalTime;
    private float currentTime;

    private void OnEnable()
    {
        currentTime = totalTime;

        StartCoroutine(GetRequest("http://localhost:5000/render_music_list"));  
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
