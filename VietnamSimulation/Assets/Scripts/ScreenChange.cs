using System.Resources;
using UnityEngine;

public class ScreenChange : MonoBehaviour
{
    public GameObject lobbyScreen;
    public GameObject militaryGoalsScreen;
    public GameObject mapSliderScreen;
    public GameObject ledgerSliderScreen;
    public GameObject decisionScreen;
    public GameObject resultsScreen;
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
        
    }

    public void MilitaryScene()
    {
        lobbyScreen.SetActive(false);
        militaryGoalsScreen.SetActive(true);
    }

    public void MapZoomIn()
    {
        militaryGoalsScreen.SetActive(false);
        mapSliderScreen.SetActive(true);
    }

    public void MapZoomOut()
    {
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
