using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
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

    public void UpdateInputFieldM1()
    {
        inputField.text = slider.value.ToString();
        alternateInputField.text = slider.value.ToString();
        alternateSlider.value = slider.value;

        resourcesAllocated = int.Parse(inputField.text);
        resourceManager.SetM1Resources(resourcesAllocated);

    }


    public void UpdateInputFieldM2()
    {
        inputField.text = slider.value.ToString();
        alternateInputField.text = slider.value.ToString();
        alternateSlider.value = slider.value;

        resourcesAllocated = int.Parse(inputField.text);
        resourceManager.SetM2Resources(resourcesAllocated);
    }


    public void UpdateInputFieldM3()
    {
        inputField.text = slider.value.ToString();
        alternateInputField.text = slider.value.ToString();
        alternateSlider.value = slider.value;

        resourcesAllocated = int.Parse(inputField.text);
        resourceManager.SetM3Resources(resourcesAllocated);
    }


    public void UpdateInputFieldM4()
    {
        inputField.text = slider.value.ToString();
        alternateInputField.text = slider.value.ToString();
        alternateSlider.value = slider.value;

        resourcesAllocated = int.Parse(inputField.text);
        resourceManager.SetM4Resources(resourcesAllocated);
    }


    public void UpdateInputFieldM5()
    {
        inputField.text = slider.value.ToString();
        alternateInputField.text = slider.value.ToString();
        alternateSlider.value = slider.value;

        resourcesAllocated = int.Parse(inputField.text);
        resourceManager.SetM5Resources(resourcesAllocated);
    }
}
