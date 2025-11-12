using UnityEngine;
using UnityEngine.UI;

public class Blinker : MonoBehaviour
{
    public Image img;
    public float speed;
    float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= speed) 
        {
            timer = 0;
            img.enabled = !img.enabled;
        }
    }
}
