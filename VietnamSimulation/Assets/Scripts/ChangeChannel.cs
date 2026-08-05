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

    public AudioSource video01AudioSource;

    int channelsViewed = 0;

    bool tvScrolled = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        channels[0].GetComponent<RawImage>().enabled = true;
        channels[1].GetComponent<RawImage>().enabled = false;
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
                if (currentChannelIndex == 4)
                {
                    video01AudioSource.mute = false;
                }

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
                if (currentChannelIndex == 4)
                {
                    video01AudioSource.mute = false;
                }

                changingChannelPrevious = false;
            }
        }

        //FIXME
        if (!tvScrolled && (channelsViewed >=4 || channelsViewed <= -4))
        {
            ResourceManager.domesticPointsInvestigated++;
            tvScrolled = true;
        }
        
    }

    public void NextChannel()
    {
        channels[currentChannelIndex].GetComponent<RawImage>().enabled = false;
        channels[currentChannelIndex].GetComponent<VideoPlayer>().SetDirectAudioMute(0, true);
        if (currentChannelIndex == 4) 
        {
            video01AudioSource.mute = true;
        }
        timeElapsed = 0;
        audioSource.PlayOneShot(staticClip);
        changingChannelNext = true;
        channelsViewed++;

    }

    
    public void PreviousChannel()
    {
        channels[currentChannelIndex].GetComponent<RawImage>().enabled = false;
        channels[currentChannelIndex].GetComponent<VideoPlayer>().SetDirectAudioMute(0, true);
        if (currentChannelIndex == 4)
        {
            video01AudioSource.mute = true;
        }
        timeElapsed = 0;
        audioSource.PlayOneShot(staticClip);
        changingChannelPrevious = true;
        channelsViewed--;

    }

}
