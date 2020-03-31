using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{

    public static ScenesManager instance;
    private static int MAIN_MENU = 0, GAME = 1, GAMEOVER = 2;
    private int highscore = 0;

    public TextMesh scoreText;
    public Text highscoreText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Load();

        Text tempHighscoreText = GameObject.Find("Canvas/HighscoreText").GetComponent<Text>();
        tempHighscoreText.text = ">> " + highscore;
    }
 
    public void MainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU);
        GameManager.instance = null;
        ShowHighscore();
    }

    public void Game()
    {
        SceneManager.LoadScene(GAME);
    }

    public void GameOver()
    {
        scoreText.text = "Score: " + GameManager.instance.score.ToString();
        SceneManager.LoadScene(GAMEOVER);
        highscore = Math.Max(GameManager.instance.score, highscore);
        Save();
    }

    public void Exit()
    {
        scoreText.text = "Score: ";
        highscoreText.text = ">> ";

        Application.Quit();
    }

    public void ShowHighscore()
    {
        Load();

        if (highscoreText != null)
        {
            highscoreText.gameObject.SetActive(true);
            highscoreText.GetComponent<Text>().text = ">> " + highscore;
        }
    }

    private void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/stats.dat", FileMode.OpenOrCreate);

        Stats stats = new Stats(highscore);

        bf.Serialize(file, stats);
        file.Close();
    }

    private void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/stats.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/stats.dat", FileMode.Open);
            Stats stats = (Stats) bf.Deserialize(file);
            file.Close();

            highscore = stats.getHighscore();
        }
    }
}

[Serializable]
class Stats
{
    private int highscore;

    public Stats(int highscore)
    {
        this.highscore = highscore;
    }
    
    public int getHighscore()
    {
        return highscore;
    }
}
