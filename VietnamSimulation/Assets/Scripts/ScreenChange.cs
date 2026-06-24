using JetBrains.Annotations;
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
    public GameObject chalkboardScreen;
    public GameObject domesticScreen;
    public GameObject typewriterScreen;
    public GameObject diplomaticScreen;
    public GameObject teletypeScreen;

    public GameObject militaryAdvisor;
    public TMP_Text militaryAdvisorText;
    public GameObject militaryCloseButton;
    public GameObject militaryNextButton;
    public Image militaryImage;

    public GameObject domesticAdvisor;
    public TMP_Text domesticAdvisorText;
    public GameObject domesticCloseButton;
    public GameObject domesticNextButton;
    public Image domesticImage;

    public GameObject diplomaticAdvisor;
    public TMP_Text diplomaticAdvisorText;
    public GameObject diplomaticCloseButton;
    public GameObject diplomaticNextButton;
    public Image diplomaticImage;

    public GameObject lobbyFile;
    public TMP_Text lobbyText;
    public Image lobbyImage;
    public Sprite[] classificationImages;
    public GameObject lobbyCloseButton;
    public GameObject lobbyNextButton;
    public Button lobbyContinueButton;

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
    bool isZoomingInOnChalkboard = false;
    bool isZoomingOutOnChalkboard = false;
    bool isFadingInOnStateDepartment = false;
    bool isZoomingInOnTeletype = false;
    bool isZoomingOutOnTeletype = false;

    Vector3 cameraCenter = new Vector3(1288, 725, -1500);
    Vector3 cameraMap = new Vector3(1283, 722, -1500);
    Vector3 cameraCorkboard = new Vector3(1284, (float)725.7, -1500);
    Vector3 cameraLedger = new Vector3((float)1297.2, 725, -1500);
    Vector3 cameraRadio = new Vector3((float)1286.74, (float)723.66, -1500);
    Vector3 cameraPhone = new Vector3((float)1291.95, (float)723.8, -1500);
    Vector3 cameraTypewriter = new Vector3((float)1284.52, (float)722.29, -1500);
    Vector3 cameraTelevision = new Vector3((float)1290.4, 726, -1500);
    Vector3 cameraChalkboard = new Vector3((float)1282.8, (float)726.21, -1500);
    Vector3 cameraRecorder = new Vector3((float)1288.08, (float)722.46, -1500);
    Vector3 cameraTeletype = new Vector3((float)1284.52, (float)722.29, -1500);

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
    public AudioClip tvStatic;
    public AudioSource radioSource;

    public AudioClip militaryIntro01;
    bool listeningToMilitaryIntro01 = false;
    public Button militaryContinueButton;

    public AudioClip domesticIntro01;
    bool listeningToDomesticIntro01 = false;
    public Button domesticContinueButton;

    public AudioClip diplomaticIntro01;
    bool listeningToDiplomaticIntro01 = false;
    public Button diplomaticContinueButton;

    public AudioClip militaryQuestionIntro;
    public AudioClip radioCrackle;
    bool radioCrackling;

    public AudioClip domesticQuestionIntro;
    public AudioClip rotaryPhone;
    bool phoneSpinning;

    public AudioClip diplomaticQuestionIntro;
    public AudioClip tapeRecorder;
    bool tapeRecording;

    bool hasEnteredWarRoom = false;
    bool hasEnteredCampaignOffice = false;
    bool hasEnteredStateDepartment = false;
    public static bool viewingResults = false;
    int numResultsViewed = 0;

    public AudioClip[] successClips;
    public AudioClip[] deterioratingClips;
    public AudioClip[] failureClips;

    public Sprite[] successImages;
    public Sprite[] deterioratingImages;
    public Sprite[] failureImages;

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
        chalkboardScreen.SetActive(true);
        diplomaticScreen.SetActive(true);
        teletypeScreen.SetActive(true);
        
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
        chalkboardScreen.SetActive(false);
        diplomaticScreen.SetActive(false);
        teletypeScreen.SetActive(false);

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager");
        resourceManager = gameManager.GetComponent<ResourceManager>();

        AllScreensActive();

    }

