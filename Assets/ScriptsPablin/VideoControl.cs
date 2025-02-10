using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoControl : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject tutorial;

    // Start is called before the first frame update
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
