using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Values")]
    public float speed;
    private float attackTimer;
    private bool invincible;
    public int health = 3;
    public int maxHealth = 3;
    public int damage = 1;
    public int projectilePenetration;//pellet health, when hit enemy reduce by 1
    public int xpGain;
    public int goldGain;
    public int gold;
    public int xp;
    public int pickupRange;
    public float pickupRangeTru;
    private int level=1;
    private int xpMax = 10;
    public float shootSpeed;
    float sP;
    public int shootSpeedLevel;
    private bool dead;
    public float power;
    
    [Space]
    [Header("UI Elements")]
    public GameObject bloodDead;
    public GameObject saveIcon;
    public Slider healthBar;
    public TextMeshProUGUI healthText;
    public SpriteRenderer soul;
    public TextMeshProUGUI powerLevel;
    public Slider powerBar;
    public SpriteRenderer sprite;
    public Attack[] attacks;
    UpgradeManager upgradeManager;
    AudioManager audioManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sP = shootSpeed;
        Application.targetFrameRate = 180;
        upgradeManager = FindFirstObjectByType<UpgradeManager>();
        audioManager = FindFirstObjectByType<AudioManager>();
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        if (PlayerPrefs.GetInt("saved") == 1)
        {
            maxHealth = PlayerPrefs.GetInt("maxHealth");
            damage =  PlayerPrefs.GetInt("damage");
            sP =  shootSpeed - (PlayerPrefs.GetInt("shootSpeed")* 0.025f);
            shootSpeedLevel = PlayerPrefs.GetInt("shootSpeed");
            xpGain = PlayerPrefs.GetInt("xpGain");
            goldGain =  PlayerPrefs.GetInt("goldGain");
            pickupRange =  PlayerPrefs.GetInt("pickupRange");
            pickupRangeTru = 1 + (pickupRange * 0.4f);
            
            print("Shoot speed lvl: "+PlayerPrefs.GetInt("shootSpeed")+ " which equates to " + sP+" secs per shot");
        }
        
        health = maxHealth;
        power = 5;
        powerBar.maxValue = power;

        //shootSpeed = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if (sP <= 0)
            {
                sP = 0.05f;
            }

            healthBar.maxValue = maxHealth;
            healthBar.value = health;
            healthText.text = "HEALTH: "+ health + "/" + maxHealth;

            transform.position += transform.up * (Input.GetAxis("Vertical") * (speed * Time.deltaTime)) +
                                  transform.right * (Input.GetAxis("Horizontal") * (speed * Time.deltaTime));

            attackTimer += Time.deltaTime;

            if (Input.GetKeyUp(KeyCode.Space))
            {
                attackTimer = sP;
            }
            if (attackTimer >= sP)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    //shoot
                    StartCoroutine(SpawnProj());
                }

                attackTimer = 0;
            }


            if (Input.GetKeyDown(KeyCode.M))
            {
                PlayerPrefs.SetInt("saved", 0);
                SceneManager.LoadScene(SceneManager.loadedSceneCount);
            }

            //y = -5, 5
            //x = -9, 9

            if (transform.position.y > 5)
            {
                transform.position += transform.up * -1;
            }

            if (transform.position.y < -5)
            {
                transform.position += transform.up * 1;
            }

            if (transform.position.x > 9)
            {
                transform.position += transform.right * -1;
            }

            if (transform.position.x < -9)
            {
                transform.position += transform.right * 1;
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (power > 0)
                {
                    Time.timeScale = 0.25f;
                    soul.enabled = true;
                    print(sprite.color);
                    sprite.color = new Color(1, 1, 1, 0.5f);
                    power -= Time.deltaTime;
                    if (power <= 0)
                    {
                        Time.timeScale = 1;
                        soul.enabled = false;
                        sprite.color = new Color(1, 1, 1, 1);
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                Time.timeScale = 1;
                soul.enabled = false;
                sprite.color = new Color(1, 1, 1, 1);
            }

            powerLevel.text = "POWER: " + (float)System.Math.Round(power, 2);
            powerBar.value = power;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyPellet") && !invincible && !dead)
        {
            Destroy(other.gameObject);
            health--;
            if (health <= 0)
            {
                Death();
            }
            StartCoroutine(Flash());
        }
        if (other.CompareTag("Gold") && !dead)
        {
            Destroy(other.gameObject);
            gold+= goldGain;
        }
        if (other.CompareTag("Xp") && !dead)
        {
            xp+=xpGain;
            if (xp >= xpMax)
            {
                xp-=xpMax;
                xpMax += (xpMax / 7);
                level++;
            }
        }
        if (other.CompareTag("Powerup") && !dead)
        {
            Powerup p = other.GetComponent<Powerup>();
            switch (p.powerupType)
            {
                case 0:
                    sP -= 0.025f;
                    break;
                case 1:
                    damage += 1;
                    break;
                case 2:
                    maxHealth += 1;
                    break;
                case 3:
                    power += 0.6f;
                    break;
                case 4:
                    health += 1;
                    break;
            }
            Destroy(other.gameObject);
        }
    }

    IEnumerator Flash()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("Shake");
        //0.35 secs at 0.05
        float flshscnds = 0.05f;
        //off on off on off on
        invincible = true;
        sprite.enabled = false;
        yield return new WaitForSeconds(flshscnds);
        
        Camera.main.GetComponent<Animator>().ResetTrigger("Shake");
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

    IEnumerator SpawnProj()
    {
        audioManager.shoot.Play();
        attacks[0].Init(damage);
        yield return new WaitForSeconds(0.2f);
    }

    public void Death()
    {
        dead = true;
        Time.timeScale = 1;
        Instantiate(audioManager.playerDeath, transform.position, Quaternion.identity);
        bloodDead.SetActive(true);
        upgradeManager.Init();

        if (FindFirstObjectByType<BossCombatSystem>() != null)
        {
            FindFirstObjectByType<BossCombatSystem>().paused = 0;
        }
        sprite.enabled = false;
    }
    
    public void SaveData()
    {
        saveIcon.SetActive(true);
        PlayerPrefs.SetInt("saved", 1);
        PlayerPrefs.SetInt("maxHealth", maxHealth);
        PlayerPrefs.SetInt("damage", damage);
        PlayerPrefs.SetInt("shootSpeed", shootSpeedLevel);
        PlayerPrefs.SetInt("xpGain", xpGain);
        PlayerPrefs.SetInt("goldGain", goldGain);
        PlayerPrefs.SetInt("pickupRange", pickupRange);
        saveIcon.SetActive(false);
    }
}
