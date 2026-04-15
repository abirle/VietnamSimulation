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
    public Camera camera;
    public Image blackscreen;
    Color transparent = new Color(0, 0, 0, 0);


    bool isZoomingInOnMap;
    Vector3 cameraCenter = new Vector3(1288, 725, -1500);
    Vector3 cameraMap = new Vector3(1283, 722, -1500);
    float timeElapsed = 0f;
    float zoomDuration = 3f;
    

    GameObject gameManager;
    ResourceManager resourceManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager");
        resourceManager = gameManager.GetComponent<ResourceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isZoomingInOnMap)
        {
            timeElapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(timeElapsed / zoomDuration);
            camera.transform.position = Vector3.Lerp(cameraCenter, cameraMap, progress);
            camera.orthographicSize = Mathf.Lerp(5, 2, progress);
            blackscreen.color = Color.Lerp(transparent, Color.black, progress);

            if (progress == 1f)
            {
                isZoomingInOnMap = false;
                militaryGoalsScreen.SetActive(false);
                mapSliderScreen.SetActive(true);
                camera.transform.position = new Vector3(1288, 725, -1500);
                camera.orthographicSize = 5;
                blackscreen.color = transparent;
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
        timeElapsed = 0f;
        isZoomingInOnMap = true;
    }

    public void MapZoomOut()
    {
        camera.orthographicSize = 5;
        camera.transform.position = new Vector3(1288, 725, -1500);

        mapSliderScreen.SetActive(false);
        militaryGoalsScreen.SetActive(true);
    }


    public void LedgerZoomIn()
    {
        militaryGoalsScreen.SetActive(false);
        ledgerSliderScreen.SetActive(true);
    }


    public void LedgerZoomOut()
    {
        ledgerSliderScreen.SetActive(false);
        militaryGoalsScreen.SetActive(true);
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
