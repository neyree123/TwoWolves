using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBorders : MonoBehaviour
{
    float vert;
    float horz;
    float scale;

    [SerializeField]GameObject[] borders;

    [SerializeField] GameObject[] players;
    [SerializeField] float distanceFromWall;

    // Start is called before the first frame update
    void Start()
    {
        //Grab the positions of the camera borders
        vert = Camera.main.orthographicSize;
        horz = vert * Screen.width / Screen.height;

        //Debug.Log("Vert: " + vert + ",Horz: " + horz);

        //Place the walls appropriately

        //Upper Wall
        scale = borders[0].transform.localScale.y / 2;

        borders[0].transform.position = new Vector3(0, vert + scale, 0);
        borders[0].transform.localScale = new Vector3(horz * 2, 1, 1);

        //Right Wall
        borders[1].transform.position = new Vector3(horz + scale, 0, 0);
        borders[1].transform.localScale = new Vector3(1, vert * 2, 1);

        //Adjust player 2 Pos
        players[1].transform.position = new Vector3(horz - distanceFromWall, 0, 0);

        //Lower Wall
        borders[2].transform.position = new Vector3(0, -vert - scale, 0);
        borders[2].transform.localScale = new Vector3(horz * 2, 1, 1);

        //Left Wall
        borders[3].transform.position = new Vector3(-horz - scale, 0, 0);
        borders[3].transform.localScale = new Vector3(1, vert * 2, 1);

        //Adjust Player 1 Pos
        players[0].transform.position = new Vector3(-horz + distanceFromWall, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
