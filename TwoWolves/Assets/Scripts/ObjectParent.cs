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
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnObject(ColorMode.White, Random.insideUnitCircle * 5);
        }
        for (int i = 0; i < 3; i++)
        {
            SpawnObject(ColorMode.Black, Random.insideUnitCircle * 5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObject(ColorMode color, Vector2 pos)
    {
        GameObject g = Instantiate(objectPrefab, pos, Quaternion.identity, transform);
        if(color == ColorMode.White)
        {
            g.GetComponent<SpriteRenderer>().color = white;
            g.tag = "White";
            whiteObjects.Add(g.transform);
        }
        else
        {
            g.GetComponent<SpriteRenderer>().color = black;
            g.tag = "Black";
            blackObjects.Add(g.transform);
        }
      
    }
}
