using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Collections.Generic;
using static UnityEngine.Rendering.DebugUI;

public class InputFieldUpdate : MonoBehaviour
{
    public TMP_InputField inputField;
    public UnityEngine.UI.Slider slider;

    public TMP_InputField alternateInputField;
    public UnityEngine.UI.Slider alternateSlider;

    int resourcesAllocated;
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

    public void SliderReleased(int sliderNum)
    {
        if (inputField.text == "")
        {
            inputField.text = "0";
        }
        if (resourceManager.numResources < 0)
        {
            float excessResources = 0 - resourceManager.numResources;

            resourcesAllocated = (int)slider.value - (int)excessResources;

            slider.value = slider.value - excessResources;
        }
        else
        {
            inputField.text = slider.value.ToString();
            alternateInputField.text = inputField.text;
            alternateSlider.value = slider.value;

            resourcesAllocated = int.Parse(inputField.text);

            if (sliderNum == 1)
            {
                resourceManager.SetM1Resources(resourcesAllocated);
            }
            if (sliderNum == 2)
            {
                resourceManager.SetM2Resources(resourcesAllocated);
            }
            if (sliderNum == 3)
            {
                resourceManager.SetM3Resources(resourcesAllocated);
            }
            if (sliderNum == 4)
            {
                resourceManager.SetM4Resources(resourcesAllocated);
            }
            if (sliderNum == 5)
            {
                resourceManager.SetM5Resources(resourcesAllocated);
            }
        }
    }



    public void UpdateInputField(int sliderNum)
    {
        if (inputField.text == "")
        {
            inputField.text = "0";
        }

        inputField.text = slider.value.ToString();
        alternateInputField.text = inputField.text;
        alternateSlider.value = slider.value;

        resourcesAllocated = int.Parse(inputField.text);

        if (sliderNum == 1)
        {
            resourceManager.SetM1Resources(resourcesAllocated);
        }
        if (sliderNum == 2)
        {
            resourceManager.SetM2Resources(resourcesAllocated);
        }
        if (sliderNum == 3)
        {
            resourceManager.SetM3Resources(resourcesAllocated);
        }
        if (sliderNum == 4)
        {
            resourceManager.SetM4Resources(resourcesAllocated);
        }
        if (sliderNum == 5)
        {
            resourceManager.SetM5Resources(resourcesAllocated);
        }
    }



    //public void UpdateInputFieldM1()
    //{
    //    inputField.text = slider.value.ToString();
    //    alternateInputField.text = inputField.text;
    //    alternateSlider.value = slider.value;

    //    resourcesAllocated = int.Parse(inputField.text);
    //    resourceManager.SetM1Resources(resourcesAllocated);
    //}


    //public void UpdateInputFieldM2()
    //{
    //    inputField.text = slider.value.ToString();
    //    alternateInputField.text = slider.value.ToString();
    //    alternateSlider.value = slider.value;

    //    resourcesAllocated = int.Parse(inputField.text);
    //    resourceManager.SetM2Resources(resourcesAllocated);
    //}


    //public void UpdateInputFieldM3()
    //{
    //    inputField.text = slider.value.ToString();
    //    alternateInputField.text = slider.value.ToString();
    //    alternateSlider.value = slider.value;

    //    resourcesAllocated = int.Parse(inputField.text);
    //    resourceManager.SetM3Resources(resourcesAllocated);
    //}


    //public void UpdateInputFieldM4()
    //{
    //    inputField.text = slider.value.ToString();
    //    alternateInputField.text = slider.value.ToString();
    //    alternateSlider.value = slider.value;

    //    resourcesAllocated = int.Parse(inputField.text);
    //    resourceManager.SetM4Resources(resourcesAllocated);
    //}


    //public void UpdateInputFieldM5()
    //{
    //    inputField.text = slider.value.ToString();
    //    alternateInputField.text = slider.value.ToString();
    //    alternateSlider.value = slider.value;

    //    resourcesAllocated = int.Parse(inputField.text);
    //    resourceManager.SetM5Resources(resourcesAllocated);
    //}

}
