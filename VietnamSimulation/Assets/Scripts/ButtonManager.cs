using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject infoText;
    public ButtonManager[] otherInfo;
    public bool infoClicked = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MoreInfo()
    {
        if (!infoClicked)
        {
            infoText.SetActive(true);
            infoClicked = true;

            foreach (ButtonManager info in otherInfo)
            {
                info.infoText.SetActive(false);
                info.infoClicked = false;
            }
        }

        else if (infoClicked)
        {
            infoText.SetActive(false);
            infoClicked = false;
        }
    }
}
