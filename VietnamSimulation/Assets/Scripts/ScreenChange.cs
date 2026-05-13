using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ScreenChange : MonoBehaviour
{
    public GameObject lobbyScreen;
    public GameObject militaryGoalsScreen;
    public GameObject mapSliderScreen;
    public GameObject ledgerSliderScreen;
    public GameObject decisionScreen;
    public GameObject resultsScreen;
    public GameObject corkboardScreen;
    public GameObject pauseScreen;
    public GameObject advisor;
    public TMP_Text advisorText;
    public GameObject closeButton;
    public GameObject nextButton;


    public Camera camera;
    public Image blackscreen;
    Color transparent = new Color(0, 0, 0, 0);


    bool isZoomingInOnMap = false;
    bool isZoomingOutOnMap = false;
    bool isZoomingInOnLedger = false;
    bool isZoomingOutOnLedger = false;
    bool isZoomingInOnCorkboard = false;
    bool isZoomingOutOnCorkboard = false;
    bool isZoomingInOnRadio = false;
    bool isZoomingOutOnRadio = false;
    bool isViewingLedger = false;
    bool isViewingMap = false;
    bool isFadingInOnWarRoom = false;
    bool isFadingOutOnWarRoom = false;

    Vector3 cameraCenter = new Vector3(1288, 725, -1500);
    Vector3 cameraMap = new Vector3(1283, 722, -1500);
    Vector3 cameraCorkboard = new Vector3(1284, (float)725.7, -1500);
    Vector3 cameraLedger = new Vector3((float)1297.2, 725, -1500);
    Vector3 cameraRadio = new Vector3((float)1286.74, (float)723.66, -1500);
    float timeElapsed = 0f;
    float secondTimeElapsed = 0f;
    float zoomDuration = 2f;
    float fadeOutDuration = 1f;
    float fadeInDuration = 1f;
    float shiftDuration = 1f;
    

    GameObject gameManager;
    ResourceManager resourceManager;
    public AudioSource audioSource;
    public AudioClip transitionSound;
    public AudioClip writingSound;

    bool hasEnteredWarRoom = false;
    bool viewingResults = false;
    int numResultsViewed = 0;

    public void AllScreensActive()
    {
        militaryGoalsScreen.SetActive(true);
        pauseScreen.SetActive(true);
        ledgerSliderScreen.SetActive(true);
        mapSliderScreen.SetActive(true);
        lobbyScreen.SetActive(true);
        decisionScreen.SetActive(true);
        resultsScreen.SetActive(true);
        corkboardScreen.SetActive(true);
    }


    public void AllScreensInactive()
    {
        lobbyScreen.SetActive(false);
        militaryGoalsScreen.SetActive(false);
        mapSliderScreen.SetActive(false);
        ledgerSliderScreen.SetActive(false);
        decisionScreen.SetActive(false);
        resultsScreen.SetActive(false);
        corkboardScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager");
        resourceManager = gameManager.GetComponent<ResourceManager>();

        AllScreensActive();

        


    }

// Update is called once per frame
void Update()
    {
        if (isZoomingInOnMap)
        {
            timeElapsed += Time.deltaTime;
            float zoomProgress = Mathf.Clamp01(timeElapsed / zoomDuration);
            float fadeOutProgress = Mathf.Clamp01(timeElapsed / fadeOutDuration);

            camera.transform.position = Vector3.Lerp(cameraCenter, cameraMap, zoomProgress);
            camera.orthographicSize = Mathf.Lerp(5, 2, zoomProgress);
            blackscreen.color = Color.Lerp(transparent, Color.black, fadeOutProgress);

            if (zoomProgress == 1f)
            {
                secondTimeElapsed += Time.deltaTime;
                militaryGoalsScreen.SetActive(false);
                mapSliderScreen.SetActive(true);
                ledgerSliderScreen.SetActive(true);
                camera.transform.position = cameraCenter;
                camera.orthographicSize = 5;
                float fadeInProgress = Mathf.Clamp01(secondTimeElapsed / fadeInDuration);
                float extraTimeProgress = Mathf.Clamp01(secondTimeElapsed / (fadeInDuration + 1));
                blackscreen.color = Color.Lerp(Color.black, transparent, fadeInProgress);

                if (extraTimeProgress == 1f)
                {
                    isZoomingInOnMap = false;
                    InputSystem.EnableDevice(Mouse.current);
                }
            }
        }

        else if (isZoomingOutOnMap)
        {
            timeElapsed += Time.deltaTime;
            float fadeOutProgress = Mathf.Clamp01(timeElapsed / fadeOutDuration);
            float extraTimeProgress = Mathf.Clamp01(timeElapsed / (fadeOutDuration + 1));
            blackscreen.color = Color.Lerp(transparent, Color.black, fadeOutProgress);

            if (extraTimeProgress == 1f)
            {
                camera.transform.position = cameraCenter;
                secondTimeElapsed += Time.deltaTime;
                mapSliderScreen.SetActive(false);
                ledgerSliderScreen.SetActive(false);
                militaryGoalsScreen.SetActive(true);
                float fadeInProgress = Mathf.Clamp01(secondTimeElapsed / fadeInDuration);
                blackscreen.color = Color.Lerp(Color.black, transparent, fadeInProgress);

                if (fadeInProgress == 1f)
                {
                    isZoomingOutOnMap = false;
                    InputSystem.EnableDevice(Mouse.current);
                }
            }
        }

        else if (isZoomingInOnCorkboard)
        {
            timeElapsed += Time.deltaTime;
            float zoomProgress = Mathf.Clamp01(timeElapsed / zoomDuration);
            float fadeOutProgress = Mathf.Clamp01(timeElapsed / fadeOutDuration);
            camera.transform.position = Vector3.Lerp(cameraCenter, cameraCorkboard, zoomProgress);
            camera.orthographicSize = Mathf.Lerp(5, 2, zoomProgress);
            blackscreen.color = Color.Lerp(transparent, Color.black, fadeOutProgress);

            if (zoomProgress == 1f)
            {
                secondTimeElapsed += Time.deltaTime;
                militaryGoalsScreen.SetActive(false);
                corkboardScreen.SetActive(true);
                camera.transform.position = cameraCenter;
                camera.orthographicSize = 5;
                float fadeInProgress = Mathf.Clamp01(secondTimeElapsed / fadeInDuration);
                float extraTimeProgress = Mathf.Clamp01(secondTimeElapsed / (fadeInDuration + 1));
                blackscreen.color = Color.Lerp(Color.black, transparent, fadeInProgress);

                if (extraTimeProgress == 1f)
                {
                    isZoomingInOnCorkboard = false;
                    InputSystem.EnableDevice(Mouse.current);
                }
            }
        }

        else if (isZoomingOutOnCorkboard)
        {
            timeElapsed += Time.deltaTime;
            float fadeOutProgress = Mathf.Clamp01(timeElapsed / fadeOutDuration);
            float extraTimeProgress = Mathf.Clamp01(timeElapsed / (fadeOutDuration + 1));
            blackscreen.color = Color.Lerp(transparent, Color.black, fadeOutProgress);

            if (extraTimeProgress == 1f)
            {
                secondTimeElapsed += Time.deltaTime;
                corkboardScreen.SetActive(false);
                militaryGoalsScreen.SetActive(true);
                float fadeInProgress = Mathf.Clamp01(secondTimeElapsed / fadeInDuration);
                blackscreen.color = Color.Lerp(Color.black, transparent, fadeInProgress);

                if (fadeInProgress == 1f)
                {
                    isZoomingOutOnCorkboard = false;
                    InputSystem.EnableDevice(Mouse.current);
                }
            }
        }

        else if (isViewingLedger)
        {
            timeElapsed += Time.deltaTime;
            float shiftProgress = Mathf.Clamp01(timeElapsed / shiftDuration);

            camera.transform.position = Vector3.Lerp(cameraCenter, cameraLedger, shiftProgress);

            if (shiftProgress == 1f)
            {
                isViewingLedger = false;
                InputSystem.EnableDevice(Mouse.current);
            }
        }

        else if (isViewingMap)
        {
            timeElapsed += Time.deltaTime;
            float shiftProgress = Mathf.Clamp01(timeElapsed / shiftDuration);

            camera.transform.position = Vector3.Lerp(cameraLedger, cameraCenter, shiftProgress);

            if (shiftProgress == 1f)
            {
                isViewingMap = false;
                InputSystem.EnableDevice(Mouse.current);
            }
        }

        else if (isZoomingInOnRadio)
        {
            timeElapsed += Time.deltaTime;
            float zoomProgress = Mathf.Clamp01(timeElapsed / zoomDuration);
            camera.transform.position = Vector3.Lerp(cameraCenter, cameraRadio, zoomProgress);
            camera.orthographicSize = Mathf.Lerp(5, (float)1.5, zoomProgress);

            if (zoomProgress == 1f)
            {
                //ResourceManager.pointsInvestigated = 0;
                advisor.SetActive(true);
                resourceManager.advisorAvailable = false;
                resourceManager.notification.SetActive(false);
                isZoomingInOnRadio = false;
                InputSystem.EnableDevice(Mouse.current);
            }
        }

        else if (isZoomingOutOnRadio)
        {
            timeElapsed += Time.deltaTime;
            float zoomProgress = Mathf.Clamp01(timeElapsed / zoomDuration);
            camera.transform.position = Vector3.Lerp(cameraRadio, cameraCenter, zoomProgress);
            camera.orthographicSize = Mathf.Lerp((float)1.5, 5, zoomProgress);

            if (zoomProgress == 1f)
            {
                isZoomingOutOnRadio = false;
                InputSystem.EnableDevice(Mouse.current);
            }
        }

        else if (isFadingInOnWarRoom)
        {
            timeElapsed += Time.deltaTime;
            float fadeOutProgress = Mathf.Clamp01(timeElapsed / fadeOutDuration);
            float extraTimeProgress = Mathf.Clamp01(timeElapsed / (fadeOutDuration + 1));
            if (numResultsViewed <= 1)
            {
                blackscreen.color = Color.Lerp(transparent, Color.black, fadeOutProgress);
            }
            else
            {
                AllScreensInactive();

                camera.transform.position = cameraRadio;
                camera.orthographicSize = (float)1.5;

                advisor.SetActive(true);
                hasEnteredWarRoom = true;

                militaryGoalsScreen.SetActive(true);
                isFadingInOnWarRoom = false;
            }

            if (extraTimeProgress == 1f)
            {
                if (!hasEnteredWarRoom || viewingResults)
                {
                    camera.transform.position = cameraRadio;
                    camera.orthographicSize = (float)1.5;

                    advisor.SetActive(true);
                    hasEnteredWarRoom = true;
                }

                secondTimeElapsed += Time.deltaTime;
                AllScreensInactive();
                militaryGoalsScreen.SetActive(true);
                float fadeInProgress = Mathf.Clamp01(secondTimeElapsed / fadeInDuration);
                blackscreen.color = Color.Lerp(Color.black, transparent, fadeInProgress);

                if (fadeInProgress == 1f)
                {
                    isFadingInOnWarRoom = false;
                    InputSystem.EnableDevice(Mouse.current);
                }
            }

        }

    }


    public void MilitaryScene()
    {
        
        audioSource.PlayOneShot(transitionSound);

        if (!isFadingInOnWarRoom)
        {
            InputSystem.DisableDevice(Mouse.current);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isFadingInOnWarRoom = true;
        }
       
    }

    public void MapZoomIn()
    {
        if (!isZoomingInOnMap)
        {
            InputSystem.DisableDevice(Mouse.current);

            audioSource.PlayOneShot(transitionSound);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingInOnMap = true;
        }
    }

    public void MapZoomOut()
    {
        if (!isZoomingOutOnMap)
        {
            InputSystem.DisableDevice(Mouse.current);

            audioSource.PlayOneShot(transitionSound);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingOutOnMap = true;
        }
    }


    public void LedgerZoomIn()
    {
        if (!isViewingLedger && !isZoomingInOnMap)
        {
            InputSystem.DisableDevice(Mouse.current);

            audioSource.PlayOneShot(transitionSound);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isViewingLedger = true;
        }
    }


    public void LedgerZoomOut()
    {
        if (!isViewingMap)
        {
            InputSystem.DisableDevice(Mouse.current);

            audioSource.PlayOneShot(transitionSound);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isViewingMap = true;
        }
    }


    public void CorkboardZoomIn()
    {
        if (!isZoomingInOnCorkboard)
        {
            InputSystem.DisableDevice(Mouse.current);

            audioSource.PlayOneShot(transitionSound);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingInOnCorkboard = true;
        }
    }

    public void CorkboardZoomOut()
    {
        if (!isZoomingOutOnCorkboard)
        {
            InputSystem.DisableDevice(Mouse.current);

            audioSource.PlayOneShot(transitionSound);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingOutOnCorkboard = true;
        }
    }

    public void RadioZoomIn()
    {
        if (resourceManager.advisorAvailable && !isZoomingInOnRadio)
        {
            InputSystem.DisableDevice(Mouse.current);

            audioSource.PlayOneShot(transitionSound);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingInOnRadio = true;
        }
    }

    public void RadioZoomOut()
    {
        advisor.SetActive(false);

        if (!isZoomingOutOnRadio)
        {
            InputSystem.DisableDevice(Mouse.current);

            audioSource.PlayOneShot(transitionSound);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingOutOnRadio = true;
        }
    }


    public void BackToLobby()
    {
        audioSource.PlayOneShot(transitionSound);

        militaryGoalsScreen.SetActive(false);
        lobbyScreen.SetActive(true);
    }


    public void Decide()
    {
        AllScreensInactive();

        lobbyScreen.SetActive(false);
        decisionScreen.SetActive(true);

    }


    public void ChangeYourMind()
    {
        decisionScreen.SetActive(false);
        lobbyScreen.SetActive(true);

    }

    public void ViewResults()
    {
        resourceManager.CalculateResults();
        resourceManager.FinalizeResults();

        closeButton.SetActive(false);
        nextButton.SetActive(true);

        resourceManager.notification.SetActive(false);
        foreach (GameObject question in resourceManager.questionsArray)
        {
            question.SetActive(false);
        }

        if (numResultsViewed == 0)
        {
            advisorText.text = resourceManager.resultsText.text;
        }
        else if (numResultsViewed == 1) 
        {
            advisorText.text = "second advisor message";
        }
        else if (numResultsViewed == 2)
        {
            advisorText.text = "third advisor message";
        }
        else if (numResultsViewed == 3)
        {
            advisorText.text = "Classification: " + ResourceManager.classification;

            nextButton.SetActive(false);
            closeButton.SetActive(true);
        }

        numResultsViewed += 1;

        decisionScreen.SetActive(false);

        audioSource.PlayOneShot(transitionSound);

        viewingResults = true;

        if (!isFadingInOnWarRoom)
        {
            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isFadingInOnWarRoom = true;
        }

        //resultsScreen.SetActive(true);
    }

}
