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
    public int maxHealth = 3;
    public int damage = 1;
    public int numberOfProjectiles;
    public int projectilePenetration;//pellet health, when hit enemy reduce by 1
    public int xpGain;
    public int goldGain;
    
    public int gold;
    public int xp;

    private int level=1;
    private int xpMax = 10;
    
    UpgradeManager upgradeManager;

    public GameObject bloodDead;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 180;
        upgradeManager = FindFirstObjectByType<UpgradeManager>();
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
                StartCoroutine(SpawnProj());
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
            if (health <= 0)
            {
                Death();
            }
            StartCoroutine(Flash());
        }

        if (other.CompareTag("Gold"))
        {
            Destroy(other.gameObject);
            gold+= goldGain;
        }
        if (other.CompareTag("Xp"))
        {
            xp+=xpGain;
            if (xp >= xpMax)
            {
                xp-=xpMax;
                xpMax += (xpMax / 7);
                level++;
            }
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

    IEnumerator SpawnProj()
    {
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            yield return new WaitForSeconds(0.2f);
            attacks[0].Init(damage);
        }
    }

    public void Death()
    {
        //Time.timeScale = 0;
        bloodDead.SetActive(true);
        //restart to wave 1
        //red screen filter
        //show upgrades board
        upgradeManager.Init();
    }
}
