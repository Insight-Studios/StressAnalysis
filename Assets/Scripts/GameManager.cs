using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int numberOfSpots = 3;
    public GameObject[] miniGamePrefabs;
    public TextMesh scoreText;

    int OverallScore = 0;

    MiniGameBase[] miniGames;

    void Start()
    {
        if(instance==null)
            instance = this;
        else
            throw new System.Exception("Singleton instantiated twice.");

        miniGames = new MiniGameBase[numberOfSpots];

        for(int i = 0; i < numberOfSpots; i++) {
            SpawnGame(i);
        }
        InputManager.instance.ReloadOnClicks();

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
        InputManager.instance.onClicks.AddRange(miniGames[location].GetComponentsInChildren<OnClickEvent>());
    }

    public void CompletedMiniGame(bool gameOver)
    {
        if (gameOver)
        {
            Debug.LogWarning("GameOver");
            return;
        }
        else
        {
            OverallScore++;
            scoreText.text = "Games Completed: " + OverallScore;
        }

        for (int i = 0; i < numberOfSpots; i++) {
            if (!miniGames[i].enabled) {
                InputManager.instance.onClicks.RemoveAll(x => new List<OnClickEvent>(miniGames[i].GetComponentsInChildren<OnClickEvent>()).Contains(x));

                Destroy(miniGames[i].gameObject);
                SpawnGame(i);
            }
        }

        //Timer to next miniGame spawn?
       
    }
}
