using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    public GameObject[] nums;
    public TextMesh resetSequence;

    public int lengthOfSequence = 5;
    public float blinkSpeed = 1;

    int numInSequence = 0;
    int[] sequence;

    bool displayedSequence = true;

    void Start()
    {
        sequence = new int[lengthOfSequence];
        ChooseSequence(lengthOfSequence);
        displayedSequence = false;

        StartCoroutine("DisplaySequence");
    }

    void Update()
    {
        NumberPressed();
    }

    void ChooseSequence(int length)
    {
        for (int i = 0; i < length; i++)
        {
            sequence[i] = Random.Range(1, 5);
        }
    }

    IEnumerator DisplaySequence()
    {
        displayedSequence = false;

        yield return new WaitForSeconds(blinkSpeed);

        for (int i = 0; i < lengthOfSequence; i++)
        {
            (nums[sequence[numInSequence] - 1]).GetComponent<TextMesh>().color = new Vector4(
                (nums[sequence[numInSequence] - 1]).GetComponent<TextMesh>().color.r, 
                (nums[sequence[numInSequence] - 1]).GetComponent<TextMesh>().color.g, 
                (nums[sequence[numInSequence] - 1]).GetComponent<TextMesh>().color.b, 
                0.5f);

            yield return new WaitForSeconds(blinkSpeed);

            (nums[sequence[numInSequence] - 1]).GetComponent<TextMesh>().color = new Vector4(
                (nums[sequence[numInSequence] - 1]).GetComponent<TextMesh>().color.r,
                (nums[sequence[numInSequence] - 1]).GetComponent<TextMesh>().color.g,
                (nums[sequence[numInSequence] - 1]).GetComponent<TextMesh>().color.b,
                1);

            yield return new WaitForSeconds(blinkSpeed);
            numInSequence++;
        }

        numInSequence = 0;
        displayedSequence = true;
    }

    void NumberPressed()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            StartCoroutine(CheckSequence(1));
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            StartCoroutine(CheckSequence(2));
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            StartCoroutine(CheckSequence(3));
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            StartCoroutine(CheckSequence(4));
        }
    }

    public void MouseInput(int num)
    {
        StartCoroutine(CheckSequence(num));
    }

    public void RestartSequence()
    {
        if (displayedSequence)
        {
            StartCoroutine(DisplaySequence());
        }
    }

    IEnumerator CheckSequence(int num)
    {
        if (numInSequence != 0)
        {
            (nums[sequence[numInSequence - 1] - 1]).GetComponent<TextMesh>().color = new Vector4(
                (nums[sequence[numInSequence - 1] - 1]).GetComponent<TextMesh>().color.r,
                (nums[sequence[numInSequence - 1] - 1]).GetComponent<TextMesh>().color.g,
                (nums[sequence[numInSequence - 1] - 1]).GetComponent<TextMesh>().color.b,
                1f);
        }

        if (displayedSequence)
        {
            (nums[sequence[numInSequence] - 1]).GetComponent<TextMesh>().color = new Vector4(
                (nums[sequence[numInSequence] - 1]).GetComponent<TextMesh>().color.r,
                (nums[sequence[numInSequence] - 1]).GetComponent<TextMesh>().color.g,
                (nums[sequence[numInSequence] - 1]).GetComponent<TextMesh>().color.b,
                0.5f);

            if (num == sequence[numInSequence])
            {
                numInSequence++;
            }
            else
            {
                Debug.Log("Memory game died");
                Destroy(gameObject);
            }

            if (numInSequence == lengthOfSequence)
            {
                Debug.Log("Memory game won");
                Destroy(gameObject);
            }

            yield return new WaitForSeconds(blinkSpeed);

            (nums[sequence[numInSequence - 1] - 1]).GetComponent<TextMesh>().color = new Vector4(
                (nums[sequence[numInSequence - 1] - 1]).GetComponent<TextMesh>().color.r,
                (nums[sequence[numInSequence - 1] - 1]).GetComponent<TextMesh>().color.g,
                (nums[sequence[numInSequence - 1] - 1]).GetComponent<TextMesh>().color.b,
                1f);
        }
    }
}
