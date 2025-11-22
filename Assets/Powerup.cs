using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Powerup : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite[] sprites;
    [Tooltip("0=shoot speed, 1=shoot speed, 2=damage up, 3=health up, 4=power")]
    public int powerupType;

    private float movDist;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movDist = 0.5f;
        if (powerupType == -1)
        {
            RollPowerup();
        }

        if(powerupType == 4 && FindFirstObjectByType<Player>().power>4.75f)
        {
            RollPowerup();
        }
        
        sr.sprite = sprites[powerupType];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RollPowerup()
    {
        powerupType = Random.Range(1, sprites.Length);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Powerup" || other.tag == "Gold")
        {
            float movDir1 = Random.Range(-movDist, movDist);
            float movDir2 = Random.Range(-movDist, movDist);
            transform.position += new Vector3(movDir1, movDir2);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Powerup" || other.tag == "Gold")
        {
            float movDir1 = Random.Range(-movDist, movDist);
            float movDir2 = Random.Range(-movDist, movDist);
            transform.position += new Vector3(movDir1, movDir2);
        }
    }
}
