using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Forces : MonoBehaviour
{
    public ObjectParent objectParent;

    [SerializeField]
    private float dampeningConstant;
    [SerializeField]
    private float cooldown;
    public bool canUse = true;
    [Header("Repulsion")]
    [SerializeField]
    private float repulseForce;
    [SerializeField]
    private float repulseRange;
    public GameObject purpleRepulsion;
    public GameObject yellowRepulsion;

    [Header("Attract")]
    [SerializeField]
    private float attractForce;
    [SerializeField]
    private float attractRange;
    public GameObject purpleAttraction;
    public GameObject yellowAttraction;

    public GameObject cancelImage;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       

    }
    public void AttractPlayer(Transform playerTransform, float playerAttractForce)
    {
        if (Vector2.Distance(transform.position, playerTransform.position) < attractRange)
        {
            Vector2 dir = (transform.position - playerTransform.position).normalized;
            Vector2 force = dir * Mathf.Lerp(playerAttractForce, 10, Vector2.Distance(transform.position, playerTransform.position) / attractRange);
            playerTransform.GetComponent<Rigidbody2D>().AddForce(force);

            Debug.Log("Drag Player");

        }
    }

    public void Attract(ColorMode color)
    {
        if(color == ColorMode.White && canUse)
        {
            foreach(Transform t in objectParent.whiteObjects)
            {
                if (Vector2.Distance(transform.position, t.position) < attractRange)
                {
                    Vector2 dir = (transform.position - t.position).normalized;
                    Vector2 force = dir * Mathf.Lerp(attractForce, 10, Vector2.Distance(transform.position, t.position) / attractRange);
                    t.GetComponent<Rigidbody2D>().AddForce(force);
                }
            }
            Instantiate(purpleAttraction, transform);
        }
        else if(canUse)
        {
            foreach (Transform t in objectParent.blackObjects)
            {
                if (Vector2.Distance(transform.position, t.position) < attractRange)
                {
                    Vector2 dir = (transform.position - t.position).normalized;
                    Vector2 force = dir * Mathf.Lerp(attractForce, 10, Vector2.Distance(transform.position, t.position) / attractRange);
                    t.GetComponent<Rigidbody2D>().AddForce(force);
                }
            }
            Instantiate(yellowAttraction, transform);
        }
    }

    public void Repulse(ColorMode color)
    {
        if (color == ColorMode.White && canUse )
        {
            foreach (Transform t in objectParent.whiteObjects)
            {
                if (Vector2.Distance(transform.position, t.position) < repulseRange)
                {
                    Vector2 dir = (transform.position - t.position).normalized;
                    Vector2 force = -dir * Mathf.Lerp(attractForce, 10, Vector2.Distance(transform.position, t.position) / repulseRange);
                    t.GetComponent<Rigidbody2D>().AddForce(force);
                }
            }
            Instantiate(purpleRepulsion,transform);
        }
        else if(canUse)
        {
            foreach (Transform t in objectParent.blackObjects)
            {
                if (Vector2.Distance(transform.position, t.position) < repulseRange)
                {
                    Vector2 dir = (transform.position - t.position).normalized;
                    Vector2 force = -dir * Mathf.Lerp(attractForce, 10, Vector2.Distance(transform.position, t.position) / repulseRange);
                    t.GetComponent<Rigidbody2D>().AddForce(force);
                }
            }
            Instantiate(yellowRepulsion,transform);
        }
    }

    public IEnumerator Cooldown()
    {
        canUse = false;
        cancelImage.SetActive(true);

        yield return new WaitForSeconds(cooldown);

        cancelImage.SetActive(false);
        canUse = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attractRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, repulseRange);
    }
}
