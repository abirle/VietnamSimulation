using UnityEngine;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public GameObject infoText;
    public ButtonManager[] otherInfo;
    public bool infoClicked = false;

    public TextMeshProUGUI correspondingSliderText;
    public TextMeshProUGUI[] otherSliderTexts;


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

            correspondingSliderText.fontMaterial.EnableKeyword("UNDERLAY_ON");

            foreach (ButtonManager info in otherInfo)
            {
                info.infoText.SetActive(false);
                info.infoClicked = false;
            }

            foreach (TextMeshProUGUI text in otherSliderTexts)
            {
                text.fontMaterial.DisableKeyword("UNDERLAY_ON");
            }
        }

        else if (infoClicked)
        {
            //correspondingSliderText.outlineWidth = 0;
            correspondingSliderText.fontMaterial.DisableKeyword("UNDERLAY_ON");

            infoText.SetActive(false);
            infoClicked = false;
        }
    }
}
