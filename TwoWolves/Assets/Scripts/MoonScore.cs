using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScore : MonoBehaviour
{

    [Header("Score Variables")]
    //Yellow is positive, purple is negative
    [SerializeField] int currentScore;
    [SerializeField] int maxScore;

    [HideInInspector] public int yellowScore;
    [HideInInspector] public int purpleScore;

    [Header("Color Variables")]
    public Color purple;
    public Color yellow;

    [Header("Child Object Variables")]
    public GameObject phaseChanger;
    SpriteRenderer phaseMat;
    private float initialRot = 90;

    public GameObject purpleSide;
    private SpriteRenderer purpleMat;
    public GameObject yellowSide;
    private SpriteRenderer yellowMat;

    [Header("Game Manager Variables")]
    [SerializeField] LayerMask collisionLayer;
    [SerializeField] float collisionRadius;
    Vector2 pos2D;

    void Start()
    {
        //Set the colors for the sprites
        phaseMat = phaseChanger.GetComponent<SpriteRenderer>();
        purpleMat = purpleSide.GetComponent<SpriteRenderer>();
        yellowMat = yellowSide.GetComponent<SpriteRenderer>();

        purpleMat.color = purple;
        yellowMat.color = yellow;

        Vector3 objPos = gameObject.transform.position;
        pos2D = new Vector2(objPos.x, objPos.y);
    }

    // Update is called once per frame
    void Update()
    {      
        currentScore = Mathf.Clamp(currentScore, -maxScore, maxScore);

        UpdatePhaseColor();
        UpdateRotation();

        TestForCollision();
    }

    /// <summary>
    /// Change the color of the phase changer based on the current score
    /// </summary>
    void UpdatePhaseColor()
    {
        if (currentScore >= 0)
            phaseMat.color = yellow;
        else
            phaseMat.color = purple;
    }

    /// <summary>
    /// Change the rotation of the phase changer based on the current score
    /// </summary>
    void UpdateRotation()
    {
        float rotationRatio = (((float)currentScore / (float)maxScore) * 90.0f);
        phaseChanger.transform.eulerAngles = new Vector3(0, initialRot + rotationRatio, 0);
    }

    /// <summary>
    /// Adjusts the score based on the objects within the moon
    /// </summary>
    void TestForCollision()
    {
        //Grab the object with range
        Collider2D[] objs = Physics2D.OverlapCircleAll(pos2D, collisionRadius, collisionLayer);

        foreach(Collider2D obj in objs)
        {
            //Adjust score based on type of object
            if (obj.CompareTag("White"))
            {
                UpdateYellowScore();
            }
            else if (obj.CompareTag("Black"))
            {
                UpdatePurpleScore();
            }

            //Get rid of the object
            Destroy(obj.gameObject);
        }

        


    }

    //Helper functions
    void UpdateYellowScore()
    {
        yellowScore++;
        Debug.Log("Yellow Score: " + yellowScore);
        currentScore++;

    }

    void UpdatePurpleScore()
    {
        purpleScore++;
        Debug.Log("Purple Score: " + purpleScore);
        currentScore--;
    }
}
