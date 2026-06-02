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
    bool dialing = false;
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

        if (dialing)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= 2.5f)
            {
                dialing = false;
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
                //InputSystem.EnableDevice(Mouse.current);
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
                //InputSystem.DisableDevice(Mouse.current);
                audioSource.PlayOneShot(radioStatic);
            }
            else if (numQuestion == 2)
            {
                screenChange.militaryAdvisorText.text = "This is a very serious and difficult question, and our strategists have been giving it intensive study. Securing the countryside, for example — if we get that right, it directly aids our efforts to stabilize the South Vietnamese government. You move one of these and it affects the others. That's the nature of what we're dealing with here. I'd think very carefully about where you put your weight.";
                questionTime = 22;
                timeElapsed = 0;
                crackling = true;
                closeButton.interactable = false;
                //InputSystem.DisableDevice(Mouse.current);
                audioSource.PlayOneShot(radioStatic);
            }
            else if (numQuestion == 3)
            {
                screenChange.militaryAdvisorText.text = "Yes, and this is a point I've been pressing. Our ability to secure and stabilize South Vietnam has a direct, measurable impact on our global credibility. Every foreign ministry in the world is watching. And on the domestic side — protecting our troops, keeping casualties as low as we can — that's what determines whether we can manage the antiwar movement and whether the party holds together for November. What happens on the ground out there doesn't stay out there. It comes home. So don't short change this side of the ledger.";
                questionTime = 35;
                timeElapsed = 0;
                crackling = true;
                closeButton.interactable = false;
                //InputSystem.DisableDevice(Mouse.current);
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



        ///FIXME
        if (room == "Campaign Office")
        {
            if (numQuestion == 1)
            {
                screenChange.domesticAdvisorText.text = "The election. I know there are a lot of moving pieces, and yes, what happens overseas matters, what happens at the negotiating table matters. But if we don't keep the Democratic Party unified and in the White House, none of it counts. We lose the Great Society, we lose the legislative agenda, we lose our ability to shape what comes next. The President told me this morning that winning in November depends on a dozen things going right at once. He's not wrong. But the election is the one that makes all the others possible. The polling tells you that, and so does every broadcast coming out of this convention.";
                questionTime = 38;
                timeElapsed = 0;
                dialing = true;
                closeButton.interactable = false;
                //InputSystem.DisableDevice(Mouse.current);
                audioSource.PlayOneShot(radioStatic);
            }
            else if (numQuestion == 2)
            {
                screenChange.domesticAdvisorText.text = "Of course they do — you can see it playing out right now on this convention floor. Responding to the antiwar movement, actually listening to what the public is telling you — that's not just about the election. That's about keeping Congress on your side. I've talked to three senators this week who told me the same thing: if the party doesn't show it's hearing the American people on this war, they can't hold the line for us on the Hill. One feeds the other — and the evening news is covering both stories as if they're the same story, because they are.";
                questionTime = 22;
                timeElapsed = 0;
                dialing = true;
                closeButton.interactable = false;
                //InputSystem.DisableDevice(Mouse.current);
                audioSource.PlayOneShot(radioStatic);
            }
            else if (numQuestion == 3)
            {
                screenChange.domesticAdvisorText.text = "Well, that's the question, isn't it. If Congress turns on us — and I'm hearing from people who say it's closer than you think — we lose our ability to sustain what we're doing over there. That's how you end up with a communist takeover, not because of what happens on the battlefield but because the votes dry up on the Hill. And if we can't get ahead of public opinion on this war, good luck protecting our troops — because the country won't stand behind a commitment it no longer believes in. That's what the polls are showing, and that's what every American with a television set is seeing tonight.";
                questionTime = 34;
                timeElapsed = 0;
                dialing = true;
                closeButton.interactable = false;
                //InputSystem.DisableDevice(Mouse.current);
                audioSource.PlayOneShot(radioStatic);
            }

            foreach (GameObject question in resourceManager.domesticQuestionsArray)
            {
                Button button = question.GetComponent<Button>();
                button.interactable = false;
            }

            //ResourceManager.pointsInvestigated = 0;

            questionAsked = true;
        }

    }
}
