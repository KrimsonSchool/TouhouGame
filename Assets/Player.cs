using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    
    public Attack[] attacks;

    private float attackTimer;
    
    public SpriteRenderer sprite;

    private bool invincible;
    
    public int health = 3;
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
        if (other.CompareTag("EnemyPellet") && !invincible)
        {
            Destroy(other.gameObject);
            health--;
            StartCoroutine(Flash());
        }
    }

    IEnumerator Flash()
    {
        //0.35 secs at 0.05
        float flshscnds = 0.05f;
        //off on off on off on
        invincible = true;
        sprite.enabled = false;
        yield return new WaitForSeconds(flshscnds);
        sprite.enabled = true;
        yield return new WaitForSeconds(flshscnds);
        sprite.enabled = false;
        yield return new WaitForSeconds(flshscnds);
        sprite.enabled = true;
        yield return new WaitForSeconds(flshscnds);
        sprite.enabled = false;
        yield return new WaitForSeconds(flshscnds);
        sprite.enabled = true;
        yield return new WaitForSeconds(flshscnds);
        sprite.enabled = false;
        yield return new WaitForSeconds(flshscnds);
        sprite.enabled = true;
        invincible=false;
    }
}
