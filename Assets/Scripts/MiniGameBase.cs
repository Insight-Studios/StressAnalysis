using UnityEngine;

public abstract class MiniGameBase : MonoBehaviour
{

    public float lifeTime = 30;
    public int requiredScore = 5;
    public TextMesh timerText;
    public TextMesh scoreText;

    private float remainingTime;
    private int currentScore;
    private bool gameOver;

    public int Score {
        get {
            return currentScore;
        }
        set {
            currentScore = value;
            scoreText.text = "Score: " + currentScore;
            if(currentScore >= requiredScore) {
                OnComplete();
                enabled = false;
            } else if (currentScore == -1) {
                gameOver = true;
                OnGameOver();
                enabled = false;
            }
        }
    }

    void Start()
    {
        remainingTime = lifeTime;
        currentScore = 0;
        gameOver = false;
        MiniGameStart();
    }

    void Update()
    {
        if (!gameOver) {
            remainingTime -= Time.deltaTime;
            if(remainingTime <= 0) {
                gameOver = true;
                timerText.text = 0.ToString();
                OnGameOver();
                enabled = false;
            } else {
                timerText.text = Mathf.CeilToInt(remainingTime).ToString();
                MiniGameUpdate();
            }
        }
    }
    
    public abstract void MiniGameStart();

    protected virtual void MiniGameUpdate() {}

    public abstract void ReceiveInput(int number);

    protected abstract void OnGameOver();

    protected abstract void OnComplete();
}
