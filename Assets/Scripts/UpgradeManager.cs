using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradePanel;
    public GameObject deathPanel;
    Player player;
    public TextMeshProUGUI goldText;
    
    public float priceIncreaseAmount;

    private GameObject[] upgradeButtons;
    string[] upgradeNames = 
    {
        "Damage +",
        "Health +",
        "Shoot Speed +",
        "Projectile Penetration +",
        "XP Gain +",
        "Gold Gain +",
        "Pickup Range"
    };

    int[] upgradeCosts = 
    {
        1, 1, 2, 1, 5, 1, 1
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
                PlayerPrefs.GetInt("shootSpeed")-1,
                PlayerPrefs.GetInt("projectilePenetration")-1,
                PlayerPrefs.GetInt("xpGain")-1,
                PlayerPrefs.GetInt("goldGain")-1,
                PlayerPrefs.GetInt("pickupRange")-1
            };

            upgradeCosts = new int[]
            {
                DetermineCost(upgradeCosts[0], upgradeLevels[0]),
                DetermineCost(upgradeCosts[1], upgradeLevels[1]),
                DetermineCost(upgradeCosts[2], upgradeLevels[2]),
                DetermineCost(upgradeCosts[3], upgradeLevels[3]),
                DetermineCost(upgradeCosts[4], upgradeLevels[4]),
                DetermineCost(upgradeCosts[5], upgradeLevels[5]),
                DetermineCost(upgradeCosts[6], upgradeLevels[6])
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
                    player.shootSpeedLevel++;
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
            upgradeCosts[upgradeNo] += Mathf.RoundToInt(upgradeCosts[upgradeNo] * priceIncreaseAmount);
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
            cCo += Mathf.RoundToInt( cCo * priceIncreaseAmount);
        }

        return cCo;
    }
    
}