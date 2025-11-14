using UnityEngine;

public class Powerup : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite[] sprites;
    [Tooltip("0=shoot speed, 1=damage up, 2=health up, 3=heal")]
    public int powerupType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (powerupType == -1)
        {
            powerupType = Random.Range(0, sprites.Length - 1);
        }
        
        sr.sprite = sprites[powerupType];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
