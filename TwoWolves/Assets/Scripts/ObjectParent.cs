using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ColorMode { White, Black }
public class ObjectParent : MonoBehaviour
{
    [SerializeField]
    Color white;
    [SerializeField]
    Color black;
    public List<Transform> blackObjects;
    public List<Transform> whiteObjects;
    [SerializeField]
    GameObject objectPrefab;
    private MoonScore moon;

    [Header("Spawn Manager Variables")]
    // Start is called before the first frame update
    public int roundTimeTotal = 50;
    public int timeBeforeSpawnRateIncrease = 20;
    public float timer = 0;
    public float maxTimeBeforeObjectSpawn = 10;
    public float minTimeBeforeObjectSpawn = 5;
    public float spawnChangeIncrement = 2;

    public TextMeshProUGUI timerText;

    public float moonWidth = 2;
    public float mapWidth = 20f;
    public float mapHeight = 8f;

    public Vector3[] whiteInitialSpawnLocations;
    public Vector3[] blackInitialSpawnLocations;

    [Header("PowerUp Spawning")]
    public float spawnDelayMin;
    public float spawnDelayMax;
    public int minSpawnAmount;
    public int maxSpawnAmount;
    public Transform powerUpParent;
    public int maxCurrentAmount;

    public List<GameObject> powerUpPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        whiteInitialSpawnLocations = new Vector3[3] { new Vector3(7.5f, 3f, 0), new Vector3(7.5f, -3f, 0), new Vector3(-5.5f, 0f, 0f) };

        blackInitialSpawnLocations = new Vector3[3] { new Vector3(-7.5f, 3f, 0), new Vector3(-7.5f, -3f, 0), new Vector3(5.5f, 0f, 0f) };

        moon = GetComponentInParent<MoonScore>();
        for (int i = 0; i < 3; i++)
        {
            SpawnObject(ColorMode.White, whiteInitialSpawnLocations[i]);
        }
        for (int i = 0; i < 3; i++)
        {
            SpawnObject(ColorMode.Black, blackInitialSpawnLocations[i]);
        }

        timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();

        StartCoroutine(SpawnObjectsOverTime());
        StartCoroutine(IncreaseSpawnRate());
        StartCoroutine(SpawnPowerups());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //Update Timer
        int min = Mathf.FloorToInt((roundTimeTotal - timer) / 60);
        int sec = Mathf.FloorToInt((roundTimeTotal - timer) % 60);

        if (roundTimeTotal - timer <= 0)
        {
            timerText.text = "0:00";
            moon.DetermineWinner();
        }
        else if(sec < 10)
        {
            timerText.text = min + ":0" + sec;
        }
        else
        {
            timerText.text = min + ":" + sec;
        }

    }

    public void SpawnObject(ColorMode color, Vector2 pos)
    {
        GameObject g = Instantiate(objectPrefab, pos, Quaternion.identity, transform);
        if(color == ColorMode.White)
        {
            g.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            g.tag = "White";
            whiteObjects.Add(g.transform);
        }
        else
        {
            g.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1f);
            g.tag = "Black";
            blackObjects.Add(g.transform);
        }
      
    }

    public IEnumerator IncreaseSpawnRate()
    {
        yield return new WaitForSeconds(timeBeforeSpawnRateIncrease);

        minTimeBeforeObjectSpawn -= spawnChangeIncrement;
        maxTimeBeforeObjectSpawn -= spawnChangeIncrement;

        if (minTimeBeforeObjectSpawn <= 0)
        {
            minTimeBeforeObjectSpawn = 1;
        }
        if (maxTimeBeforeObjectSpawn <= 1)
        {
            maxTimeBeforeObjectSpawn = 2;
        }

        //Debug.Log("IncreaseSpawnRate");

        if (timer < roundTimeTotal)
        {
            StartCoroutine(IncreaseSpawnRate());
        }
    }

    public IEnumerator SpawnObjectsOverTime()
    {
        

        yield return new WaitForSeconds(Random.Range(minTimeBeforeObjectSpawn, maxTimeBeforeObjectSpawn));

        //Find Spawn Locations
        bool purpleSpawned = false;
        bool yellowSpawned = false;

        while (!purpleSpawned)
        {
            Vector3 purplePos = new Vector3(Random.Range(-(mapWidth / 2), mapWidth / 2), Random.Range(-(mapHeight / 2), mapHeight / 2));

            //Debug.Log(purplePos);

            if ((purplePos - transform.position).magnitude < moonWidth)
            {
                continue;
            }
            else
            {
                SpawnObject(ColorMode.Black, purplePos);
                purpleSpawned = true;
            }
        }

        while (!yellowSpawned)
        {
            Vector3 yellowPos = new Vector3(Random.Range(-(mapWidth / 2), mapWidth / 2), Random.Range(-(mapHeight / 2), mapHeight / 2));

            //Debug.Log(yellowPos);

            if ((yellowPos - transform.position).magnitude < moonWidth)
            {
                continue;
            }
            else
            {
                SpawnObject(ColorMode.White, yellowPos);
                yellowSpawned = true;
            }
        }

        if (timer < roundTimeTotal)
        {
            StartCoroutine(SpawnObjectsOverTime());
        }

    }

    public IEnumerator SpawnPowerups()
    {
        while(timer < roundTimeTotal)
        {
            int currentPowerups = powerUpParent.childCount;
            int max = maxCurrentAmount -  currentPowerups > maxSpawnAmount ? maxSpawnAmount : maxCurrentAmount - currentPowerups;
            if (currentPowerups < maxCurrentAmount)
            {
                for (int i = 0; i < Random.Range(minSpawnAmount, max); i++)
                {
                    bool spawned = false;
                    while (!spawned)
                    {
                        Vector3 pos = new Vector3(Random.Range(-(mapWidth / 2), mapWidth / 2), Random.Range(-(mapHeight / 2), mapHeight / 2));

                        //Debug.Log(yellowPos);

                        if ((pos - transform.position).magnitude < moonWidth)
                        {
                            continue;
                        }
                        else
                        {
                            Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Count)], pos, Quaternion.identity, powerUpParent);
                            spawned = true;
                        }
                    }
                }
            }
            yield return new WaitForSeconds(Random.Range(spawnDelayMin, spawnDelayMax));
        }
    }
}
