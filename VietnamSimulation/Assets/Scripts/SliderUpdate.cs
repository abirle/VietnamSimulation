using TMPro;
//using UnityEditor.Build.Content;
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

    AudioSource audioSource;
    AudioClip paperSlide;
    bool slidePlaying;
    float slideDuration = 2f;

    AudioClip penWriting;
    bool penPlaying;
    float penDuration = 3f;

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
        penWriting = screenChange.writingSound;


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

        float penProgress = Mathf.Clamp01(timeElapsed / penDuration);

        if (penProgress == 1f)
        {
            penPlaying = false;
            timeElapsed = 0f;
            penProgress = 0f;
        }

    }

    public void InputFieldEntered(int fieldNum)
    {
        if (resourceManager.numResources < 0)
        {
            float excessResources = 0 - resourceManager.numResources;

            resourcesAllocated = int.Parse(inputField.text) - (int)excessResources;

            inputField.text = (float.Parse(inputField.text) - excessResources).ToString();
        }

        else
        {
            slider.value = float.Parse(inputField.text);
            alternateSlider.value = slider.value;
            alternateInputField.text = inputField.text;

            resourcesAllocated = (int)slider.value;

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
            else if (fieldNum == 6)
            {
                resourceManager.SetDi6Resources(resourcesAllocated);
            }
            else if (fieldNum == 7)
            {
                resourceManager.SetDi7Resources(resourcesAllocated);
            }
            else if (fieldNum == 8)
            {
                resourceManager.SetDi8Resources(resourcesAllocated);
            }
            else if (fieldNum == 9)
            {
                resourceManager.SetDi9Resources(resourcesAllocated);
            }
            else if (fieldNum == 10)
            {
                resourceManager.SetDo10Resources(resourcesAllocated);
            }
            else if (fieldNum == 11)
            {
                resourceManager.SetDo11Resources(resourcesAllocated);
            }
            else if (fieldNum == 12)
            {
                resourceManager.SetDo12Resources(resourcesAllocated);
            }
            else if (fieldNum == 13)
            {
                resourceManager.SetDo13Resources(resourcesAllocated);
            }
            else if (fieldNum == 14)
            {
                resourceManager.SetDo14Resources(resourcesAllocated);
            }
            else if (fieldNum == 15)
            {
                resourceManager.SetDo15Resources(resourcesAllocated);
            }

        }

        timeElapsed = 0f;
    }


    public void UpdateSlider(int fieldNum)
    {
        slider.value = float.Parse(inputField.text);
        alternateSlider.value = slider.value;
        alternateInputField.text = inputField.text;

        resourcesAllocated = (int)slider.value;

        if (!slidePlaying && (fieldNum == 1 || fieldNum == 2 || fieldNum == 5))
        {
            audioSource.PlayOneShot(paperSlide);
            audioSource.pitch = 0.5f;
            slidePlaying = true;
        }
        else if (!penPlaying && (fieldNum == 3 || fieldNum == 4))
        {
            audioSource.PlayOneShot(penWriting);
            audioSource.pitch = 1f;
            penPlaying = true;
        }

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
        else if (fieldNum == 6)
        {
            resourceManager.SetDi6Resources(resourcesAllocated);
        }
        else if (fieldNum == 7)
        {
            resourceManager.SetDi7Resources(resourcesAllocated);
        }
        else if (fieldNum == 8)
        {
            resourceManager.SetDi8Resources(resourcesAllocated);
        }
        else if (fieldNum == 9)
        {
            resourceManager.SetDi9Resources(resourcesAllocated);
        }
        else if (fieldNum == 10)
        {
            resourceManager.SetDo10Resources(resourcesAllocated);
        }
        else if (fieldNum == 11)
        {
            resourceManager.SetDo11Resources(resourcesAllocated);
        }
        else if (fieldNum == 12)
        {
            resourceManager.SetDo12Resources(resourcesAllocated);
        }
        else if (fieldNum == 13)
        {
            resourceManager.SetDo13Resources(resourcesAllocated);
        }
        else if (fieldNum == 14)
        {
            resourceManager.SetDo14Resources(resourcesAllocated);
        }
        else if (fieldNum == 15)
        {
            resourceManager.SetDo15Resources(resourcesAllocated);
        }

    }


    //public void UpdateSliderM1()
    //{
 
    //    slider.value = float.Parse(inputField.text);
    //    alternateSlider.value = slider.value;
    //    alternateInputField.text = inputField.text;

    //    resourcesAllocated = (int)slider.value;
    //    resourceManager.SetM1Resources(resourcesAllocated);

    //    if (!slidePlaying)
    //    {
    //        audioSource.PlayOneShot(paperSlide);
    //        slidePlaying = true;
    //    }

    //}


    //public void UpdateSliderM2()
    //{
    //    slider.value = float.Parse(inputField.text);
    //    alternateSlider.value = float.Parse(inputField.text);
    //    alternateInputField.text = inputField.text;

    //    resourcesAllocated = (int)slider.value;
    //    resourceManager.SetM2Resources(resourcesAllocated);

    //    if (!slidePlaying)
    //    {
    //        audioSource.PlayOneShot(paperSlide);
    //        slidePlaying = true;
    //    }

    //}


    //public void UpdateSliderM3()
    //{
    //    slider.value = float.Parse(inputField.text);
    //    alternateSlider.value = float.Parse(inputField.text);
    //    alternateInputField.text = inputField.text;

    //    resourcesAllocated = (int)slider.value;
    //    resourceManager.SetM3Resources(resourcesAllocated);
    //}


    //public void UpdateSliderM4()
    //{
    //    slider.value = float.Parse(inputField.text);
    //    alternateSlider.value = float.Parse(inputField.text);
    //    alternateInputField.text = inputField.text;

    //    resourcesAllocated = (int)slider.value;
    //    resourceManager.SetM4Resources(resourcesAllocated);
    //}


    //public void UpdateSliderM5()
    //{
    //    slider.value = float.Parse(inputField.text);
    //    alternateSlider.value = float.Parse(inputField.text);
    //    alternateInputField.text = inputField.text;

    //    resourcesAllocated = (int)slider.value;
    //    resourceManager.SetM5Resources(resourcesAllocated);

    //    if (!slidePlaying)
    //    {
    //        audioSource.PlayOneShot(paperSlide);
    //        slidePlaying = true;
    //    }

    //}

}
