using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public float speed;

    public bool shoots;
    public GameObject proj;
    private float timer;
    public float shootSpeed;

    public GameObject[] dropped;

    private int health=3;

    private bool dir;
    private float moveTime;
    private float movingTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movingTimer += Time.deltaTime;
        if (movingTimer >= moveTime)
        {
            moveTime = Random.Range(1, 6);
            dir = !dir;
            movingTimer = 0;
        }

        if (dir)
        {
            transform.position+=transform.right * speed* Time.deltaTime;
        }
        else
        {
            transform.position-=transform.right * speed* Time.deltaTime;
        }
        
        transform.position+=transform.up*speed*Time.deltaTime;

        if (shoots)
        {
            timer += Time.deltaTime;
            if (timer >= shootSpeed)
            {
                timer = 0;
                Instantiate(proj, transform.position, Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerPellet"))
        {
            health -= other.GetComponent<Pellet>().damage;
            
            Destroy(other.gameObject);
            if (health <= 0)
            {
                foreach (GameObject drop in dropped)
                {
                    Instantiate(drop, transform.position, Quaternion.identity);
                }

                Destroy(this.gameObject);
            }
        }
    }
}
