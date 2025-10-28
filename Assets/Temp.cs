using UnityEngine;

public class Temp : MonoBehaviour
{
    public float life;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= life)
        {
            Destroy(this.gameObject);
        }
    }
}
