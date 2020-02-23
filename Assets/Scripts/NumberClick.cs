using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberClick : MonoBehaviour {

    public int amountOfNums = 10;
    public TextMesh displayNumText;
    public TextMesh score;
    public GameObject[] nums;

    int[] numList;
    int currentNum;

	void Start ()
    {
        numList = new int[amountOfNums];

        currentNum = 0;

        for (int i = 0; i < amountOfNums; i++)
        {
            numList[i] = Random.Range(1, 10);
        }

        displayNumText.text = numList[0] + "";
        score.text = "Score: " + currentNum;
    }

    void Update()
    {
        NumberPressed();
    }

    public void CheckNum (int numPressed)
    {
        if (numPressed == numList[currentNum])
        {
            currentNum++;

            score.text = "Score: " + currentNum;

            if (currentNum == amountOfNums)
            {
                Debug.Log("NumberClick Completed!");
                Destroy(gameObject);
            }

            else { displayNumText.text = numList[currentNum] + ""; }
        }

        else
        {
            Debug.Log("Wrong num! You lost!");
            Destroy(gameObject);
        }
    }

    void NumberPressed()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            CheckNum(1);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            CheckNum(2);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            CheckNum(3);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            CheckNum(4);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            CheckNum(5);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            CheckNum(6);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            CheckNum(7);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            CheckNum(8);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            CheckNum(9);
        }
    }
}
