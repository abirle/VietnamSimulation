using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Video;

public class ChangeChannel : MonoBehaviour
{
    public GameObject[] channels;
    int currentChannelIndex = 0;
    public GameObject tvStatic;
    bool changingChannelNext;
    bool changingChannelPrevious;

    float timeElapsed = 0;
    float staticTime = 1;

    public AudioSource audioSource;
    public AudioClip staticClip;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (changingChannelNext)
        {
            tvStatic.GetComponent<RawImage>().enabled = true;
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= staticTime)
            {
                currentChannelIndex++;

                if (currentChannelIndex >= channels.Length)
                {
                    currentChannelIndex = 0;
                }

                tvStatic.GetComponent<RawImage>().enabled = false;
                channels[currentChannelIndex].GetComponent<RawImage>().enabled = true;
                channels[currentChannelIndex].GetComponent<VideoPlayer>().SetDirectAudioMute(0, false);

                changingChannelNext = false;
            }
        }

        else if (changingChannelPrevious)
        {
            tvStatic.GetComponent<RawImage>().enabled = true;
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= staticTime)
            {
                currentChannelIndex--;

                if (currentChannelIndex < 0)
                {
                    currentChannelIndex = channels.Length - 1;
                }

                tvStatic.GetComponent<RawImage>().enabled = false;
                channels[currentChannelIndex].GetComponent<RawImage>().enabled = true;
                channels[currentChannelIndex].GetComponent<VideoPlayer>().SetDirectAudioMute(0, false);

                changingChannelPrevious = false;
            }
        }
        
    }

    public void NextChannel()
    {
        channels[currentChannelIndex].GetComponent<RawImage>().enabled = false;
        channels[currentChannelIndex].GetComponent<VideoPlayer>().SetDirectAudioMute(0, true);
        timeElapsed = 0;
        audioSource.PlayOneShot(staticClip);
        changingChannelNext = true;

    }

    
    public void PreviousChannel()
    {
        channels[currentChannelIndex].GetComponent<RawImage>().enabled = false;
        channels[currentChannelIndex].GetComponent<VideoPlayer>().SetDirectAudioMute(0, true);
        timeElapsed = 0;
        audioSource.PlayOneShot(staticClip);
        changingChannelPrevious = true;

    }

}
