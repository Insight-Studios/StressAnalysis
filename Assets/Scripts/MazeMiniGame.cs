using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMiniGame : MiniGameBase
{
    
    public bool[,] grid;

    public bool[] g;
    public Vector2 spawnLocation;
    public Vector2 finishLocation;

    private int[] turtle;
    private int[] target;

    public override void MiniGameStart()
    {
        grid = new bool[,] {
            {true, true, true, false, true},
            {true, false, true, false, true},
            {true, false, true, false, true},
            {true, false, true, false, true},
            {true, false, true, true, true}
        };

        turtle = new int[] {Mathf.RoundToInt(spawnLocation.x), Mathf.RoundToInt(spawnLocation.y)};
        target = new int[] {Mathf.RoundToInt(finishLocation.x), Mathf.RoundToInt(finishLocation.y)};
    }

    public override void SendInput(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.DownArrow:
                if (turtle[1] + 2 < 5 && grid[turtle[1] + 1, turtle[0]])
                {
                    turtle[1] += 2;
                }
                break;
            case KeyCode.UpArrow:
                if (turtle[1] - 2 >= 0 && grid[turtle[1] - 1, turtle[0]])
                {
                    turtle[1] -= 2;
                }
                break;
            case KeyCode.RightArrow:
                if (turtle[0] + 2 < 5 && grid[turtle[1], turtle[0] + 1])
                {
                    turtle[0] += 2;
                }
                break;
            case KeyCode.LeftArrow:
                if (turtle[0] - 2 >= 0 && grid[turtle[1], turtle[0] - 1])
                {
                    turtle[0] -= 2;
                }
                break;
        }
    }

    protected override void MiniGameUpdate()
    {
        print(turtle[0] + "," + turtle[1] + "<- turtle target ->" + target[0] + "," + target[1]);
        if (turtle[0] == target[0] && turtle[1] == target[1])
        {
            print("hello world");
            Score++;
            print(Score);
        }
    }
}
