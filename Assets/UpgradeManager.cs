using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradePanel;
    public GameObject deathPanel;
    Player player;
    public TextMeshProUGUI goldText;

    private GameObject[] upgradeButtons;
    string[] upgradeNames = 
    {
        "Damage +",
        "Health +",
        "Num of Projectiles",
        "Projectile Penetration +",
        "XP Gain +",
        "Gold Gain +",
        "Pickup Range"
    };

    int[] upgradeCosts = 
    {
        1, 1, 1, 1, 1, 1, 1
    };

    private int[] upgradeLevels =
    {
        0, 0, 0, 0, 0, 0, 0
    };

    //damage, health, no of proj, proj pene, xp gain, gold gain
    void Start()
    {
        player = FindFirstObjectByType<Player>();
    }

    void Update()
    {
    }

    public void Init()
    {
        if (PlayerPrefs.GetInt("saved") == 1)
        {
            upgradeLevels = new int[]
            {
                PlayerPrefs.GetInt("damage") - 1,
                PlayerPrefs.GetInt("maxHealth") - 3,
                PlayerPrefs.GetInt("numberOfProjectiles")-1,
                PlayerPrefs.GetInt("projectilePenetration")-1,
                PlayerPrefs.GetInt("xpGain")-1,
                PlayerPrefs.GetInt("goldGain")-1,
                PlayerPrefs.GetInt("pickupRange")-1
            };

            upgradeCosts = new int[]
            {
                DetermineCost(1, upgradeLevels[0]),
                DetermineCost(1, upgradeLevels[1]),
                DetermineCost(1, upgradeLevels[2]),
                DetermineCost(1, upgradeLevels[3]),
                DetermineCost(1, upgradeLevels[4]),
                DetermineCost(1, upgradeLevels[5]),
                DetermineCost(1, upgradeLevels[6])
            };
        }
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        upgradePanel.SetActive(true);
        UpdateUi();
    }

    public void Upgrade(int upgradeNo)
    {
        if (player.gold >= upgradeCosts[upgradeNo])
        {
            switch (upgradeNo)
            {
                case 0:
                    player.damage++;
                    break;
                case 1:
                    player.maxHealth++;
                    player.health++;
                    break;
                case 2:
                    player.numberOfProjectiles++;
                    break;
                case 3:
                    player.projectilePenetration++;
                    break;
                case 4:
                    player.xpGain++;
                    break;
                case 5:
                    player.goldGain++;
                    break;
            }

            upgradeLevels[upgradeNo]++;
            player.gold -= upgradeCosts[upgradeNo];
            upgradeCosts[upgradeNo] += Mathf.RoundToInt(upgradeCosts[upgradeNo] * 1.2f);
            upgradeButtons[upgradeNo].GetComponent<Animation>().Play();
            UpdateUi();
            player.SaveData();
        }
    }

    public void Exit()
    {
        upgradePanel.SetActive(false);
        deathPanel.SetActive(true);
    }

    void UpdateUi()
    {
        goldText.text = ": " + player.gold;
        upgradeButtons = GameObject.FindGameObjectsWithTag("UpgradeButton");

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = upgradeNames[i]+": ["+upgradeLevels[i]+"]"+"\n($" + upgradeCosts[i] + ")";
        }
    }

    //NOT WORKIN...
    public int DetermineCost(int cost, int level)
    {
        int cCo = cost;
        for (int i = 0; i < level; i++)
        {
            cCo += Mathf.RoundToInt( cCo * 1.2f);
        }

        return cCo;
    }
    
}