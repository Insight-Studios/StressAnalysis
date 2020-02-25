using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] miniGames;
    bool[] spotFilled;

    void Start()
    {
        spotFilled = new bool[3];

        SpawnGame(0);
        SpawnGame(1);
        SpawnGame(2);
    }

    void SpawnGame(int location) //0 left, 1 middle, 2 right
    {
        switch (location)
        {
            case 0:
                Instantiate(miniGames[Random.Range(0, miniGames.Length)], new Vector3(transform.position.x - 6, transform.position.y, transform.position.z), transform.rotation);
                break;

            case 1:
                Instantiate(miniGames[Random.Range(0, miniGames.Length)], new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                break;

            case 2:
                Instantiate(miniGames[Random.Range(0, miniGames.Length)], new Vector3(transform.position.x + 6, transform.position.y, transform.position.z), transform.rotation);
                break;

            default:
                Debug.LogError("Not Valid Spawning Location");
                break;

        }
        spotFilled[location] = true;
    }

    public void CompletedMiniGame(int location)
    {
        spotFilled[location] = false;

        //Timer to next miniGame spawn?

        SpawnGame(location);
    }
}
