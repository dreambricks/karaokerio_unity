using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public List<VideoClip> videoClips;
    private Dictionary<string, VideoClip> videoClipDictionary;

    void Awake()
    {
        videoClipDictionary = new Dictionary<string, VideoClip>();
        foreach (var clip in videoClips)
        {
            videoClipDictionary[clip.name] = clip;
        }
    }

    public VideoClip GetVideoClipByName(string name)
    {
        if (videoClipDictionary.TryGetValue(name, out VideoClip clip))
        {
            return clip;
        }
        else
        {
            return null;
        }
    }
}