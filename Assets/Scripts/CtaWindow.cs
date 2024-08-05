using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtaWindow : MonoBehaviour
{

    [SerializeField] private UDPReceiver udp;
    [SerializeField] private GameObject countDown;

    private void Update()
    {
        VerifyData();
    }

    private void VerifyData()
    {
        string[] messages = udp.data.Split(" ");
        if (messages[0] == "start") { 
            countDown.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}
