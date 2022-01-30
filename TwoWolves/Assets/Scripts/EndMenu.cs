using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndMenu : MonoBehaviour
{
    public TextMeshProUGUI winnerText;
    public GameObject yellowWolf;
    public GameObject purpleWolf;
    private void Start()
    {
        Debug.Log(GameInfo.winner);
        if (GameInfo.winner != "No one")
        {
            winnerText.text = "You have fed the " + GameInfo.winner + " Wolf!";
            if (GameInfo.winner == "Yellow")
            {
                purpleWolf.SetActive(false);
            }
            else if (GameInfo.winner == "Purple")
            {
                yellowWolf.SetActive(false);
            }
        }
        else
        {
            winnerText.text = "The fights continues...";
        }
    }

    public void BackToStart()
    {
        SceneManager.LoadScene("MainScreen");
    }
}
