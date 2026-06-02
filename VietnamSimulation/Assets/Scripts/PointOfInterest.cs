using NUnit.Framework.Constraints;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointOfInterest : MonoBehaviour
{
    public Button poiButton;
    //public GameObject poiText; 
    public GameObject poiImage01;
    public GameObject poiImage02;
    public GameObject poiCloseButton;
    public GameObject poiShiftButtons;
    public GameObject buttonBlock;
    public string room;

    //public Sprite originalImage;
    //public Sprite newImage;

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
                if (room == "War Tent")
                {
                    ResourceManager.militaryPointsInvestigated++;
                }
                else if (room == "Campaign Office")
                {
                    ResourceManager.domesticPointsInvestigated++;
                }
                if (room == "State Department")
                {
                    ResourceManager.diplomaticPointsInvestigated++;
                }

                poiClickedFirst = true;
            }
            audioSource.PlayOneShot(openSound);

            //poiText.SetActive(true);
            poiImage01.SetActive(true);
            poiCloseButton.SetActive(true);
            poiShiftButtons.SetActive(true);
            buttonBlock.SetActive(true);
            //poiButton.image.sprite = newImage;
            poiButton.GetComponent<Image>().material = null;

            poiClicked = true;
        }

    }

    public void POINext()
    {
        audioSource.PlayOneShot(openSound);

        poiImage01.SetActive(false);
        poiImage02.SetActive(true);
    }

    public void POIPrevious()
    {
        audioSource.PlayOneShot(closeSound);

        poiImage02.SetActive(false);
        poiImage01.SetActive(true);
    }

    public void PointofInterestClose()
    {
        audioSource.PlayOneShot(closeSound);

        //poiButton.image.sprite = originalImage;

        poiClicked = false;

        //poiText.SetActive(false);
        poiImage01.SetActive(false);
        poiImage02.SetActive(false);
        poiCloseButton.SetActive(false);
        poiShiftButtons.SetActive(false);
        buttonBlock.SetActive(false);

    }


    public void Hovered()
    {
        if (!poiClicked)
        {
            poiButton.GetComponent<Image>().material = glowMaterial;
        }

    }

    public void Unhovered()
    {
        poiButton.GetComponent<Image>().material = null;

    }

}
