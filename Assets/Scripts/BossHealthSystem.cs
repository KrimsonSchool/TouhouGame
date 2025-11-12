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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        audioManager = FindFirstObjectByType<AudioManager>();

        eod = GameObject.Find("EOD");

        healthBar = GameObject.Find("BossHealth").GetComponent<Slider>();
        shieldBar = GameObject.Find("BossShield").GetComponent<Slider>();


        healthBar.maxValue = health;
        shieldBar.maxValue = shieldHealth;

        healthBar.enabled = true;
        shieldBar.enabled = true;


    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health;
        shieldBar.value = shieldHealth;

        if (health <= 0)
        {
            healthBar.enabled = false;
        }
        else
        {
            healthBar.enabled = true;
        }
        if (shieldHealth <= 0)
        {
            shieldBar.enabled = false;
        }
        else
        {
            shieldBar.enabled = true;
        }
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
                    FindFirstObjectByType<WaveManager>().inBossFight = false;
                    Destroy(this.gameObject);
                    //END OF DEMO
                    if (gameObject.name == "Boss2")
                    {
                        eod.GetComponentInChildren<Image>(true).gameObject.SetActive(true);
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.Confined;
                    }
                }
            }
            Instantiate(audioManager.hitEffect, other.transform.position, Quaternion.identity);

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
