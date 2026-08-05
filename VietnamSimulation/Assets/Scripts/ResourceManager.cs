using System.Collections.Generic;
using System.Resources;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Audio.ProcessorInstance;


public class ResourceManager : MonoBehaviour
{
    public TMP_Text resourceNumberText;
    public TMP_Text lobbyResourceText;
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

    int inputM1 = 0;
    int inputM2 = 0;
    int inputM3 = 0;
    int inputM4 = 0;
    int inputM5 = 0;
    int inputDi6 = 0;
    int inputDi7 = 0;
    int inputDi8 = 0;
    int inputDi9 = 0;
    int inputDo10 = 0;
    int inputDo11 = 0;
    int inputDo12 = 0;
    int inputDo13 = 0;
    int inputDo14 = 0;
    int inputDo15 = 0;


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

    public static bool failureCalculatedM1 = false;
    public static bool failureCalculatedM2 = false;
    public static bool failureCalculatedM3 = false;
    public static bool failureCalculatedM4 = false;
    public static bool failureCalculatedM5 = false;
    public static bool failureCalculatedDi6 = false;
    public static bool failureCalculatedDi7 = false;
    public static bool failureCalculatedDi8 = false;
    public static bool failureCalculatedDi9 = false;
    public static bool failureCalculatedDo10 = false;
    public static bool failureCalculatedDo11 = false;
    public static bool failureCalculatedDo12 = false;
    public static bool failureCalculatedDo13 = false;
    public static bool failureCalculatedDo14 = false;
    public static bool failureCalculatedDo15 = false;

    public static bool successCalculatedM1 = false;
    public static bool successCalculatedM2 = false;
    public static bool successCalculatedM3 = false;
    public static bool successCalculatedM4 = false;
    public static bool successCalculatedM5 = false;
    public static bool successCalculatedDi6 = false;
    public static bool successCalculatedDi7 = false;
    public static bool successCalculatedDi8 = false;
    public static bool successCalculatedDi9 = false;
    public static bool successCalculatedDo10 = false;
    public static bool successCalculatedDo11 = false;
    public static bool successCalculatedDo12 = false;
    public static bool successCalculatedDo13 = false;
    public static bool successCalculatedDo14 = false;
    public static bool successCalculatedDo15 = false;

    public static bool deterioratingCalculatedM1 = false;
    public static bool deterioratingCalculatedM2 = false;
    public static bool deterioratingCalculatedM3 = false;
    public static bool deterioratingCalculatedM4 = false;
    public static bool deterioratingCalculatedM5 = false;
    public static bool deterioratingCalculatedDi6 = false;
    public static bool deterioratingCalculatedDi7 = false;
    public static bool deterioratingCalculatedDi8 = false;
    public static bool deterioratingCalculatedDi9 = false;
    public static bool deterioratingCalculatedDo10 = false;
    public static bool deterioratingCalculatedDo11 = false;
    public static bool deterioratingCalculatedDo12 = false;
    public static bool deterioratingCalculatedDo13 = false;
    public static bool deterioratingCalculatedDo14 = false;
    public static bool deterioratingCalculatedDo15 = false;

    public static int militaryPointsInvestigated = 0;
    public static int militaryPointsThreshold = 11;
    public GameObject radioNotification;
    public bool militaryAdvisorAvailable = false;
    public GameObject[] militaryQuestionsArray;

    public static int domesticPointsInvestigated = 0;
    public static int domesticPointsThreshold = 4;
    public GameObject phoneNotification;
    public bool domesticAdvisorAvailable = false;
    public GameObject[] domesticQuestionsArray;

    public static int diplomaticPointsInvestigated = 0;
    public static int diplomaticPointsThreshold = 11;
    public GameObject recorderNotification;
    public bool diplomaticAdvisorAvailable = false;
    public GameObject[] diplomaticQuestionsArray;

    public static string classification = "";

