using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            
            foreach (MiniGameBase miniGame in miniGames)
            {
                if (miniGame != null) miniGame.gameObject.SetActive(!value);
            }
        }
    }
    private bool isPaused = false;

    public static GameManager instance = null;
    
    public Vector2[] miniGamePositions;
    [SerializeField] private float initialMiniGameTime = 30;
    [SerializeField] private float nextTimePercent;
    [SerializeField] private float startSpawnDelay;
    [SerializeField] private GameObject[] miniGamePrefabs;
    [SerializeField] private TextMesh scoreText;
    [SerializeField] private GameObject pauseMenu;

    private int numberOfSpots;
    private int gamesCompleted;

    [HideInInspector] public MiniGameBase[] miniGames;
    [HideInInspector] public int score;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            instance.Start();
            Destroy(gameObject);
        }
    }

    public void Start()
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
            yield return new WaitWhile(() => {return IsPaused;});
            SpawnGame(i);
            yield return new WaitForSeconds(startSpawnDelay);
        }
    }
    void SpawnGame(int location) //0 left, 1 middle, 2 right
    {
        GameObject newMiniGame;

        newMiniGame = Instantiate(miniGamePrefabs[Random.Range(0, miniGamePrefabs.Length)], (Vector3) miniGamePositions[location], transform.rotation);
        miniGames[location] = newMiniGame.GetComponent<MiniGameBase>();
        miniGames[location].Lifetime = initialMiniGameTime * Mathf.Pow(nextTimePercent, gamesCompleted);
        InputManager.instance.RegisterGameObject(miniGames[location].gameObject);
        miniGames[location].gameObject.SetActive(!IsPaused);
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
                    score += Mathf.RoundToInt(100*(miniGames[i].RemainingTime/miniGames[i].Lifetime)*gamesCompleted);
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

        ScenesManager.instance.GameOver();
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
}