// Update is called once per frame
    void FixedUpdate()
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
                radioSource.mute = true;
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
            Debug.Log(camera.orthographicSize);

            if (zoomProgress == 1f)
            {
                radioSource.mute = false;
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

        ///FIXME
        else if (isFadingInOnStateDepartment)
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

                camera.transform.position = cameraRecorder;
                camera.orthographicSize = (float)1.5;

                diplomaticAdvisor.SetActive(true);
                hasEnteredStateDepartment = true;

                diplomaticScreen.SetActive(true);
                isFadingInOnStateDepartment = false;
            }

            if (extraTimeProgress == 1f)
            {
                if (!hasEnteredStateDepartment || viewingResults)
                {
                    camera.transform.position = cameraRecorder;
                    camera.orthographicSize = (float)1.5;

                    diplomaticAdvisor.SetActive(true);
                    hasEnteredStateDepartment = true;

                }

                secondTimeElapsed += Time.deltaTime;
                AllScreensInactive();
                diplomaticScreen.SetActive(true);
                float fadeInProgress = Mathf.Clamp01(secondTimeElapsed / fadeInDuration);
                blackscreen.color = Color.Lerp(Color.black, transparent, fadeInProgress);

                if (fadeInProgress == 1f)
                {
                    isFadingInOnStateDepartment = false;
                    InputSystem.EnableDevice(Mouse.current);

                    if (camera.orthographicSize == (float)1.5)
                    {
                        audioSource.PlayOneShot(tapeRecorder);
                        tapeRecording = true;
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
                camera.transform.position = cameraCenter;
                camera.orthographicSize = 5;


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
                        //audioSource.PlayOneShot(domesticIntro01);
                        //listeningToDomesticIntro01 = true;
                        audioSource.PlayOneShot(rotaryPhone);
                        phoneSpinning = true;
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
                domesticAdvisorText.text = "Listen, I'm due on air shortly, but I've got a minute if you need me. One question, and then I've gotta go.";
                resourceManager.domesticAdvisorAvailable = false;
                resourceManager.phoneNotification.SetActive(false);
                isZoomingInOnPhone = false;
                audioSource.PlayOneShot(domesticQuestionIntro);
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

        else if (isZoomingInOnTypewriter)
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


        else if (isZoomingInOnChalkboard)
        {
            timeElapsed += Time.deltaTime;
            float zoomProgress = Mathf.Clamp01(timeElapsed / zoomDuration);
            float fadeOutProgress = Mathf.Clamp01(timeElapsed / fadeOutDuration);
            camera.transform.position = Vector3.Lerp(cameraCenter, cameraChalkboard, zoomProgress);
            camera.orthographicSize = Mathf.Lerp(5, 2, zoomProgress);
            blackscreen.color = Color.Lerp(transparent, Color.black, fadeOutProgress);

            if (zoomProgress == 1f)
            {
                secondTimeElapsed += Time.deltaTime;
                domesticScreen.SetActive(false);
                chalkboardScreen.SetActive(true);
                camera.transform.position = cameraCenter;
                camera.orthographicSize = 5;
                float fadeInProgress = Mathf.Clamp01(secondTimeElapsed / fadeInDuration);
                float extraTimeProgress = Mathf.Clamp01(secondTimeElapsed / (fadeInDuration + 1));
                blackscreen.color = Color.Lerp(Color.black, transparent, fadeInProgress);

                if (extraTimeProgress == 1f)
                {
                    isZoomingInOnChalkboard = false;
                    InputSystem.EnableDevice(Mouse.current);
                }
            }
        }

        else if (isZoomingOutOnChalkboard)
        {
            timeElapsed += Time.deltaTime;
            float fadeOutProgress = Mathf.Clamp01(timeElapsed / fadeOutDuration);
            float extraTimeProgress = Mathf.Clamp01(timeElapsed / (fadeOutDuration + 1));
            blackscreen.color = Color.Lerp(transparent, Color.black, fadeOutProgress);

            if (extraTimeProgress == 1f)
            {
                secondTimeElapsed += Time.deltaTime;
                chalkboardScreen.SetActive(false);
                domesticScreen.SetActive(true);
                float fadeInProgress = Mathf.Clamp01(secondTimeElapsed / fadeInDuration);
                blackscreen.color = Color.Lerp(Color.black, transparent, fadeInProgress);

                if (fadeInProgress == 1f)
                {
                    isZoomingOutOnChalkboard = false;
                    InputSystem.EnableDevice(Mouse.current);
                }
            }
        }

        //FIXME

        else if (isZoomingInOnTeletype)
        {
            timeElapsed += Time.deltaTime;
            float zoomProgress = Mathf.Clamp01(timeElapsed / zoomDuration);
            float fadeOutProgress = Mathf.Clamp01(timeElapsed / fadeOutDuration);

            camera.transform.position = Vector3.Lerp(cameraCenter, cameraTeletype, zoomProgress);
            camera.orthographicSize = Mathf.Lerp(5, 2, zoomProgress);
            blackscreen.color = Color.Lerp(transparent, Color.black, fadeOutProgress);

            if (zoomProgress == 1f)
            {
                secondTimeElapsed += Time.deltaTime;
                diplomaticScreen.SetActive(false);
                teletypeScreen.SetActive(true);
                camera.transform.position = cameraCenter;
                camera.orthographicSize = 5;
                float fadeInProgress = Mathf.Clamp01(secondTimeElapsed / fadeInDuration);
                float extraTimeProgress = Mathf.Clamp01(secondTimeElapsed / (fadeInDuration + 1));
                blackscreen.color = Color.Lerp(Color.black, transparent, fadeInProgress);

                if (extraTimeProgress == 1f)
                {
                    isZoomingInOnTeletype = false;
                    InputSystem.EnableDevice(Mouse.current);
                }
            }
        }

        else if (isZoomingOutOnTeletype)
        {
            timeElapsed += Time.deltaTime;
            float fadeOutProgress = Mathf.Clamp01(timeElapsed / fadeOutDuration);
            float extraTimeProgress = Mathf.Clamp01(timeElapsed / (fadeOutDuration + 1));
            blackscreen.color = Color.Lerp(transparent, Color.black, fadeOutProgress);

            if (extraTimeProgress == 1f)
            {
                camera.transform.position = cameraCenter;
                secondTimeElapsed += Time.deltaTime;
                teletypeScreen.SetActive(false);
                diplomaticScreen.SetActive(true);
                float fadeInProgress = Mathf.Clamp01(secondTimeElapsed / fadeInDuration);
                blackscreen.color = Color.Lerp(Color.black, transparent, fadeInProgress);

                if (fadeInProgress == 1f)
                {
                    isZoomingOutOnTeletype = false;
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
                if (!viewingResults)
                {
                    audioSource.PlayOneShot(militaryIntro01);
                    listeningToMilitaryIntro01 = true;
                    timeElapsed = 0;
                }
            }
        }

        else if (phoneSpinning)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= 2.5f)
            {
                phoneSpinning = false;
                if (!viewingResults)
                {
                    audioSource.PlayOneShot(domesticIntro01);
                    listeningToDomesticIntro01 = true;
                    timeElapsed = 0;
                }
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

            if (timeElapsed >= 53)
            {
                domesticContinueButton.interactable = true;
                listeningToDomesticIntro01 = false;
                InputSystem.EnableDevice(Mouse.current);
                audioSource.PlayOneShot(rotaryPhone);

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

    public void ChalkboardZoomIn()
    {
        if (!isZoomingInOnChalkboard)
        {
            InputSystem.DisableDevice(Mouse.current);

            audioSource.PlayOneShot(transitionSound);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingInOnChalkboard = true;
        }
    }

    public void ChalkboardZoomOut()
    {
        if (!isZoomingOutOnChalkboard)
        {
            InputSystem.DisableDevice(Mouse.current);

            audioSource.PlayOneShot(transitionSound);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingOutOnChalkboard = true;
        }
    }


    public void TeletypeZoomIn()
    {
        if (!isZoomingInOnTeletype)
        {
            InputSystem.DisableDevice(Mouse.current);

            audioSource.PlayOneShot(transitionSound);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingInOnTeletype = true;
        }
    }

    public void TeletypeZoomOut()
    {
        if (!isZoomingOutOnTeletype)
        {
            InputSystem.DisableDevice(Mouse.current);

            audioSource.PlayOneShot(transitionSound);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingOutOnTeletype = true;
        }
    }


    public void EnterStateDepartment()
    {
        audioSource.PlayOneShot(transitionSound);

        if (!isFadingInOnStateDepartment)
        {
            InputSystem.DisableDevice(Mouse.current);

            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isFadingInOnStateDepartment = true;
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

        militaryContinueButton.gameObject.SetActive(false);
        militaryCloseButton.SetActive(false);
        militaryNextButton.SetActive(true);
        militaryImage.gameObject.SetActive(true);

        resourceManager.radioNotification.SetActive(false);
        foreach (GameObject question in resourceManager.militaryQuestionsArray)
        {
            question.SetActive(false);
        }

        decisionScreen.SetActive(false);
        viewingResults = true;
        audioSource.Stop();
        audioSource.PlayOneShot(transitionSound);

        if (numResultsViewed == 0)
        {
            if (!isFadingInOnWarRoom)
            {
                timeElapsed = 0f;
                secondTimeElapsed = 0f;
                isFadingInOnWarRoom = true;
            }

            if (ResourceManager.successCalculatedM1)
            {
                militaryAdvisorText.text = "I believe the resource commitment here has been, at least under the present circumstances, the right call. U.S.and ARVN forces are holding the major cities. The countryside is still contested, but it remains under government control. The Viet Cong took heavy losses during Tet, and Hanoi has not been able to achieve a decisive breakthrough. Our credibility with our allies is intact, and South Vietnam continues to function as a state.Now, I want to be clear: the cost has been immense.Troop levels are up, casualties are significant, and this war is far from over.But the central objective — preventing a communist takeover — has held. That is the foundation everything else sits on.";
                audioSource.PlayOneShot(successClips[0]);
                militaryImage.sprite = successImages[0];
            }
            else if (ResourceManager.deterioratingCalculatedM1)
            {
                militaryAdvisorText.text = "I have to be direct with you. South Vietnam is surviving, but only barely, and that is not a position I'm comfortable reporting. The Viet Cong and North Vietnamese forces have infiltrated much of the countryside. The ARVN is struggling to maintain control outside the cities. Pacification programs are collapsing in rural areas, and refugees are flooding into Saigon and the urban centers. I believe I'm correct in saying that international observers now see a government that is formally in power but effectively besieged. We have prevented total collapse — for now. But this situation cannot hold. Allies are beginning to question our staying power, and I would tell you that the resources allocated here were not sufficient for what we are facing. This is a very serious problem, and it is getting worse.";
                audioSource.PlayOneShot(deterioratingClips[0]);
                militaryImage.sprite = deterioratingImages[0];
            }
            else
            {
                militaryAdvisorText.text = "Let me be absolutely clear on what has happened here. South Vietnam is disintegrating. Communist forces, emboldened by their battlefield gains, have overrun large parts of the countryside and are moving on the major cities. The Thieu government is on the edge of collapse — it cannot inspire loyalty, it cannot command its own security forces, and it cannot govern. We are now confronting what I would have considered, six months ago, unthinkable: after everything this country has invested — the troops, the resources, the lives — we are facing the prospect of outright defeat. And I will tell you something else: this does not stay in Vietnam. The fall of South Vietnam is triggering a crisis of credibility across the globe. Every ally we have is watching, and every adversary is taking note. The resources committed to this objective were wholly inadequate, and the consequences of that decision are now playing out in front of us.";
                audioSource.PlayOneShot(failureClips[0]);
                militaryImage.sprite = failureImages[0];
            }
        }
        else if (numResultsViewed == 1) 
        {
            if (ResourceManager.successCalculatedM2)
            {
                militaryAdvisorText.text = "I believe I'm correct in saying that the resource allocation here has allowed our forces to mount effective counteroffensives after Tet. Major Viet Cong units have been badly degraded — on the order of several thousand casualties in key engagements — and North Vietnamese regulars have been pushed back from areas they held just weeks ago. Our commanders can point to clear tactical victories, and the data suggests momentum is shifting in our favor. Now, I want to be careful here. These successes are measured in battles won, not in war-ending progress. The enemy retreats, regroups, and prepares for the next round. That is the nature of what we're dealing with. But at least under the present circumstances, our troops on the ground feel they have regained the initiative, and that is not nothing. ";
                audioSource.PlayOneShot(successClips[1]);
                militaryImage.sprite = successImages[1];
            }
            else if (ResourceManager.deterioratingCalculatedM2)
            {
                militaryAdvisorText.text = "I have to be direct with you about the situation in the field. Our forces are holding their ground, but they cannot decisively beat back the Viet Cong or the North Vietnamese. Battles are dragging on inconclusively. We are inflicting heavy casualties — I believe the ratio is still in our favor — but the enemy seems capable of replenishing its ranks at a rate we did not anticipate. Soldiers on the ground are growing frustrated. Familiar villages are being fought over again and again. The military picture, frankly, is one of stalemate: neither side can declare victory. And I will tell you that the American public is increasingly questioning whether what we call \"progress\" has any meaning. The resources allocated here were not adequate to achieve what we needed, and we are now living with the consequences of that. ...";
                audioSource.PlayOneShot(deterioratingClips[1]);
                militaryImage.sprite = deterioratingImages[1];
            }
            else
            {
                militaryAdvisorText.text = "Let me be absolutely clear about what has happened. With insufficient resources devoted to combat operations, our forces have been outmaneuvered. The Viet Cong have launched coordinated attacks across multiple provinces, and North Vietnamese units are exploiting gaps in our lines that should never have existed. Casualties are mounting. Morale is dropping. Even bases we considered secure now feel vulnerable. And every evening, the American public is watching images of setbacks and bloody fighting on their television sets, and the perception — I would say the accurate perception — is that we are losing this war. I can't overstate the seriousness of this. Military failure on this scale deepens every political crisis we face, in Saigon and in Washington, and it leaves us with very little room to turn things around. The resources committed here were wholly inadequate, and we are now paying for that in American lives.";
                audioSource.PlayOneShot(failureClips[1]);
                militaryImage.sprite = failureImages[1];
            }
        }
        else if (numResultsViewed == 2)
        {
            if (ResourceManager.successCalculatedM3)
            {
                militaryAdvisorText.text = "The resource commitment to pacification is showing measurable results. Villages that were, not long ago, firmly under Viet Cong control now have functioning local militias. Schools and clinics are reopening under government protection. Roads are becoming safer for commerce, and farmers are beginning to feel secure enough to plant crops and revive local markets. You can see the effect of the hearts and minds programs -- they are making tangible progress on the ground. Now, communist cadres still operate in the shadows. Their grip has weakened, but they have not disappeared. Rural South Vietnam is showing flickers of stability for the first time in years, but I want to be clear: these gains are fragile and costly to maintain. At least under present circumstances, the resource allocation here was the right decision. Don't let that slip.";
                audioSource.PlayOneShot(successClips[2]);
                militaryImage.sprite = successImages[2];
            }
            else if (ResourceManager.deterioratingCalculatedM3)
            {
                militaryAdvisorText.text = "I have to give you an honest assessment of the pacification effort. The results are mixed, and in many areas they are failing. U.S. and ARVN units clear villages during the day. The Viet Cong come back at night. Strategic Hamlets are falling into disrepair. Corruption among local officials is alienating the very peasants we are trying to protect. Refugees from contested zones are flooding into the cities and straining resources we do not have to spare. Rural families are living in constant fear, unsure which side to trust, and many are hedging -- offering food and shelter to both our forces and the Viet Cong. You can see the heavy drain upon our position here. The countryside has become a patchwork of zones under tenuous control, and the overall situation is stagnant and unstable. The resources allocated to pacification were not adequate for the scale of what we are dealing with, and we are living with that now. ";
                audioSource.PlayOneShot(deterioratingClips[2]);
                militaryImage.sprite = deterioratingImages[2];
            }
            else
            {
                militaryAdvisorText.text = "I want there to be no misunderstanding about what has happened here. The South Vietnamese countryside has slipped decisively into communist hands. U.S. patrols rarely venture beyond their bases. ARVN units are either absent or ineffective. Viet Cong tax collectors and political officers are operating openly in villages, while government representatives have been driven out or killed. Farmers are fleeing en masse to the urban centers, creating a humanitarian crisis that is compounding every other problem we face. The South Vietnamese state has shrunk to what I would describe as an archipelago of insecure cities surrounded by hostile territory. For the ordinary Vietnamese peasant, the war has already been decided. And I will tell you something else: this was not inevitable. The resources committed to pacification were wholly inadequate, and the consequences are now visible to everyone.";
                audioSource.PlayOneShot(failureClips[2]);
                militaryImage.sprite = failureImages[2];
            }
        }
        else if (numResultsViewed == 3)
        {
            if (ResourceManager.successCalculatedM4)
            {
                militaryAdvisorText.text = "The resource allocation to political stabilization has produced what I would call measurable, if limited, progress. President Thieu has consolidated his position. Corruption has been curbed -- not eliminated, but curbed. Elections were stage-managed, I think we all understand that, but they were broadly accepted. Government ministries are beginning to function with greater efficiency. And here is what matters operationally: ARVN morale is improving. Soldiers feel they are fighting for a government that has some legitimacy. Saigon appears, at least for now, capable of governing. U.S. officials can point to genuine progress and make the case that Vietnamization might eventually succeed. Now, the war itself remains unresolved. But the political foundation -- which is what everything else depends on -- is holding. That's the situation.";
                audioSource.PlayOneShot(successClips[3]);
                //militaryImage.sprite = successImages[3];
            }
            else if (ResourceManager.deterioratingCalculatedM4)
            {
                militaryAdvisorText.text = "Frankly, the political situation in Saigon is not where it needs to be. The Thieu government is limping along, riven by corruption and factional rivalries. Civil servants are skimming U.S. aid. ARVN officers are selling weapons on the black market. In the rural areas, government officials are seen not as protectors but as parasites. Thieu is clinging to power, but I believe I'm correct in saying that his legitimacy rests almost entirely on American money and American soldiers. Both the South Vietnamese public and international observers are beginning to ask the question we do not want asked: whether Saigon is a government worth defending. It's a problem with which we are seized, and I would tell you that the resources allocated here were not sufficient to build the kind of political stability this situation demands.";
                audioSource.PlayOneShot(deterioratingClips[3]);
            }
            else
            {
                militaryAdvisorText.text = "What has happened here is a political failure of the first order, and I want to be precise about what that means. The South Vietnamese state has collapsed in credibility. Rival generals are plotting against Thieu. Corruption scandals are exploding in the press. Ordinary citizens view their government as hopelessly illegitimate. ARVN units are deserting in large numbers. Those who remain are fighting half-heartedly, and they know why -- their leaders are corrupt and disconnected from the war they are being asked to fight. And here is what should concern you most: for many villagers, the Viet Cong -- despite their brutality -- now appear more disciplined and more reliable than Saigon's own officials. U.S. strategy is unraveling because Washington is propping up a government that scarcely exists outside the walls of the presidential palace. The resources committed to political stabilization were wholly inadequate. That is the fact of the matter.";
                audioSource.PlayOneShot(failureClips[3]);
            }
            //militaryAdvisorText.text = "Classification: " + ResourceManager.classification;
        }
        else if (numResultsViewed == 4)
        {
            if (ResourceManager.successCalculatedM5)
            {
                militaryAdvisorText.text = "It's always a balancing of gains and losses in terms of U.S. lives, and I believe the resource allocation here has tipped that balance in the right direction. Investments in equipment, mobility, and defensive planning are keeping casualties relatively low. Helicopters are providing rapid evacuation. Bases have been hardened against attack. Patrols are better supplied and better supported. The soldiers on the ground feel -- and this matters -- that their commanders are taking their safety seriously. That sustains morale in the field. At home, casualty figures are no longer dominating the evening news in the same way, and that has eased some of the public anger. The war continues. But the perception is growing that American lives are not being wasted carelessly, and that perception has a basis in fact.";
                audioSource.PlayOneShot(successClips[4]);
                //militaryImage.sprite = successImages[4];
            }
            else if (ResourceManager.deterioratingCalculatedM5)
            {
                militaryAdvisorText.text = "I can't be satisfied with what I'm seeing on troop protection, and I have to tell you why. The measures are inconsistent. Some units are receiving adequate support and evacuation capability. Others are being left exposed in dangerous zones with nothing close to what they need. Ambushes and mines are taking a steady toll. Casualty numbers are rising month by month. Soldiers are beginning to doubt the wisdom of operations that put them at risk for battles that don't appear to change the overall situation. Morale is dropping as men watch their friends wounded or killed. And this does not stay in the field. The rising body count is fueling antiwar demonstrations at home and eroding public confidence in the government's strategy. I would tell you that the resources allocated to protecting our forces were not sufficient, and the price of that is being paid in American lives.";
                audioSource.PlayOneShot(deterioratingClips[4]);
            }
            else
            {
                militaryAdvisorText.text = "Let me be absolutely clear on this point. Casualties are soaring. Poorly supported patrols are walking into ambushes. Bases are being shelled nightly. Medical evacuations cannot keep pace with battlefield injuries. Our soldiers have come to believe -- and I cannot tell you they are wrong -- that their lives are being squandered for a war with no end in sight. Desertions are climbing. Drug use is climbing. Morale has collapsed in the ranks. And every evening, American families are watching flag-draped coffins and grieving widows on their television sets. Public outrage has spiked. Congress is growing restless. Even our allies abroad are asking how long we can sustain losses at this rate. To obtain our political objective -- which is a very limited objective -- at as small as possible cost in American life: that was the standard. The resources committed to troop protection were wholly inadequate by that standard, and the consequences are measured in lives.";
                audioSource.PlayOneShot(failureClips[4]);
            }
        }
        else if (numResultsViewed == 5)
        {
            domesticContinueButton.gameObject.SetActive(false);
            domesticCloseButton.SetActive(false);
            domesticNextButton.SetActive(true);
            domesticImage.gameObject.SetActive(true);

            if (!isFadingInOnCampaignOffice)
            {
                timeElapsed = 0f;
                secondTimeElapsed = 0f;
                isFadingInOnCampaignOffice = true;
            }

            if (ResourceManager.successCalculatedDo10)
            {
                domesticAdvisorText.text = "Listen, I wanted to call because for once I have something good to report.\r\n\r\nWhatever you're doing over there with the messaging, it's working. The party is holding together. I've talked to half a dozen state chairs this week, and they're telling me the same thing -- Humphrey looks credible, the constituencies are lining up, and the campaign has found a way to talk about Vietnam that doesn't drive the moderates out the door. The bombing pauses and concessions have convinced the antiwar people that someone is listening, and the loyalists still see a firm commitment to American credibility abroad.\r\n\r\nI spoke with the President this morning. He's noticing it too. The polling in the key states looks competitive, and the ground operation is actually mobilizing the way it's supposed to. People are showing up. Steady leadership abroad, continued progress at home -- that's the message getting through.\r\n\r\nYou've preserved enough political capital to keep Washington functioning. The polls are reflecting it, and tonight's broadcasts will too. \r\n";
                audioSource.PlayOneShot(successClips[5]);
                //domesticImage.sprite = successImages[5];
            }
            else if (ResourceManager.deterioratingCalculatedDo10)
            {
                domesticAdvisorText.text = "I wish I were calling with better news.\r\n\r\nThe unity is fraying, and it's not just behind closed doors anymore. I'm watching Democrats break publicly from the administration's Vietnam policy -- on camera, in interviews, on this convention floor. The antiwar activists are rallying against the candidate, and the campaign can't seem to settle on a message. Voters don't know where the party stands, and that uncertainty is killing you in the battleground states.\r\n\r\nI'm hearing from people on the ground that grassroots mobilization is uneven at best, and the opposition is picking up momentum in exactly the places you can't afford to lose.\r\n\r\nThe internal divisions are making it harder to control the narrative and harder to influence policy. The numbers are moving in the wrong direction, and every night the evening news is showing a party at war with itself. That's what the American people are taking away from this.\r\n";
                audioSource.PlayOneShot(deterioratingClips[5]);
            }
            else
            {
                domesticAdvisorText.text = "I don't know how else to say this -- the party is coming apart.\r\n\r\nKey figures are defecting. I've watched people I've known for twenty years go on the record criticizing the administration's Vietnam strategy, and the candidate is standing out there politically isolated. The antiwar protests are dominating every broadcast -- mine included -- and the opposition is consolidating support among moderates and disaffected voters faster than I've ever seen.\r\n\r\nOn the ground, the campaign infrastructure is barely functioning. They can't get volunteers, they can't organize rallies, and voter mobilization has collapsed in the states that matter. I spoke with the President this morning, and I could hear it in his voice -- he knows what this means.\r\n\r\nWashington's credibility is damaged, the ability to conduct this war is constrained by the chaos here, and what allies and adversaries are seeing on their television screens is American political leadership falling apart. Turn on any channel tonight and that's the story.\r\n";
                audioSource.PlayOneShot(failureClips[5]);
            }
        }
        else if (numResultsViewed == 6)
        {
            if (ResourceManager.successCalculatedDo11)
            {
                domesticAdvisorText.text = "I have to tell you, I'm pleased to report this one.\r\n\r\nThe administration has handled the public on this war better than I thought possible. The press briefings have been sharp, the speeches have hit the right notes, and those selective concessions have convinced a lot of Americans that the government actually understands what people are feeling without making us look weak.\r\n\r\nThe protests are still out there, but they've stayed largely peaceful and contained. I've talked to people in Senator Mansfield's office this week, and they're saying the same thing I'm hearing from sources across the country -- public opinion has stabilized, and some say it's even shifted slightly in favor of continued, measured engagement. Families of soldiers are writing letters that say support from home feels steadier.\r\n\r\nPolicymakers can actually focus on strategy now instead of spending every waking hour on damage control. That's what the right messaging buys you -- and it shows in the polling. Keep it there.\r\n";
                audioSource.PlayOneShot(successClips[6]);
                //domesticImage.sprite = successImages[6];
            }
            else if (ResourceManager.deterioratingCalculatedDo11)
            {
                domesticAdvisorText.text = "Listen, I need you to hear this.\r\n\r\nPublic dissatisfaction is growing, and the protests are getting harder to ignore. The antiwar activists are dominating my broadcasts and everyone else's -- civilian casualties, bloody battles, government mismanagement. That's what Americans are seeing every night in their living rooms. The administration is still putting out statements and making minor adjustments, but it reads as indecisive, and the public is losing confidence.\r\n\r\nI'm hearing from soldiers' families that morale is slipping because support from home feels uncertain. And the political people can't figure out how to coordinate domestic messaging with what's happening overseas. One feeds the other.\r\n\r\nThe approval numbers are sliding, and every evening broadcast is leading with the protests. That's the picture America is getting of this administration.\r\n";
                audioSource.PlayOneShot(deterioratingClips[6]);
            }
            else
            {
                domesticAdvisorText.text = "What I'm seeing out here is a crisis, and I need you to understand that.\r\n\r\nThe antiwar movement has surged past anything we anticipated. It's dominating every headline, and there is nothing the administration can do right now to get ahead of it. Large-scale protests, strikes, campus unrest across the country -- policymakers are completely on the defensive.\r\n\r\nSoldiers' families are telling me they're afraid to put on the evening news. The hostility from home is real, and morale is collapsing. Congress is under enormous pressure to limit funding or push for withdrawal. I've talked to Senator Fulbright's people, and they say the votes for restrictions are closer than anyone in the White House wants to admit.\r\n\r\nCronkite said it last month, and the numbers are proving him right -- the American public has turned on this war. Every night that story gets louder on every channel in the country, and there is nothing coming out of this convention that can compete with it.\r\n";
                audioSource.PlayOneShot(failureClips[6]);
            }
        }
        else if (numResultsViewed == 7)
        {
            if (ResourceManager.successCalculatedDo12)
            {
                domesticAdvisorText.text = "I wanted to call with some good news for a change. Congress is holding. The key committees approved the funding without major amendments, the partisan attacks have stayed muted, and the leadership is backing Vietnam out loud -- which steadies our allies and tells the soldiers in the field the country is behind them. And it's reaching them: with the money flowing, commanders are getting what they need, and the pacification programs and troop rotations are moving without getting hung up in Washington.\r\n\r\nI spoke with the President this morning, and he's relieved -- Congress is steady enough that he can run the war and protect the domestic agenda at once. The story on the evening news tonight is unity, not division, and that's worth more than people in this building realize. \r\n";
                //audioSource.PlayOneShot(successClips[7]);
                //domesticImage.sprite = successImages[7];
            }
            else if (ResourceManager.deterioratingCalculatedDo12)
            {
                domesticAdvisorText.text = "I need to tell you what I'm hearing on the Hill, because it's not good. Support in Congress is getting uneven -- legislators demanding more oversight, dragging their feet on funding, attaching conditions that tie the administration's hands. The hearings are swallowed up by Vietnam, and the uncertainty is spilling into the policy and the planning. It's reaching the field, too: I'm hearing commanders are hitting sporadic shortages and restrictions, and the men out there read it as Washington not knowing what it wants. The White House is stuck running the war while it fights the Hill for the means to run it.\r\n\r\nFamilies of soldiers are noticing, the allies see the discord -- diplomatic sources tell me it's making people nervous -- and congressional approval keeps sliding every time a contentious hearing hits the evening news. And that's what worries me -- the confidence holding this administration together is wearing thin, and it won't take much more to crack it.\r\n";
                //audioSource.PlayOneShot(deterioratingClips[7]);
            }
            else
            {
                domesticAdvisorText.text = "Listen, what's happening on the Hill right now is exactly what I was afraid of. Congress is actively working against the strategy -- funding delayed and cut, oversight committees going after the war effort on camera. I talked to Senator Gore's and Senator Fulbright's offices, and there's a real push for withdrawal or hard limits that nobody thinks can be stopped. And it's gutting the war itself: I'm hearing the shortfalls have hit the field, commanders can't mount the offensives, the pacification operations are stalling.\r\n\r\nThe families are watching Congress and the White House go at each other on television, seeing a government that can't agree the war is worth fighting. The President's furious, but he knows the damage is done. Allies and adversaries alike are reading American resolve coming apart -- and tonight every channel's lead story is a divided Washington. \r\n";
                //audioSource.PlayOneShot(failureClips[7]);
            }
        }
        else if (numResultsViewed == 8)
        {
            if (ResourceManager.successCalculatedDo13)
            {
                domesticAdvisorText.text = "I have to say, you've managed something I wasn't sure was possible.\r\n\r\nThe administration has kept inflation and unemployment under control despite what this war is costing. Defense spending is being balanced with domestic programs, and the public actually believes the government knows how to handle guns and butter at the same time. And it carries through to the men over there -- the steady funding keeps the equipment, the supplies, the support flowing without interruption.\r\n\r\nI've talked to families across the country this week, and the mood is cautiously optimistic -- people feel like the government has a handle on things. The President told me this morning that confidence in Washington is steady, and that's keeping the public willing to tolerate the war.\r\n\r\nThe polling reflects it. Keep the resources where they are.\r\n";
                //audioSource.PlayOneShot(successClips[8]);
                //domesticImage.sprite = successImages[8];
            }
            else if (ResourceManager.deterioratingCalculatedDo13)
            {
                domesticAdvisorText.text = "I'm calling because the economic picture is starting to worry me, and it should worry you too.\r\n\r\nInflation is climbing, consumer confidence is shaky, and the budget fight between military and domestic spending is getting ugly. I'm hearing from people in labor and on the Hill that prices for basic goods are going up, and unrest is showing up where it hadn't before -- sporadic strikes, walkouts, real frustration from people whose wages aren't keeping up. And the squeeze is reaching the field: I'm told the logistics and support start to falter whenever the budget tightens, and the men notice.\r\n\r\nFamilies are feeling it too, and they're connecting it to the war. Every night the evening news runs another story about rising prices alongside footage from Vietnam, and people draw their own conclusions.\r\n\r\nThe approval numbers on the economy are sliding, and the administration can't hold the line on a story that says both the war and the economy are under control.\r\n";
                //audioSource.PlayOneShot(deterioratingClips[8]);
            }
            else
            {
                domesticAdvisorText.text = "I don't know how to soften this, so I won't try.\r\n\r\nThe economy is failing under the weight of this war. Inflation has spiked, unemployment is rising, and there are shortages of essential goods people can see and feel every single day. Great Society programs are being cut or delayed, and the public is furious -- I've talked to people across this country who feel abandoned by their government. And it's bleeding into the war itself: I'm hearing the men are short on supplies, reinforcements are running late, the support just isn't there on the battlefield.\r\n\r\nFamilies of soldiers are writing letters about what they can't afford at home while their sons are fighting overseas. Trust in Washington is collapsing, and opposition to this war is louder than it has ever been.\r\n\r\nI spoke with the President this morning. He knows that allies and adversaries alike are reading the economic turmoil as a sign of American weakness. The numbers don't lie, and neither does the nightly news. \r\n";
                //audioSource.PlayOneShot(failureClips[8]);
            }
        }
        else if (numResultsViewed == 9)
        {
            if (ResourceManager.successCalculatedDo14)
            {
                domesticAdvisorText.text = "14: Success";
                //audioSource.PlayOneShot(successClips[9]);
                //domesticImage.sprite = successImages[9];
            }
            else if (ResourceManager.deterioratingCalculatedDo14)
            {
                domesticAdvisorText.text = "14: Situation Deteriorating";
                //audioSource.PlayOneShot(deterioratingClips[9]);
            }
            else
            {
                domesticAdvisorText.text = "14: Failure";
                //audioSource.PlayOneShot(failureClips[9]);
            }
        }
        else if (numResultsViewed == 10)
        {
            if (ResourceManager.successCalculatedDo15)
            {
                domesticAdvisorText.text = "15: Success";
                //audioSource.PlayOneShot(successClips[10]);
                //domesticImage.sprite = successImages[10];
            }
            else if (ResourceManager.deterioratingCalculatedDo15)
            {
                domesticAdvisorText.text = "15: Situation Deteriorating";
                //audioSource.PlayOneShot(deterioratingClips[10]);
            }
            else
            {
                domesticAdvisorText.text = "15: Failure";
                //audioSource.PlayOneShot(failureClips[10]);
            }
        }
        else if (numResultsViewed == 11)
        {
            diplomaticContinueButton.gameObject.SetActive(false);
            diplomaticCloseButton.SetActive(false);
            diplomaticNextButton.SetActive(true);
            diplomaticImage.gameObject.SetActive(true);

            if (!isFadingInOnStateDepartment)
            {
                timeElapsed = 0f;
                secondTimeElapsed = 0f;
                isFadingInOnStateDepartment = true;
            }

            if (ResourceManager.successCalculatedDi6)
            {
                diplomaticAdvisorText.text = "6: Success";
                //audioSource.PlayOneShot(successClips[11]);
                //diplomaticImage.sprite = successImages[11];
            }
            else if (ResourceManager.deterioratingCalculatedDi6)
            {
                diplomaticAdvisorText.text = "6: Situation Deteriorating";
                //audioSource.PlayOneShot(deterioratingClips[11]);
            }
            else
            {
                diplomaticAdvisorText.text = "6: Failure";
                //audioSource.PlayOneShot(failureClips[11]);
            }
        }
        else if (numResultsViewed == 12)
        {
            if (ResourceManager.successCalculatedDi7)
            {
                diplomaticAdvisorText.text = "7: Success";
                //audioSource.PlayOneShot(successClips[12]);
                //diplomaticImage.sprite = successImages[12];
            }
            else if (ResourceManager.deterioratingCalculatedDi7)
            {
                diplomaticAdvisorText.text = "7: Situation Deteriorating";
                //audioSource.PlayOneShot(deterioratingClips[12]);
            }
            else
            {
                diplomaticAdvisorText.text = "7: Failure";
                //audioSource.PlayOneShot(failureClips[12]);
            }
        }
        else if (numResultsViewed == 13)
        {
            if (ResourceManager.successCalculatedDi8)
            {
                diplomaticAdvisorText.text = "8: Success";
                //audioSource.PlayOneShot(successClips[13]);
                //diplomaticImage.sprite = successImages[13];
            }
            else if (ResourceManager.deterioratingCalculatedDi8)
            {
                diplomaticAdvisorText.text = "8: Situation Deteriorating";
                //audioSource.PlayOneShot(deterioratingClips[13]);
            }
            else
            {
                diplomaticAdvisorText.text = "8: Failure";
                //audioSource.PlayOneShot(failureClips[13]);
            }
        }
        else if (numResultsViewed == 14)
        {
            if (ResourceManager.successCalculatedDi9)
            {
                diplomaticAdvisorText.text = "9: Success";
                //audioSource.PlayOneShot(successClips[14]);
                //diplomaticImage.sprite = successImages[14];
            }
            else if (ResourceManager.deterioratingCalculatedDi9)
            {
                diplomaticAdvisorText.text = "9: Situation Deteriorating";
                //audioSource.PlayOneShot(deterioratingClips[14]);
            }
            else
            {
                diplomaticAdvisorText.text = "9: Failure";
                //audioSource.PlayOneShot(failureClips[14]);
            }
        }
        else if (numResultsViewed == 15)
        {
            if (!isFadingInOnLobby)
            {
                timeElapsed = 0f;
                secondTimeElapsed = 0f;
                isFadingInOnLobby = true;
            }

            lobbyContinueButton.gameObject.SetActive(false);
            lobbyCloseButton.SetActive(false);
            lobbyNextButton.SetActive(true);

            lobbyFile.SetActive(true);

            ResourceManager resourceManager = gameManager.GetComponent<ResourceManager>();

            string allResourcesText = resourceManager.GetAllResourcesText();

            lobbyText.text = allResourcesText;
        }
        else if (numResultsViewed == 16)
        {
            if (ResourceManager.classification == "Warrior")
            {
                lobbyText.text = "Classification: Warrior";
                lobbyImage.sprite = classificationImages[0];
            }
            else if (ResourceManager.classification == "Politician")
            {
                lobbyText.text = "Classification: Politician";
                lobbyImage.sprite = classificationImages[1];
            }
            else if (ResourceManager.classification == "Diplomat")
            {
                lobbyText.text = "Classification: Diplomat";
                lobbyImage.sprite = classificationImages[2];
            }
        }


        numResultsViewed += 1;

        //resultsScreen.SetActive(true);
    }

}
