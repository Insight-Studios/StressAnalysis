using UnityEngine;

public abstract class MiniGameBase : MonoBehaviour
{
    public float lifeTime = 30;
    public float warningPercentage;
    public Material warningMat;
    public int requiredScore = 5;
    public GameObject background;
    public TextMesh timerText;
    public TextMesh scoreText;

    private bool gameOver;
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
            if (value == -1)
                OnEnd(true);
            else if(currentScore >= requiredScore) {
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

        if (remainingTime <= lifeTime * warningPercentage)
        {
            background.GetComponent<MeshRenderer>().material = warningMat;
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
