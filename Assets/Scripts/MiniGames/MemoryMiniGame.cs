using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryMiniGame : MiniGameBase
{
    [SerializeField] private float blinkProp = 0.025f;
    [SerializeField] private float minBlinkTime = 0.1f;
    [SerializeField] private float waitProp = 0.005f;
    [SerializeField] private TextMesh[] numbers;

    private int pos = 0;
    private int[] sequence;
    private Color[] originalColors;
    private int[] values;

    private IEnumerator currDisplaySequence;
    private IEnumerator CurrentDisplaySequence {
        get
        {
            return currDisplaySequence;
        }
        set
        {
            if(currDisplaySequence != null)
                StopCoroutine(currDisplaySequence);
            currDisplaySequence = value;
            
            for(int i = 0; i < numbers.Length; i++) {
                numbers[i].color = originalColors[i];
            }

            if(currDisplaySequence != null)
                StartCoroutine(currDisplaySequence);
        }
    }

    protected override void MiniGameStart()
    {
        sequence = new int[requiredScore];

        originalColors = new Color[numbers.Length];
        values = new int[numbers.Length];

        for(int i = 0; i < numbers.Length; i++) {
            originalColors[i] = numbers[i].color;
            values[i] = int.Parse(numbers[i].text);
        }
        CreateSequence();
    }

    protected override void SendNumber(int number)
    {
        CurrentDisplaySequence = null;

        if (number == values[sequence[pos]]) {
            Score++;
            pos++;
        } else {
            Score = 0;
            CreateSequence();
        }
    }

    public override void SendInput(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.KeypadEnter:
            case KeyCode.Return:
                CreateSequence();
                break;
        }

        base.SendInput(key);
    }

    protected override void OnComplete()
    {
        CurrentDisplaySequence = null;
        Debug.Log("Memory completed");
    }

    protected override void OnGameOver()
    {
        CurrentDisplaySequence = null;
        Debug.Log("Memory failed");
    }

    void CreateSequence()
    {
        for(int i = 0; i < requiredScore; i++) {
            sequence[i] = Random.Range(0, numbers.Length);
        }
        Score = 0;
        pos = 0;
        CurrentDisplaySequence = DisplaySequence();
    }

    IEnumerator DisplaySequence()
    {
        for(int i = 0; i < sequence.Length; i++) {
            int num = sequence[i];
            Color original = numbers[num].color;
            numbers[num].color = Color.white;

            yield return new WaitForSeconds(Mathf.Max(blinkProp*RemainingTime, minBlinkTime));

            numbers[num].color = original;
            yield return new WaitForSeconds(waitProp*RemainingTime);
        }
    }
}
