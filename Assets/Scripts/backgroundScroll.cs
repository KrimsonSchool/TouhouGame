using UnityEngine;

public class backgroundScroll : MonoBehaviour
{
    public float speed;
    public float limit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (speed * Time.deltaTime);
        if (transform.position.y <= limit)
        {
            transform.position = Vector2.zero;
        }
    }
}
