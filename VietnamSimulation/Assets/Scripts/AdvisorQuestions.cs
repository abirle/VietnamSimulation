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
    bool reeling = false;
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
            
            if (timeElapsed >= 1.5) 
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

        if (reeling)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= 1.5f)
            {
                reeling = false;
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
                questionTime = 28;
                timeElapsed = 0;
                crackling = true;
                closeButton.interactable = false;
                //InputSystem.DisableDevice(Mouse.current);
                audioSource.PlayOneShot(radioStatic);
            }
            else if (numQuestion == 2)
            {
                screenChange.militaryAdvisorText.text = "This is a very serious and difficult question, and our strategists have been giving it intensive study. Securing the countryside, for example — if we get that right, it directly aids our efforts to stabilize the South Vietnamese government. You move one of these and it affects the others. That's the nature of what we're dealing with here. I'd think very carefully about where you put your weight.";
                questionTime = 19;
                timeElapsed = 0;
                crackling = true;
                closeButton.interactable = false;
                //InputSystem.DisableDevice(Mouse.current);
                audioSource.PlayOneShot(radioStatic);
            }
            else if (numQuestion == 3)
            {
                screenChange.militaryAdvisorText.text = "Yes, and this is a point I've been pressing. Our ability to secure and stabilize South Vietnam has a direct, measurable impact on our global credibility. Every foreign ministry in the world is watching. And on the domestic side — protecting our troops, keeping casualties as low as we can — that's what determines whether we can manage the antiwar movement and whether the party holds together for November. What happens on the ground out there doesn't stay out there. It comes home. So don't short change this side of the ledger.";
                questionTime = 28;
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
                screenChange.domesticAdvisorText.text = "It's the election, and I know that sounds simple with everything else you're juggling — yes, what happens overseas matters, what happens at the negotiating table matters, I'm not waving any of that off. But I've watched how this town works when a party loses the White House, and everything you care about goes with it — the Great Society, the agenda, any say in what comes next. The President told me this morning that November depends on a dozen things breaking right at once, and he's right about that. But the election's the one that keeps the rest of them in play. Watch the polling and watch what's coming off this convention floor — they're both telling you the same thing.";
                questionTime = 39;
                timeElapsed = 0;
                dialing = true;
                closeButton.interactable = false;
                //InputSystem.DisableDevice(Mouse.current);
                audioSource.PlayOneShot(radioStatic);
            }
            else if (numQuestion == 2)
            {
                screenChange.domesticAdvisorText.text = "Of course they do — you can see it playing out on this convention floor right now. Listening to the antiwar movement, actually hearing what the public's telling you, isn't just about the election — it's about keeping Congress with you. I've talked to three senators this week who all told me the same thing: if the party won't show it's hearing people on this war, they can't hold the line for us on the Hill. And the evening news is covering both as one story, because that's what it is.\r\n";
                questionTime = 32;
                timeElapsed = 0;
                dialing = true;
                closeButton.interactable = false;
                //InputSystem.DisableDevice(Mouse.current);
                audioSource.PlayOneShot(radioStatic);
            }
            else if (numQuestion == 3)
            {
                screenChange.domesticAdvisorText.text = "Well, that's the question, isn't it. If Congress turns on us — and I'm hearing it's closer than you'd think — we lose our ability to sustain what we're doing over there. A communist takeover doesn't have to come on the battlefield, you understand — it can come from the votes drying up on the Hill. And if you can't get ahead of public opinion on this war, good luck protecting our troops — the country won't stand behind a commitment it's stopped believing in. That's what the polls show, and what every American with a television set is watching tonight.";
                questionTime = 31;
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


        //FIXME
        if (room == "State Department")
        {
            if (numQuestion == 1)
            {
                screenChange.diplomaticAdvisorText.text = "It is our own reputation. In my judgment the whole of our position rests on a single thing: whether other governments believe we will do what we have said we will do. \r\nNo one can assure you that good intentions, kept to ourselves, count for anything abroad; what counts is what we have actually done, and whether the world reads it as resolve. The foundation is our standing in the world — our prestige, and whether our word is still believed.";
                questionTime = 37;
                timeElapsed = 0;
                reeling = true;
                closeButton.interactable = false;
                //InputSystem.DisableDevice(Mouse.current);
                audioSource.PlayOneShot(radioStatic);
            }
            else if (numQuestion == 2)
            {
                screenChange.diplomaticAdvisorText.text = "Yes. Open peace talks, or manage our relations with Moscow and Peking, and you add to our standing in the world. The other side does not deal seriously with a government it judges unreliable, and the capitals watching take their measure of us from whether we can bring an adversary to terms. I would urge you to put your influence there.";
                questionTime = 21;
                timeElapsed = 0;
                reeling = true;
                closeButton.interactable = false;
                //InputSystem.DisableDevice(Mouse.current);
                audioSource.PlayOneShot(radioStatic);
            }
            else if (numQuestion == 3)
            {
                screenChange.diplomaticAdvisorText.text = "Everything. The answer involves a judgment as to the costs to the United States. When Australia, New Zealand, Thailand, Taiwan, South Korea, and the Philippines stand with us, we are stronger in the field and stronger in the world; when they fall away, we are weaker in both. And the calendar matters here too. A serious opening toward talks before November could change how the war looks to the country, which counts for a good deal in an election year. What happens abroad is felt at home soon enough. \r\n";
                questionTime = 33;
                timeElapsed = 0;
                reeling = true;
                closeButton.interactable = false;
                //InputSystem.DisableDevice(Mouse.current);
                audioSource.PlayOneShot(radioStatic);
            }

            foreach (GameObject question in resourceManager.diplomaticQuestionsArray)
            {
                Button button = question.GetComponent<Button>();
                button.interactable = false;
            }

            //ResourceManager.pointsInvestigated = 0;

            questionAsked = true;
        }

    }
}
