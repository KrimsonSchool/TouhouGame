using System;
using System.Collections;
using UnityEngine;

public class BossHealthSystem : MonoBehaviour
{
    public GameObject forceField;
    public SpriteRenderer spriteRenderer;

    public int shieldHealth;
    public int health;
    
    private Coroutine _flickerRoutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerPellet"))
        {
            if (shieldHealth > 0)
            {
                shieldHealth-= other.GetComponent<Pellet>().damage;
                if (_flickerRoutine == null)
                    _flickerRoutine = StartCoroutine(Flicker());
            }
            else
            {
                if (health == 66 || health == 33)
                {
                    shieldHealth = 100 + (100-health);
                }
                if (_flickerRoutine == null)
                    _flickerRoutine = StartCoroutine(FlickerSprite());
                health-=other.GetComponent<Pellet>().damage;

                if (health <= 0)
                {
                    FindFirstObjectByType<WaveManager>().inBossFight = false;
                    Destroy(this.gameObject);
                    //END OF DEMO
                }
            }
            Destroy(other.gameObject);
        }
    }

    IEnumerator Flicker()
    {
        forceField.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        forceField.SetActive(false);
        _flickerRoutine = null;
    }

    IEnumerator FlickerSprite()
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.enabled = true;
        _flickerRoutine = null;
    }
}
