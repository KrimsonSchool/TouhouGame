using UnityEngine;

public class EnemyType : MonoBehaviour
{
    public GameObject enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(float pos)
    {
        print("Spawn enemy");
        Instantiate(enemy, new Vector2(pos, 5.25f), Quaternion.identity);
    }
}
