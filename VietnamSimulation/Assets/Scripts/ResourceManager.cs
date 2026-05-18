using System.Collections.Generic;
using System.Resources;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ResourceManager : MonoBehaviour
{
    public TMP_Text resourceNumberText;
    public TMP_Text whatsLeftText;
    public TMP_Text resultsText;

    int totalResources = 200;
    public int numResources;

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

    public static int pointsInvestigated = 0;
    public static int pointsThreshold = 2;
    public GameObject radioNotification;
    public bool militaryAdvisorAvailable = false;
    public GameObject[] militaryQuestionsArray;

    public GameObject phoneNotification;
    public bool domesticAdvisorAvailable = false;
    public GameObject[] domesticQuestionsArray;

    public static string classification = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numResources = totalResources;
        resourceNumberText.text = "$" + numResources.ToString() + "M";
        whatsLeftText.text = "You have " + numSliders.ToString() + " untouched categories and " + numResources.ToString() + " resources left.";
    }

    // Update is called once per frame
    void Update()
    {
        if (militaryAdvisorAvailable)
        {
            foreach (GameObject question in militaryQuestionsArray) 
            {
                question.SetActive(true); 
            }
        }

        if (domesticAdvisorAvailable)
        {
            foreach (GameObject question in domesticQuestionsArray)
            {
                question.SetActive(true);
            }
        }

        if (pointsInvestigated >= pointsThreshold)
        {
            radioNotification.SetActive(true);
            militaryAdvisorAvailable = true;

            phoneNotification.SetActive(true);
            domesticAdvisorAvailable = true;

            foreach (GameObject question in militaryQuestionsArray)
            {
                Button button = question.GetComponent<Button>();
                if (!question.GetComponent<AdvisorQuestions>().questionAsked)
                {
                    button.interactable = true;
                }
            }

            foreach (GameObject question in domesticQuestionsArray)
            {
                Button button = question.GetComponent<Button>();
                if (!question.GetComponent<AdvisorQuestions>().questionAsked)
                {
                    button.interactable = true;
                }
            }

            pointsInvestigated = 0;
        }

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


    public void PrintResources()
    {
        Debug.Log("---RESOURCE STATUS--- M1: " + numM1Resources
                + " M2: " + numM2Resources
                + " M3: " + numM3Resources
                + " M4: " + numM4Resources 
                + " M5: " + numM5Resources);
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

        //MILITARY GOALS

        // Prevent a Communist takeover of South Vietnam
        if (numM1Resources < 15)
        {
            //failure

            if (!failureCalculatedM1)
            {
                numM2Resources -= 10;
                numM5Resources -= 5;

                numM3Resources -= 100;
                numM4Resources -= 100;

                Debug.Log("M1 FAIL: M2 -10; M5 - 5; M3 & M4 -100");
                PrintResources();

                if (successCalculatedM1)
                {
                    numM3Resources -= 10;
                    numM4Resources -= 5;

                    Debug.Log("*previous M1 success fix; M3 -10; M4 -5");
                    PrintResources();

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
                Debug.Log("M1 SITUATION DETERIORATING");
                PrintResources();

                if (successCalculatedM1)
                {
                    numM3Resources -= 10;
                    numM4Resources -= 5;

                    Debug.Log("*previous M1 success fix; M3 -10; M4 -5");
                    PrintResources();

                }
                if (failureCalculatedM1)
                {
                    numM2Resources += 10;
                    numM5Resources += 5;

                    numM3Resources += 100;
                    numM4Resources += 100;

                    Debug.Log("*previous M1 failure fix; M2 +10; M5 +5; M3 & M4 +100");
                    PrintResources();

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

            if (!successCalculatedM1)
            {
                numM3Resources += 10;
                numM4Resources += 5;

                Debug.Log("M1 SUCCESS: M3 +10; M4 +5");
                PrintResources();

                if (failureCalculatedM1)
                {
                    numM2Resources += 10;
                    numM5Resources += 5;

                    numM3Resources += 100;
                    numM4Resources += 100;

                    Debug.Log("*previous M1 failure fix; M2 +10; M5 +5; M3 & M4 +100");
                    PrintResources();

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

            if (!failureCalculatedM2)
            {
                numDi7Resources -= 5;

                Debug.Log("M2 FAIL: Di7 -5");
                PrintResources();

                if (successCalculatedM2)
                {
                    numM1Resources -= 5;
                    numM5Resources -= 10;

                    Debug.Log("*previous M2 success fix; M1 -5; M5 -10");
                    PrintResources();

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
                Debug.Log("M2 SITUATION DETERIORATING");
                PrintResources();

                if (successCalculatedM2)
                {
                    numM1Resources -= 5;
                    numM5Resources -= 10;

                    Debug.Log("*previous M2 success fix; M1 -5; M5 -10");
                    PrintResources();
                }
                if (failureCalculatedM2)
                {
                    numDi7Resources += 5;

                    Debug.Log("*previous M2 failure fix; Di7 +5");
                    PrintResources();
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

            if (!successCalculatedM2)
            {
                numM1Resources += 5;
                numM5Resources += 10;

                Debug.Log("M2 SUCCESS: M1 +5; M5 +10");
                PrintResources();

                if (failureCalculatedM2)
                {
                    numDi7Resources += 5;

                    Debug.Log("*previous M2 failure fix; Di7 +5");
                    PrintResources();
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

            if (!failureCalculatedM3)
            {
                numM1Resources -= 5;

                Debug.Log("M3 FAIL: M1 -5");
                PrintResources();

                if (successCalculatedM3)
                {
                    numM4Resources -= 5;
                    numDi6Resources -= 3;

                    Debug.Log("*previous M3 success fix; M4 -5; Di6 -3");
                    PrintResources();
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
                Debug.Log("M3: SITUATION DETERIORATING");
                PrintResources();

                if (successCalculatedM3)
                {
                    numM4Resources -= 5;
                    numDi6Resources -= 3;

                    Debug.Log("*previous M3 success fix; M4 -5; Di6 -3");
                    PrintResources();
                }
                if (failureCalculatedM3)
                {
                    numM1Resources += 5;

                    Debug.Log("*previous M3 failure fix; M1 +5");
                    PrintResources();
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

            if (!successCalculatedM3)
            {
                numM4Resources += 5;
                numDi6Resources += 3;

                Debug.Log("M3 SUCCESS: M4 +5; Di6 +3");
                PrintResources();

                if (failureCalculatedM3)
                {
                    numM1Resources += 5;

                    Debug.Log("*previous M3 failure fix; M1 +5");
                    PrintResources();
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

            if (!failureCalculatedM4)
            {
                numM1Resources -= 5;

                Debug.Log("M4 FAIL: M1 -5");
                PrintResources();

                if (successCalculatedM4)
                {
                    numDo10Resources -= 5;
                    numDi6Resources -= 3;

                    Debug.Log("*previous M4 success fix; Do10 -5; Di6 -3");
                    PrintResources();
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
                Debug.Log("M4: SITUATION DETERIORATING");
                PrintResources();

                if (successCalculatedM4)
                {
                    numDo10Resources -= 5;
                    numDi6Resources -= 3;

                    Debug.Log("*previous M4 success fix; Do10 -5; Di6 -3");
                    PrintResources();
                }
                if (failureCalculatedM4)
                {
                    numM1Resources += 5;

                    Debug.Log("*previous M4 failure fix; M1 +5");
                    PrintResources();
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

            if (!successCalculatedM4)
            {
                numDo10Resources += 5;
                numDi6Resources += 3;

                Debug.Log("M4 SUCCESS: Do10 +5; Di6 +3");
                PrintResources();

                if (failureCalculatedM4)
                {
                    numM1Resources += 5;

                    Debug.Log("*previous M4 failure fix; M1 +5");
                    PrintResources();
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

            if (!failureCalculatedM5)
            {
                numM1Resources -= 5;
                numDo11Resources -= 3;

                Debug.Log("M5 FAIL: M1 -5; Do11 -3");
                PrintResources();

                if (successCalculatedM5)
                {
                    numDo10Resources -= 3;
                    numDo11Resources -= 5;

                    Debug.Log("*previous M5 success fix; Do10 -3; Do11 -5");
                    PrintResources();
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
                Debug.Log("M5: SITUATION DETERIORATING");
                PrintResources();

                if (successCalculatedM5)
                {
                    numDo10Resources -= 3;
                    numDo11Resources -= 5;

                    Debug.Log("*previous M5 success fix; Do10 -3; Do11 -5");
                    PrintResources();
                }
                if (failureCalculatedM5)
                {
                    numM1Resources += 5;
                    numDo11Resources += 3;

                    Debug.Log("*previous M5 failure fix; M1 +5; Do11 +3");
                    PrintResources();
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

            if (!successCalculatedM5)
            {
                numDo10Resources += 3;
                numDo11Resources += 5;

                Debug.Log("M5 SUCCESS: Do10 +3; Do11 +5");
                PrintResources();

                if (failureCalculatedM5)
                {
                    numM1Resources += 5;
                    numDo11Resources += 3;

                    Debug.Log("*previous M5 failure fix; M1 +5; Do11 +3");
                    PrintResources();
                }
                successCalculatedM5 = true;
                deterioratingCalculatedM5 = false;
                failureCalculatedM5 = false;
                CalculateResults();
                return;
            }
        }


        //DIPLOMATIC GOALS

        //Di6: Preserve American global credibility
        //  success: 15+
        //  situation deteriorating: 7-14
        //  failure: <7
        //      interdependencies:
        //          success: +3 to #8; +3 to #9
        //          failure: -5 to #8


        //Di7: Open peace negotiations with North Vietnam
        //  success: 20+
        //  situation deteriorating: 10-19
        //  failure: <10
        //      interdependencies:
        //          success: +5 to #10; +3 to #6
        //          failure: -5 to #2


        //Di8: Maintain allied support
        //  success: 15+
        //  situation deteriorating: 7-14
        //  failure: <7
        //      interdependencies:
        //          success: +3 to #1
        //          failure: -5 to #6


        //Di9: Manage relations with the Soviet Union and China
        //  success: 20+
        //  situation deteriorating: 10-19
        //  failure: <10
        //      interdependencies:
        //          success: +3 to #6; +3 to #7
        //          failure: -5 to #2


        //DOMESTIC GOALS

        //Do10: Win the 1968 Presidential Election / Maintain Democratic party Unity
        //  success: 25+
        //  situation deteriorating: 12-24
        //  failure: <12
        //      interdependencies:
        //          success: +3 to #12
        //          failure: -5 to #11


        //Do11: Respond to growing antiwar movement and public opinion
        //  success: 20+
        //  situation deteriorating: 10-19
        //  failure: <10
        //      interdependencies:
        //          success: +5 to #12; +3 to #10
        //          failure: -5 to #5; -5 to #10


        //Do12: Maintain confidence of Congress
        //  success: 15+
        //  situation deteriorating: 7-14
        //  failure: <7
        //      interdependencies:
        //          success: +3 to #13
        //          failure: -5 to #1


        //Do13: Manage U.S. economy
        //  success: 20+
        //  situation deteriorating: 10-19
        //  failure: <10
        //      interdependencies:
        //          success: +3 to #14
        //          failure: -5 to #15


        //Do14: Preserve Johnson's domestic "Great Society" programs
        //  success: 20+
        //  situation deteriorating: 10-19
        //  failure: <10
        //      interdependencies:
        //          success: +3 to #15
        //          failure: -5 to #13


        //Do15: Handle civil rights and urban unrest
        //  success: 15+
        //  situation deteriorating: 7-14
        //  failure: <7
        //      interdependencies:
        //          success: +3 to #11
        //          failure: -5 to #10



        return;
    }


    //Outcomes of calculations

    public void FinalizeResults()
    {
        // Prevent a Communist takeover of South Vietnam
        if (numM1Resources < 15)
        {
            resultsText.text = "M1: failure - " + numM1Resources;
        }
        else if (numM1Resources < 30)
        {
            resultsText.text = "M1: situation deteriorating - " + numM1Resources;
        }
        else
        {
            resultsText.text = "M1: success - " + numM1Resources;
        }

        // Achieve military success in the field
        if (numM2Resources < 12)
        {
            resultsText.text += " M2: failure - " + numM2Resources;
        }
        else if (numM2Resources < 25)
        {
            resultsText.text += " M2: situation deteriorating - " + numM2Resources;
        }
        else
        {
            resultsText.text += " M2: success - " + numM2Resources;
        }

        // Secure South Vietnamese countryside (pacification)
        if (numM3Resources < 12)
        {
            resultsText.text += " M3: failure - " + numM3Resources;
        }
        else if (numM3Resources < 25)
        {
            resultsText.text += " M3: situation deteriorating - " + numM3Resources;
        }
        else
        {
            resultsText.text += " M3: success - " + numM3Resources;
        }

        // Stabilize and strengthen the South Vietnamese government
        if (numM4Resources < 10)
        {
            resultsText.text += " M4: failure - " + numM4Resources;
        }
        else if (numM4Resources < 20)
        {
            resultsText.text += " M4: situation deteriorating - " + numM4Resources;
        }
        else
        {
            resultsText.text += " M4: success - " + numM4Resources;
        }

        // Protect U.S. troops and minimize casualties
        if (numM5Resources < 10)
        {
            resultsText.text += " M5: failure - " + numM5Resources;
        }
        else if (numM5Resources < 20)
        {
            resultsText.text += " M5: situation deteriorating - " + numM5Resources;
        }
        else
        {
            resultsText.text += " M5: success - " + numM5Resources;
        }

        int militaryResources = numM1Resources + numM2Resources + numM3Resources + numM4Resources + numM5Resources;

        if (militaryResources > 70)
        {
            classification = "Hawk";
        }
        else
        {
            classification = "Dove";
        }

    }



}
