using UnityEngine;

public abstract class MiniGameBase : MonoBehaviour
{
    [HideInInspector] public float redPercentage;
    [HideInInspector] public float yellowPercentage;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject[] checkBoxes;
    [SerializeField] private Animator timerAnimator;
    public float Lifetime {
        get {
            return lifetime;
        }
        set
        {
            remainingTime = remainingTime/lifetime * value;
            lifetime = value;
            timerAnimator.speed = 1/value;
        }
    }
    private float lifetime = 30;

    protected int requiredScore;

    private bool gameOver;
    private float remainingTime;
    private int currentScore;

    public bool GameOver { get { return gameOver; } }
    public float LifeTime { get { return remainingTime; } }

    public int Score {
        get {
            return currentScore;
        }
        set {
            currentScore = value;
            if (value == 0)
            {
                for (int i = 0; i < requiredScore; i++)
                {
                    checkBoxes[i].GetComponent<CheckBox>().DisableCheck();
                }
            }
            else
            {
                checkBoxes[value - 1].GetComponent<CheckBox>().ActivateCheck();
            }

            if (value == -1)
                OnEnd(true);
            else if(currentScore >= requiredScore) {
                OnEnd(false);
            }
        }
    }

    void Awake()
    {
        timerAnimator.keepAnimatorControllerStateOnDisable = true;
    }

    void Start()
    {
        requiredScore = checkBoxes.Length;
        remainingTime = lifetime;
        currentScore = 0;
        gameOver = false;
        MiniGameStart();
    }

    void Update()
    {
        if (!gameOver) {
            remainingTime -= Time.deltaTime;
            if(remainingTime <= 0) {
                OnEnd(true);
            } else {
                MiniGameUpdate();
            }
        }

        if (remainingTime <= lifetime * yellowPercentage)
        {
            if (remainingTime <= lifetime * redPercentage)
            {
                background.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                background.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
        }
    }

    private void OnEnd(bool failed)
    {
        if(failed) {
            gameOver = true;
            OnGameOver();
        } else {
            OnComplete();
        }

        enabled = false;
        GameManager.instance.CompletedMiniGame(failed);
        MiniGameEnd();
    }
    
    protected abstract void MiniGameStart();

    protected virtual void MiniGameUpdate() {}

    protected virtual void MiniGameEnd() {}

    protected virtual void SendNumber(int number) {}

    public virtual void SendInput(KeyCode key)
    {
        switch (key)
        {            
            case KeyCode.Alpha0:
            case KeyCode.Keypad0:
                SendNumber(0);
                break;
            case KeyCode.Alpha1:
            case KeyCode.Keypad1:
                SendNumber(1);
                break;
            case KeyCode.Alpha2:
            case KeyCode.Keypad2:
                SendNumber(2);
                break;
            case KeyCode.Alpha3:
            case KeyCode.Keypad3:
                SendNumber(3);
                break;
            case KeyCode.Alpha4:
            case KeyCode.Keypad4:
                SendNumber(4);
                break;
            case KeyCode.Alpha5:
            case KeyCode.Keypad5:
                SendNumber(5);
                break;
            case KeyCode.Alpha6:
            case KeyCode.Keypad6:
                SendNumber(6);
                break;
            case KeyCode.Alpha7:
            case KeyCode.Keypad7:
                SendNumber(7);
                break;
            case KeyCode.Alpha8:
            case KeyCode.Keypad8:
                SendNumber(8);
                break;
            case KeyCode.Alpha9:
            case KeyCode.Keypad9:
                SendNumber(9);
                break;
        }
    }

    protected virtual void OnGameOver() {}

    protected virtual void OnComplete() {}
}
