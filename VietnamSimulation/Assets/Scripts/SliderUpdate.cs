using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
        sourceParent = GameObject.FindGameObjectWithTag("AudioSource");
        resourceManager = gameManager.GetComponent<ResourceManager>();
        screenChange = resourceManager.GetComponent<ScreenChange>();
        audioSource = sourceParent.GetComponent<AudioSource>();
        paperSlide = audioSource.clip;
        //screenChange.pauseScreen.SetActive(false);
        //screenChange.ledgerSliderScreen.SetActive(false);
        //screenChange.mapSliderScreen.SetActive(false);


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


    public void UpdateSliderM1()
    {
        //if ((resourceManager.numResources - float.Parse(inputField.text)) < 0)
        //{
        //    float excessResources = float.Parse(inputField.text) - resourceManager.numResources;
        //    slider.value = resourceManager.numResources;
        //    alternateSlider.value = resourceManager.numResources;
        //    alternateInputField.text = resourceManager.numResources.ToString();
        //}
        //else
        //{
        //    slider.value = float.Parse(inputField.text);
        //    alternateSlider.value = float.Parse(inputField.text);
        //    alternateInputField.text = inputField.text;
        //}

        slider.value = float.Parse(inputField.text);
        alternateSlider.value = float.Parse(inputField.text);
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
