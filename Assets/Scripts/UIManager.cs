using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] private Text m_ScoreText;
    [SerializeField] private GameObject m_DeathScreen;

    void Update()
    {
        m_ScoreText.text = GameManager.instance.Score.ToString();

        if(playerManager == null)
        {
            //player dead
            m_DeathScreen.SetActive(true);
        }
    }

    public void OnClickRestart()
    {
        GameManager.instance.PlayGame();
    }

    public void OnClickQuit()
    {
        GameManager.instance.QuitGame();
    }
}
