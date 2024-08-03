using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoManager videoManager;
    public VideoPlayer videoPlayer;

    void Start()
    {
        // Exemplo de como pegar um videoclip pelo nome
        string videoName = "MeuVideo";
        VideoClip clip = videoManager.GetVideoClipByName(videoName);

        if (clip != null)
        {
            videoPlayer.clip = clip;
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("VideoClip não encontrado: " + videoName);
        }
    }
}
