using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UdpClientManager : MonoBehaviour
{
    private UdpClient udpClient;
    private IPEndPoint remoteEndPoint;

    public string serverIP;
    public int serverPort;

    public event Action<string> OnMessageReceived;

    public string data;

    async void Start()
    {
        udpClient = new UdpClient();
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
        udpClient.BeginReceive(new AsyncCallback(OnUdpDataReceived), null);

        SendMessage("Unity conected!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SendMessage("test");
            Debug.Log("Hello, this is a message sent by pressing Space!");
        }
    }

    public void SendMessage(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        udpClient.Send(data, data.Length, remoteEndPoint);
    }

    private void OnUdpDataReceived(IAsyncResult result)
    {
        byte[] receivedData = udpClient.EndReceive(result, ref remoteEndPoint);
        string receivedMessage = Encoding.UTF8.GetString(receivedData);

        Debug.Log("Received message: " + receivedMessage);

        OnMessageReceived?.Invoke(receivedMessage);

        udpClient.BeginReceive(new AsyncCallback(OnUdpDataReceived), null);
    }

    private void OnDestroy()
    {
        udpClient.Close();
    }
}
