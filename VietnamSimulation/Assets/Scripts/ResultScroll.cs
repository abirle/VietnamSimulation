using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResultScroll : MonoBehaviour
{
    bool isScrolling;
    float timeElapsed = 0;
    public float scrollDuration = 60;
    Vector3 startingTextPosition = new Vector3(-44.7f, -137.2f, 0);
    Vector3 endingTextPosition = new Vector3(-44.7f, 200f, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isScrolling)
        {
            timeElapsed += Time.deltaTime;
            float scrollProgress = Mathf.Clamp01(timeElapsed / scrollDuration);

            gameObject.transform.localPosition = Vector3.Lerp(startingTextPosition, endingTextPosition, scrollProgress);
            
            if (scrollProgress >= 1f)
            {
                Debug.Log(scrollProgress);
                isScrolling = false;
            }
        }

    }

    public void Scroll()
    {
        timeElapsed = 0;
        isScrolling = true;
        Debug.Log("scroll function entered");
    }
}
