using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteRepulsion : MonoBehaviour
{
    Forces force;
    [SerializeField] ColorMode color;

    // Start is called before the first frame update
    void Start()
    {
        force = gameObject.GetComponent<Forces>();
    }

    // Update is called once per frame
    void Update()
    {
        if(force.canUse)
        {
            force.Repulse(color);
            StartCoroutine(force.Cooldown());
        }
    }
}
