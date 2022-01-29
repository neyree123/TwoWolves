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

    public enum Anchor {TopLeft, Top, TopRight, Right, BottomRight, Bottom, BottomLeft, Left, Center};

    // Start is called before the first frame update
    void Start()
    {
        //Grab the positions of the camera borders
        vert = Camera.main.orthographicSize;
        horz = vert * Screen.width / Screen.height;

        //Place the walls appropriately

        //Upper Wall
        scale = borders[0].transform.localScale.y / 2;

        SetRelativePos(borders[0], Anchor.Top, -scale);
        borders[0].transform.localScale = new Vector3(horz * 2, 1, 1);

        //Right Wall
        SetRelativePos(borders[1], Anchor.Right, -scale);
        borders[1].transform.localScale = new Vector3(1, vert * 2, 1);

        //Adjust player 2 Pos
        SetRelativePos(players[1], Anchor.Right, distanceFromWall);
        //players[1].transform.position = new Vector3(horz - distanceFromWall, 0, 0);

        //Lower Wall
        SetRelativePos(borders[2], Anchor.Bottom, -scale);
        borders[2].transform.localScale = new Vector3(horz * 2, 1, 1);

        //Left Wall
        SetRelativePos(borders[3], Anchor.Left, -scale);
        borders[3].transform.localScale = new Vector3(1, vert * 2, 1);

        //Adjust Player 1 Pos
        SetRelativePos(players[0], Anchor.Left, distanceFromWall);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sets the position of an object relative to a point on the border
    /// </summary>
    /// <param name="obj">Gameobject being attached</param>
    /// <param name="point">Part of the border to attach to</param>
    /// <param name="distance">Distance from the point</param>
    public void SetRelativePos(GameObject obj, Anchor point, float distance = 0)
    {
        switch(point)
        {
            case Anchor.Bottom:
                obj.transform.position = new Vector3(0, -vert + distance, 0);
                break;

            case Anchor.BottomLeft:
                obj.transform.position = new Vector3(-horz + distance, -vert + distance, 0);
                break;

            case Anchor.BottomRight:
                obj.transform.position = new Vector3(horz - distance, -vert + distance, 0);
                break;

            case Anchor.Left:
                obj.transform.position = new Vector3(-horz + distance, 0, 0);
                break;

            case Anchor.Right:
                obj.transform.position = new Vector3(horz - distance, 0, 0);
                break;

            case Anchor.Top:
                obj.transform.position = new Vector3(0, vert - distance, 0);
                break;

            case Anchor.TopLeft:
                obj.transform.position = new Vector3(-horz + distance, vert - distance, 0);
                break;

            case Anchor.TopRight:
                obj.transform.position = new Vector3(horz - distance, vert - distance, 0);
                break;

            case Anchor.Center:
                obj.transform.position = new Vector3(0, 0, 0);
                break;
        }
    }
}
