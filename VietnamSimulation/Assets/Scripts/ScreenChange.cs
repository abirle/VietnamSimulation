using System;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ScreenChange : MonoBehaviour
{
    public GameObject lobbyScreen;
    public GameObject militaryScreen;
    public GameObject mapSliderScreen;
    public GameObject ledgerSliderScreen;
    public GameObject decisionScreen;
    public GameObject resultsScreen;
    public GameObject corkboardScreen;
    public GameObject pauseScreen;
    public GameObject televisionScreen;

    public GameObject domesticScreen;
    public GameObject typewriterScreen;

    public GameObject militaryAdvisor;
    public TMP_Text militaryAdvisorText;
    public GameObject militaryCloseButton;
    public GameObject militaryNextButton;

    public GameObject domesticAdvisor;
    public TMP_Text domesticAdvisorText;
    public GameObject domesticCloseButton;
    public GameObject domesticNextButton;


    public Camera camera;
    public Image blackscreen;
    Color transparent = new Color(0, 0, 0, 0);


    bool isZoomingInOnMap = false;
    bool isZoomingOutOnMap = false;
    bool isZoomingInOnCorkboard = false;
    bool isZoomingOutOnCorkboard = false;
    bool isZoomingInOnRadio = false;
    bool isZoomingOutOnRadio = false;
    bool isViewingLedger = false;
    bool isViewingMap = false;
    bool isFadingInOnWarRoom = false;
    bool isFadingInOnLobby = false;
    bool isFadingInOnCampaignOffice = false;
    bool isZoomingInOnPhone = false;
    bool isZoomingOutOnPhone = false;
    bool isZoomingInOnTypewriter = false;
    bool isZoomingOutOnTypewriter = false;
    bool isZoomingInOnTelevision = false;
    bool isZoomingOutOnTelevision = false;


    Vector3 cameraCenter = new Vector3(1288, 725, -1500);
    Vector3 cameraMap = new Vector3(1283, 722, -1500);
    Vector3 cameraCorkboard = new Vector3(1284, (float)725.7, -1500);
    Vector3 cameraLedger = new Vector3((float)1297.2, 725, -1500);
    Vector3 cameraRadio = new Vector3((float)1286.74, (float)723.66, -1500);
    Vector3 cameraPhone = new Vector3((float)1291.95, (float)723.8, -1500);
    Vector3 cameraTypewriter = new Vector3((float)1284.52, (float)722.29, -1500);
    Vector3 cameraTelevision = new Vector3((float)1290.4, 726, -1500);

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

    public AudioClip militaryIntro01;
    bool listeningToMilitaryIntro01 = false;
    public Button militaryContinueButton;

    public AudioClip militaryQuestionIntro;
    public AudioClip radioCrackle;
    public AudioClip tvStatic;
    bool radioCrackling;

    public AudioClip domesticIntro01;
    bool listeningToDomesticIntro01 = false;
    public Button domesticContinueButton;

    bool hasEnteredWarRoom = false;
    bool hasEnteredCampaignOffice = false;
    bool viewingResults = false;
    int numResultsViewed = 0;

    public void AllScreensActive()
    {
        militaryScreen.SetActive(true);
        pauseScreen.SetActive(true);
        ledgerSliderScreen.SetActive(true);
        mapSliderScreen.SetActive(true);
        lobbyScreen.SetActive(true);
        decisionScreen.SetActive(true);
        resultsScreen.SetActive(true);
        corkboardScreen.SetActive(true);
        domesticScreen.SetActive(true);
        typewriterScreen.SetActive(true);
        televisionScreen.SetActive(true);
    }


    public void AllScreensInactive()
    {
        lobbyScreen.SetActive(false);
        militaryScreen.SetActive(false);
        mapSliderScreen.SetActive(false);
        ledgerSliderScreen.SetActive(false);
        decisionScreen.SetActive(false);
        resultsScreen.SetActive(false);
        corkboardScreen.SetActive(false);
        pauseScreen.SetActive(false);
        domesticScreen.SetActive(false);
        typewriterScreen.SetActive(false);
        televisionScreen.SetActive(false);

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
                militaryScreen.SetActive(false);
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
                militaryScreen.SetActive(true);
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
                militaryScreen.SetActive(false);
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
                militaryScreen.SetActive(true);
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
                militaryAdvisor.SetActive(true);
                militaryAdvisorText.text = "I've got a meeting with Wheeler in five minutes. You have one question. Go ahead.";
                resourceManager.militaryAdvisorAvailable = false;
                resourceManager.radioNotification.SetActive(false);
                isZoomingInOnRadio = false;
                audioSource.PlayOneShot(militaryQuestionIntro);
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

                militaryAdvisor.SetActive(true);
                hasEnteredWarRoom = true;

                militaryScreen.SetActive(true);
                isFadingInOnWarRoom = false;
            }

            if (extraTimeProgress == 1f)
            {
                if (!hasEnteredWarRoom || viewingResults)
                {
                    camera.transform.position = cameraRadio;
                    camera.orthographicSize = (float)1.5;

                    militaryAdvisor.SetActive(true);
                    hasEnteredWarRoom = true;

                }

                secondTimeElapsed += Time.deltaTime;
                AllScreensInactive();
                militaryScreen.SetActive(true);
                float fadeInProgress = Mathf.Clamp01(secondTimeElapsed / fadeInDuration);
                blackscreen.color = Color.Lerp(Color.black, transparent, fadeInProgress);

                if (fadeInProgress == 1f)
                {
                    isFadingInOnWarRoom = false;
                    InputSystem.EnableDevice(Mouse.current);

                    if (camera.orthographicSize == (float)1.5)
                    {
                        audioSource.PlayOneShot(radioCrackle);
                        radioCrackling = true;
                        timeElapsed = 0;
                    }
                }
            }

        }

        else if (isFadingInOnLobby)
        {
            timeElapsed += Time.deltaTime;
            float fadeOutProgress = Mathf.Clamp01(timeElapsed / fadeOutDuration);
            float extraTimeProgress = Mathf.Clamp01(timeElapsed / (fadeOutDuration + 1));
            blackscreen.color = Color.Lerp(transparent, Color.black, fadeOutProgress);

            if (extraTimeProgress == 1f)
            {
                secondTimeElapsed += Time.deltaTime;
                AllScreensInactive();
                lobbyScreen.SetActive(true);
                float fadeInProgress = Mathf.Clamp01(secondTimeElapsed / fadeInDuration);
                blackscreen.color = Color.Lerp(Color.black, transparent, fadeInProgress);

                if (fadeInProgress == 1f)
                {
                    isFadingInOnLobby = false;
                    InputSystem.EnableDevice(Mouse.current);
                }
            }

        }

        else if (isFadingInOnCampaignOffice)
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

                camera.transform.position = cameraPhone;
                camera.orthographicSize = (float)1.5;

                domesticAdvisor.SetActive(true);
                hasEnteredWarRoom = true;

                domesticScreen.SetActive(true);
                isFadingInOnCampaignOffice = false;
            }

            if (extraTimeProgress == 1f)
            {
                if (!hasEnteredCampaignOffice || viewingResults)
                {
                    camera.transform.position = cameraPhone;
                    camera.orthographicSize = (float)1.5;

                    domesticAdvisor.SetActive(true);
                    hasEnteredCampaignOffice = true;
                }

                secondTimeElapsed += Time.deltaTime;
                AllScreensInactive();
                domesticScreen.SetActive(true);
                float fadeInProgress = Mathf.Clamp01(secondTimeElapsed / fadeInDuration);
                blackscreen.color = Color.Lerp(Color.black, transparent, fadeInProgress);

                if (fadeInProgress == 1f)
                {
                    isFadingInOnCampaignOffice = false;
                    InputSystem.EnableDevice(Mouse.current);

                    if (camera.orthographicSize == (float)1.5)
                    {
                        audioSource.PlayOneShot(domesticIntro01);
                        listeningToDomesticIntro01 = true;
                        timeElapsed = 0;
                    }
                }
            }

        }

        else if (isZoomingInOnPhone)
        {
            timeElapsed += Time.deltaTime;
            float zoomProgress = Mathf.Clamp01(timeElapsed / zoomDuration);
            camera.transform.position = Vector3.Lerp(cameraCenter, cameraPhone, zoomProgress);
            camera.orthographicSize = Mathf.Lerp(5, (float)1.5, zoomProgress);

            if (zoomProgress == 1f)
            {
                //ResourceManager.pointsInvestigated = 0;
                domesticAdvisor.SetActive(true);
                resourceManager.domesticAdvisorAvailable = false;
                resourceManager.phoneNotification.SetActive(false);
                isZoomingInOnPhone = false;
                InputSystem.EnableDevice(Mouse.current);
            }
        }

        else if (isZoomingOutOnPhone)
        {
            timeElapsed += Time.deltaTime;
            float zoomProgress = Mathf.Clamp01(timeElapsed / zoomDuration);
            camera.transform.position = Vector3.Lerp(cameraPhone, cameraCenter, zoomProgress);
            camera.orthographicSize = Mathf.Lerp((float)1.5, 5, zoomProgress);

            if (zoomProgress == 1f)
            {
                isZoomingOutOnPhone = false;
                InputSystem.EnableDevice(Mouse.current);
            }
        }

        //FIXME

        if (isZoomingInOnTypewriter)
        {
            timeElapsed += Time.deltaTime;
            float zoomProgress = Mathf.Clamp01(timeElapsed / zoomDuration);
            float fadeOutProgress = Mathf.Clamp01(timeElapsed / fadeOutDuration);

            camera.transform.position = Vector3.Lerp(cameraCenter, cameraTypewriter, zoomProgress);
            camera.orthographicSize = Mathf.Lerp(5, 2, zoomProgress);
            blackscreen.color = Color.Lerp(transparent, Color.black, fadeOutProgress);

            if (zoomProgress == 1f)
            {
                secondTimeElapsed += Time.deltaTime;
                domesticScreen.SetActive(false);
                typewriterScreen.SetActive(true);
                camera.transform.position = cameraCenter;
                camera.orthographicSize = 5;
                float fadeInProgress = Mathf.Clamp01(secondTimeElapsed / fadeInDuration);
                float extraTimeProgress = Mathf.Clamp01(secondTimeElapsed / (fadeInDuration + 1));
                blackscreen.color = Color.Lerp(Color.black, transparent, fadeInProgress);

                if (extraTimeProgress == 1f)
                {
                    isZoomingInOnTypewriter = false;
                    InputSystem.EnableDevice(Mouse.current);
                }
            }
        }

        else if (isZoomingOutOnTypewriter)
        {
            timeElapsed += Time.deltaTime;
            float fadeOutProgress = Mathf.Clamp01(timeElapsed / fadeOutDuration);
            float extraTimeProgress = Mathf.Clamp01(timeElapsed / (fadeOutDuration + 1));
            blackscreen.color = Color.Lerp(transparent, Color.black, fadeOutProgress);

            if (extraTimeProgress == 1f)
            {
                camera.transform.position = cameraCenter;
                secondTimeElapsed += Time.deltaTime;
                typewriterScreen.SetActive(false);
                domesticScreen.SetActive(true);
                float fadeInProgress = Mathf.Clamp01(secondTimeElapsed / fadeInDuration);
                blackscreen.color = Color.Lerp(Color.black, transparent, fadeInProgress);

                if (fadeInProgress == 1f)
                {
                    isZoomingOutOnTypewriter = false;
                    InputSystem.EnableDevice(Mouse.current);
                }
            }
        }

        //FIXME

        else if (isZoomingInOnTelevision)
        {
            timeElapsed += Time.deltaTime;
            float zoomProgress = Mathf.Clamp01(timeElapsed / zoomDuration);
            float fadeOutProgress = Mathf.Clamp01(timeElapsed / fadeOutDuration);
            camera.transform.position = Vector3.Lerp(cameraCenter, cameraTelevision, zoomProgress);
            camera.orthographicSize = Mathf.Lerp(5, 2, zoomProgress);
            blackscreen.color = Color.Lerp(transparent, Color.black, fadeOutProgress);

            if (zoomProgress == 1f)
            {
                secondTimeElapsed += Time.deltaTime;
                domesticScreen.SetActive(false);
                televisionScreen.SetActive(true);
                camera.transform.position = cameraCenter;
                camera.orthographicSize = 5;
                float fadeInProgress = Mathf.Clamp01(secondTimeElapsed / fadeInDuration);
                float extraTimeProgress = Mathf.Clamp01(secondTimeElapsed / (fadeInDuration + 1));
                blackscreen.color = Color.Lerp(Color.black, transparent, fadeInProgress);

                if (extraTimeProgress == 1f)
                {
                    isZoomingInOnTelevision = false;
                    InputSystem.EnableDevice(Mouse.current);
                }
            }
        }

        else if (isZoomingOutOnTelevision)
        {
            timeElapsed += Time.deltaTime;
            float fadeOutProgress = Mathf.Clamp01(timeElapsed / fadeOutDuration);
            float extraTimeProgress = Mathf.Clamp01(timeElapsed / (fadeOutDuration + 1));
            blackscreen.color = Color.Lerp(transparent, Color.black, fadeOutProgress);

            if (extraTimeProgress == 1f)
            {
                secondTimeElapsed += Time.deltaTime;
                televisionScreen.SetActive(false);
                domesticScreen.SetActive(true);
                float fadeInProgress = Mathf.Clamp01(secondTimeElapsed / fadeInDuration);
                blackscreen.color = Color.Lerp(Color.black, transparent, fadeInProgress);

                if (fadeInProgress == 1f)
                {
                    isZoomingOutOnTelevision = false;
                    InputSystem.EnableDevice(Mouse.current);
                }
            }
        }



        else if (radioCrackling)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= 1)
            {
                radioCrackling = false;
                audioSource.PlayOneShot(militaryIntro01);
                listeningToMilitaryIntro01 = true;
                timeElapsed = 0;
            }
        }

        else if (listeningToMilitaryIntro01)
        {
            timeElapsed += Time.deltaTime;
            //InputSystem.DisableDevice(Mouse.current);

            if (timeElapsed >= 42)
            {
                militaryContinueButton.interactable = true;
                listeningToMilitaryIntro01 = false;
                InputSystem.EnableDevice(Mouse.current);
                audioSource.PlayOneShot(radioCrackle);

            }
        }

        else if (listeningToDomesticIntro01)
        {
            timeElapsed += Time.deltaTime;
            //InputSystem.DisableDevice(Mouse.current);

            if (timeElapsed >= 42)
            {
                domesticContinueButton.interactable = true;
                listeningToDomesticIntro01 = false;
                InputSystem.EnableDevice(Mouse.current);
                audioSource.PlayOneShot(radioCrackle);

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
        if (resourceManager.militaryAdvisorAvailable && !isZoomingInOnRadio)
        {
            InputSystem.DisableDevice(Mouse.current);

            audioSource.PlayOneShot(transitionSound);

            militaryCloseButton.GetComponent<Button>().interactable = false;

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingInOnRadio = true;
        }
    }

    public void RadioZoomOut()
    {
        militaryAdvisor.SetActive(false);

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
        if (!isFadingInOnLobby)
        {
            InputSystem.DisableDevice(Mouse.current);

            audioSource.PlayOneShot(transitionSound);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isFadingInOnLobby = true;
        }

    }

    public void EnterCampaignOffice()
    {

        audioSource.PlayOneShot(transitionSound);

        if (!isFadingInOnCampaignOffice)
        {
            InputSystem.DisableDevice(Mouse.current);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isFadingInOnCampaignOffice = true;
        }

    }

    public void PhoneZoomIn()
    {
        if (resourceManager.domesticAdvisorAvailable && !isZoomingInOnPhone)
        {
            InputSystem.DisableDevice(Mouse.current);

            audioSource.PlayOneShot(transitionSound);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingInOnPhone = true;
        }
    }

    public void PhoneZoomOut()
    {
        domesticAdvisor.SetActive(false);

        if (!isZoomingOutOnPhone)
        {
            InputSystem.DisableDevice(Mouse.current);

            audioSource.PlayOneShot(transitionSound);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingOutOnPhone = true;
        }
    }


    public void TypewriterZoomIn()
    {
        if (!isZoomingInOnTypewriter)
        {
            InputSystem.DisableDevice(Mouse.current);

            audioSource.PlayOneShot(transitionSound);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingInOnTypewriter = true;
        }
    }

    public void TypewriterZoomOut()
    {
        if (!isZoomingOutOnTypewriter)
        {
            InputSystem.DisableDevice(Mouse.current);

            audioSource.PlayOneShot(transitionSound);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingOutOnTypewriter = true;
        }
    }

    public void TelevisionZoomIn()
    {
        if (!isZoomingInOnTelevision)
        {
            InputSystem.DisableDevice(Mouse.current);

            audioSource.PlayOneShot(tvStatic);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingInOnTelevision = true;
        }
    }

    public void TelevisionZoomOut()
    {
        if (!isZoomingOutOnTelevision)
        {
            InputSystem.DisableDevice(Mouse.current);

            audioSource.PlayOneShot(tvStatic);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingOutOnTelevision = true;
        }
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

        militaryCloseButton.SetActive(false);
        militaryNextButton.SetActive(true);

        resourceManager.radioNotification.SetActive(false);
        foreach (GameObject question in resourceManager.militaryQuestionsArray)
        {
            question.SetActive(false);
        }

        if (numResultsViewed == 0)
        {
            if (ResourceManager.successCalculatedM1)
            {
                militaryAdvisorText.text = "I believe the resource commitment here has been, at least under the present circumstances, the right call. U.S.and ARVN forces are holding the major cities. The countryside is still contested, but it remains under government control. The Viet Cong took heavy losses during Tet, and Hanoi has not been able to achieve a decisive breakthrough. Our credibility with our allies is intact, and South Vietnam continues to function as a state.Now, I want to be clear: the cost has been immense.Troop levels are up, casualties are significant, and this war is far from over.But the central objective — preventing a communist takeover — has held. That is the foundation everything else sits on.";
            }
            else if (ResourceManager.deterioratingCalculatedM1)
            {
                militaryAdvisorText.text = "I have to be direct with you. South Vietnam is surviving, but only barely, and that is not a position I'm comfortable reporting. The Viet Cong and North Vietnamese forces have infiltrated much of the countryside. The ARVN is struggling to maintain control outside the cities. Pacification programs are collapsing in rural areas, and refugees are flooding into Saigon and the urban centers. I believe I'm correct in saying that international observers now see a government that is formally in power but effectively besieged. We have prevented total collapse — for now. But this situation cannot hold. Allies are beginning to question our staying power, and I would tell you that the resources allocated here were not sufficient for what we are facing. This is a very serious problem, and it is getting worse.";
            }
            else
            {
                militaryAdvisorText.text = "Let me be absolutely clear on what has happened here. South Vietnam is disintegrating. Communist forces, emboldened by their battlefield gains, have overrun large parts of the countryside and are moving on the major cities. The Thieu government is on the edge of collapse — it cannot inspire loyalty, it cannot command its own security forces, and it cannot govern. We are now confronting what I would have considered, six months ago, unthinkable: after everything this country has invested — the troops, the resources, the lives — we are facing the prospect of outright defeat. And I will tell you something else: this does not stay in Vietnam. The fall of South Vietnam is triggering a crisis of credibility across the globe. Every ally we have is watching, and every adversary is taking note. The resources committed to this objective were wholly inadequate, and the consequences of that decision are now playing out in front of us.";
            }
        }
        else if (numResultsViewed == 1) 
        {
            if (ResourceManager.successCalculatedM2)
            {
                militaryAdvisorText.text = "I believe I'm correct in saying that the resource allocation here has allowed our forces to mount effective counteroffensives after Tet. Major Viet Cong units have been badly degraded — on the order of several thousand casualties in key engagements — and North Vietnamese regulars have been pushed back from areas they held just weeks ago. Our commanders can point to clear tactical victories, and the data suggests momentum is shifting in our favor. Now, I want to be careful here. These successes are measured in battles won, not in war-ending progress. The enemy retreats, regroups, and prepares for the next round. That is the nature of what we're dealing with. But at least under the present circumstances, our troops on the ground feel they have regained the initiative, and that is not nothing. ";
            }
            else if (ResourceManager.deterioratingCalculatedM2)
            {
                militaryAdvisorText.text = "I have to be direct with you about the situation in the field. Our forces are holding their ground, but they cannot decisively beat back the Viet Cong or the North Vietnamese. Battles are dragging on inconclusively. We are inflicting heavy casualties — I believe the ratio is still in our favor — but the enemy seems capable of replenishing its ranks at a rate we did not anticipate. Soldiers on the ground are growing frustrated. Familiar villages are being fought over again and again. The military picture, frankly, is one of stalemate: neither side can declare victory. And I will tell you that the American public is increasingly questioning whether what we call \"progress\" has any meaning. The resources allocated here were not adequate to achieve what we needed, and we are now living with the consequences of that. ...";
            }
            else
            {
                militaryAdvisorText.text = "Let me be absolutely clear about what has happened. With insufficient resources devoted to combat operations, our forces have been outmaneuvered. The Viet Cong have launched coordinated attacks across multiple provinces, and North Vietnamese units are exploiting gaps in our lines that should never have existed. Casualties are mounting. Morale is dropping. Even bases we considered secure now feel vulnerable. And every evening, the American public is watching images of setbacks and bloody fighting on their television sets, and the perception — I would say the accurate perception — is that we are losing this war. I can't overstate the seriousness of this. Military failure on this scale deepens every political crisis we face, in Saigon and in Washington, and it leaves us with very little room to turn things around. The resources committed here were wholly inadequate, and we are now paying for that in American lives.";
            }
        }
        else if (numResultsViewed == 2)
        {
            if (ResourceManager.successCalculatedM3)
            {
                militaryAdvisorText.text = "The resource commitment to pacification is showing measurable results. Villages that were, not long ago, firmly under Viet Cong control now have functioning local militias. Schools and clinics are reopening under government protection. Roads are becoming safer for commerce, and farmers are beginning to feel secure enough to plant crops and revive local markets. You can see the effect of the hearts and minds programs -- they are making tangible progress on the ground. Now, communist cadres still operate in the shadows. Their grip has weakened, but they have not disappeared. Rural South Vietnam is showing flickers of stability for the first time in years, but I want to be clear: these gains are fragile and costly to maintain. At least under present circumstances, the resource allocation here was the right decision. Don't let that slip.";
            }
            else if (ResourceManager.deterioratingCalculatedM3)
            {
                militaryAdvisorText.text = "I have to give you an honest assessment of the pacification effort. The results are mixed, and in many areas they are failing. U.S. and ARVN units clear villages during the day. The Viet Cong come back at night. Strategic Hamlets are falling into disrepair. Corruption among local officials is alienating the very peasants we are trying to protect. Refugees from contested zones are flooding into the cities and straining resources we do not have to spare. Rural families are living in constant fear, unsure which side to trust, and many are hedging -- offering food and shelter to both our forces and the Viet Cong. You can see the heavy drain upon our position here. The countryside has become a patchwork of zones under tenuous control, and the overall situation is stagnant and unstable. The resources allocated to pacification were not adequate for the scale of what we are dealing with, and we are living with that now. ";
            }
            else
            {
                militaryAdvisorText.text = "I want there to be no misunderstanding about what has happened here. The South Vietnamese countryside has slipped decisively into communist hands. U.S. patrols rarely venture beyond their bases. ARVN units are either absent or ineffective. Viet Cong tax collectors and political officers are operating openly in villages, while government representatives have been driven out or killed. Farmers are fleeing en masse to the urban centers, creating a humanitarian crisis that is compounding every other problem we face. The South Vietnamese state has shrunk to what I would describe as an archipelago of insecure cities surrounded by hostile territory. For the ordinary Vietnamese peasant, the war has already been decided. And I will tell you something else: this was not inevitable. The resources committed to pacification were wholly inadequate, and the consequences are now visible to everyone.";
            }
        }
        else if (numResultsViewed == 3)
        {
            if (ResourceManager.successCalculatedM4)
            {
                militaryAdvisorText.text = "The resource allocation to political stabilization has produced what I would call measurable, if limited, progress. President Thieu has consolidated his position. Corruption has been curbed -- not eliminated, but curbed. Elections were stage-managed, I think we all understand that, but they were broadly accepted. Government ministries are beginning to function with greater efficiency. And here is what matters operationally: ARVN morale is improving. Soldiers feel they are fighting for a government that has some legitimacy. Saigon appears, at least for now, capable of governing. U.S. officials can point to genuine progress and make the case that Vietnamization might eventually succeed. Now, the war itself remains unresolved. But the political foundation -- which is what everything else depends on -- is holding. That's the situation.";
            }
            else if (ResourceManager.deterioratingCalculatedM4)
            {
                militaryAdvisorText.text = "Frankly, the political situation in Saigon is not where it needs to be. The Thieu government is limping along, riven by corruption and factional rivalries. Civil servants are skimming U.S. aid. ARVN officers are selling weapons on the black market. In the rural areas, government officials are seen not as protectors but as parasites. Thieu is clinging to power, but I believe I'm correct in saying that his legitimacy rests almost entirely on American money and American soldiers. Both the South Vietnamese public and international observers are beginning to ask the question we do not want asked: whether Saigon is a government worth defending. It's a problem with which we are seized, and I would tell you that the resources allocated here were not sufficient to build the kind of political stability this situation demands.";
            }
            else
            {
                militaryAdvisorText.text = "What has happened here is a political failure of the first order, and I want to be precise about what that means. The South Vietnamese state has collapsed in credibility. Rival generals are plotting against Thieu. Corruption scandals are exploding in the press. Ordinary citizens view their government as hopelessly illegitimate. ARVN units are deserting in large numbers. Those who remain are fighting half-heartedly, and they know why -- their leaders are corrupt and disconnected from the war they are being asked to fight. And here is what should concern you most: for many villagers, the Viet Cong -- despite their brutality -- now appear more disciplined and more reliable than Saigon's own officials. U.S. strategy is unraveling because Washington is propping up a government that scarcely exists outside the walls of the presidential palace. The resources committed to political stabilization were wholly inadequate. That is the fact of the matter.";
            }
            //militaryAdvisorText.text = "Classification: " + ResourceManager.classification;
        }
        else if (numResultsViewed == 4)
        {
            if (ResourceManager.successCalculatedM5)
            {
                militaryAdvisorText.text = "It's always a balancing of gains and losses in terms of U.S. lives, and I believe the resource allocation here has tipped that balance in the right direction. Investments in equipment, mobility, and defensive planning are keeping casualties relatively low. Helicopters are providing rapid evacuation. Bases have been hardened against attack. Patrols are better supplied and better supported. The soldiers on the ground feel -- and this matters -- that their commanders are taking their safety seriously. That sustains morale in the field. At home, casualty figures are no longer dominating the evening news in the same way, and that has eased some of the public anger. The war continues. But the perception is growing that American lives are not being wasted carelessly, and that perception has a basis in fact.";
            }
            else if (ResourceManager.deterioratingCalculatedM5)
            {
                militaryAdvisorText.text = "I can't be satisfied with what I'm seeing on troop protection, and I have to tell you why. The measures are inconsistent. Some units are receiving adequate support and evacuation capability. Others are being left exposed in dangerous zones with nothing close to what they need. Ambushes and mines are taking a steady toll. Casualty numbers are rising month by month. Soldiers are beginning to doubt the wisdom of operations that put them at risk for battles that don't appear to change the overall situation. Morale is dropping as men watch their friends wounded or killed. And this does not stay in the field. The rising body count is fueling antiwar demonstrations at home and eroding public confidence in the government's strategy. I would tell you that the resources allocated to protecting our forces were not sufficient, and the price of that is being paid in American lives.";
            }
            else
            {
                militaryAdvisorText.text = "Let me be absolutely clear on this point. Casualties are soaring. Poorly supported patrols are walking into ambushes. Bases are being shelled nightly. Medical evacuations cannot keep pace with battlefield injuries. Our soldiers have come to believe -- and I cannot tell you they are wrong -- that their lives are being squandered for a war with no end in sight. Desertions are climbing. Drug use is climbing. Morale has collapsed in the ranks. And every evening, American families are watching flag-draped coffins and grieving widows on their television sets. Public outrage has spiked. Congress is growing restless. Even our allies abroad are asking how long we can sustain losses at this rate. To obtain our political objective -- which is a very limited objective -- at as small as possible cost in American life: that was the standard. The resources committed to troop protection were wholly inadequate by that standard, and the consequences are measured in lives.";
            }
        }
        else if (numResultsViewed == 5)
        {
            if (!isFadingInOnCampaignOffice)
            {
                timeElapsed = 0f;
                secondTimeElapsed = 0f;
                isFadingInOnCampaignOffice = true;
            }

            if (ResourceManager.successCalculatedDo10)
            {
                domesticAdvisorText.text = "";
            }
            else if (ResourceManager.deterioratingCalculatedDo10)
            {
                domesticAdvisorText.text = "";
            }
            else
            {
                domesticAdvisorText.text = "";
            }
        }
        else if (numResultsViewed == 6)
        {
            if (ResourceManager.successCalculatedDo11)
            {
                domesticAdvisorText.text = "";
            }
            else if (ResourceManager.deterioratingCalculatedDo11)
            {
                domesticAdvisorText.text = "";
            }
            else
            {
                domesticAdvisorText.text = "";
            }
        }
        else if (numResultsViewed == 7)
        {
            if (ResourceManager.successCalculatedDo12)
            {
                domesticAdvisorText.text = "";
            }
            else if (ResourceManager.deterioratingCalculatedDo12)
            {
                domesticAdvisorText.text = "";
            }
            else
            {
                domesticAdvisorText.text = "";
            }
        }
        else if (numResultsViewed == 8)
        {
            if (ResourceManager.successCalculatedDo13)
            {
                domesticAdvisorText.text = "";
            }
            else if (ResourceManager.deterioratingCalculatedDo13)
            {
                domesticAdvisorText.text = "";
            }
            else
            {
                domesticAdvisorText.text = "";
            }
        }
        else if (numResultsViewed == 9)
        {
            if (ResourceManager.successCalculatedDo14)
            {
                domesticAdvisorText.text = "";
            }
            else if (ResourceManager.deterioratingCalculatedDo14)
            {
                domesticAdvisorText.text = "";
            }
            else
            {
                domesticAdvisorText.text = "";
            }
        }
        else if (numResultsViewed == 10)
        {
            if (ResourceManager.successCalculatedDo15)
            {
                domesticAdvisorText.text = "";
            }
            else if (ResourceManager.deterioratingCalculatedDo15)
            {
                domesticAdvisorText.text = "";
            }
            else
            {
                domesticAdvisorText.text = "";
            }
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
