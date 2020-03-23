using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{

    public static ScenesManager instance;
    private static int MAIN_MENU = 0, GAME = 1, GAMEOVER = 2;

    public TextMesh scoreText;
    public Rigidbody2D nailPhysics;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MainMenu()
    {
        GameManager.instance = null;
        scoreText.text = "Score: ";
        SceneManager.LoadScene(MAIN_MENU);
    }

    public void Game()
    {
        SceneManager.LoadScene(GAME);
    }

    public void GameOver()
    {
        scoreText.text = "Score: " + GameManager.instance.score.ToString();
        SceneManager.LoadScene(GAMEOVER);
    }
}
