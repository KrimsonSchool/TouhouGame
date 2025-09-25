using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    
    public Attack[] attacks;

    private float attackTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 180;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position+=transform.up * (Input.GetAxis("Vertical") * (speed*Time.deltaTime)) +  transform.right * (Input.GetAxis("Horizontal") * (speed*Time.deltaTime));

        attackTimer+=Time.deltaTime;

        if (attackTimer >= 0.1f)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                //shoot
                attacks[0].Init();
            }

            attackTimer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyPellet"))
        {
            Destroy(other.gameObject);
        }
    }
}
