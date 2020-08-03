using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    private VideoPlayer player;
    
    void Start()
    {
        player = GetComponent<VideoPlayer>();
    }

    private RenderTexture newRenderTexture(VideoClip videoClip)
    {
        var renderTexture = new RenderTexture((int)videoClip.width, (int)videoClip.height, 0, RenderTextureFormat.Default);
        Debug.Log(videoClip.width);
        return renderTexture;
    }
    public void SetAndPlayVideo(VideoClip videoClip)
    {
        player.clip = videoClip;
        //player.targetTexture = newRenderTexture(videoClip);
        player.Play();

        Debug.Log("Play video");
    }
}
