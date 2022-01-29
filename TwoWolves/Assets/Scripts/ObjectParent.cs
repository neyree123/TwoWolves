using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Spawn Manager Variables")]
    // Start is called before the first frame update
    public int roundTimeTotal = 50;
    public int timeBeforeSpawnRateIncrease = 20;
    public float timer = 0;
    public float maxTimeBeforeObjectSpawn = 10;
    public float minTimeBeforeObjectSpawn = 5;
    public float spawnChangeIncrement = 2;

    public float moonWidth = 2;
    public float mapWidth = 20f;
    public float mapHeight = 8f;

    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < 3; i++)
        //{
        //    SpawnObject(ColorMode.White, Random.insideUnitCircle * 5);
        //}
        //for (int i = 0; i < 3; i++)
        //{
        //    SpawnObject(ColorMode.Black, Random.insideUnitCircle * 5);
        //}

        StartCoroutine(SpawnObjectsOverTime());
        StartCoroutine(IncreaseSpawnRate());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
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

        Debug.Log("IncreaseSpawnRate");

        if (timer < roundTimeTotal)
        {
            StartCoroutine(IncreaseSpawnRate());
        }
    }

    public IEnumerator SpawnObjectsOverTime()
    {
        //Find Spawn Locations
        bool purpleSpawned = false;
        bool yellowSpawned = false;

        while (!purpleSpawned)
        {
            Vector3 purplePos = new Vector3(Random.Range(-(mapWidth/2), mapWidth / 2), Random.Range(-(mapHeight/2), mapHeight/2));

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

        yield return new WaitForSeconds(Random.Range(minTimeBeforeObjectSpawn, maxTimeBeforeObjectSpawn));

        if (timer < roundTimeTotal)
        {
            StartCoroutine(SpawnObjectsOverTime());
        }

    }
}
