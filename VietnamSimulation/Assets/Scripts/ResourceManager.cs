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

    bool failureCalculatedM1 = false;
    bool failureCalculatedM2 = false;
    bool failureCalculatedM3 = false;
    bool failureCalculatedM4 = false;
    bool failureCalculatedM5 = false;

    bool successCalculatedM1 = false;
    bool successCalculatedM2 = false;
    bool successCalculatedM3 = false;
    bool successCalculatedM4 = false;
    bool successCalculatedM5 = false;

    bool deterioratingCalculatedM1 = false;
    bool deterioratingCalculatedM2 = false;
    bool deterioratingCalculatedM3 = false;
    bool deterioratingCalculatedM4 = false;
    bool deterioratingCalculatedM5 = false;



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
            //failure

            numM2Resources -= 10;
            numM5Resources -= 5;

            numM3Resources -= 100;
            numM4Resources -= 100;

            if (!failureCalculatedM1)
            {
                if (successCalculatedM1)
                {
                    numM3Resources -= 10;
                    numM4Resources -= 5;
                }
                failureCalculatedM1 = true;
                deterioratingCalculatedM1 = false;
                successCalculatedM1 = false;
                CalculateResults();
                return;
            }
        }
        else if (numM1Resources < 30)
        {
            //situation deteriorating

            if (!deterioratingCalculatedM1)
            {
                if (successCalculatedM1)
                {
                    numM3Resources -= 10;
                    numM4Resources -= 5;
                }
                if (failureCalculatedM1)
                {
                    numM2Resources += 10;
                    numM5Resources += 5;

                    numM3Resources += 100;
                    numM4Resources += 100;
                }
                deterioratingCalculatedM1 = true;
                failureCalculatedM1 = false;
                successCalculatedM1 = false;
                CalculateResults();
                return;
            }
        }
        else
        {
            //success

            numM3Resources += 10;
            numM4Resources += 5;

            if (!successCalculatedM1)
            {
                if (failureCalculatedM1)
                {
                    numM2Resources += 10;
                    numM5Resources += 5;

                    numM3Resources += 100;
                    numM4Resources += 100;
                }
                successCalculatedM1 = true;
                deterioratingCalculatedM1 = false;
                failureCalculatedM1 = false;
                CalculateResults();
                return;
            }
        }

        // Achieve military success in the field
        if (numM2Resources < 12)
        {
            //failure

            numDi7Resources -= 5;

            if (!failureCalculatedM2)
            {
                if (successCalculatedM2)
                {
                    numM1Resources -= 5;
                    numM5Resources -= 10;
                }
                failureCalculatedM2 = true;
                deterioratingCalculatedM2 = false;
                successCalculatedM2 = false;
                CalculateResults();
                return;
            }
        }
        else if (numM2Resources < 25)
        {
            //situation deteriorating

            if (!deterioratingCalculatedM2)
            {
                if (successCalculatedM2)
                {
                    numM1Resources -= 5;
                    numM5Resources -= 10;
                }
                if (failureCalculatedM2)
                {
                    numDi7Resources += 5;
                }
                deterioratingCalculatedM2 = true;
                failureCalculatedM2 = false;
                successCalculatedM2 = false;
                CalculateResults();
                return;
            }
        }
        else
        {
            //success

            numM1Resources += 5;
            numM5Resources += 10;

            if (!successCalculatedM2)
            {
                if (failureCalculatedM2)
                {
                    numDi7Resources += 5;
                }
                successCalculatedM2 = true;
                deterioratingCalculatedM2 = false;
                failureCalculatedM2 = false;
                CalculateResults();
                return;
            }
        }

        // Secure South Vietnamese countryside (pacification)
        if (numM3Resources < 12)
        {
            //failure

            numM1Resources -= 5;

            if (!failureCalculatedM3)
            {
                if (successCalculatedM3)
                {
                    numM4Resources -= 5;
                    numDi6Resources -= 3;
                }
                failureCalculatedM3 = true;
                deterioratingCalculatedM3 = false;
                successCalculatedM3 = false;
                CalculateResults();
                return;
            }
        }
        else if (numM3Resources < 25)
        {
            //situation deteriorating

            if (!deterioratingCalculatedM3)
            {
                if (successCalculatedM3)
                {
                    numM4Resources -= 5;
                    numDi6Resources -= 3;
                }
                if (failureCalculatedM3)
                {
                    numM1Resources += 5;
                }
                deterioratingCalculatedM3 = true;
                failureCalculatedM3 = false;
                successCalculatedM3 = false;
                CalculateResults();
                return;
            }
        }
        else
        {
            //success

            numM4Resources += 5;
            numDi6Resources += 3;

            if (!successCalculatedM3)
            {
                if (failureCalculatedM3)
                {
                    numM1Resources += 5;
                }
                successCalculatedM3 = true;
                deterioratingCalculatedM3 = false;
                failureCalculatedM3 = false;
                CalculateResults();
                return;
            }
        }

        // Stabilize and strengthen the South Vietnamese government
        if (numM4Resources < 10)
        {
            //failure

            numM1Resources -= 5;

            if (!failureCalculatedM4)
            {
                if (successCalculatedM4)
                {
                    numDo10Resources -= 5;
                    numDi6Resources -= 3;
                }
                failureCalculatedM4 = true;
                deterioratingCalculatedM4 = false;
                successCalculatedM4 = false;
                CalculateResults();
                return;
            }
        }
        else if (numM4Resources < 20)
        {
            //situation deteriorating

            if (!deterioratingCalculatedM4)
            {
                if (successCalculatedM4)
                {
                    numDo10Resources -= 5;
                    numDi6Resources -= 3;
                }
                if (failureCalculatedM4)
                {
                    numM1Resources += 5;
                }
                deterioratingCalculatedM4 = true;
                failureCalculatedM4 = false;
                successCalculatedM4 = false;
                CalculateResults();
                return;
            }
        }
        else
        {
            //success

            numDo10Resources += 5;
            numDi6Resources += 3;

            if (!successCalculatedM4)
            {
                if (failureCalculatedM4)
                {
                    numM1Resources += 5;
                }
                successCalculatedM4 = true;
                deterioratingCalculatedM4 = false;
                failureCalculatedM4 = false;
                CalculateResults();
                return;
            }
        }

        // Protect U.S. troops and minimize casualties
        if (numM5Resources < 10)
        {
            //failure

            numM1Resources -= 5;
            numDo11Resources -= 3;

            if (!failureCalculatedM5)
            {
                if (successCalculatedM5)
                {
                    numDo10Resources -= 3;
                    numDo11Resources -= 5;
                }
                failureCalculatedM5 = true;
                deterioratingCalculatedM5 = false;
                successCalculatedM5 = false;
                CalculateResults();
                return;
            }
        }
        else if (numM5Resources < 20)
        {
            //situation deteriorating

            if (!deterioratingCalculatedM5)
            {
                if (successCalculatedM5)
                {
                    numDo10Resources -= 3;
                    numDo11Resources -= 5;
                }
                if (failureCalculatedM5)
                {
                    numM1Resources += 5;
                    numDo11Resources += 3;
                }
                deterioratingCalculatedM5 = true;
                failureCalculatedM5 = false;
                successCalculatedM5 = false;
                CalculateResults();
                return;
            }
        }
        else
        {
            //success

            numDo10Resources += 3;
            numDo11Resources += 5;

            if (!successCalculatedM5)
            {
                if (failureCalculatedM4)
                {
                    numM1Resources += 5;
                    numDo11Resources += 3;
                }
                successCalculatedM5 = true;
                deterioratingCalculatedM5 = false;
                failureCalculatedM5 = false;
                CalculateResults();
                return;
            }
        }

        return;

    }


    //Outcomes of calculations

    public void FinalizeResults()
    {
        // Prevent a Communist takeover of South Vietnam
        if (numM1Resources < 15)
        {
            resultsText.text = "M1: failure";
        }
        else if (numM1Resources < 30)
        {
            resultsText.text = "M1: situation deteriorating";
        }
        else
        {
            resultsText.text = "M1: success";
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
