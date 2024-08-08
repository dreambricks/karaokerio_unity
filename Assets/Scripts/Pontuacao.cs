using System;
using UnityEngine;
using UnityEngine.UI;

public class Pontuacao : MonoBehaviour
{
    public float time;
    public Text text;


    [SerializeField] private QrCodeWindow qrcode;


    private void OnEnable()
    {
        GetPoints();
        Invoke("GotoQRCode", time);
    }

    public void GotoQRCode()
    {
        qrcode.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }


    void GetPoints()
    {
        System.Random random = new System.Random();

        int min = 700;
        int max = 1001;

        int randomNumber = random.Next(min, max);

        text.text = randomNumber.ToString();
    }
}
