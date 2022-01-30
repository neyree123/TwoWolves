using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndMenu : MonoBehaviour
{
    public TextMeshProUGUI winnerText;
    private void Start()
    {
        if (GameInfo.winner != "No one")
        {
            winnerText.text = "You have fed the " + GameInfo.winner + " Wolf!";
        }
        else
        {
            winnerText.text = "You have fed neither wolf.";
        }
    }

    public void BackToStart()
    {
        SceneManager.LoadScene("MainScreen");
    }
}
