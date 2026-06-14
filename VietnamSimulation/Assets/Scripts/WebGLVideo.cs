using UnityEngine;
using UnityEngine.Video;

public class WebGLVideo : MonoBehaviour
{
    VideoPlayer videoPlayer;
    public string video;
    string videoPath;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, video);
        videoPlayer.url = videoPath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
