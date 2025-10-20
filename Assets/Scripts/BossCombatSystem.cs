using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BossCombatSystem : MonoBehaviour
{
    [SerializeField]
    public struct BossEntry
    {
        public int attackIndex;
        public float timing;

        public BossEntry(int i, float f)
        {
            attackIndex = i;
            timing = f;
        }
    }

    private float _timer;
    public Attack[] attacks;

    public string fileName = "boss1.txt"; // Put this in Assets/StreamingAssets
    public List<BossEntry> bossEntries = new List<BossEntry>();

    private int index;

    public int paused = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ReadBossFile();
        paused = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (index < bossEntries.Count)
        {
            _timer += Time.deltaTime * paused;
            if (_timer >= bossEntries[index].timing)
            {
                attacks[bossEntries[index].attackIndex].Init(1);
                index++;
                _timer = 0;
            }
        }
        else
        {
            index = 0;
        }
    }

    void ReadBossFile()
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue; // skip empty lines
                if (line.StartsWith("//")) continue;

                string[] parts = line.Split(' ');

                if (parts.Length == 2)
                {
                    if (float.TryParse(parts[0], out float floatVal) &&
                        int.TryParse(parts[1], out int intVal))
                    {
                        bossEntries.Add(new BossEntry(intVal, floatVal));
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
        }
    }
}