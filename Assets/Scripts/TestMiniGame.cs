using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMiniGame : MiniGameBase
{
    public override void MiniGameStart()
    {
        Debug.Log("Initialized TestMiniGame");
    }

    protected override void MiniGameUpdate()
    {
        Debug.Log("Updated TestMiniGame");
    }

    protected override void OnComplete()
    {
        Debug.Log("MiniGame Test complete");
    }

    protected override void OnGameOver()
    {
        Debug.Log("MiniGame Test failed");
    }

    public override void ReceiveInput(int number)
    {
        Debug.Log("Received #:" +  number);
    }
}
