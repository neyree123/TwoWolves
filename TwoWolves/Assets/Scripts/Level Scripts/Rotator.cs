using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float distance;
    float prevDis;

    // Start is called before the first frame update
    void Start()
    {
        prevDis = distance;
        UpdateDistance();
    }

    // Update is called once per frame
    void Update()
    {

        if (prevDis != distance)
            UpdateDistance();

        //transform.Rotate()
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
}
