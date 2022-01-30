using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public float portalTime = 1;
    private bool expanding = false;
    //private bool contracting = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Expand());
    }

    // Update is called once per frame
    void Update()
    {
        if (expanding)
        {
            transform.localScale = transform.localScale + new Vector3(.03f, .03f, 0);
        }
        else
        {
            transform.localScale = transform.localScale - new Vector3(.03f, .03f, 0);
        }
    }

    public IEnumerator Expand()
    {
        expanding = true;
        yield return new WaitForSeconds(portalTime/2);
        expanding = false;
        StartCoroutine(Contract());
    }

    public IEnumerator Contract()
    {
        yield return new WaitForSeconds(portalTime / 2);
        Destroy(this);
    }
}
