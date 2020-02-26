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

    public bool GameOver {get {return gameOver;}}

    public int Score {
        get {
            return currentScore;
        }
        set {
            currentScore = value;
            scoreText.text = "Score: " + currentScore;
            if(currentScore >= requiredScore) {
                OnEnd(0);
            } else if (currentScore == -1) {
                OnEnd(1);
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
                timerText.text = 0.ToString();
                OnEnd(1);
            } else {
                timerText.text = Mathf.CeilToInt(remainingTime).ToString();
                MiniGameUpdate();
            }
        }
    }

    private void OnEnd(int code)
    {
        if(code == 1) {
            gameOver = true;
            OnGameOver();
        } else {
            OnComplete();
        }
        enabled = false;
        GameManager.instance.CompletedMiniGame();
    }
    
    public abstract void MiniGameStart();

    protected virtual void MiniGameUpdate() {}

    public abstract void ReceiveInput(int number);

    protected abstract void OnGameOver();

    protected abstract void OnComplete();
}
