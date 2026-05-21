using Mono.Cecil;
using System;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.UI;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

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

    Vector3 cameraCenter = new Vector3(1288, 725, -1500);
    Vector3 cameraMap = new Vector3(1283, 722, -1500);
    Vector3 cameraCorkboard = new Vector3(1284, (float)725.7, -1500);
    Vector3 cameraLedger = new Vector3((float)1297.2, 725, -1500);
    Vector3 cameraRadio = new Vector3((float)1286.74, (float)723.66, -1500);
    Vector3 cameraPhone = new Vector3((float)1290.33, (float)722.90, -1500);
    Vector3 cameraTypewriter = new Vector3(1283, 722, -1500);
    
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
                    if (!viewingResults)
                    {
                        audioSource.PlayOneShot(militaryIntro01);
                        listeningToMilitaryIntro01 = true;
                    }
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

        else if (listeningToMilitaryIntro01)
        {
            timeElapsed += Time.deltaTime;
            InputSystem.DisableDevice(Mouse.current);

            if (timeElapsed >= 41)
            {
                militaryContinueButton.interactable = true;
                listeningToMilitaryIntro01 = false;
                InputSystem.EnableDevice(Mouse.current);
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
            militaryAdvisorText.text = resourceManager.resultsText.text;
        }
        else if (numResultsViewed == 1) 
        {
            if (ResourceManager.successCalculatedM1)
            {
                militaryAdvisorText.text = "I believe the resource commitment here has been, at least under the present circumstances, the right call. U.S.and ARVN forces are holding the major cities. The countryside is still contested, but it remains under government control. The Viet Cong took heavy losses during Tet, and Hanoi has not been able to achieve a decisive breakthrough. Our credibility with our allies is intact, and South Vietnam continues to function as a state.Now, I want to be clear: the cost has been immense.Troop levels are up, casualties are significant, and this war is far from over.But the central objective — preventing a communist takeover — has held. That is the foundation everything else sits on.";
            }
            else if (ResourceManager.deterioratingCalculatedM1)
            {
                militaryAdvisorText.text = "I have to be direct with you. South Vietnam is surviving, but only barely, and that is not a position I'm comfortable reporting. The Viet Cong and North Vietnamese forces have infiltrated much of the countryside. The ARVN is struggling to maintain control outside the cities. Pacification programs are collapsing in rural areas, and refugees are flooding into Saigon and the urban centers. I believe I'm correct in saying that international observers now see a government that is formally in power but effectively besieged. We have prevented total collapse — for now. But this situation cannot hold. Allies are beginning to question our staying power, and I would tell you that the resources allocated here were not sufficient for what we are facing. This is a very serious problem, and it is getting worse.\r\n";
            }
            else
            {
                militaryAdvisorText.text = "Let me be absolutely clear on what has happened here. South Vietnam is disintegrating. Communist forces, emboldened by their battlefield gains, have overrun large parts of the countryside and are moving on the major cities. The Thieu government is on the edge of collapse — it cannot inspire loyalty, it cannot command its own security forces, and it cannot govern. We are now confronting what I would have considered, six months ago, unthinkable: after everything this country has invested — the troops, the resources, the lives — we are facing the prospect of outright defeat. And I will tell you something else: this does not stay in Vietnam. The fall of South Vietnam is triggering a crisis of credibility across the globe. Every ally we have is watching, and every adversary is taking note. The resources committed to this objective were wholly inadequate, and the consequences of that decision are now playing out in front of us.";
            }
            //militaryAdvisorText.text = "second advisor message";
        }
        else if (numResultsViewed == 2)
        {
            militaryAdvisorText.text = "third advisor message";
        }
        else if (numResultsViewed == 3)
        {
            militaryAdvisorText.text = "fourth advisor message";
            //militaryAdvisorText.text = "Classification: " + ResourceManager.classification;

            //militaryNextButton.SetActive(false);
            //militaryCloseButton.SetActive(true);
        }
        else if (numResultsViewed == 4)
        {
            militaryAdvisorText.text = "fifth advisor message";
        }
        else if (numResultsViewed == 5)
        {
            if (!isFadingInOnCampaignOffice)
            {
                timeElapsed = 0f;
                secondTimeElapsed = 0f;
                isFadingInOnCampaignOffice = true;
            }

            domesticAdvisorText.text = resourceManager.resultsText.text;
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
