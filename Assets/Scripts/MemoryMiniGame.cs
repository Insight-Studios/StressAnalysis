﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryMiniGame : MiniGameBase
{
    public float blinkTime = 1;
    public TextMesh[] numbers;

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

    public override void MiniGameStart()
    {
        sequence = new int[requiredScore];

        originalColors = new Color[numbers.Length];
        values = new int[numbers.Length];

        for(int i = 0; i < numbers.Length; i++) {
            originalColors[i] = numbers[i].color;
            values[i] = int.Parse(numbers[i].text);
        }
        CreateSequence();
        CurrentDisplaySequence = DisplaySequence();
    }

    public override void ReceiveInput(int number)
    {
        CurrentDisplaySequence = null;

        switch (number) {
            case -1:
                CreateSequence();
                CurrentDisplaySequence = DisplaySequence();
                break;
            default:
                if (number == values[sequence[pos]]) {
                    Score++;
                    pos++;
                } else {
                    Score = -1;
                }
            break;
        }

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
    }

    IEnumerator DisplaySequence()
    {
        for(int i = 0; i < sequence.Length; i++) {
            int num = sequence[i];
            Color original = numbers[num].color;
            numbers[num].color = Color.white;

            yield return new WaitForSeconds(blinkTime);

            numbers[num].color = original;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
