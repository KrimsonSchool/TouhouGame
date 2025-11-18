using UnityEngine;

public class Pickup : MonoBehaviour
{    
    public float speed;
    public float drawSpeed;
    public float drawRange;
    Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        drawRange = player.pickupRangeTru;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position+=transform.up*speed*Time.deltaTime;

        if (Vector2.Distance(transform.position, player.gameObject.transform.position) <= drawRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.gameObject.transform.position, (drawSpeed*-1)*Time.deltaTime);
        }
    }
}
