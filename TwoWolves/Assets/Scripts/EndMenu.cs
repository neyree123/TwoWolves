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
        winnerText.text = GameInfo.winner + " wins!";
    }

    public void BackToStart()
    {
        SceneManager.LoadScene("MainScreen");
    }
}
