using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public GameObject resourceScreen;
    public GameObject militaryGoalsScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MilitaryScene()
    {
        resourceScreen.SetActive(false);
        militaryGoalsScreen.SetActive(true);

    }
}
