using UnityEngine;

public class MathGame: MonoBehaviour {

    public float time;
    public float startTimeDelay;

    int numberCorrect;
    int hiddenNumber;
    int operation;

    public TextMesh equation;
    public TextMesh score;
    public TextMesh timer;

    bool gameOver;

    void Start()
    {
        time += startTimeDelay;

        gameOver = false;

        //timer = GetComponentInChildren<TextMesh>();
        //equation = GameObject.Find("Equation").GetComponent<TextMesh>();
        //score = GameObject.Find("Score").GetComponent<TextMesh>();
        score.text = "Score: 0";

        ChooseOperation();
	}

    void Update()
    {
        NumberPressed();

        if (!gameOver)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                gameOver = true;
            }
            int timeLeft = Mathf.RoundToInt(time);

            timer.text = "Timer: " + timeLeft.ToString();
        }
        else timer.text = "Timer: 0";
    }

    void ChooseOperation()
    {
        operation = Random.Range(1, 5);

        if (operation == 1) Add();
        else if (operation == 2) Sub();
        else if (operation == 3) Mult();
        else if (operation == 4) Div();
    }

    void Add()
    {
        int first = Random.Range(1, 10);
        int second = Random.Range(1, 10);
        int result = first + second;

        DisplayProblem(first, second, result);
    }

    void Sub()
    {
        int first = Random.Range(10, 20);
        int second = Random.Range(1, 10);
        int result = first - second;

        DisplayProblem(first, second, result);
    }

    void Mult()
    {
        int first = Random.Range(1, 10);
        int second = Random.Range(1, 10);
        int result = first * second;

        DisplayProblem(first, second, result);
    }

    void Div()
    {
        int quoient = Random.Range(1, 10);
        int divisor = Random.Range(1, 10);
        int dividend = quoient * divisor;

        DisplayProblem(dividend, divisor, quoient);
    }

    void DisplayProblem(int first, int second, int result)
    {
        int hiddenPosition = Random.Range(1, 4);

        if (first >= 10 && hiddenPosition == 1) //Division
        {
            hiddenPosition = 2;
        }
        else if (result > 9 && hiddenPosition == 3) //Multiplication
        {
            hiddenPosition = 2;
        }

        string symbol = " ";
        if (operation == 1) symbol = "+ ";
        else if (operation == 2) symbol = "- ";
        else if (operation == 3) symbol = "x ";
        else if (operation == 4) symbol = "÷ ";

        string top = first.ToString();
        string middle = second.ToString();
        string bottom = result.ToString();

        if (hiddenPosition == 1)
        {
            hiddenNumber = first;
            top = "?";
        }
        else if (hiddenPosition == 2)
        {
            hiddenNumber = second;
            middle = "?";
        }
        else if (hiddenPosition == 3)
        {
            hiddenNumber = result;
            bottom = "?";
        }

        equation.text = top + "\n" + symbol + middle + "\n" + bottom;
    }

    public void CheckAnswer(int numberChoise)
    {
        if (gameOver) return;

        if (hiddenNumber == numberChoise)
        {
            //You gots it right!
            numberCorrect++;
            score.text = "Score: " + numberCorrect.ToString();
            ChooseOperation();
        }

        else
        {
            numberCorrect--;
            score.text = "Score: " + numberCorrect.ToString();
            ChooseOperation();
        }
    }

    void NumberPressed()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            CheckAnswer(1);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            CheckAnswer(2);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            CheckAnswer(3);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            CheckAnswer(4);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            CheckAnswer(5);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            CheckAnswer(6);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            CheckAnswer(7);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            CheckAnswer(8);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            CheckAnswer(9);
        }
    }
}
