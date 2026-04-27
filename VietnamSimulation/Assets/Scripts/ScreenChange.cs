using System.Resources;
using UnityEngine;
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


    public Camera camera;
    public Image blackscreen;
    Color transparent = new Color(0, 0, 0, 0);


    bool isZoomingInOnMap = false;
    bool isZoomingOutOnMap = false;
    bool isZoomingInOnLedger = false;
    bool isZoomingOutOnLedger = false;
    bool isZoomingInOnCorkboard = false;
    bool isZoomingOutOnCorkboard = false;
    bool isViewingLedger = false;
    bool isViewingMap = false;

    Vector3 cameraCenter = new Vector3(1288, 725, -1500);
    Vector3 cameraMap = new Vector3(1283, 722, -1500);
    Vector3 cameraCorkboard = new Vector3(1284, (float)725.7, -1500);
    Vector3 cameraLedger = new Vector3(1306, 725, -1500);
    float timeElapsed = 0f;
    float secondTimeElapsed = 0f;
    float zoomDuration = 3f;
    float fadeOutDuration = 2f;
    float fadeInDuration = 2f;
    float shiftDuration = 1f;
    

    GameObject gameManager;
    ResourceManager resourceManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager");
        resourceManager = gameManager.GetComponent<ResourceManager>();
        militaryGoalsScreen.SetActive(true);
        pauseScreen.SetActive(true);
        ledgerSliderScreen.SetActive(true);
        mapSliderScreen.SetActive(true);
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
                camera.transform.position = cameraCenter;
                camera.orthographicSize = 5;
                float fadeInProgress = Mathf.Clamp01(secondTimeElapsed / fadeInDuration);
                float extraTimeProgress = Mathf.Clamp01(secondTimeElapsed / (fadeInDuration + 1));
                blackscreen.color = Color.Lerp(Color.black, transparent, fadeInProgress);

                if (extraTimeProgress == 1f)
                {
                    isZoomingInOnMap = false;
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
                secondTimeElapsed += Time.deltaTime;
                mapSliderScreen.SetActive(false);
                militaryGoalsScreen.SetActive(true);
                float fadeInProgress = Mathf.Clamp01(secondTimeElapsed / fadeInDuration);
                blackscreen.color = Color.Lerp(Color.black, transparent, fadeInProgress);

                if (fadeInProgress == 1f)
                {
                    isZoomingOutOnMap = false;
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
            }
        }

    }

    public void MilitaryScene()
    {
        lobbyScreen.SetActive(false);
        militaryGoalsScreen.SetActive(true);
    }

    public void MapZoomIn()
    {
        if (!isZoomingInOnMap)
        {
            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingInOnMap = true;
        }
    }

    public void MapZoomOut()
    {
        if (!isZoomingOutOnMap)
        {
            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingOutOnMap = true;
        }
    }


    public void LedgerZoomIn()
    {
        if (!isViewingLedger && !isZoomingInOnMap)
        {
            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isViewingLedger = true;
        }
    }


    public void LedgerZoomOut()
    {
        if (!isViewingMap)
        {
            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isViewingMap = true;
        }
    }


    public void CorkboardZoomIn()
    {
        if (!isZoomingInOnCorkboard)
        {
            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingInOnCorkboard = true;
        }
    }

    public void CorkboardZoomOut()
    {
        if (!isZoomingOutOnCorkboard)
        {
            timeElapsed = 0f;
            secondTimeElapsed = 0f;
            isZoomingOutOnCorkboard = true;
        }
    }


    public void BackToLobby()
    {
        militaryGoalsScreen.SetActive(false);
        lobbyScreen.SetActive(true);
    }


    public void Decide()
    {
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
        decisionScreen.SetActive(false);
        resultsScreen.SetActive(true);

        resourceManager.CalculateResults();
        resourceManager.FinalizeResults();
    }

}
