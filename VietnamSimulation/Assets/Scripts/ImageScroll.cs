using Unity.VisualScripting;
using UnityEngine;

public class ImageScroll : MonoBehaviour
{
    public GameObject[] images;
    public Transform[] imageTransformReferences;
    GameObject currentImage;
    GameObject rightImage;
    GameObject leftImage;
    int rightImageIndex = 1;
    int leftImageIndex = -1;

    bool startZoom = true;
    bool isScrollingRight = false;
    bool isScrollingLeft = false;

    public float scrollSpeed;
    public float distanceTraveled = 0;
    public float shiftDistance;

    static float oldScaleFactor = 0.5f;
    Vector3 oldScale = new Vector3(oldScaleFactor, oldScaleFactor, oldScaleFactor);
    static float scaleFactor = 1.1f;
    Vector3 newScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    static float scaleSpeed = 0.0125f;
    Vector3 scaleIncrement = new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);

    //public int numPages;
    //int currentPage = 1;
    public PointOfInterest poi;
    bool hasBeenOpened = false;
    bool hasFoundPoi = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentImage = images[0];
        rightImage = images[1];
        leftImage = null;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startZoom)
        {
            distanceTraveled += Time.deltaTime * scrollSpeed;
            images[0].transform.localScale += scaleIncrement;

            if (Vector3.Distance(images[0].transform.localScale, newScale) < 0.1)
            {
                startZoom = false;
            }
            
            
        }

        else if (isScrollingRight)
        {
            if (rightImageIndex < images.Length)
            {
                foreach (GameObject image in images)
                {
                    image.transform.Translate(-(Time.deltaTime * scrollSpeed), 0, 0);
                    distanceTraveled += Time.deltaTime * scrollSpeed;

                    if (image == rightImage && Vector3.Distance(image.transform.localScale, newScale) > 0.1)
                    {
                        image.transform.localScale += scaleIncrement;
                    }
                    else if (image == currentImage && Vector3.Distance(image.transform.localScale, oldScale) > 0.1)
                    {
                        image.transform.localScale -= scaleIncrement;
                    }

                    if (distanceTraveled >= shiftDistance)
                    {
                        isScrollingRight = false;
                        leftImage = currentImage;
                        currentImage = rightImage;
                        rightImageIndex++;
                        if (rightImageIndex >= images.Length)
                        {
                            rightImage = null;
                            
                            if (!hasFoundPoi)
                            {
                                if (poi.room == "War Tent")
                                {
                                    ResourceManager.militaryPointsInvestigated++;
                                    Debug.Log(ResourceManager.militaryPointsInvestigated);

                                }
                                if (poi.room == "Campaign Office")
                                {
                                    ResourceManager.domesticPointsInvestigated++;
                                    Debug.Log(ResourceManager.domesticPointsInvestigated);

                                }
                                if (poi.room == "State Department")
                                {
                                    ResourceManager.diplomaticPointsInvestigated++;
                                    Debug.Log(ResourceManager.diplomaticPointsInvestigated);
                                }

                                hasFoundPoi = true;
                            }

                        }
                        else
                        {
                            rightImage = images[rightImageIndex];
                        }
                        leftImageIndex++;

                        break;
                    }

                }

            }

        }


        else if (isScrollingLeft)
        {
            if (leftImageIndex >= 0)
            {
                foreach (GameObject image in images)
                {
                    image.transform.Translate((Time.deltaTime * scrollSpeed), 0, 0);
                    distanceTraveled += Time.deltaTime * scrollSpeed;

                    if (image == leftImage && Vector3.Distance(image.transform.localScale, newScale) > 0.1)
                    {
                        image.transform.localScale += scaleIncrement;
                    }
                    else if (image == currentImage && Vector3.Distance(image.transform.localScale, oldScale) > 0.1)
                    {
                        image.transform.localScale -= scaleIncrement;
                    }

                    if (distanceTraveled >= shiftDistance)
                    {
                        isScrollingLeft = false;
                        rightImage = currentImage;
                        currentImage = leftImage;
                        leftImageIndex--;
                        if (leftImageIndex < 0)
                        {
                            leftImage = null;
                        }
                        else
                        {
                            leftImage = images[leftImageIndex];
                        }
                        rightImageIndex--;

                        break;
                    }
                }

            }

        }
        
    }

    public void ScrollRight()
    {
        if (!isScrollingRight)
        {
            distanceTraveled = 0;

            isScrollingLeft = false;
            isScrollingRight = true;
        }
    }


    public void ScrollLeft()
    {
        if (!isScrollingLeft)
        {
            distanceTraveled = 0;

            isScrollingRight = false;
            isScrollingLeft = true;
        }
    }

    public void Open()
    {
        if (!hasBeenOpened){

            if (poi.room == "War Tent")
            {
                ResourceManager.militaryPointsInvestigated--;
                Debug.Log(ResourceManager.militaryPointsInvestigated);
            }
            if (poi.room == "Campaign Office")
            {
                ResourceManager.domesticPointsInvestigated--;
                Debug.Log(ResourceManager.domesticPointsInvestigated);
            }
            if (poi.room == "State Department")
            {
                ResourceManager.diplomaticPointsInvestigated--;
                Debug.Log(ResourceManager.diplomaticPointsInvestigated);
            }

            hasBeenOpened = true;
        }

    }
}
