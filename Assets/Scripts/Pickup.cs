using UnityEngine;

public class Pickup : MonoBehaviour
{    
    public float speed;
    public float drawSpeed;
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position+=transform.up*speed*Time.deltaTime;

        if (Vector2.Distance(transform.position, player.transform.position) <= drawSpeed)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, (speed*-1)*Time.deltaTime);
        }
    }
}
