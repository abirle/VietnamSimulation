using NUnit.Framework.Constraints;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointOfInterest : MonoBehaviour
{
    public Button poiButton01;
    public GameObject poiText01; 
    public GameObject poiImage01;
    public GameObject poiCloseButton;

    public Sprite originalImage;
    public Sprite newImage;

    public Material glowMaterial;

    AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;

    bool poiClickedFirst = false;
    bool poiClicked = false;

    GameObject gameManager;
    ResourceManager resourceManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager");
        resourceManager = gameManager.GetComponent<ResourceManager>();
    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PointofInterestClick()
    {
        if (!poiClicked)
        {
            if (!poiClickedFirst)
            {
                poiClickedFirst = true;
                ResourceManager.pointsInvestigated++;
            }
            audioSource.PlayOneShot(openSound);

            poiText01.SetActive(true);
            poiImage01.SetActive(true);
            poiCloseButton.SetActive(true);
            poiButton01.image.sprite = newImage;
            poiButton01.GetComponent<Image>().material = null;

            poiClicked = true;
        }

    }


    public void PointofInterestClose()
    {
        audioSource.PlayOneShot(closeSound);

        poiButton01.image.sprite = originalImage;

        poiClicked = false;

        poiText01.SetActive(false);
        poiImage01.SetActive(false);
        poiCloseButton.SetActive(false);

    }


    public void Hovered()
    {
        if (!poiClicked)
        {
            poiButton01.GetComponent<Image>().material = glowMaterial;
        }

    }

    public void Unhovered()
    {
        poiButton01.GetComponent<Image>().material = null;

    }

}
