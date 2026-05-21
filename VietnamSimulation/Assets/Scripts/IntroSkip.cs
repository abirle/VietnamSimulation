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

    public GameObject closeButton;
    public GameObject nextButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (listening)
        {
            if (numClicked == 1)
            {
                timeElapsed += Time.deltaTime;

                if (timeElapsed >= 40)
                {
                    nextButton.GetComponent<Button>().interactable = true;
                    listening = false;
                    InputSystem.EnableDevice(Mouse.current);
                }
            }
            else if (numClicked == 2)
            {
                timeElapsed += Time.deltaTime;

                if (timeElapsed >= 38)
                {
                    closeButton.GetComponent<Button>().interactable = true;
                    listening = false;
                    InputSystem.EnableDevice(Mouse.current);

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
                introText.text = "Now, in the countryside, the situation is, if anything, worse. The pacification programs are faltering. Strategic hamlets that were meant to protect local populations and isolate the Viet Cong — many of them have been abandoned or overrun. South Vietnamese units, in several provinces, are refusing to fight. Corruption in the government out there continues to undermine every effort to provide security and basic services. Our commanders are dealing with three problems at once: protecting their own troops, coordinating air and ground operations, and keeping supply lines open under conditions that are more difficult than we have publicly acknowledged.";
                audioSource.PlayOneShot(introClips[0]);
                numClicked++;
                nextButton.GetComponent<Button>().interactable = false;
                timeElapsed = 0;
                listening = true;
                InputSystem.DisableDevice(Mouse.current);

            }
            else if (numClicked == 1) 
            {
                introText.text = "I understand there are other demands on your resources. The diplomatic situation, the domestic picture — I'm not going to tell you those don't matter. But at least under the present circumstances, everything depends on what happens on the ground. It's always a balancing of gains and losses, and I will tell you plainly: if we do not hold the military position in Vietnam, our present target policies — diplomatic, domestic, all of it — fall apart. There is no negotiation from weakness. There is no domestic agenda that survives a collapse out there.\r\n\nThat's the situation. Think carefully about where these resources go.";
                audioSource.PlayOneShot(introClips[1]);
                numClicked++;
                nextButton.SetActive(false);
                closeButton.SetActive(true);
                closeButton.GetComponent<Button>().interactable = false;
                timeElapsed = 0;
                listening = true;
                InputSystem.DisableDevice(Mouse.current);

            }

        }

    }
}
