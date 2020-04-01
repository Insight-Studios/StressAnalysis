using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : GameManager
{

    private int currentMiniGame;
    private Dropdown dropdown;

    public Text messageText;

    public string[] messages;
    
    new void Start()
    {
        numberOfSpots = 2;
        miniGames = new MiniGameBase[numberOfSpots];
        score = 0;
        gamesCompleted = 0;
        currentMiniGame = 0;
        ChangeMinigame(0);
    }

    protected override void SpawnGame(int location)
    {
        GameObject newMiniGame;

        newMiniGame = Instantiate(miniGamePrefabs[currentMiniGame], miniGamePositions[location], transform.rotation);
        miniGames[location] = newMiniGame.GetComponent<MiniGameBase>();
        miniGames[location].Lifetime = initialMiniGameTime;
        miniGames[location].redPercentage = redPercentage;
        miniGames[location].yellowPercentage = yellowPercentage;
        miniGames[location].transform.localScale = transform.localScale;
        InputManager.instance.RegisterGameObject(miniGames[location].gameObject);
        miniGames[location].gameObject.SetActive(!IsPaused);

    }

    public override void CompletedMiniGame(bool gameOver)
    {
        InputManager.instance.UnRegisterGameObject(miniGames[0].gameObject);
        InputManager.instance.UnRegisterGameObject(miniGames[1].gameObject);
        Destroy(miniGames[0].gameObject);
        Destroy(miniGames[1].gameObject);
        SpawnGame(0);
        SpawnGame(1);
        InputManager.instance.SelectedMiniGameIndex = 0;
    }

    public void ChangeMinigame(int i)
    {
        currentMiniGame = i;
        messageText.text = messages[i];
        Restart();
    }

}
