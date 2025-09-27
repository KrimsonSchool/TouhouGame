using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    public struct WaveEntry
    {
        public float timing;
        public int enemyIndex;
        public float pos;

        public WaveEntry(float t, int i, float p)
        {
            timing = t;
            enemyIndex = i;
            pos = p;
        }
    }

    public string fileName = "wave1.txt";
    public List<WaveEntry> waveEntries = new List<WaveEntry>();

    private float _timer;
    public EnemyType[] enemies;

    public int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ReadWaveFile();
    }

    // Update is called once per frame
    async Task Update()
    {
        if (index < waveEntries.Count)
        {
            _timer += Time.deltaTime;
            if (_timer >= waveEntries[index].timing)
            {
                print("spawn wit timing of " +  waveEntries[index].timing);
                enemies[waveEntries[index].enemyIndex].Init(waveEntries[index].pos);
                index++;
                _timer = 0;
            }
        }
        else
        {
            waveEntries.Clear();
            //next wave or boss
            print("Next Wave!");
            await FindFirstObjectByType<WaveManager>().IncrementWave();
        }
    }

    public async Task ReadWaveFile()
    {
        print("reading from " + fileName);
        string path = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue; // skip empty lines
                if (line.StartsWith("//")) continue;

                string[] parts = line.Split(' ');

                if (parts.Length == 3)
                {
                    if (float.TryParse(parts[0], out float time) &&
                        int.TryParse(parts[1], out int indx) &&
                        float.TryParse(parts[2], out float pos))
                    {
                        waveEntries.Add(new WaveEntry(time, indx, pos));
                    }
                    else
                    {
                        Debug.LogWarning($"Invalid line format: {line}");
                    }
                }
            }
        }
        else
        {
            Debug.LogError($"File not found at {path}");
            this.enabled = false;
        }
    }
}