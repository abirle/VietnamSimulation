using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AdvisorQuestions : MonoBehaviour
{
    GameObject gameManager;
    ScreenChange screenChange;
    ResourceManager resourceManager;

    Button questionButton;

    public string room;
    public int numQuestion;
    public bool questionAsked = false;

    public AudioSource audioSource;
    public AudioClip radioStatic;
    public AudioClip questionClip;

    float timeElapsed = 0;
    int questionTime;
    bool listening = false;
    bool crackling = false;
    public Button closeButton;


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
        if (crackling)
        {
            timeElapsed += Time.deltaTime;
            
            if (timeElapsed >= 1) 
            {
                crackling = false;
                audioSource.PlayOneShot(questionClip);
                listening = true;
                timeElapsed = 0;
            }
        }

        if (listening)
        {
            timeElapsed += Time.deltaTime;
            
            if (timeElapsed >= questionTime)
            {
                closeButton.interactable = true;
                InputSystem.EnableDevice(Mouse.current);
                listening = false;
                audioSource.PlayOneShot(radioStatic);

            }
        }
    }

    public void AskQuestion()
    {
        if (room == "War Tent")
        {
            if (numQuestion == 1)
            {
                screenChange.militaryAdvisorText.text = "Let me be absolutely clear on this. If we do not prevent a communist takeover of South Vietnam, everything else falls apart. Stabilizing their government, securing the countryside — none of that is possible if we lose the central objective. And I'll tell you something else: if that goes, it's not just the strategic position. It's our own people out there. The effect on troop morale and force protection would be severe. Make sure your resources reflect that.";
                questionTime = 29;
                timeElapsed = 0;
                crackling = true;
                closeButton.interactable = false;
                InputSystem.DisableDevice(Mouse.current);
                audioSource.PlayOneShot(radioStatic);
            }
            else if (numQuestion == 2)
            {
                screenChange.militaryAdvisorText.text = "This is a very serious and difficult question, and our strategists have been giving it intensive study. Securing the countryside, for example — if we get that right, it directly aids our efforts to stabilize the South Vietnamese government. You move one of these and it affects the others. That's the nature of what we're dealing with here. I'd think very carefully about where you put your weight.";
                questionTime = 22;
                timeElapsed = 0;
                crackling = true;
                closeButton.interactable = false;
                InputSystem.DisableDevice(Mouse.current);
                audioSource.PlayOneShot(radioStatic);
            }
            else if (numQuestion == 3)
            {
                screenChange.militaryAdvisorText.text = "Yes, and this is a point I've been pressing. Our ability to secure and stabilize South Vietnam has a direct, measurable impact on our global credibility. Every foreign ministry in the world is watching. And on the domestic side — protecting our troops, keeping casualties as low as we can — that's what determines whether we can manage the antiwar movement and whether the party holds together for November. What happens on the ground out there doesn't stay out there. It comes home. So don't short change this side of the ledger.";
                questionTime = 35;
                timeElapsed = 0;
                crackling = true;
                closeButton.interactable = false;
                InputSystem.DisableDevice(Mouse.current);
                audioSource.PlayOneShot(radioStatic);
            }

            foreach (GameObject question in resourceManager.militaryQuestionsArray)
            {
                Button button = question.GetComponent<Button>();
                button.interactable = false;
            }

            //ResourceManager.pointsInvestigated = 0;

            questionAsked = true;
        }

    }
}
