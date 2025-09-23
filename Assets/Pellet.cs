using UnityEngine;

public class Pellet : MonoBehaviour
{
    public bool autoTarget;
    public float speed;

    private float _lifeTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (speed * Time.deltaTime);
        
        _lifeTimer+=Time.deltaTime;
        if (_lifeTimer >= 10)
        {
            Destroy(this.gameObject);
        }
    }
}
