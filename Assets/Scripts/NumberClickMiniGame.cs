using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberClickMiniGame : MiniGameBase
{
    public int thisNumber = -1; 

    public TextMesh DisplayNum;

    public override void MiniGameStart()
    {
        if (thisNumber == -1) {
            thisNumber = Random.Range(1, 10);
        }

        DisplayNum.text = thisNumber.ToString();
    }

    public override void ReceiveInput(int number)
    {
        if(number == thisNumber && enabled) {
            thisNumber = Random.Range(1, 10);
            DisplayNum.text = thisNumber.ToString();
            Score ++;
        }
        else if(number != thisNumber) {
            Score = -1;
        }
    }

    protected override void OnComplete()
    {
        DisplayNum.text = "#";
        Debug.Log("Number Click completed");
        GameObject.Find("Managers").GetComponent<GameManager>().CompletedMiniGame(Mathf.RoundToInt(transform.position.x / 6 + 1));
    }

    protected override void OnGameOver()
    {
        Debug.Log("Number Click failed");
    }
}
