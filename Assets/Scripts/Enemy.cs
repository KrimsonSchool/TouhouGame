using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float sideSpeed;

    public bool shoots;
    public GameObject proj;
    private float timer;
    public float shootSpeed;

    public GameObject[] dropped;
    public int[] chance;

    public int health=3;

    private bool dir;
    private float moveTime;
    private float movingTimer;    
    AudioManager audioManager;

    private bool edgePass;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();

        dir = Random.Range(0, 1) == 0;
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
            if (edgePass)
            {
                edgePass = false;
            }
        }

        if (dir)
        {
            transform.position+=transform.right * sideSpeed* Time.deltaTime;
        }
        else
        {
            transform.position-=transform.right * sideSpeed* Time.deltaTime;
        }

        if (!edgePass)
        {
            if (transform.position.x >= 6 || transform.position.x <= -6)
            {
                dir = !dir;
                movingTimer = 0;
                edgePass = true;
            }
        }

        if (transform.position.y <= -6)
        {
            transform.position += transform.up * 12;
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
            audioManager.impact.Play();
            
            Instantiate(audioManager.hitEffect, other.transform.position, Quaternion.identity);
            
            Destroy(other.gameObject);
            if (health <= 0)
            {
                for (int i = 0; i < dropped.Length; i++)
                {
                    if (Random.Range(0, 100) <= chance[i])
                    {
                        Instantiate(dropped[i], transform.position, Quaternion.identity);
                        break;
                    }
                }

                Destroy(this.gameObject);
            }
        }
    }
}
