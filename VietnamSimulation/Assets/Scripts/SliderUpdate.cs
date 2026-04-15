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

    int resourcesAllocated;
    GameObject gameManager;
    ResourceManager resourceManager;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Debug.Log("awake");
        gameManager = GameObject.FindGameObjectWithTag("Manager");
        resourceManager = gameManager.GetComponent<ResourceManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void UpdateSliderM1()
    {
        slider.value = float.Parse(inputField.text);
        alternateSlider.value = float.Parse(inputField.text); 
        alternateInputField.text = inputField.text;

        resourcesAllocated = (int)slider.value;
        resourceManager.SetM1Resources(resourcesAllocated);

    }


    public void UpdateSliderM2()
    {
        slider.value = float.Parse(inputField.text);
        alternateSlider.value = float.Parse(inputField.text);
        alternateInputField.text = inputField.text;

        resourcesAllocated = (int)slider.value;
        resourceManager.SetM2Resources(resourcesAllocated);

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

    }
}
