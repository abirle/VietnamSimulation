using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class IntroSkip : MonoBehaviour
{
    int numClicked = 0;
    public string room;
    bool listening = false;
    float timeElapsed = 0;

    public TMP_Text introText;
    public AudioSource audioSource;
    public AudioClip[] introClips;
    public AudioClip crackle;
    bool crackling = false;

    public GameObject closeButton;
    public GameObject nextButton;
    public GameObject ballRedactedIntro01;
    public GameObject ballRedactedIntro02;
    public GameObject ballRedactedIntro03;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (crackling)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= 1)
            {
                if (numClicked == 1)
                {
                    audioSource.PlayOneShot(introClips[0]);
                }
                else if (numClicked == 2)
                {
                    audioSource.PlayOneShot(introClips[1]);
                }

                listening = true;
                crackling = false;
            }
        }

        if (listening)
        {
            if (numClicked == 1 && room == "War Tent")
            {
                timeElapsed += Time.deltaTime;

                if (timeElapsed >= 41)
                {
                    nextButton.GetComponent<Button>().interactable = true;
                    listening = false;
                    InputSystem.EnableDevice(Mouse.current);
                    audioSource.PlayOneShot(crackle);

                }
            }
            else if (numClicked == 2 && room == "War Tent")
            {
                timeElapsed += Time.deltaTime;

                if (timeElapsed >= 41)
                {
                    closeButton.GetComponent<Button>().interactable = true;
                    listening = false;
                    InputSystem.EnableDevice(Mouse.current);
                    audioSource.PlayOneShot(crackle);
                }
            }

            else if (numClicked == 1 && room == "Campaign Office")
            {
                timeElapsed += Time.deltaTime;

                if (timeElapsed >= 37)
                {
                    nextButton.GetComponent<Button>().interactable = true;
                    listening = false;
                    InputSystem.EnableDevice(Mouse.current);
                    audioSource.PlayOneShot(crackle);
                }
            }

            else if (numClicked == 2 && room == "Campaign Office")
            {
                timeElapsed += Time.deltaTime;

                if (timeElapsed >= 35)
                {
                    closeButton.GetComponent<Button>().interactable = true;
                    listening = false;
                    InputSystem.EnableDevice(Mouse.current);
                    audioSource.PlayOneShot(crackle);
                }
            }

            else if (numClicked == 1 && room == "State Department")
            {
                timeElapsed += Time.deltaTime;

                if (timeElapsed >= 47)
                {
                    nextButton.GetComponent<Button>().interactable = true;
                    listening = false;
                    InputSystem.EnableDevice(Mouse.current);
                    audioSource.PlayOneShot(crackle);
                }
            }

            else if (numClicked == 2 && room == "State Department")
            {
                timeElapsed += Time.deltaTime;

                if (timeElapsed >= 49)
                {
                    closeButton.GetComponent<Button>().interactable = true;
                    listening = false;
                    InputSystem.EnableDevice(Mouse.current);
                    audioSource.PlayOneShot(crackle);
                }
            }
        }
    }

    public void Continue()
    {
        if (room == "War Tent")
        {
            if (numClicked == 0)
            {
                introText.text = "Now, in the countryside, the situation is, if anything, worse. The pacification programs are faltering. Strategic hamlets that were meant to protect local populations and isolate the Viet Cong — many of them have been abandoned or overrun. South Vietnamese units, in several provinces, are refusing to fight. \r\nCorruption in the government out there continues to undermine every effort to provide security and basic services. Our commanders are dealing with three problems at once: protecting their own troops, coordinating air and ground operations, and keeping supply lines open under conditions that are more difficult than we have publicly acknowledged.";
                audioSource.PlayOneShot(crackle);
                numClicked++;
                nextButton.GetComponent<Button>().interactable = false;
                timeElapsed = 0;
                crackling = true;
                //InputSystem.DisableDevice(Mouse.current);

            }
            else if (numClicked == 1) 
            {
                introText.text = "I understand there are other demands on your resources. The diplomatic situation, the domestic picture — I'm not going to tell you those don't matter. But at least under the present circumstances, everything depends on what happens on the ground. \r\nIt's always a balancing of gains and losses, and I will tell you plainly: if we do not hold the military position in Vietnam, our present target policies — diplomatic, domestic, all of it — fall apart. There is no negotiation from weakness. There is no domestic agenda that survives a collapse out there.\r\n\nThat's the situation. Think carefully about where these resources go.";
                audioSource.PlayOneShot(crackle);
                numClicked++;
                nextButton.SetActive(false);
                closeButton.SetActive(true);
                closeButton.GetComponent<Button>().interactable = false;
                timeElapsed = 0;
                crackling = true;
                //InputSystem.DisableDevice(Mouse.current);

            }

        }


        if (room == "Campaign Office")
        {
            if (numClicked == 0)
            {
                introText.text = "I spoke with the President this morning. He's worried about Congress — the pressure to cut commitments is coming from inside his own party now, not just the Republicans. He doesn't know how much longer he can keep the caucus in line.\r\n\r\nListen — he's trying to keep the Great Society going too, and it's getting away from him, because the schools can't make the new funding work and the money's running out now that the war's eating everything. Medicare and Medicaid are still expanding, civil rights is still moving, but the evening broadcasts are running stories about rising prices and stalled programs, and labor unrest wherever wages aren't keeping up.\r\n";
                audioSource.PlayOneShot(crackle);
                numClicked++;
                nextButton.GetComponent<Button>().interactable = false;
                timeElapsed = 0;
                crackling = true;
                //InputSystem.DisableDevice(Mouse.current);

            }

            else if (numClicked == 1)
            {
                introText.text = "I know you've got military and diplomatic people fighting for these same resources, and I'm not telling you they're wrong. But what happens at home is what people see every night, and come November it's what they'll be voting on. I've covered enough of these to tell you — when the country stops believing an administration, that's the ballgame.\r\n\r\nSo before you decide, do yourself a favor and turn on the television, see what the country's actually watching tonight, and take a good look at where the polling is. You don't have to take my word for it — it's all right there, and you'll want to see it for yourself before you decide where this goes. \r\n";
                audioSource.PlayOneShot(crackle);
                numClicked++;
                nextButton.SetActive(false);
                closeButton.SetActive(true);
                closeButton.GetComponent<Button>().interactable = false;
                timeElapsed = 0;
                crackling = true;
                //InputSystem.DisableDevice(Mouse.current);

            }
        }


        if (room == "State Department")
        {
            if (numClicked == 0)
            {
                //introText.text = "Understand first what is being tested. It is the credibility of our commitments and our prestige around the world, and it is being tested now as never before. Our friends abroad — the Australians, the South Koreans, the Filipinos — have sent troops and stood with us, and that counts for a great deal. But the NATO capitals and the neutral ones are watching, and they have begun to doubt that we can sustain a commitment of this size; the longer they watch, the more critical of us the world becomes.\r\n\r\nI watched the French exhaust themselves on this same ground at Dien Bien Phu. No one has demonstrated that an outside power can win another people's war there by force alone — which does not mean we cannot succeed, only that we cannot yet be sure.\r\n";
                introText.text = "";
                ballRedactedIntro01.SetActive(false);
                ballRedactedIntro02.SetActive(true);
                audioSource.PlayOneShot(crackle);
                numClicked++;
                nextButton.GetComponent<Button>().interactable = false;
                timeElapsed = 0;
                crackling = true;
                //InputSystem.DisableDevice(Mouse.current);

            }

            else if (numClicked == 1)
            {
                //introText.text = "Moscow and Peking read that same doubt, and they are answering it: more weapons, more advisers, more support to Hanoi, to test our resolve and widen the war if they can. Unless we give the Soviets a political alternative they can support, they have no reason to hold back. The instruments that will decide this are diplomatic, and they must be used while we still have the freedom of maneuver to use them.\r\n\r\nSo before you commit anything, read what your people put in front of you, and listen to the recordings — mine and the others at your disposal. They will tell you how we are seen abroad. After that, it is a matter of judgment. Not all things are the same size, and you cannot chase every one of them. Set your order of priorities, and spend your resources where they will do the most. \r\n";
                introText.text = "";
                ballRedactedIntro02.SetActive(false);
                ballRedactedIntro03.SetActive(true);
                audioSource.PlayOneShot(crackle);
                numClicked++;
                nextButton.SetActive(false);
                closeButton.SetActive(true);
                closeButton.GetComponent<Button>().interactable = false;
                timeElapsed = 0;
                crackling = true;
                //InputSystem.DisableDevice(Mouse.current);

            }
        }

    }
}
