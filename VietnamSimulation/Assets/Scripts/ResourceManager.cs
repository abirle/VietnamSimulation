using TMPro;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public TMP_Text resourceNumberText;
    public TMP_Text whatsLeftText;
    public TMP_Text resultsText;

    int totalResources = 200;
    int numResources;

    int numM1Resources = 0;
    int numM2Resources = 0;
    int numM3Resources = 0;
    int numM4Resources = 0;
    int numM5Resources = 0;
    int numDi6Resources = 0;
    int numDi7Resources = 0;
    int numDi8Resources = 0;
    int numDi9Resources = 0;
    int numDo10Resources = 0;
    int numDo11Resources = 0;
    int numDo12Resources = 0;
    int numDo13Resources = 0;
    int numDo14Resources = 0;
    int numDo15Resources = 0;

    int numSliders = 15;

    bool hasClickedM1 = false;
    bool hasClickedM2 = false;
    bool hasClickedM3 = false;
    bool hasClickedM4 = false;
    bool hasClickedM5 = false;
    bool hasClickedDi6 = false;
    bool hasClickedDi7 = false;
    bool hasClickedDi8 = false;
    bool hasClickedDi9 = false;
    bool hasClickedDo10 = false;
    bool hasClickedDo11 = false;
    bool hasClickedDo12 = false;
    bool hasClickedDo13 = false;
    bool hasClickedDo14 = false;
    bool hasClickedDo15 = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numResources = totalResources;
        resourceNumberText.text = numResources.ToString();
        whatsLeftText.text = "You have " + numSliders.ToString() + " untouched categories and " + numResources.ToString() + " resources left.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ResourcesToText()
    {
        numResources = totalResources 
            - numM1Resources 
            - numM2Resources 
            - numM3Resources 
            - numM4Resources 
            - numM5Resources
            - numDi6Resources
            - numDi7Resources
            - numDi8Resources
            - numDi9Resources
            - numDo10Resources
            - numDo11Resources
            - numDo12Resources
            - numDo13Resources
            - numDo14Resources
            - numDo15Resources; 
        resourceNumberText.text = numResources.ToString();

        whatsLeftText.text = "You have " + numSliders.ToString() + " untouched categories and " + numResources.ToString() + " resources left.";

    }


    public void SetM1Resources(int resourcesUsed)
    {
        numM1Resources = resourcesUsed;
        ResourcesToText();

        if (!hasClickedM1)
        {
            hasClickedM1 = true;
            numSliders--;
        }

    }


    public void SetM2Resources(int resourcesUsed)
    {
        numM2Resources = resourcesUsed;
        ResourcesToText();

        if (!hasClickedM2)
        {
            hasClickedM2 = true;
            numSliders--;
        }
    }


    public void SetM3Resources(int resourcesUsed)
    {
        numM3Resources = resourcesUsed;
        ResourcesToText();

        if (!hasClickedM3)
        {
            hasClickedM3 = true;
            numSliders--;
        }
    }


    public void SetM4Resources(int resourcesUsed)
    {
        numM4Resources = resourcesUsed;
        ResourcesToText();

        if (!hasClickedM4)
        {
            hasClickedM4 = true;
            numSliders--;
        }
    }


    public void SetM5Resources(int resourcesUsed)
    {
        numM5Resources = resourcesUsed;
        ResourcesToText();

        if (!hasClickedM5)
        {
            hasClickedM5 = true;
            numSliders--;
        }
    }

    public void CalculateResults()
    {
        //INTERDEPENDENCY CALCULATIONS

        // Prevent a Communist takeover of South Vietnam
        if (numM1Resources < 15)
        {
            resultsText.text = "M1: failure";

            numM3Resources = -100;
            numM3Resources = -100;
        }
        else if (numM1Resources < 30)
        {
            resultsText.text = "M1: situation deteriorating";

            numM2Resources -= 10;
            numM5Resources -= 5;
        }
        else
        {
            resultsText.text = "M1: success";

            numM3Resources += 10;
            numM3Resources += 5;
        }

        // Achieve military success in the field
        if (numM2Resources < 12)
        {
            resultsText.text += " M2: failure";
        }
        else if (numM2Resources < 25)
        {
            resultsText.text += " M2: situation deteriorating";
        }
        else
        {
            resultsText.text += " M2: success";
        }

        // Secure South Vietnamese countryside (pacification)
        if (numM3Resources < 12)
        {
            resultsText.text += " M3: failure";
        }
        else if (numM3Resources < 25)
        {
            resultsText.text += " M3: situation deteriorating";
        }
        else
        {
            resultsText.text += " M3: success";
        }

        // Stabilize and strengthen the South Vietnamese government
        if (numM4Resources < 10)
        {
            resultsText.text += " M4: failure";
        }
        else if (numM4Resources < 20)
        {
            resultsText.text += " M4: situation deteriorating";
        }
        else
        {
            resultsText.text += " M4: success";
        }

        // Protect U.S. troops and minimize casualties
        if (numM5Resources < 10)
        {
            resultsText.text += " M5: failure";
        }
        else if (numM5Resources < 20)
        {
            resultsText.text += " M5: situation deteriorating";
        }
        else
        {
            resultsText.text += " M5: success";
        }




        //FINAL RESULTS


        // Prevent a Communist takeover of South Vietnam
        if (numM1Resources < 15)
        {
            resultsText.text = "M1: failure";

            numM3Resources = -100;
            numM3Resources = -100;
        }
        else if (numM1Resources < 30)
        {
            resultsText.text = "M1: situation deteriorating";

            numM2Resources -= 10;
            numM5Resources -= 5;
        }
        else
        {
            resultsText.text = "M1: success";

            numM3Resources += 10;
            numM3Resources += 5;
        }

        // Achieve military success in the field
        if (numM2Resources < 12)
        {
            resultsText.text += " M2: failure";
        }
        else if (numM2Resources < 25)
        {
            resultsText.text += " M2: situation deteriorating";
        }
        else
        {
            resultsText.text += " M2: success";
        }

        // Secure South Vietnamese countryside (pacification)
        if (numM3Resources < 12)
        {
            resultsText.text += " M3: failure";
        }
        else if (numM3Resources < 25)
        {
            resultsText.text += " M3: situation deteriorating";
        }
        else
        {
            resultsText.text += " M3: success";
        }

        // Stabilize and strengthen the South Vietnamese government
        if (numM4Resources < 10)
        {
            resultsText.text += " M4: failure";
        }
        else if (numM4Resources < 20)
        {
            resultsText.text += " M4: situation deteriorating";
        }
        else
        {
            resultsText.text += " M4: success";
        }

        // Protect U.S. troops and minimize casualties
        if (numM5Resources < 10)
        {
            resultsText.text += " M5: failure";
        }
        else if (numM5Resources < 20)
        {
            resultsText.text += " M5: situation deteriorating";
        }
        else
        {
            resultsText.text += " M5: success";
        }


    }



}
