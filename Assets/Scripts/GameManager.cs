using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool IsPaused 
    {
        get
        {
            return isPaused;
        }

        set
        {
            foreach (MiniGameBase miniGame in miniGames)
            {
                miniGame.gameObject.SetActive(!value);
            }

            if (value)
            {
                InputManager.instance.selectedMiniGameIcon.SetActive(false);
            }
            else
            {
                InputManager.instance.selectedMiniGameIcon.SetActive(InputManager.instance.SelectedMiniGame!=null);
            }

            pauseMenu.SetActive(value);
            isPaused = value;
        }
    }
    private bool isPaused = false;
    public static GameManager instance = null;
    public int numberOfSpots = 3;
    public float startSpawnDelay;
    public GameObject[] miniGamePrefabs;
    public TextMesh scoreText;
    public GameObject pauseMenu;

    private int score;

    [HideInInspector]
    public MiniGameBase[] miniGames;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
            throw new System.Exception("Singleton instantiated twice.");
        }
    }

    void Start()
    {
        miniGames = new MiniGameBase[numberOfSpots];

        for(int i = 0; i < numberOfSpots; i++) {
            SpawnGame(i);
        }

        score = 0;
        scoreText.text = "Score: " + score;
        IsPaused = false;
    }

    void SpawnGame(int location) //0 left, 1 middle, 2 right
    {
        GameObject newMiniGame;

        switch (location)
        {
            case 0:
                newMiniGame = Instantiate(miniGamePrefabs[Random.Range(0, miniGamePrefabs.Length)], new Vector3(transform.position.x - 6, transform.position.y, transform.position.z), transform.rotation);
                break;

            case 1:
                newMiniGame = Instantiate(miniGamePrefabs[Random.Range(0, miniGamePrefabs.Length)], new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                break;

            case 2:
                newMiniGame = Instantiate(miniGamePrefabs[Random.Range(0, miniGamePrefabs.Length)], new Vector3(transform.position.x + 6, transform.position.y, transform.position.z), transform.rotation);
                break;

            default:
                Debug.LogError("Not Valid Spawning Location");
                newMiniGame = null;
                break;

        }
        miniGames[location] = newMiniGame.GetComponent<MiniGameBase>();
        InputManager.instance.RegisterGameObject(miniGames[location].gameObject);
    }

    public void CompletedMiniGame(bool gameOver)
    {
        if (gameOver)
        {
            GameOver();
            return;
        }
        else
        {
            score++;
            scoreText.text = "Score: " + score;
        }

        for (int i = 0; i < numberOfSpots; i++) {
            if (!miniGames[i].enabled) {
                InputManager.instance.UnRegisterGameObject(miniGames[i].gameObject);
                Destroy(miniGames[i].gameObject);
                SpawnGame(i);
                if (i == InputManager.instance.SelectedMiniGameIndex)
                {
                    InputManager.instance.SelectedMiniGameIndex = i;
                }
            }
        }       
    }

    public void GameOver()
    {
        foreach (MiniGameBase miniGame in miniGames)
        {
            InputManager.instance.UnRegisterGameObject(miniGame.gameObject);
            InputManager.instance.selectedMiniGameIcon.SetActive(false);
            Destroy(miniGame.gameObject);
        }

        SceneManager.LoadScene(2);
        
        DontDestroyOnLoad(scoreText);
        scoreText.GetComponent<TextMesh>().color = Color.white;
        scoreText.GetComponent<TextMesh>().fontSize = 100;
        scoreText.GetComponent<TextMesh>().text += "!"; 
    }

    public void UnPause()
    {
        IsPaused = false;
    }

    public void Pause()
    {
        IsPaused = true;
    }

    public void Restart()
    {
        foreach (MiniGameBase miniGame in miniGames)
        {
            InputManager.instance.UnRegisterGameObject(miniGame.gameObject);
            Destroy(miniGame.gameObject);
        }

        Start();
    }

    public void Exit()
    {
        GameManager.instance = null;
        SceneManager.LoadScene(0);
    }
}
