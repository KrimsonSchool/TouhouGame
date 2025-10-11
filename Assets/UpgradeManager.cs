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
}