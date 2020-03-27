using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunchAttackMiniGame : MiniGameBase
{
    public float spawnDelayProp;
    public float bugSpawnRadius;
    public float bugEatRadius;
    public float bugSpeed;

    public GameObject spray;
    public GameObject sprayDiagonal;
    public GameObject mist;
    public GameObject mistDiagonal;
    public GameObject bug;

    int sprayPos; //pos based on unit circle 45 degree incriments
    GameObject currentBug;
    int currentBugPos = -1;
    float timeSinceSpawn;

    protected override void MiniGameStart()
    {
        sprayPos = -1;

        spray.SetActive(false);
        mist.SetActive(false);
        sprayDiagonal.SetActive(false);    
        mistDiagonal.SetActive(false);
    }

    protected override void MiniGameUpdate()
    {
        if(InputManager.instance.SelectedMiniGame != null && InputManager.instance.SelectedMiniGame.gameObject == gameObject)
        {
            UpdateSprayPos();

            if ((Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Space)) && sprayPos != -1)
            {
                fireWeapon();
            }
            else
            {
                mistDiagonal.SetActive(false);
                mist.SetActive(false);
            }
        }
        else
        {
            mistDiagonal.SetActive(false);
            mist.SetActive(false);
        }

        if (currentBug != null && currentBug.transform.localPosition.x * currentBug.transform.localPosition.x + currentBug.transform.localPosition.y * currentBug.transform.localPosition.y <= bugEatRadius * bugEatRadius)
        {
            Score = 0;
            timeSinceSpawn = 0;
            Destroy(currentBug);
            currentBugPos = -1;
        }

        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn >= spawnDelayProp * RemainingTime && currentBug == null)
        {
            SpawnBug();
            timeSinceSpawn = 0;
        }
    }

    void SpawnBug()
    {
        currentBugPos = Random.Range(0, 8);

        if(currentBug != null)
        {
            Debug.LogError("Multiple bugs being created");
        }

        currentBug = Instantiate(bug, gameObject.transform);
        currentBug.transform.localPosition =  new Vector3(Mathf.Cos(currentBugPos * Mathf.PI / 4), Mathf.Sin(currentBugPos * Mathf.PI / 4), 0) * bugSpawnRadius;
        currentBug.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90 + currentBugPos * 45));

        currentBug.GetComponent<Rigidbody2D>().velocity = new Vector2(-Mathf.Cos(currentBugPos * Mathf.PI / 4), -Mathf.Sin(currentBugPos * Mathf.PI / 4)) * bugSpeed;
    }

    void UpdateSprayPos()
    {
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            sprayPos = 1;
            spray.SetActive(false);
            mist.SetActive(false);
            sprayDiagonal.SetActive(true);
            mistDiagonal.SetActive(false);

            sprayDiagonal.transform.rotation = Quaternion.Euler(0, 0, -90);
            mistDiagonal.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            sprayPos = 7;
            spray.SetActive(false);
            mist.SetActive(false);
            sprayDiagonal.SetActive(true);
            mistDiagonal.SetActive(false);

            sprayDiagonal.transform.rotation = Quaternion.Euler(0, 0, 180);
            mistDiagonal.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            sprayPos = 5;
            spray.SetActive(false);
            mist.SetActive(false);
            sprayDiagonal.SetActive(true);
            mistDiagonal.SetActive(false);

            sprayDiagonal.transform.rotation = Quaternion.Euler(0, 0, 90);
            mistDiagonal.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            sprayPos = 3;
            spray.SetActive(false);
            mist.SetActive(false);
            sprayDiagonal.SetActive(true);
            mistDiagonal.SetActive(false);

            sprayDiagonal.transform.rotation = Quaternion.Euler(0, 0, 0);
            mistDiagonal.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            sprayPos = 2;
            spray.SetActive(true);
            mist.SetActive(false);
            sprayDiagonal.SetActive(false);
            mistDiagonal.SetActive(false);

            spray.transform.rotation = Quaternion.Euler(0, 0, -90);
            mist.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            sprayPos = 0;
            spray.SetActive(true);
            mist.SetActive(false);
            sprayDiagonal.SetActive(false);
            mistDiagonal.SetActive(false);

            spray.transform.rotation = Quaternion.Euler(0, 0, 180);
            mist.transform.rotation = Quaternion.Euler(0, 0, 180);
        }        
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            sprayPos = 6;
            spray.SetActive(true);
            mist.SetActive(false);
            sprayDiagonal.SetActive(false);
            mistDiagonal.SetActive(false);

            spray.transform.rotation = Quaternion.Euler(0, 0, 90);
            mist.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            sprayPos = 4;
            spray.SetActive(true);
            mist.SetActive(false);
            sprayDiagonal.SetActive(false);
            mistDiagonal.SetActive(false);

            spray.transform.rotation = Quaternion.Euler(0, 0, 0);
            mist.transform.rotation = Quaternion.Euler(0, 0, 0);
        }        
        else
        {
            return;
        }
    }

    void fireWeapon()
    {
        if(sprayPos % 2 == 1)
        {
            mistDiagonal.SetActive(true);
        }
        else
        {
            mist.SetActive(true);
        }

        if(sprayPos == currentBugPos)
        {
            Score++;
            Destroy(currentBug);
            currentBugPos = -1;
        }
    }
}
