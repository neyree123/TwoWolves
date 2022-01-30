using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    Forces forces;
    public Transform purpleTransform;
    public Transform yellowTransform;

    public float playerAttractForce = 10000;

    private Vector3 purpleSpawnPosition;
    private Vector3 yellowSpawnPosition;

    [SerializeField] GameObject borderMang;
    [SerializeField] SetBorders.Anchor anchor;
    public Vector3 offset;

    public float portalTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        forces = GetComponent<Forces>();

        purpleSpawnPosition = purpleTransform.position;
        yellowSpawnPosition = yellowTransform.position;

        SetBorders bord = borderMang.GetComponent<SetBorders>();
        bord.SetRelativePos(gameObject, anchor);

        transform.position += offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (forces.canUse)
        {
            forces.Attract(ColorMode.White);
            forces.Attract(ColorMode.Black);
            forces.AttractPlayer(purpleTransform, playerAttractForce);
            forces.AttractPlayer(yellowTransform, playerAttractForce);
            StartCoroutine(forces.Cooldown());
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Black")
        {
            forces.objectParent.blackObjects.Remove(collision.transform);
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "White")
        {
            forces.objectParent.whiteObjects.Remove(collision.transform);
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Player")
        {
            ColorMode cm = collision.gameObject.GetComponent<PlayerController>().colorMode;

            if (cm == ColorMode.White)
            {
                yellowTransform.position = yellowSpawnPosition;
            }
            else
            {
                purpleTransform.position = purpleSpawnPosition;
            }
        }
    }

    public IEnumerator SpawnPortal()
    {
        yield return new WaitForSeconds(portalTime);
    }

}
