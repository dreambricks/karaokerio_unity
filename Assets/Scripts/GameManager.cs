using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject cta;
    [SerializeField] GameObject countDown;
    [SerializeField] GameObject video;
    [SerializeField] GameObject qrCode;

    [SerializeField] UdpClientManager udpClientManager;

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
        udpClientManager = FindObjectOfType<UdpClientManager>();
        udpClientManager.OnMessageReceived += HandleReceivedMessage;

        ChangeState(AppState.cta);
    }

    private void HandleReceivedMessage(string message)
    {
        
        switch (message)
        {
            case "cta":
                ChangeState(AppState.cta);
                break;
            case "countDown":
                ChangeState(AppState.countDown);
                break;
            case "video":
                ChangeState(AppState.video);
                break;
            case "qrCode":
                ChangeState(AppState.qrCode);
                break;
            default:
                Debug.LogWarning("Unknown message received: " + message);
                break;
        }
    }

    private void ChangeState(AppState newState)
    {
        currentState = newState;
        Debug.Log("Changed state to: " + currentState);

        // Aqui você pode adicionar lógica para mostrar/esconder telas com base no estado
        switch (currentState)
        {
            case AppState.cta:
                
                break;
            case AppState.countDown:
                
                break;
            case AppState.video:
                
                break;
            case AppState.qrCode:
                
                break;
        }
    }


    private void OnDestroy()
    {
        udpClientManager.OnMessageReceived -= HandleReceivedMessage;
    }



}
