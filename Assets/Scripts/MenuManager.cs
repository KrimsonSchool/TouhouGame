using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject upgradeMenu;
    public GameObject levelUpScreen;
    public TextMeshProUGUI levelText;
    private Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Upgrades()
    {
        upgradeMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void Retry()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LevelUp()
    {
        player.xp -= player.xpMax;
        player.level++;
        player.xpMax = Mathf.RoundToInt(player.xpMax*1.3f);
        levelUpScreen.SetActive(false);

        player.SaveData();
        
        print("checking xp...");
        print("has " + player.xp+" xp out of " + player.xpMax);
        if (player.xp >= player.xpMax)
        {
            print("so must re trigger...");
            InitLevelUp();
        }
    }

    public void InitLevelUp()
    {
        levelUpScreen.SetActive(true);
        levelText.text = player.level + "->" + (player.level + 1);
    }
}
