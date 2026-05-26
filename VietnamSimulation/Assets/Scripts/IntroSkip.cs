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

                if (timeElapsed >= 31)
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

                if (timeElapsed >= 55)
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
                introText.text = "And here's the other thing you cannot afford to ignore — the President is still trying to hold the Great Society together. Schools are struggling to make the new funding work. Medicare and Medicaid are expanding, civil rights initiatives are still moving forward, but the money is running out because the war is eating everything. The economists are worried about inflation and deficits, and you're seeing labor unrest in places where wages aren't keeping up with prices.\r\nI know there are military and diplomatic priorities competing for these resources. But what's happening on the home front is what the American people see and feel every day, and it's what they'll be voting on in November. If you lose the public, none of the rest of it holds together. Make sure your messaging and resources reflect that.\r\n";
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
