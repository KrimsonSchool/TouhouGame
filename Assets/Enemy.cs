using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    public bool shoots;
    public GameObject proj;
    private float timer;
    public float shootSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
            Destroy(this.gameObject);
        }
    }
}
