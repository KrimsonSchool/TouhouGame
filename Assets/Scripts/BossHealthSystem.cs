using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthSystem : MonoBehaviour
{
    public GameObject forceField;
    public SpriteRenderer spriteRenderer;

    public int shieldHealth;
    public int health;
    
    private Coroutine _flickerRoutine;

    public GameObject eod;

    Slider healthBar;
    Slider shieldBar;

    bool sixsixpersh;
    bool thirthirpersh;
    
    AudioManager audioManager;

    public GameObject powerup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        audioManager = FindFirstObjectByType<AudioManager>();

        eod = GameObject.Find("EOD");

        healthBar = audioManager.healthBar;
        shieldBar = audioManager.shieldBar;
        
        healthBar.maxValue = health;
        shieldBar.maxValue = shieldHealth;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
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
                if(health<=66 && !sixsixpersh)
                {
                    shieldHealth = 100 + (100 - health);
                    sixsixpersh = true;
                }
                if (health <= 33 && !thirthirpersh)
                {
                    shieldHealth = 100 + (100 - health);
                    thirthirpersh = true;
                }
                if (_flickerRoutine == null)
                    _flickerRoutine = StartCoroutine(FlickerSprite());
                health-=other.GetComponent<Pellet>().damage;

                if (health <= 0)
                {
                    
                    UpdateHealthBar();
                    FindFirstObjectByType<WaveManager>().inBossFight = false;
                    Powerup pw = Instantiate(powerup, transform.position, Quaternion.identity).GetComponent<Powerup>();
                    pw.powerupType = 0;
                     pw = Instantiate(powerup, transform.position, Quaternion.identity).GetComponent<Powerup>();
                    pw.powerupType = 5;
                     pw = Instantiate(powerup, transform.position, Quaternion.identity).GetComponent<Powerup>();
                    pw.powerupType = 0;
                    Instantiate(powerup, transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                    //END OF DEMO
                    if (gameObject.name == "Boss2")
                    {
                        FindAnyObjectByType<DialogueManager>().StartConversation(2);
                    }
                }
            }
            Instantiate(audioManager.hitEffect, other.transform.position, Quaternion.identity);
            audioManager.impact.Play();
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

    public void UpdateHealthBar()
    {
        if (healthBar.enabled)
        {
            healthBar.value = health;
        }

        if (shieldBar.enabled)
        {
            shieldBar.value = shieldHealth;
        }

        if (health <= 0)
        {
            healthBar.gameObject.SetActive(false);
        }
        else
        {
            healthBar.gameObject.SetActive(true);
        }
        if (shieldHealth <= 0)
        {
            shieldBar.gameObject.SetActive(false);
        }
        else
        {
            shieldBar.gameObject.SetActive(true);
        }
    }
}
