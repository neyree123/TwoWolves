using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float distance;
    float prevDis;
    [SerializeField] GameObject borderMang;
    [SerializeField] SetBorders.Anchor anchor;
    [SerializeField] float distanceFromAnchor;
    [SerializeField] float offset;

    private enum Direction { Clockwise, CounterClockwise};
    [SerializeField] Direction direction;

    int numOfChildren;

    // Start is called before the first frame update
    void Start()
    {
        prevDis = distance;
        UpdateDistance();

        //Set connection for the anchor points

        if(!borderMang)
        {
            SetBorders bord = borderMang.GetComponent<SetBorders>();
            bord.SetRelativePos(gameObject, anchor, distanceFromAnchor, offset);
        }
      
        //Set direction of the rotation
        if (direction == Direction.Clockwise)
            speed *= 1;
        else if (direction == Direction.CounterClockwise)
            speed *= -1;

        //Setup position and rotation of children objects
        numOfChildren = transform.childCount;

        if(numOfChildren > 0)
        {
            SetPosition(GetDegrees(numOfChildren));
            SetRotation(GetDegrees(numOfChildren));
        }      
    }

    // Update is called once per frame
    void Update()
    {

        if (prevDis != distance)
            UpdateDistance();

        transform.eulerAngles -= new Vector3(0, 0, (Time.deltaTime * speed));

        prevDis = distance;
    }

    void UpdateDistance()
    {
        foreach (Transform child in transform)
        {
            child.position = child.position.normalized * distance;
        }
    }

    void SetPosition(float degrees)
    {
        for(int i = 0; i < numOfChildren; i++)
        {
            float xPos = transform.position.x + Mathf.Cos(degrees * i) * distance;
            float yPos = transform.position.y + Mathf.Sin(degrees * i) * distance;

            transform.GetChild(i).position = new Vector3(xPos, yPos, 0);
        }
    }

    void SetRotation(float degrees)
    {
        for (int i = 0; i < numOfChildren; i++)
        {
            transform.GetChild(i).eulerAngles = new Vector3(0, 0, (90 * i));
        }
    }


    /// <summary>
    /// Get an equal number of degrees based on the number of objects
    /// </summary>
    /// <param name="numOfObjects"></param>
    /// <returns></returns>
    float GetDegrees(int numOfObjects)
    {
        
        float degrees = (360/numOfObjects) * Mathf.Deg2Rad;

        return degrees;
    }
}
