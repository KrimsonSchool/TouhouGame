using System.Threading.Tasks;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //waves are in streamingassets, named wave1, 2 etc.
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int waveIndex = 1;
    int bossIndex = 0;
    WaveSystem waveSystem;
    
    public GameObject[] bosses;

    public bool inBossFight;
    void Start()
    {
        waveSystem = FindFirstObjectByType<WaveSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async Task IncrementWave()
    {
        if (!inBossFight)
        {
            waveIndex++;
            Debug.LogWarning("At Wave "+ waveIndex);
            if (waveIndex % 5 == 0)
            {
                //dialogue first, then boss spawn...
                print("Boss time!!!");
                FindAnyObjectByType<DialogueManager>().StartConversation(bossIndex);
                inBossFight = true;
            }
            else
            {
                waveSystem.fileName = "wave" + waveIndex + ".txt";
                waveSystem.index = 0;
                await waveSystem.ReadWaveFile();
            }
        }
    }

    public void SpawnBoss(int index)
    {
        bossIndex = index;
        print("Spawn boss index "+ bossIndex);
        Instantiate(bosses[bossIndex], transform.position, Quaternion.identity);
        bossIndex++;
    }
}
