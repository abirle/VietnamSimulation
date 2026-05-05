using Unity.VisualScripting;
using UnityEngine;

public class RadioPlayPause : MonoBehaviour
{
    bool radioClicked = false;
    bool radioPlaying = false;
    public AudioSource audioSource;
    GameObject gameManager;
    ResourceManager resourceManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager");
        resourceManager = gameManager.GetComponent<ResourceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RadioClicked()
    {
        if (!resourceManager.advisorAvailable && !radioClicked)
        {
            audioSource.Play();
            radioClicked = true;
            radioPlaying = true;
        }
        else if (!resourceManager.advisorAvailable && radioPlaying)
        {
            audioSource.mute = true;
            radioPlaying = false;
        }
        else if (!resourceManager.advisorAvailable && !radioPlaying)
        {
            audioSource.mute = false;
            radioPlaying = true;
        }
    }
}
