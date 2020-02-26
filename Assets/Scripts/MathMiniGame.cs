using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathMiniGame : MiniGameBase
{

    public TextMesh upperNumberText, lowerNumberText, symbolText, solutionText;

    private Operation currentOperation;
    private TextMesh[] numberTexts;
    private int[] numbers;
    private int missing;

    public override void MiniGameStart()
    {
        numbers = new int[3];
        numberTexts = new TextMesh[] {upperNumberText, lowerNumberText, solutionText};
        CreateEquation();
        DisplayEquation();
    }

    public override void ReceiveInput(int number)
    {
        switch(number) {
            case -2:
                if (int.Parse(numberTexts[missing].text) == numbers[missing]) {
                    CreateEquation();
                    DisplayEquation();
                    Score++;
                } else {
                    Score = -1;
                }
                break;
            case -1:
                numberTexts[missing].text = "☐";
                break;
            default:
                if(numberTexts[missing].text == "☐")
                    numberTexts[missing].text = "";
                numberTexts[missing].text += number.ToString();
                break;
        }
    }

    protected override void OnComplete()
    {
        for(int i = 0; i < numberTexts.Length; i++) {
            numberTexts[i].text = "#";
        }
        symbolText.text = "@";
    }

    protected override void OnGameOver()
    {
        Debug.Log("Math failed");
    }

    void CreateEquation() {
        missing = Random.Range(0, numbers.Length);
        currentOperation = (Operation) Random.Range(0, 4);

        int num1 = Random.Range(1, 10);
        int num2 = Random.Range(0, 10);
        switch (currentOperation) {
            case Operation.ADD:
            case Operation.MULTIPLY:
                numbers[(int)Numbers.UPPER] = num1;
                numbers[(int)Numbers.LOWER] = num2;
                break;
            case Operation.SUBTRACT:
            case Operation.DIVIDE:
                numbers[(int)Numbers.UPPER] = OperationUtils.Apply(OperationUtils.GetInverse(currentOperation), num1, num2);
                numbers[(int)Numbers.LOWER] = num1;
                break;
        }
        numbers[(int)Numbers.SOLUTION] = OperationUtils.Apply(currentOperation, numbers[(int)Numbers.UPPER], numbers[(int)Numbers.LOWER]);

        if(numbers[missing] != 0 && numbers[(int)Numbers.SOLUTION] == 0 && (currentOperation == Operation.MULTIPLY || currentOperation == Operation.DIVIDE)) {
            if(missing == 2)
                missing--;
            else
                missing++;
        }
    }

    void DisplayEquation() {
        for(int i = 0; i < numbers.Length; i++) {
            numberTexts[i].text = numbers[i].ToString();
        }
        numberTexts[missing].text = "☐";
        symbolText.text = OperationUtils.GetSymbol(currentOperation);
    }

    private enum Numbers {
        UPPER,
        LOWER,
        SOLUTION
    }
}

enum Operation {
    ADD,
    SUBTRACT,
    MULTIPLY,
    DIVIDE    
}

internal static class OperationUtils {

    private static string[] symbols = new string[] {"+", "–", "×", "÷"};

    public static string GetSymbol(Operation operation) {
        return symbols[(int) operation];
    }

    public static Operation GetInverse(Operation operation) {
        switch (operation) {
            case Operation.ADD:
                return Operation.SUBTRACT;
            case Operation.SUBTRACT:
                return Operation.ADD;
            case Operation.MULTIPLY:
                return Operation.DIVIDE;
            case Operation.DIVIDE:
                return Operation.MULTIPLY;
            default:
                return Operation.ADD;
        }
    }

    public static int Apply(Operation operation, int x, int y) {
        switch (operation) {
            case Operation.ADD:
                return x + y;
            case Operation.SUBTRACT:
                return x - y;
            case Operation.MULTIPLY:
                return x * y;
            case Operation.DIVIDE:
                return x / y;
            default:
                return -1;
        }
    }
}