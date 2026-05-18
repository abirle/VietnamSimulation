using System.Resources;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AdvisorQuestions : MonoBehaviour
{
    GameObject gameManager;
    ScreenChange screenChange;
    ResourceManager resourceManager;

    Button questionButton;

    public int numQuestion;
    public bool questionAsked = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager");
        resourceManager = gameManager.GetComponent<ResourceManager>();
        screenChange = gameManager.GetComponent<ScreenChange>();
        questionButton = gameObject.GetComponent<Button>();
        questionButton.interactable = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AskQuestion()
    {
        if (numQuestion == 1)
        {
            screenChange.militaryAdvisorText.text = "Question #1 Answer";
        }
        else if (numQuestion == 2)
        {
            screenChange.militaryAdvisorText.text = "Question #2 Answer";
        }
        else if (numQuestion == 3)
        {
            screenChange.militaryAdvisorText.text = "Question #3 Answer";
        }

        foreach(GameObject question in resourceManager.militaryQuestionsArray)
        {
            Button button = question.GetComponent<Button>();
            button.interactable = false;
        }

        //ResourceManager.pointsInvestigated = 0;

        questionAsked = true;
    }
}
