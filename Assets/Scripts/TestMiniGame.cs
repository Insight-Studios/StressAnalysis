using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMiniGame : MiniGameBase
{
    protected override void MinigameUpdate()
    {
        // Debug.Log("Update Test");
    }

    protected override void OnComplete()
    {
        Debug.Log("MiniGame Test complete");
    }

    protected override void OnGameOver()
    {
        Debug.Log("MiniGame Test failed");
    }
}
