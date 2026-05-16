using Unity.VisualScripting;
using UnityEngine;

public class ImageScroll : MonoBehaviour
{
    public GameObject[] images;
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

    public static float scaleSpeed = 0.001f;
    Vector3 scaleIncrement = new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentImage = images[0];
        rightImage = images[1];
        leftImage = null;

    }

    // Update is called once per frame
    void Update()
    {
        if (startZoom)
        {
            distanceTraveled += Time.deltaTime * scrollSpeed;
            images[0].transform.localScale += scaleIncrement;

            if (distanceTraveled >= (shiftDistance / 2))
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

                    if (image == rightImage)
                    {
                        image.transform.localScale += scaleIncrement;
                    }
                    else if (image == currentImage)
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

                    if (image == leftImage)
                    {
                        image.transform.localScale += scaleIncrement;
                    }
                    else if (image == currentImage)
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
}
