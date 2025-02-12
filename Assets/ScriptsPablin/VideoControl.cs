using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoControl : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject tutorial;
    public GameObject video;

    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            videoPlayer.Stop();
            tutorial.SetActive(true);
        }
    }

    public void InicioAnim()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        vp.Stop();
        vp.gameObject.SetActive(false);
        tutorial.SetActive(true);
    }
}
