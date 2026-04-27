using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NUnit.Framework.Constraints;

public class PointOfInterest : MonoBehaviour
{
    public Button poiButton01;
    public GameObject poiText01; 
    public GameObject poiImage01;

    public Sprite originalImage;
    public Sprite newImage;

    public Material glowMaterial;

    bool poiClicked = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PointofInterest01()
    {
        if (!poiClicked)
        {
            poiText01.SetActive(true);
            poiImage01.SetActive(true);
            poiButton01.image.sprite = newImage;
            poiButton01.GetComponent<Image>().material = null;

            poiClicked = true;
        }
        else if (poiClicked) 
        {
            poiText01.SetActive(false);
            poiImage01.SetActive(false);
            poiButton01.image.sprite = originalImage;

            poiClicked = false;
        }

    }

    public void Hovered()
    {
        if (!poiClicked)
        {
            poiButton01.GetComponent<Image>().material = glowMaterial;
        }

        Debug.Log("Hovered");
    }

    public void Unhovered()
    {
        poiButton01.GetComponent<Image>().material = null;

        Debug.Log("Unhovered");
    }

}
