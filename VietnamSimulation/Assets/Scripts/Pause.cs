using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    bool paused = false;
    public Camera mainCamera;
    Vector3 previousCameraPosition;
    float previousCameraOrthographic;
    public GameObject pauseSliders;
    public GameObject[] slidersArray;
    int activeSlidersIndex = 1;
    public ScreenChange screenChange;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenChange.pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!paused)
            {
                pauseSliders.SetActive(false);
                slidersArray[activeSlidersIndex].SetActive(true);
                foreach (GameObject sliders in slidersArray)
                {
                    if (sliders != slidersArray[activeSlidersIndex])
                    {
                        sliders.SetActive(false);
                    }
                }
                previousCameraOrthographic = mainCamera.orthographicSize;
                previousCameraPosition = mainCamera.transform.position;
                mainCamera.transform.position = new Vector3(1288, 725, -1500);
                mainCamera.orthographicSize = 5;
                pauseMenu.SetActive(true);
                paused = true;
            }
            else if (paused)
            {
                mainCamera.orthographicSize = previousCameraOrthographic;
                mainCamera.transform.position = previousCameraPosition;
                pauseMenu.SetActive(false);
                paused = false;
            }
        }
    }


    public void nextSliders()
    {
        if (activeSlidersIndex == 0)
        {
            slidersArray[0].SetActive(false);
            slidersArray[1].SetActive(true);
            activeSlidersIndex = 1;
        }

        else if (activeSlidersIndex == 1) 
        {
            slidersArray[1].SetActive(false);
            slidersArray[2].SetActive(true);
            activeSlidersIndex = 2;
        }
    }


    public void previousSliders()
    {
        if (activeSlidersIndex == 1)
        {
            slidersArray[1].SetActive(false);
            slidersArray[0].SetActive(true);
            activeSlidersIndex = 0;
        }

        else if (activeSlidersIndex == 2)
        {
            slidersArray[2].SetActive(false);
            slidersArray[1].SetActive(true);
            activeSlidersIndex = 1;
        }
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        paused = false;
    }
}
