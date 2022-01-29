using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScore : MonoBehaviour
{
    // Start is called before the first frame update

    //Test test

    [Header("Score Variables")]
    [SerializeField] int currentScore;
    [SerializeField] int maxScore;

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

    void Start()
    {
        //
        phaseMat = phaseChanger.GetComponent<SpriteRenderer>();
        purpleMat = purpleSide.GetComponent<SpriteRenderer>();
        yellowMat = yellowSide.GetComponent<SpriteRenderer>();

        purpleMat.color = purple;
        yellowMat.color = yellow;

    }

    // Update is called once per frame
    void Update()
    {      
        currentScore = Mathf.Clamp(currentScore, -maxScore, maxScore);

        UpdatePhaseColor();
        UpdateRotation();
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

    //Helper functions

   //Update tomorrow
    public void AddToScore(int score) => currentScore += score;
}
