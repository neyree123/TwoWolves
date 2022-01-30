using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    public float distance;

    public GameObject[] walls;
    public Vector3[] wallStartPos;
    public Vector3 wallEndPos;
    public float temp;
    public Transform moon;

    public bool down = true;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < walls.Length; i++)
        {
            wallStartPos[i] += moon.transform.position; 
            walls[i].transform.position = wallStartPos[i];

        }

        wallEndPos = new Vector3(0, wallStartPos[1].y - distance, 0);

        temp = wallStartPos[1].y;

    }

    // Update is called once per frame
    void Update()
    {
        walls[0].transform.position += new Vector3(-speed * Time.deltaTime / 2, -speed * Time.deltaTime / 2, 0);
        walls[1].transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
        walls[2].transform.position += new Vector3(speed * Time.deltaTime / 2, -speed * Time.deltaTime / 2, 0);

        if (walls[1].transform.position.y < wallEndPos.y && down)
        {
            speed *= -1;
            down = false;
        }
        else if (walls[1].transform.position.y > wallStartPos[1].y && !down)
        {
            speed *= -1;
            down = true;
        }
    }
}
