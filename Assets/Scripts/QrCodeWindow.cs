using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class QrCodeWindow : MonoBehaviour
{
    [SerializeField] private GameObject cta;
    [SerializeField] private UDPReceiver udp;

    public Image image;

    public Text text;

    public float totalTime;
    private float currentTime;

    private void OnEnable()
    {
        SetPoints();
        image.sprite = null;
        currentTime = totalTime;

        
        Invoke("TryConnectApi", 3f);
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            cta.SetActive(true);
            StartCoroutine(GetRequest("http://localhost:5000/render_terms"));
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

    void TryConnectApi()
    {
        if (image.sprite == null)
        {
            GetNewQRCode();
    
            InvokeRepeating("TryConnectApi", 5f, 5f);
        }
        else
        {
    
            CancelInvoke("TryConnectApi");
        }
    }

    void GetNewQRCode()
    {
        string apiUrl = "http://localhost:5000";
        string fullUrl = apiUrl + "/qr";

        StartCoroutine(FetchQRCode(fullUrl));
    }

    IEnumerator FetchQRCode(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error getting QR code: " + webRequest.error);
            }
            else
            {
                Debug.Log("Success getting the QR Code!");
                Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16f);
                image.sprite = sprite;
            }
        }
    }


    void SetPoints()
    {
        System.Random random = new System.Random();

        int min = 700;
        int max = 1001;
        int randomNumber = random.Next(min, max);

        text.text = randomNumber.ToString();
    }
}
