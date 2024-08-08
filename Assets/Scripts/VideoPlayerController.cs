using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoManager videoManager;
    public VideoPlayer videoPlayer;

    [SerializeField] UDPReceiver udpClientManager;

    private void Update()
    {
        HandleReceivedMessage(udpClientManager.data);
    }

    private void HandleReceivedMessage(string message)
    {
        string[] messages = message.Split(',');

        if (messages[0] == "start")
        {
            string videoName = messages[1];
         

            VideoClip clip = videoManager.GetVideoClipByName(videoName);

            if (clip != null)
            {
                videoPlayer.clip = clip;
            }
            else
            {
                Debug.Log("VideoClip não encontrado: " + videoName);
            }
        }

    }
}
