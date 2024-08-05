using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject cta;
    [SerializeField] GameObject countDown;
    [SerializeField] GameObject video;
    [SerializeField] GameObject qrCode;

    //[SerializeField] UdpClientManager udpClientManager;

    private enum AppState
    {
        cta,
        countDown,
        video,
        qrCode
    }

    private AppState currentState;

    private void Awake()
    {
        cta.gameObject.SetActive(true);
        countDown.gameObject.SetActive(false);
        video.gameObject.SetActive(false);
        qrCode.gameObject.SetActive(false);
    }

    void Start()
    {

    }
}
