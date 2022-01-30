using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBorders : MonoBehaviour
{
    public float vert;
    public float horz;
    float scale;

    [SerializeField]GameObject[] borders;

    [SerializeField] GameObject[] players;
    Vector3[] playerStartPos = new Vector3[2];
    [SerializeField] float distanceFromWall;
    [SerializeField] float wolfOffset;

    public enum Anchor {TopLeft, Top, TopRight, Right, BottomRight, Bottom, BottomLeft, Left, Center};

    [SerializeField] Anchor yellowAnchor;
    [SerializeField] Anchor purpleAnchor;

    private void Awake()
    {
        //Grab the positions of the camera borders
        vert = Camera.main.orthographicSize;
        horz = vert * Screen.width / Screen.height;
    }
    // Start is called before the first frame update
    void Start()
    {
    
        //Place the walls appropriately

        //Upper Wall
        scale = borders[0].transform.localScale.y / 2;

        SetRelativePos(borders[0], Anchor.Top, -scale);
        borders[0].transform.localScale = new Vector3(horz * 2, 1, 1);

        //Right Wall
        SetRelativePos(borders[1], Anchor.Right, -scale);
        borders[1].transform.localScale = new Vector3(1, vert * 2, 1);

        //Adjust Yellow player 2 Pos
        SetRelativePos(players[1], yellowAnchor, distanceFromWall, wolfOffset);
        playerStartPos[1] = players[1].transform.position;

        //Lower Wall
        SetRelativePos(borders[2], Anchor.Bottom, -scale);
        borders[2].transform.localScale = new Vector3(horz * 2, 1, 1);

        //Left Wall
        SetRelativePos(borders[3], Anchor.Left, -scale);
        borders[3].transform.localScale = new Vector3(1, vert * 2, 1);

        //Adjust Purple Player 1 Pos
        SetRelativePos(players[0], purpleAnchor, distanceFromWall, -wolfOffset);
        playerStartPos[0] = players[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 2; i++)
        {
            KeepPlayerInBounds(players[i], playerStartPos[i]);
        }
    }

    /// <summary>
    /// Sets the position of an object relative to a point on the border
    /// </summary>
    /// <param name="obj">Gameobject being attached</param>
    /// <param name="point">Part of the border to attach to</param>
    /// <param name="distance">Distance from the point</param>
    public void SetRelativePos(GameObject obj, Anchor point, float distance = 0, float offset = 0)
    {
        //Debug.Log(obj.name + " is set to " + point);

        switch(point)
        {
            case Anchor.Bottom:
                obj.transform.position = new Vector3(offset, -vert + distance, 0);
                break;

            case Anchor.BottomLeft:
                obj.transform.position = new Vector3(-horz + distance, -vert + distance, 0);
                break;

            case Anchor.BottomRight:
                obj.transform.position = new Vector3(horz - distance, -vert + distance, 0);
                break;

            case Anchor.Left:
                obj.transform.position = new Vector3(-horz + distance, offset, 0);
                break;

            case Anchor.Right:
                obj.transform.position = new Vector3(horz - distance, offset, 0);
                break;

            case Anchor.Top:
                obj.transform.position = new Vector3(offset, vert - distance, 0);
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

    void KeepPlayerInBounds(GameObject player, Vector3 startPos)
    {
        float x = player.transform.position.x;
        float y = player.transform.position.y;

        if (x > horz || x < -horz || y > vert || y < -vert)
            player.transform.position = startPos;
    }
}
