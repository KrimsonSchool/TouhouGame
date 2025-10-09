using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradePanel;
    public GameObject deathPanel;
    Player player;
    public TextMeshProUGUI goldText;

    public int[] upgradeCosts = new []
    {
        1,1,1,1,1,1
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
        upgradePanel.SetActive(true);
        goldText.text = ": "+player.gold;
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
            player.gold -= upgradeCosts[upgradeNo];
        }
    }

    public void Exit()
    {
        upgradePanel.SetActive(false);
        deathPanel.SetActive(true);
    }

}
