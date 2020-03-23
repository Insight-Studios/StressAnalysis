using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMiniGame : MiniGameBase
{
    [SerializeField]
    private SpriteRenderer mazeRenderer;
    [SerializeField]
    private SpriteRenderer[] trailRenderers;

    private int current;
    private int[] turtle;
    private int[] target;
    private bool[,] trailGrid;

    protected override void MiniGameStart()
    {
        current = Random.Range(0, grids.GetLength(0));

        trailGrid = new bool[,]
        {
            {true, false, true, false, true},
            {false, false, false, false, false},
            {true, false, true, false, true},
            {false, false, false, false, false},
            {true, false, true, false, true},
        };

        mazeRenderer.sprite = mazes[current];
        turtle = new int[] {Mathf.RoundToInt(spawnLocations[current].x), Mathf.RoundToInt(spawnLocations[current].y)};
        target = new int[] {Mathf.RoundToInt(finishLocations[current].x), Mathf.RoundToInt(finishLocations[current].y)};

        UpdateTrailGrid();
    }

    public override void SendInput(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.DownArrow:
                if (turtle[1] + 2 < 5 && grids[current, turtle[1] + 1, turtle[0]])
                {
                    trailGrid[turtle[1] + 1, turtle[0]] = !trailGrid[turtle[1] + 1, turtle[0]];
                    UpdateTrailGrid();
                    turtle[1] += 2;
                }
                break;
            case KeyCode.UpArrow:
                if (turtle[1] - 2 >= 0 && grids[current, turtle[1] - 1, turtle[0]])
                {
                    trailGrid[turtle[1] - 1, turtle[0]] = !trailGrid[turtle[1] - 1, turtle[0]];
                    UpdateTrailGrid();
                    turtle[1] -= 2;
                }
                break;
            case KeyCode.RightArrow:
                if (turtle[0] + 2 < 5 && grids[current, turtle[1], turtle[0] + 1])
                {
                    trailGrid[turtle[1], turtle[0] + 1] = !trailGrid[turtle[1], turtle[0] + 1];
                    UpdateTrailGrid();
                    turtle[0] += 2;
                }
                break;
            case KeyCode.LeftArrow:
                if (turtle[0] - 2 >= 0 && grids[current, turtle[1], turtle[0] - 1])
                {
                    trailGrid[turtle[1], turtle[0] - 1] = !trailGrid[turtle[1], turtle[0] - 1];
                    UpdateTrailGrid();
                    turtle[0] -= 2;
                }
                break;
        }
        print (trailGrid.ToString());
    }

    protected override void MiniGameUpdate()
    {  
        if (turtle[0] == target[0] && turtle[1] == target[1])
        {
            Score++;
            int lastCurrent = current;
            for (int i = 0; current == lastCurrent && i < 1000; i++)
            {
                MiniGameStart();
            }
        }
    }

    private void UpdateTrailGrid()
    {
        int intersection = 0;
        for (int i = 1; i < trailGrid.Length; i+=2)
        {
            trailRenderers[intersection].gameObject.SetActive(trailGrid[Mathf.FloorToInt(i/trailGrid.GetLength(0)), i % trailGrid.GetLength(0)]);
            intersection++;
        }
    }
    
    [SerializeField]
    private Sprite[] mazes;
    [SerializeField]
    private Vector2[] spawnLocations;
    [SerializeField]
    private Vector2[] finishLocations;
        
    private bool[,,] grids = new bool[,,] 
    {
        { //#1
            {true, true, true, false, true},
            {true, false, true, false, true},
            {true, false, true, false, true},
            {true, false, true, false, true},
            {true, false, true, true, true},
        },
        { //#2
            {true, true, true, false, true},
            {false, false, true, false, true},
            {true, true, true, false, true},
            {true, false, true, false, true},
            {true, false, true, true, true},
        },
        { //#3
            {true, true, true, true, true},
            {true, false, false, false, false},
            {true, true, true, true, true},
            {false, false, true, false, true},
            {true, true, true, false, true},
        },
        { //#4
            {true, false, true, false, true},
            {true, false, true, false, true},
            {true, true, true, false, true},
            {true, false, false, false, true},
            {true, true, true, true, true},
        },
        { //#5
            {true, true, true, false, true},
            {false, false, true, false, true},
            {true, true, true, false, true},
            {true, false, false, false, true},
            {true, true, true, true, true},
        },
        { //#6
            {true, false, true, true, true},
            {true, false, true, false, false},
            {true, false, true, true, true},
            {true, false, false, false, true},
            {true, true, true, true, true},
        },
        { //#7
            {true, false, true, false, true},
            {true, false, true, false, true},
            {true, false, true, true, true},
            {true, false, false, false, true},
            {true, true, true, true, true},
        },
        { //#8
            {true, false, true, false, true},
            {true, false, true, false, true},
            {true, false, true, false, true},
            {true, false, true, false, true},
            {true, true, true, true, true},
        },
        { //#9
            {true, false, true, false, true},
            {true, false, true, false, true},
            {true, false, true, true, true},
            {true, false, true, false, false},
            {true, true, true, false, true},
        },
        { //#10
            {true, false, true, true, true},
            {true, false, true, false, false},
            {true, false, true, true, true},
            {true, false, true, false, false},
            {true, true, true, false, true},
        },
        { //#11
            {true, true, true, false, true},
            {false, false, true, false, true},
            {true, true, true, false, true},
            {false, false, true, false, true},
            {true, false, true, true, true},
        },
        { //#12
            {true, false, true, false, true},
            {true, false, true, false, true},
            {true, true, true, false, true},
            {false, false, true, false, true},
            {true, false, true, true, true},
        },
        { //#13
            {true, false, true, false, true},
            {true, false, true, false, true},
            {true, true, true, false, true},
            {false, false, true, false, true},
            {true, false, true, true, true},
        },
        { //#14
            {true, true, true, true, true},
            {false, false, true, false, false},
            {true, false, true, false, true},
            {true, false, true, false, true},
            {true, true, true, false, true}
        },
        { //#15
            {true, false, true, true, true},
            {true, false, false, false, true},
            {true, false, true, true, true},
            {true, false, false, false, true},
            {true, true, true, true, true}
        },
        { //#16
            {true, true, true, true, true},
            {false, false, true, false, true},
            {true, false, true, false, true},
            {true, false, true, false, true},
            {true, true, true, false, true}
        },
        { //#17
            {true, true, true, true, true},
            {false, false, true, false, true},
            {true, false, true, false, true},
            {true, false, true, false, false},
            {true, true, true, true, true}
        },
    };

}
