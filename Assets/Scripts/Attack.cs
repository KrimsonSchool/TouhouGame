using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject pellet;
    
    public bool circleAttack;

    private float rot;
    public int nom;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //circle attack
    public void Init(int dmg)
    {
        if (circleAttack)
        {
            for (int i = 0; i < nom; i++)
            {
                rot = (360 / nom) * i;
                GameObject pel = Instantiate(pellet, transform.position, Quaternion.identity);
                pel.transform.eulerAngles = new Vector3(0, 0, rot);
                
                if (pel.GetComponent<Pellet>() != null)
                {
                    pel.GetComponent<Pellet>().damage = dmg;
                }
                else
                {
                    pel.GetComponentInChildren<Pellet>().damage = dmg;
                }
            }
        }
        else
        {
            GameObject pel = Instantiate(pellet, transform.position, Quaternion.identity);
            
            pel.GetComponent<Pellet>().damage = dmg;
        }
        
    }
}
