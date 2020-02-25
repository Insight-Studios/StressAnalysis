using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberClick : MiniGameBase
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
            Score += 1;
            thisNumber = Random.Range(1, 10);
            DisplayNum.text = thisNumber.ToString();
        }
        else if(number != thisNumber) {
            Score = 0;
        }
    }

    protected override void MiniGameUpdate()
    {
        
    }

    protected override void OnComplete()
    {
        Debug.Log("Number Click completed");
    }

    protected override void OnGameOver()
    {
        Debug.Log("Number Click failed");
    }
}
