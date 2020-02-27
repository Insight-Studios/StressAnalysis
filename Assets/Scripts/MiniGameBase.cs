using UnityEngine;

public abstract class MiniGameBase : MonoBehaviour
{

    public float lifeTime = 30;
    public int requiredScore = 5;
    public TextMesh timerText;
    public TextMesh scoreText;

    [HideInInspector]
    public bool gameOver;

    private float remainingTime;
    private int currentScore;

    public bool GameOver {get {return gameOver;}}

    public int Score {
        get {
            return currentScore;
        }
        set {
            currentScore = value;
            scoreText.text = "Score: " + currentScore;
            if(currentScore >= requiredScore) {
                OnEnd(false);
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
                OnEnd(true);
            } else {
                timerText.text = Mathf.CeilToInt(remainingTime).ToString();
                MiniGameUpdate();
            }
        }
    }

    private void OnEnd(bool timedOut)
    {
        if(timedOut) {
            gameOver = true;
            OnGameOver();
        } else {
            OnComplete();
        }
        enabled = false;
        GameManager.instance.CompletedMiniGame(timedOut);
    }
    
    public abstract void MiniGameStart();

    protected virtual void MiniGameUpdate() {}

    public abstract void ReceiveInput(int number);

    protected abstract void OnGameOver();

    protected abstract void OnComplete();
}
