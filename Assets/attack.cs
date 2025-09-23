using UnityEngine;

public class attack : MonoBehaviour
{
    public GameObject pellet;
    
    public bool circleAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //circle attack
    public void Init()
    {
        Instantiate(pellet,  transform.position, Quaternion.identity);
    }
}
