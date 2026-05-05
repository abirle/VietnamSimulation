using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Collections.Generic;


public class SliderUpdate: MonoBehaviour
{
    public UnityEngine.UI.Slider slider;
    public TMP_InputField inputField;

    public UnityEngine.UI.Slider alternateSlider;
    public TMP_InputField alternateInputField;

    GameObject sourceParent;
    AudioSource audioSource;
    AudioClip paperSlide;
    bool slidePlaying;
    float slideDuration = 2f;

    int resourcesAllocated;
    GameObject gameManager;
    ResourceManager resourceManager;
    ScreenChange screenChange;

    float timeElapsed = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager");
        audioSource = gameManager.GetComponent<AudioSource>();
        resourceManager = gameManager.GetComponent<ResourceManager>();
        screenChange = gameManager.GetComponent<ScreenChange>();
        paperSlide = audioSource.clip;

    }


    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        float slideProgress = Mathf.Clamp01(timeElapsed / slideDuration);

        if (slideProgress == 1f)
        {
            slidePlaying = false;
            timeElapsed = 0f;
            slideProgress = 0f;
        }

    }

    public void InputFieldEntered(int fieldNum)
    {
        if (resourceManager.numResources < 0)
        {
            float excessResources = 0 - resourceManager.numResources;

            resourcesAllocated = int.Parse(inputField.text) - (int)excessResources;

            if (fieldNum == 1)
            {
                resourceManager.SetM1Resources(resourcesAllocated);
            }
            else if (fieldNum == 2)
            {
                resourceManager.SetM2Resources(resourcesAllocated);
            }
            else if (fieldNum == 3)
            {
                resourceManager.SetM3Resources(resourcesAllocated);
            }
            else if (fieldNum == 4)
            {
                resourceManager.SetM4Resources(resourcesAllocated);
            }
            else if (fieldNum == 5)
            {
                resourceManager.SetM5Resources(resourcesAllocated);
            }

            inputField.text = (float.Parse(inputField.text) - excessResources).ToString();

        }
        else
        {
            slider.value = float.Parse(inputField.text);
            alternateSlider.value = slider.value;
            alternateInputField.text = inputField.text;

            resourcesAllocated = (int)slider.value;
            resourceManager.SetM1Resources(resourcesAllocated);
        }
    }

    public void UpdateSliderM1()
    {
 
        slider.value = float.Parse(inputField.text);
        alternateSlider.value = slider.value;
        alternateInputField.text = inputField.text;

        resourcesAllocated = (int)slider.value;
        resourceManager.SetM1Resources(resourcesAllocated);


        if (!slidePlaying)
        {
            audioSource.PlayOneShot(paperSlide);
            slidePlaying = true;
        }

    }


    public void UpdateSliderM2()
    {
        slider.value = float.Parse(inputField.text);
        alternateSlider.value = float.Parse(inputField.text);
        alternateInputField.text = inputField.text;

        resourcesAllocated = (int)slider.value;
        resourceManager.SetM2Resources(resourcesAllocated);

        if (!slidePlaying)
        {
            audioSource.PlayOneShot(paperSlide);
            slidePlaying = true;
        }

    }


    public void UpdateSliderM3()
    {
        slider.value = float.Parse(inputField.text);
        alternateSlider.value = float.Parse(inputField.text);
        alternateInputField.text = inputField.text;

        resourcesAllocated = (int)slider.value;
        resourceManager.SetM3Resources(resourcesAllocated);
    }


    public void UpdateSliderM4()
    {
        slider.value = float.Parse(inputField.text);
        alternateSlider.value = float.Parse(inputField.text);
        alternateInputField.text = inputField.text;

        resourcesAllocated = (int)slider.value;
        resourceManager.SetM4Resources(resourcesAllocated);
    }


    public void UpdateSliderM5()
    {
        slider.value = float.Parse(inputField.text);
        alternateSlider.value = float.Parse(inputField.text);
        alternateInputField.text = inputField.text;

        resourcesAllocated = (int)slider.value;
        resourceManager.SetM5Resources(resourcesAllocated);

        if (!slidePlaying)
        {
            audioSource.PlayOneShot(paperSlide);
            slidePlaying = true;
        }

    }
}
