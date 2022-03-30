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
        m_ScoreText.text = "Score: "+ GameManager.instance.Score;

        if(playerManager == null)
        {
            //player dead
            m_DeathScreen.SetActive(true);
        }

        if(m_DeathScreen.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                GameManager.instance.PlayGame();
            }
            else if(Input.GetKeyDown(KeyCode.Q))
            {
                GameManager.instance.QuitGame();
            }
        }
    }
}
