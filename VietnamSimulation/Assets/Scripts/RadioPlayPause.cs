using Unity.VisualScripting;
using UnityEngine;

public class RadioPlayPause : MonoBehaviour
{
    bool radioClicked = false;
    bool radioPlaying = false;
    public AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RadioClicked()
    {
        if (!radioClicked)
        {
            audioSource.Play();
            radioClicked = true;
            radioPlaying = true;
        }
        else if (radioPlaying)
        {
            audioSource.mute = true;
            radioPlaying = false;
        }
        else if (!radioPlaying)
        {
            audioSource.mute = false;
            radioPlaying = true;
        }
    }
}
