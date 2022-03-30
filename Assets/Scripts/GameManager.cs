using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int Score;
    public bool TileToggle = true;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }


    public void ToggleFallingTiles(bool tog)
    {
        TileToggle = tog;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Score = 0; //reset score
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