    public static int roomsComplete = 0;
    public static bool researched = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numResources = totalResources;
        lobbyResourceText.text = "$" + numResources.ToString() + "M";
        resourceNumberText.text = "$" + numResources.ToString() + "M";
        whatsLeftText.text = "You have " + numSliders.ToString() + " untouched categories and " + numResources.ToString() + " resources left.";
    }

    // Update is called once per frame
    void Update()
    {
        if (militaryPointsInvestigated > 0 || domesticPointsInvestigated > 0 || diplomaticPointsInvestigated > 0)
        {
            researched = true;
        }

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

        if (diplomaticAdvisorAvailable)
        {
            foreach (GameObject question in diplomaticQuestionsArray)
            {
                question.SetActive(true);
            }
        }

        if (militaryPointsInvestigated >= militaryPointsThreshold)
        {
            radioNotification.SetActive(true);
            militaryAdvisorAvailable = true;

            foreach (GameObject question in militaryQuestionsArray)
            {
                Button button = question.GetComponent<Button>();
                if (!question.GetComponent<AdvisorQuestions>().questionAsked)
                {
                    button.interactable = true;
                }
            }

            militaryPointsInvestigated = 0;
            roomsComplete++;
        }

        if (domesticPointsInvestigated >= domesticPointsThreshold)
        {
            phoneNotification.SetActive(true);
            domesticAdvisorAvailable = true;

            foreach (GameObject question in domesticQuestionsArray)
            {
                Button button = question.GetComponent<Button>();
                if (!question.GetComponent<AdvisorQuestions>().questionAsked)
                {
                    button.interactable = true;
                }
            }

            domesticPointsInvestigated = 0;
            roomsComplete++;
        }

        if (diplomaticPointsInvestigated >= diplomaticPointsThreshold)
        {
            recorderNotification.SetActive(true);
            diplomaticAdvisorAvailable = true;

            foreach (GameObject question in diplomaticQuestionsArray)
            {
                Button button = question.GetComponent<Button>();
                if (!question.GetComponent<AdvisorQuestions>().questionAsked)
                {
                    button.interactable = true;
                }
            }

            diplomaticPointsInvestigated = 0;
            roomsComplete++;
        }

        if (ScreenChange.viewingResults)
        {
            foreach (GameObject question in militaryQuestionsArray)
            {
                question.SetActive(false);
            }

            foreach (GameObject question in domesticQuestionsArray)
            {
                question.SetActive(false);
            }

            foreach (GameObject question in diplomaticQuestionsArray)
            {
                question.SetActive(false);
            }
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
        lobbyResourceText.text = "$" + numResources.ToString() + "M";
        resourceNumberText.text = "$" + numResources.ToString() + "M";

        whatsLeftText.text = "You have " + numSliders.ToString() + " untouched categories and " + numResources.ToString() + " resources left.";

    }

    public string GetAllResourcesText()
    {
        string allResourcesText = "MILITARY OBJECTIVES \r\nPrevent Communist Takeover: " + numM1Resources
                                    + " \r\nAchieve Military Success: " + numM2Resources
                                    + " \r\nSecure South Vietnamese Countryside: " + numM3Resources
                                    + " \r\nStabilize South Vietnamese Government: " + numM4Resources
                                    + " \r\nProtect Troops, Minimize Casualties: " + numM5Resources
                                    + " \r\n\r\nDIPLOMATIC OBJECTIVES"
                                    + " \r\nPreserve American Global Credibility: " + numDi6Resources
                                    + " \r\nOpen Peace Negotiations: " + numDi7Resources
                                    + " \r\nMaintain Allied Support: " + numDi8Resources
                                    + " \r\nManage USSR and China: " + numDi9Resources
                                    + " \r\n\r\nDOMESTIC OBJECTIVES"
                                    + " \r\nWin 1968 Election, Democratic Unity: " + numDo10Resources
                                    + " \r\nRespond to Antiwar Movement, Public Opinion: " + numDo11Resources
                                    + " \r\nMaintain Confidence of Congress: " + numDo12Resources
                                    + " \r\nManage U.S. Economy: " + numDo13Resources
                                    + " \r\nPreserve Great Society Programs: " + numDo14Resources
                                    + " \r\nHandle Civil Rights/Urban Unrest: " + numDo15Resources;
        return allResourcesText;
    }

    public void PrintResources()
    {
        Debug.Log("---RESOURCE STATUS--- M1: " + numM1Resources
                                    + " M2: " + numM2Resources
                                    + " M3: " + numM3Resources
                                    + " M4: " + numM4Resources 
                                    + " M5: " + numM5Resources
                                    + " Di6: " + numDi6Resources
                                    + " Di7: " + numDi7Resources
                                    + " Di8: " + numDi8Resources
                                    + " Di9: " + numDi9Resources
                                    + " Do10: " + numDo10Resources
                                    + " Do11: " + numDo11Resources
                                    + " Do12: " + numDo12Resources
                                    + " Do13: " + numDo13Resources
                                    + " Do14: " + numDo14Resources
                                    + " Do15: " + numDo15Resources);
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


    public void SetDi6Resources(int resourcesUsed)
    {
        numDi6Resources = resourcesUsed;
        ResourcesToText();

        if (!hasClickedDi6)
        {
            hasClickedDi6 = true;
            numSliders--;
        }
    }


    public void SetDi7Resources(int resourcesUsed)
    {
        numDi7Resources = resourcesUsed;
        ResourcesToText();

        if (!hasClickedDi7)
        {
            hasClickedDi7 = true;
            numSliders--;
        }
    }


    public void SetDi8Resources(int resourcesUsed)
    {
        numDi8Resources = resourcesUsed;
        ResourcesToText();

        if (!hasClickedDi8)
        {
            hasClickedDi8 = true;
            numSliders--;
        }
    }


    public void SetDi9Resources(int resourcesUsed)
    {
        numDi9Resources = resourcesUsed;
        ResourcesToText();

        if (!hasClickedDi9)
        {
            hasClickedDi9 = true;
            numSliders--;
        }
    }


    public void SetDo10Resources(int resourcesUsed)
    {
        numDo10Resources = resourcesUsed;
        ResourcesToText();

        if (!hasClickedDo10)
        {
            hasClickedDo10 = true;
            numSliders--;
        }
    }


    public void SetDo11Resources(int resourcesUsed)
    {
        numDo11Resources = resourcesUsed;
        ResourcesToText();

        if (!hasClickedDo11)
        {
            hasClickedDo11 = true;
            numSliders--;
        }
    }


    public void SetDo12Resources(int resourcesUsed)
    {
        numDo12Resources = resourcesUsed;
        ResourcesToText();

        if (!hasClickedDo12)
        {
            hasClickedDo12 = true;
            numSliders--;
        }
    }


    public void SetDo13Resources(int resourcesUsed)
    {
        numDo13Resources = resourcesUsed;
        ResourcesToText();

        if (!hasClickedDo13)
        {
            hasClickedDo13 = true;
            numSliders--;
        }
    }


    public void SetDo14Resources(int resourcesUsed)
    {
        numDo14Resources = resourcesUsed;
        ResourcesToText();

        if (!hasClickedDo14)
        {
            hasClickedDo14 = true;
            numSliders--;
        }
    }


    public void SetDo15Resources(int resourcesUsed)
    {
        numDo15Resources = resourcesUsed;
        ResourcesToText();

        if (!hasClickedDo15)
        {
            hasClickedDo15 = true;
            numSliders--;
        }
    }


    public void SetOriginalInputs()
    {
        inputM1 = numM1Resources;
        inputM2 = numM2Resources;
        inputM3 = numM3Resources;
        inputM4 = numM4Resources;
        inputM5 = numM5Resources;
        inputDi6 = numDi6Resources;
        inputDi7 = numDi7Resources;
        inputDi8 = numDi8Resources;
        inputDi9 = numDi9Resources;
        inputDo10 = numDo10Resources;
        inputDo11 = numDo11Resources;
        inputDo12 = numDo12Resources;
        inputDo13 = numDo13Resources;
        inputDo14 = numDo14Resources;
        inputDo15 = numDo15Resources;
    }



    //INTERDEPENDENCY CALCULATIONS

    public void CalculateResults()
    {
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

        // Preserve American global credibility
        if (numDi6Resources < 7)
        {
            //failure

            if (!failureCalculatedDi6)
            {
                numDi8Resources -= 5;

                Debug.Log("Di6 FAIL: Di8 -5");
                PrintResources();

                if (successCalculatedDi6)
                {
                    numDi8Resources -= 3;
                    numDi9Resources -= 3;

                    Debug.Log("*previous Di6 success fix; Di8 -3; Di9 -3");
                    PrintResources();

                }
                failureCalculatedDi6 = true;
                deterioratingCalculatedDi6 = false;
                successCalculatedDi6 = false;
                CalculateResults();
                return;
            }
        }
        else if (numDi6Resources < 15)
        {
            //situation deteriorating

            if (!deterioratingCalculatedDi6)
            {
                Debug.Log("Di6 SITUATION DETERIORATING");
                PrintResources();

                if (successCalculatedDi6)
                {
                    numDi8Resources -= 3;
                    numDi9Resources -= 3;

                    Debug.Log("*previous Di6 success fix; Di8 -3; Di9 -3");
                    PrintResources();

                }
                if (failureCalculatedDi6)
                {
                    numDi8Resources += 5;

                    Debug.Log("*previous Di6 failure fix; Di8 +5");
                    PrintResources();

                }
                deterioratingCalculatedDi6 = true;
                failureCalculatedDi6 = false;
                successCalculatedDi6 = false;
                CalculateResults();
                return;
            }
        }
        else
        {
            //success

            if (!successCalculatedDi6)
            {
                numDi8Resources += 3;
                numDi9Resources += 3;

                Debug.Log("Di6 SUCCESS: Di8 +3; Di9 +3");
                PrintResources();

                if (failureCalculatedDi6)
                {
                    numDi8Resources += 5;

                    Debug.Log("*previous Di6 failure fix; Di8 +5"); 
                    PrintResources();

                }
                successCalculatedDi6 = true;
                deterioratingCalculatedDi6 = false;
                failureCalculatedDi6 = false;
                CalculateResults();
                return;
            }
        }


        //Di7: Open peace negotiations with North Vietnam
        //  success: 20+
        //  situation deteriorating: 10-19
        //  failure: <10
        //      interdependencies:
        //          success: +5 to #10; +3 to #6
        //          failure: -5 to #2

        // Open peace negotiations with North Vietnam
        if (numDi7Resources < 10)
        {
            //failure

            if (!failureCalculatedDi7)
            {
                numM2Resources -= 5;

                Debug.Log("Di7 FAIL: M2 -5");
                PrintResources();

                if (successCalculatedDi7)
                {
                    numDo10Resources -= 5;
                    numDi6Resources -= 3;

                    Debug.Log("*previous Di7 success fix; Do10 -5; Di6 -3");
                    PrintResources();

                }
                failureCalculatedDi7 = true;
                deterioratingCalculatedDi7 = false;
                successCalculatedDi7 = false;
                CalculateResults();
                return;
            }
        }
        else if (numDi7Resources < 20)
        {
            //situation deteriorating

            if (!deterioratingCalculatedDi7)
            {
                Debug.Log("Di7 SITUATION DETERIORATING");
                PrintResources();

                if (successCalculatedDi7)
                {
                    numDo10Resources -= 5;
                    numDi6Resources -= 3;

                    Debug.Log("*previous Di7 success fix; Do10 -5; Di6 -3");
                    PrintResources();

                }
                if (failureCalculatedDi7)
                {
                    numM2Resources += 5;

                    Debug.Log("*previous Di7 failure fix; M2 +5");
                    PrintResources();

                }
                deterioratingCalculatedDi7 = true;
                failureCalculatedDi7 = false;
                successCalculatedDi7 = false;
                CalculateResults();
                return;
            }
        }
        else
        {
            //success

            if (!successCalculatedDi7)
            {
                numDo10Resources += 5;
                numDi6Resources += 3;

                Debug.Log("Di7 SUCCESS: Di10 +5; Di6 +3");
                PrintResources();

                if (failureCalculatedDi7)
                {
                    numM2Resources += 5;

                    Debug.Log("*previous Di7 failure fix; M2 +5");
                    PrintResources();

                }
                successCalculatedDi7 = true;
                deterioratingCalculatedDi7 = false;
                failureCalculatedDi7 = false;
                CalculateResults();
                return;
            }
        }


        //Di8: Maintain allied support
        //  success: 15+
        //  situation deteriorating: 7-14
        //  failure: <7
        //      interdependencies:
        //          success: +3 to #1
        //          failure: -5 to #6

        // Maintain allied support
        if (numDi8Resources < 7)
        {
            //failure

            if (!failureCalculatedDi8)
            {
                numDi6Resources -= 5;

                Debug.Log("Di8 FAIL: Di6 -5");
                PrintResources();

                if (successCalculatedDi8)
                {
                    numM1Resources -= 3;

                    Debug.Log("*previous Di8 success fix; M1 -3");
                    PrintResources();

                }
                failureCalculatedDi8 = true;
                deterioratingCalculatedDi8 = false;
                successCalculatedDi8 = false;
                CalculateResults();
                return;
            }
        }
        else if (numDi8Resources < 15)
        {
            //situation deteriorating

            if (!deterioratingCalculatedDi8)
            {
                Debug.Log("Di8 SITUATION DETERIORATING");
                PrintResources();

                if (successCalculatedDi8)
                {
                    numM1Resources -= 3;

                    Debug.Log("*previous Di8 success fix; M1 -3");
                    PrintResources();

                }
                if (failureCalculatedDi8)
                {
                    numDi6Resources += 5;

                    Debug.Log("*previous Di8 failure fix; Di6 +5");
                    PrintResources();

                }
                deterioratingCalculatedDi8 = true;
                failureCalculatedDi8 = false;
                successCalculatedDi8 = false;
                CalculateResults();
                return;
            }
        }
        else
        {
            //success

            if (!successCalculatedDi8)
            {
                numM1Resources += 3;

                Debug.Log("Di8 SUCCESS: M1 +3");
                PrintResources();

                if (failureCalculatedDi8)
                {
                    numDi6Resources += 5;

                    Debug.Log("*previous Di8 failure fix; Di6 +5");
                    PrintResources();

                }
                successCalculatedDi8 = true;
                deterioratingCalculatedDi8 = false;
                failureCalculatedDi8 = false;
                CalculateResults();
                return;
            }
        }


        //Di9: Manage relations with the Soviet Union and China
        //  success: 20+
        //  situation deteriorating: 10-19
        //  failure: <10
        //      interdependencies:
        //          success: +3 to #6; +3 to #7
        //          failure: -5 to #2

        // Manage relations with the Soviet Union and China
        if (numDi9Resources < 10)
        {
            //failure

            if (!failureCalculatedDi9)
            {
                numM2Resources -= 5;

                Debug.Log("Di9 FAIL: M2 -5");
                PrintResources();

                if (successCalculatedDi9)
                {
                    numDi6Resources -= 3;
                    numDi7Resources -= 3;

                    Debug.Log("*previous Di9 success fix; Di6 -3; Di7 -3");
                    PrintResources();

                }
                failureCalculatedDi9 = true;
                deterioratingCalculatedDi9 = false;
                successCalculatedDi9 = false;
                CalculateResults();
                return;
            }
        }
        else if (numDi9Resources < 20)
        {
            //situation deteriorating

            if (!deterioratingCalculatedDi9)
            {
                Debug.Log("Di9 SITUATION DETERIORATING");
                PrintResources();

                if (successCalculatedDi9)
                {
                    numDi6Resources -= 3;
                    numDi7Resources -= 3;

                    Debug.Log("*previous Di9 success fix; Di6 -3; Di7 -3");
                    PrintResources();

                }
                if (failureCalculatedDi9)
                {
                    numM2Resources += 5;

                    Debug.Log("*previous Di9 failure fix; M2 +5");
                    PrintResources();

                }
                deterioratingCalculatedDi9 = true;
                failureCalculatedDi9 = false;
                successCalculatedDi9 = false;
                CalculateResults();
                return;
            }
        }
        else
        {
            //success

            if (!successCalculatedDi9)
            {
                numDi6Resources += 3;
                numDi7Resources += 3;

                Debug.Log("Di9 SUCCESS: Di6 +3; Di7 +3");
                PrintResources();

                if (failureCalculatedDi9)
                {
                    numM2Resources += 5;

                    Debug.Log("*previous Di9 failure fix; M2 +5");
                    PrintResources();

                }
                successCalculatedDi9 = true;
                deterioratingCalculatedDi9 = false;
                failureCalculatedDi9 = false;
                CalculateResults();
                return;
            }
        }


        //DOMESTIC GOALS

        //Do10: Win the 1968 Presidential Election / Maintain Democratic party Unity
        //  success: 25+
        //  situation deteriorating: 12-24
        //  failure: <12
        //      interdependencies:
        //          success: +3 to #12
        //          failure: -5 to #11

        // Win the 1968 Presidential Election / Maintain Democratic party Unity
        if (numDo10Resources < 25)
        {
            //failure

            if (!failureCalculatedDo10)
            {
                numDo11Resources -= 5;

                Debug.Log("Do10 FAIL: Do11 -5");
                PrintResources();

                if (successCalculatedDo10)
                {
                    numDo12Resources -= 3;

                    Debug.Log("*previous Do10 success fix; Do12 -3");
                    PrintResources();
                }
                failureCalculatedDo10 = true;
                deterioratingCalculatedDo10 = false;
                successCalculatedDo10 = false;
                CalculateResults();
                return;
            }
        }
        //else if (numDo10Resources < 25)
        //{
        //    //situation deteriorating

        //    if (!deterioratingCalculatedDo10)
        //    {
        //        Debug.Log("Do10: SITUATION DETERIORATING");
        //        PrintResources();

        //        if (successCalculatedDo10)
        //        {
        //            numDo12Resources -= 3;

        //            Debug.Log("*previous Do10 success fix; Do12 -3");
        //            PrintResources();
        //        }
        //        if (failureCalculatedDo10)
        //        {
        //            numDo11Resources += 5;

        //            Debug.Log("*previous Do10 failure fix; Do11 +5");
        //            PrintResources();
        //        }
        //        deterioratingCalculatedDo10 = true;
        //        failureCalculatedDo10 = false;
        //        successCalculatedDo10 = false;
        //        CalculateResults();
        //        return;
        //    }
        //}
        else
        {
            //success

            if (!successCalculatedDo10)
            {
                numDo12Resources += 3;

                Debug.Log("Do10 SUCCESS: Do12 +3");
                PrintResources();

                if (failureCalculatedDo10)
                {
                    numDo11Resources += 5;

                    Debug.Log("*previous Do10 failure fix; Do11 +5");
                    PrintResources();
                }
                successCalculatedDo10 = true;
                deterioratingCalculatedDo10 = false;
                failureCalculatedDo10 = false;
                CalculateResults();
                return;
            }
        }


        //Do11: Respond to growing antiwar movement and public opinion
        //  success: 20+
        //  situation deteriorating: 10-19
        //  failure: <10
        //      interdependencies:
        //          success: +5 to #12; +3 to #10
        //          failure: -5 to #5; -5 to #10

        // Respond to growing antiwar movement and public opinion
        if (numDo11Resources < 10)
        {
            //failure

            if (!failureCalculatedDo11)
            {
                numM5Resources -= 5;
                numDo10Resources -= 5;

                Debug.Log("Do11 FAIL: M5 -5; Do10 -5");
                PrintResources();

                if (successCalculatedDo11)
                {
                    numDo12Resources -= 5;
                    numDo10Resources -= 3;

                    Debug.Log("*previous Do11 success fix; Do12 -5; Do10 -3");
                    PrintResources();
                }
                failureCalculatedDo11 = true;
                deterioratingCalculatedDo11 = false;
                successCalculatedDo11 = false;
                CalculateResults();
                return;
            }
        }
        else if (numDo11Resources < 25)
        {
            //situation deteriorating

            if (!deterioratingCalculatedDo11)
            {
                Debug.Log("Do11: SITUATION DETERIORATING");
                PrintResources();

                if (successCalculatedDo11)
                {
                    numDo12Resources -= 5;
                    numDo10Resources -= 3;

                    Debug.Log("*previous Do11 success fix; Do12 -5; Do10 -3");
                    PrintResources();
                }
                if (failureCalculatedDo11)
                {
                    numM5Resources += 5;
                    numDo10Resources += 5;

                    Debug.Log("*previous Do11 failure fix; M5 +5; Do10 +5");
                    PrintResources();
                }
                deterioratingCalculatedDo11 = true;
                failureCalculatedDo11 = false;
                successCalculatedDo11 = false;
                CalculateResults();
                return;
            }
        }
        else
        {
            //success

            if (!successCalculatedDo11)
            {
                numDo12Resources += 5;
                numDo10Resources += 3;

                Debug.Log("Do11 SUCCESS: Do12 +5; Do10 +3");
                PrintResources();

                if (failureCalculatedDo11)
                {
                    numM5Resources += 5;
                    numDo10Resources += 5;

                    Debug.Log("*previous Do11 failure fix; M5 +5; Do10 +5");
                    PrintResources();
                }
                successCalculatedDo11 = true;
                deterioratingCalculatedDo11 = false;
                failureCalculatedDo11 = false;
                CalculateResults();
                return;
            }
        }


        //Do12: Maintain confidence of Congress
        //  success: 15+
        //  situation deteriorating: 7-14
        //  failure: <7
        //      interdependencies:
        //          success: +3 to #13
        //          failure: -5 to #1

        // Maintain confidence of Congress
        if (numDo12Resources < 7)
        {
            //failure

            if (!failureCalculatedDo12)
            {
                numM1Resources -= 5;

                Debug.Log("Do12 FAIL: M1 -5");
                PrintResources();

                if (successCalculatedDo12)
                {
                    numDo13Resources -= 3;

                    Debug.Log("*previous Do12 success fix; Do13 -3");
                    PrintResources();
                }
                failureCalculatedDo12 = true;
                deterioratingCalculatedDo12 = false;
                successCalculatedDo12 = false;
                CalculateResults();
                return;
            }
        }
        else if (numDo12Resources < 15)
        {
            //situation deteriorating

            if (!deterioratingCalculatedDo12)
            {
                Debug.Log("Do12: SITUATION DETERIORATING");
                PrintResources();

                if (successCalculatedDo12)
                {
                    numDo13Resources -= 3;

                    Debug.Log("*previous Do12 success fix; Do13 -3");
                    PrintResources();
                }
                if (failureCalculatedDo12)
                {
                    numM1Resources += 5;

                    Debug.Log("*previous Do12 failure fix; M1 +5");
                    PrintResources();
                }
                deterioratingCalculatedDo12 = true;
                failureCalculatedDo12 = false;
                successCalculatedDo12 = false;
                CalculateResults();
                return;
            }
        }
        else
        {
            //success

            if (!successCalculatedDo12)
            {
                numDo13Resources += 3;

                Debug.Log("Do12 SUCCESS: Do13 +3");
                PrintResources();

                if (failureCalculatedDo12)
                {
                    numM1Resources += 5;

                    Debug.Log("*previous Do12 failure fix; M1 +5");
                    PrintResources();
                }
                successCalculatedDo12 = true;
                deterioratingCalculatedDo12 = false;
                failureCalculatedDo12 = false;
                CalculateResults();
                return;
            }
        }


        //Do13: Manage U.S. economy
        //  success: 20+
        //  situation deteriorating: 10-19
        //  failure: <10
        //      interdependencies:
        //          success: +3 to #14
        //          failure: -5 to #15

        // Manage U.S. economy
        if (numDo13Resources < 10)
        {
            //failure

            if (!failureCalculatedDo13)
            {
                numDo15Resources -= 5;

                Debug.Log("Do13 FAIL: Do15 -5");
                PrintResources();

                if (successCalculatedDo13)
                {
                    numDo14Resources -= 3;

                    Debug.Log("*previous Do13 success fix; Do14 -3");
                    PrintResources();
                }
                failureCalculatedDo13 = true;
                deterioratingCalculatedDo13 = false;
                successCalculatedDo13 = false;
                CalculateResults();
                return;
            }
        }
        else if (numDo13Resources < 25)
        {
            //situation deteriorating

            if (!deterioratingCalculatedDo13)
            {
                Debug.Log("Do13: SITUATION DETERIORATING");
                PrintResources();

                if (successCalculatedDo13)
                {
                    numDo14Resources -= 3;

                    Debug.Log("*previous Do13 success fix; Do14 -3");
                    PrintResources();
                }
                if (failureCalculatedDo13)
                {
                    numDo15Resources += 5;

                    Debug.Log("*previous Do13 failure fix; Do15 +5");
                    PrintResources();
                }
                deterioratingCalculatedDo13 = true;
                failureCalculatedDo13 = false;
                successCalculatedDo13 = false;
                CalculateResults();
                return;
            }
        }
        else
        {
            //success

            if (!successCalculatedDo13)
            {
                numDo14Resources += 3;

                Debug.Log("Do13 SUCCESS: Do14 +3");
                PrintResources();

                if (failureCalculatedDo13)
                {
                    numDo15Resources += 5;

                    Debug.Log("*previous Do13 failure fix; Do15 +5");
                    PrintResources();
                }
                successCalculatedDo13 = true;
                deterioratingCalculatedDo13 = false;
                failureCalculatedDo13 = false;
                CalculateResults();
                return;
            }
        }


        //Do14: Preserve Johnson's domestic "Great Society" programs
        //  success: 20+
        //  situation deteriorating: 10-19
        //  failure: <10
        //      interdependencies:
        //          success: +3 to #15
        //          failure: -5 to #13

        // Preserve Johnson's domestic "Great Society" programs
        if (numDo14Resources < 10)
        {
            //failure

            if (!failureCalculatedDo14)
            {
                numDo13Resources -= 5;

                Debug.Log("Do14 FAIL: Do13 -5");
                PrintResources();

                if (successCalculatedDo14)
                {
                    numDo15Resources -= 3;

                    Debug.Log("*previous Do14 success fix; Do15 -3");
                    PrintResources();
                }
                failureCalculatedDo14 = true;
                deterioratingCalculatedDo14 = false;
                successCalculatedDo14 = false;
                CalculateResults();
                return;
            }
        }
        else if (numDo14Resources < 25)
        {
            //situation deteriorating

            if (!deterioratingCalculatedDo14)
            {
                Debug.Log("Do14: SITUATION DETERIORATING");
                PrintResources();

                if (successCalculatedDo14)
                {
                    numDo15Resources -= 3;

                    Debug.Log("*previous Do14 success fix; Do15 -3");
                    PrintResources();
                }
                if (failureCalculatedDo14)
                {
                    numDo13Resources += 5;

                    Debug.Log("*previous Do14 failure fix; Do13 +5");
                    PrintResources();
                }
                deterioratingCalculatedDo14 = true;
                failureCalculatedDo14 = false;
                successCalculatedDo14 = false;
                CalculateResults();
                return;
            }
        }
        else
        {
            //success

            if (!successCalculatedDo14)
            {
                numDo15Resources += 3;

                Debug.Log("Do14 SUCCESS: Do15 +3");
                PrintResources();

                if (failureCalculatedDo14)
                {
                    numDo13Resources += 5;

                    Debug.Log("*previous Do14 failure fix; Do13 +5");
                    PrintResources();
                }
                successCalculatedDo14 = true;
                deterioratingCalculatedDo14 = false;
                failureCalculatedDo14 = false;
                CalculateResults();
                return;
            }
        }


        //Do15: Handle civil rights and urban unrest
        //  success: 15+
        //  situation deteriorating: 7-14
        //  failure: <7
        //      interdependencies:
        //          success: +3 to #11
        //          failure: -5 to #10

        // Handle civil rights and urban unrest
        if (numDo15Resources < 7)
        {
            //failure

            if (!failureCalculatedDo15)
            {
                numDo10Resources -= 5;

                Debug.Log("Do15 FAIL: Do10 -5");
                PrintResources();

                if (successCalculatedDo15)
                {
                    numDo11Resources -= 3;

                    Debug.Log("*previous Do15 success fix; Do11 -3");
                    PrintResources();
                }
                failureCalculatedDo15 = true;
                deterioratingCalculatedDo15 = false;
                successCalculatedDo15 = false;
                CalculateResults();
                return;
            }
        }
        else if (numDo15Resources < 15)
        {
            //situation deteriorating

            if (!deterioratingCalculatedDo15)
            {
                Debug.Log("Do15: SITUATION DETERIORATING");
                PrintResources();

                if (successCalculatedDo15)
                {
                    numDo11Resources -= 3;

                    Debug.Log("*previous Do15 success fix; Do11 -3");
                    PrintResources();
                }
                if (failureCalculatedDo15)
                {
                    numDo10Resources += 5;

                    Debug.Log("*previous Do15 failure fix; Do10 +5");
                    PrintResources();
                }
                deterioratingCalculatedDo15 = true;
                failureCalculatedDo15 = false;
                successCalculatedDo15 = false;
                CalculateResults();
                return;
            }
        }
        else
        {
            //success

            if (!successCalculatedDo15)
            {
                numDo11Resources += 3;

                Debug.Log("Do15 SUCCESS: Do11 +3");
                PrintResources();

                if (failureCalculatedDo15)
                {
                    numDo10Resources += 5;

                    Debug.Log("*previous Do15 failure fix; Do10 +5");
                    PrintResources();
                }
                successCalculatedDo15 = true;
                deterioratingCalculatedDo15 = false;
                failureCalculatedDo15 = false;
                CalculateResults();
                return;
            }
        }


        return;
    }



    //CALCULATION OUTCOMES

    public void FinalizeResults()
    {
        // Prevent a Communist takeover of South Vietnam
        if (numM1Resources < 15)
        {
            resultsText.text = "M1: failure - " + numM1Resources + "\r\n\r\n";
        }
        else if (numM1Resources < 30)
        {
            resultsText.text = "M1: situation deteriorating - " + numM1Resources + "\r\n\r\n";
        }
        else
        {
            resultsText.text = "M1: success - " + numM1Resources + "\r\n\r\n";
        }

        // Achieve military success in the field
        if (numM2Resources < 12)
        {
            resultsText.text += " M2: failure - " + numM2Resources + "\r\n\r\n";
        }
        else if (numM2Resources < 25)
        {
            resultsText.text += " M2: situation deteriorating - " + numM2Resources + "\r\n\r\n";
        }
        else
        {
            resultsText.text += " M2: success - " + numM2Resources + "\r\n\r\n";
        }

        // Secure South Vietnamese countryside (pacification)
        if (numM3Resources < 12)
        {
            resultsText.text += " M3: failure - " + numM3Resources + "\r\n\r\n";
        }
        else if (numM3Resources < 25)
        {
            resultsText.text += " M3: situation deteriorating - " + numM3Resources + "\r\n\r\n";
        }
        else
        {
            resultsText.text += " M3: success - " + numM3Resources + "\r\n\r\n";
        }

        // Stabilize and strengthen the South Vietnamese government
        if (numM4Resources < 10)
        {
            resultsText.text += " M4: failure - " + numM4Resources + "\r\n\r\n";
        }
        else if (numM4Resources < 20)
        {
            resultsText.text += " M4: situation deteriorating - " + numM4Resources + "\r\n\r\n";
        }
        else
        {
            resultsText.text += " M4: success - " + numM4Resources + "\r\n\r\n";
        }

        // Protect U.S. troops and minimize casualties
        if (numM5Resources < 10)
        {
            resultsText.text += " M5: failure - " + numM5Resources + "\r\n\r\n";
        }
        else if (numM5Resources < 20)
        {
            resultsText.text += " M5: situation deteriorating - " + numM5Resources + "\r\n\r\n";
        }
        else
        {
            resultsText.text += " M5: success - " + numM5Resources + "\r\n\r\n";
        }


        //Di6: Preserve American global credibility
        //  success: 15+
        //  situation deteriorating: 7-14
        //  failure: <7

        // Preserve American global credibility
        if (numDi6Resources < 7)
        {
            resultsText.text += " Di6: failure - " + numDi6Resources + "\r\n\r\n";
        }
        else if (numDi6Resources < 15)
        {
            resultsText.text += " Di6: situation deteriorating - " + numDi6Resources + "\r\n\r\n";
        }
        else
        {
            resultsText.text += " Di6: success - " + numDi6Resources + "\r\n\r\n";
        }


        //Di7: Open peace negotiations with North Vietnam
        //  success: 20+
        //  situation deteriorating: 10-19
        //  failure: <10

        // Open peace negotiations with North Vietnam
        if (numDi7Resources < 10)
        {
            resultsText.text += " Di7: failure - " + numDi7Resources + "\r\n\r\n";
        }
        else if (numDi7Resources < 20)
        {
            resultsText.text += " Di7: situation deteriorating - " + numDi7Resources + "\r\n\r\n";
        }
        else
        {
            resultsText.text += " Di7: success - " + numDi7Resources + "\r\n\r\n";
        }


        //Di8: Maintain allied support
        //  success: 15+
        //  situation deteriorating: 7-14
        //  failure: <7

        // Maintain allied support
        if (numDi8Resources < 7)
        {
            resultsText.text += " Di8: failure - " + numDi8Resources + "\r\n\r\n";
        }
        else if (numDi8Resources < 15)
        {
            resultsText.text += " Di8: situation deteriorating - " + numDi8Resources + "\r\n\r\n";
        }
        else
        {
            resultsText.text += " Di8: success - " + numDi8Resources + "\r\n\r\n";
        }


        //Di9: Manage relations with the Soviet Union and China
        //  success: 20+
        //  situation deteriorating: 10-19
        //  failure: <10

        // Manage relations with the Soviet Union and China
        if (numDi9Resources < 10)
        {
            resultsText.text += " Di9: failure - " + numDi9Resources + "\r\n\r\n";
        }
        else if (numDi9Resources < 20)
        {
            resultsText.text += " Di9: situation deteriorating - " + numDi9Resources + "\r\n\r\n";
        }
        else
        {
            resultsText.text += " Di9: success - " + numDi9Resources + "\r\n\r\n";
        }


        //Do10: Win the 1968 Presidential Election / Maintain Democratic party Unity
        //  success: 25+
        //  situation deteriorating: 12-24
        //  failure: <12

        // Win the 1968 Presidential Election / Maintain Democratic party Unity
        if (numDo10Resources < 12)
        {
            resultsText.text += " Do10: failure - " + numDo10Resources + "\r\n\r\n";
        }
        else if (numDo10Resources < 25)
        {
            resultsText.text += " Do10: situation deteriorating - " + numDo10Resources + "\r\n\r\n";
        }
        else
        {
            resultsText.text += " Do10: success - " + numDo10Resources + "\r\n\r\n";
        }


        //Do11: Respond to growing antiwar movement and public opinion
        //  success: 20+
        //  situation deteriorating: 10-19
        //  failure: <10

        // Respond to growing antiwar movement and public opinion
        if (numDo11Resources < 10)
        {
            resultsText.text += " Do11: failure - " + numDo11Resources + "\r\n\r\n";
        }
        else if (numDo11Resources < 20)
        {
            resultsText.text += " Do11: situation deteriorating - " + numDo11Resources + "\r\n\r\n";
        }
        else
        {
            resultsText.text += " Do11: success - " + numDo11Resources + "\r\n\r\n";
        }

        //Do12: Maintain confidence of Congress
        //  success: 15+
        //  situation deteriorating: 7-14
        //  failure: <7

        // Maintain confidence of Congress
        if (numDo12Resources < 7)
        {
            resultsText.text += " Do12: failure - " + numDo12Resources + "\r\n\r\n";
        }
        else if (numDo12Resources < 15)
        {
            resultsText.text += " Do12: situation deteriorating - " + numDo12Resources + "\r\n\r\n";
        }
        else
        {
            resultsText.text += " Do12: success - " + numDo12Resources + "\r\n\r\n";
        }


        //Do13: Manage U.S. economy
        //  success: 20+
        //  situation deteriorating: 10-19
        //  failure: <10

        // Manage U.S. economy
        if (numDo13Resources < 10)
        {
            resultsText.text += " Do13: failure - " + numDo13Resources + "\r\n\r\n";
        }
        else if (numDo13Resources < 20)
        {
            resultsText.text += " Do13: situation deteriorating - " + numDo13Resources + "\r\n\r\n";
        }
        else
        {
            resultsText.text += " Do13: success - " + numDo13Resources + "\r\n\r\n";
        }


        //Do14: Preserve Johnson's domestic "Great Society" programs
        //  success: 20+
        //  situation deteriorating: 10-19
        //  failure: <10

        // Preserve Johnson's domestic "Great Society" programs
        if (numDo14Resources < 10)
        {
            resultsText.text += " Do14: failure - " + numDo14Resources + "\r\n\r\n";
        }
        else if (numDo14Resources < 20)
        {
            resultsText.text += " Do14: situation deteriorating - " + numDo14Resources + "\r\n\r\n";
        }
        else
        {
            resultsText.text += " Do14: success - " + numDo14Resources + "\r\n\r\n";
        }


        //Do15: Handle civil rights and urban unrest
        //  success: 15+
        //  situation deteriorating: 7-14
        //  failure: <7

        // Preserve Johnson's domestic "Great Society" programs
        if (numDo15Resources < 7)
        {
            resultsText.text += " Do15: failure - " + numDo15Resources + "\r\n\r\n";
        }
        else if (numDo15Resources < 15)
        {
            resultsText.text += " Do15: situation deteriorating - " + numDo15Resources + "\r\n\r\n";
        }
        else
        {
            resultsText.text += " Do15: success - " + numDo15Resources + "\r\n\r\n";
        }



        int militaryResources = inputM1 + inputM2 + inputM3 + inputM4 + inputM5;
        float militaryResourcesAverage = militaryResources / 5;

        int domesticResources = inputDo10 + inputDo11 + inputDo12 + inputDo13 + inputDo14 + inputDo15;
        float domesticResourcesAverage = domesticResources / 6;

        int diplomaticResources = inputDi6 + inputDi7 + inputDi8 + inputDi9;
        float diplomaticResourcesAverage = diplomaticResources / 4;

        float militaryDomesticDifference = militaryResourcesAverage - domesticResourcesAverage;
        militaryDomesticDifference = (militaryDomesticDifference * 2) / 2;
        float domesticDiplomaticDifference = domesticResourcesAverage - diplomaticResourcesAverage;
        domesticDiplomaticDifference = (domesticDiplomaticDifference * 2) / 2;
        float diplomaticMilitaryDifference = diplomaticResourcesAverage - militaryResourcesAverage;
        diplomaticMilitaryDifference = (diplomaticMilitaryDifference * 2) / 2;
        Debug.Log(militaryDomesticDifference);
        Debug.Log(domesticDiplomaticDifference);
        Debug.Log(diplomaticMilitaryDifference);

        //Military ~= Domestic ~= Diplomatic
        if (militaryDomesticDifference <= 15 && domesticDiplomaticDifference <= 15 && diplomaticMilitaryDifference <= 15)
        {
            classification = "Fence-Sitter";
        }
        //Military > Domestic
        else if (militaryResourcesAverage > domesticResourcesAverage)
        {
            //Military > Domestic > Diplomatic
            if (domesticResourcesAverage > diplomaticResourcesAverage) 
            {
                classification = "Warrior";
            }
            //Diplomatic > Domestic
            else if (diplomaticResourcesAverage > domesticResourcesAverage)
            {
                //Military > Diplomatic > Domestic
                if (militaryResourcesAverage > diplomaticResourcesAverage)
                {
                    classification = "Warrior";
                }
                //Diplomatic > Military > Domestic
                else if (diplomaticResourcesAverage > militaryResourcesAverage)
                {
                    classification = "Diplomat";
                }
            }
        }
        //Domestic > Military
        else if (domesticResourcesAverage > militaryResourcesAverage)
        {
            //Domestic > Military > Diplomatic
            if (militaryResourcesAverage > diplomaticResourcesAverage)
            {
                classification = "Politician";
            }
            //Diplomatic > Domestic > Military
            else if (diplomaticResourcesAverage > domesticResourcesAverage)
            {
                classification = "Diplomat";
            }
            //Domestic > Diplomatic > Military
            else if (domesticResourcesAverage > diplomaticResourcesAverage)
            {
                classification = "Politician";
            }
        }

    }

}
