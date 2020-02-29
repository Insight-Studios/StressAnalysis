using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberClickMiniGame : MiniGameBase
{
    public int thisNumber = -1; 

    public TextMesh displayNum;

    public override void MiniGameStart()
    {
        if (thisNumber == -1) {
            thisNumber = Random.Range(1, 10);
        }

        displayNum.text = thisNumber + "";
    }

    public override void ReceiveInput(int number)
    {
        if(number == thisNumber && enabled) {
            thisNumber = Random.Range(1, 10);

            if (displayNum.text == thisNumber + ""){
                thisNumber = Mathf.Abs(thisNumber - 4) + 1;
            }

            displayNum.text = thisNumber.ToString();
            Score ++;
        }else if(number != thisNumber) {
            Score = 0;
        }
    }

    protected override void OnComplete()
    {
        displayNum.text = "#";
        Debug.Log("Number Click completed");
    }

    protected override void OnGameOver()
    {
        Debug.Log("Number Click failed");
    }
}
