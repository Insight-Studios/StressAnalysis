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
    
    public Vector2[] miniGamePositions;
    public float initialMiniGameTime = 30;
    public float nextTimePercent;
    public float startSpawnDelay;
    [HideInInspector]

    public GameObject[] miniGamePrefabs;
    public TextMesh scoreText;
    public GameObject pauseMenu;

    private int numberOfSpots;
    private int gamesCompleted;
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
        numberOfSpots = miniGamePositions.Length;
        miniGames = new MiniGameBase[numberOfSpots];
        
        StartCoroutine(SpawnAllGames());

        score = 0;
        pauseMenu.SetActive(false);
        isPaused = false;
        gamesCompleted = 0;
        scoreText.text = "Score: " + gamesCompleted;
    }

    IEnumerator SpawnAllGames()
    {
        for (int i = 0; i < numberOfSpots; i++)
        {
            SpawnGame(i);
            yield return new WaitForSeconds(startSpawnDelay);
        }
    }

    void SpawnGame(int location) //0 left, 1 middle, 2 right
    {
        GameObject newMiniGame;

        newMiniGame = Instantiate(miniGamePrefabs[Random.Range(0, miniGamePrefabs.Length)], (Vector3) miniGamePositions[location], transform.rotation);
        miniGames[location] = newMiniGame.GetComponent<MiniGameBase>();
        miniGames[location].RemainingTime = initialMiniGameTime * Mathf.Pow(nextTimePercent, gamesCompleted);
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
            for (int i = 0; i < numberOfSpots; i++) {
                if (!miniGames[i].enabled) {
                    gamesCompleted++;
                    score += Mathf.RoundToInt(100 / Mathf.Pow(nextTimePercent, gamesCompleted) + miniGames[i].RemainingTime);
                    scoreText.text = "Score: " + score;
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
