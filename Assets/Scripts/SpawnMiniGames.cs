using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMiniGames : MonoBehaviour {

    public MiniGameBase[] miniGames;

	void Start ()
    {
        Instantiate(miniGames[Random.Range(0, miniGames.Length)], transform.position, transform.rotation);
        Instantiate(miniGames[Random.Range(0, miniGames.Length)], new Vector3(transform.position.x + 6, transform.position.y, transform.position.z), transform.rotation);
        Instantiate(miniGames[Random.Range(0, miniGames.Length)], new Vector3(transform.position.x + 12, transform.position.y, transform.position.z), transform.rotation);
    }
}
