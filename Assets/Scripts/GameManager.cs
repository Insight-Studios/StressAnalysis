using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int numberOfSpots = 3;
    public float startSpawnDelay;
    public GameObject[] miniGamePrefabs;
    public TextMesh scoreText;

    int OverallScore = 0;

    MiniGameBase[] miniGames;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else 
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

        scoreText.text = "Games Completed: " + OverallScore;
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
            OverallScore++;
            scoreText.text = "Games Completed: " + OverallScore;
        }

        for (int i = 0; i < numberOfSpots; i++) {
            if (!miniGames[i].enabled) {
                InputManager.instance.UnRegisterGameObject(miniGames[i].gameObject);

                Destroy(miniGames[i].gameObject);
                SpawnGame(i);
            }
        }

        //Timer to next miniGame spawn?
       
    }

    public void GameOver()
    {
        foreach(MiniGameBase miniGame in miniGames)
        {
            InputManager.instance.UnRegisterGameObject(miniGame.gameObject);
            Destroy(miniGame.gameObject);
        }

        SceneManager.LoadScene(1);
    }
}
