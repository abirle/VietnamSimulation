using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    bool paused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!paused)
            {
                pauseMenu.SetActive(true);
                paused = true;
            }
            else if (paused)
            {
                pauseMenu.SetActive(false);
                paused = false;
            }
        }
    }

}
