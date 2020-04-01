using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;
    public GameManager gameManager;
    public GameObject selectedMiniGameIcon;
    List<OnClickEvent> onClicks = new List<OnClickEvent>();

    public MiniGameBase SelectedMiniGame
    {
        get
        {
            return selectedMiniGame;
        }
        set
        {
            bool exists = false;
            for (int i = 0; i < gameManager.miniGames.Length; i++)
            {
                if (gameManager.miniGames[i] == value)
                {
                    selectedMiniGameIndex = i;
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                Debug.LogWarning("Invalid SelectedMiniGame");
                return;
            }
            
            selectedMiniGame = value;
            selectedMiniGameIcon.SetActive(true);
            selectedMiniGameIcon.transform.position = (Vector3) gameManager.miniGamePositions[selectedMiniGameIndex] - Vector3.back;
        }
    }
    MiniGameBase selectedMiniGame;
    public int SelectedMiniGameIndex
    {
        get 
        {
            return selectedMiniGameIndex;
        }
        set 
        {
            if (value >= gameManager.miniGames.Length)
                value = 0;
            else if (value < 0)
                value = gameManager.miniGames.Length - 1;
            if (gameManager.miniGames[value] != null)
            {
                selectedMiniGameIndex = value;
                selectedMiniGame = gameManager.miniGames[value];
                selectedMiniGameIcon.SetActive(true);
                selectedMiniGameIcon.transform.position = (Vector3) gameManager.miniGamePositions[value] - Vector3.back;
            }
        }
    }
    int selectedMiniGameIndex;

    Vector3 prevMousePos;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        if (Physics.Raycast(ray, out Hit))
        {
            MiniGameBase hoveredMiniGame = Hit.collider.gameObject.GetComponentInParent<MiniGameBase>();
            if (Input.GetMouseButtonDown(0))
            {
                for (int i = 0; i < onClicks.Count; i++)
                {
                    if (Hit.collider.gameObject == onClicks[i].gameObject)
                    {
                        onClicks[i].Clicked();
                        break;
                    }
                }

                if (hoveredMiniGame != null)
                {
                    SelectedMiniGame = hoveredMiniGame;
                }
                else
                {
                    selectedMiniGame = null;
                    selectedMiniGameIcon.SetActive(false);
                }
            }
        }
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && Input.GetKeyDown(e.keyCode))
        {
            //MiniGame Selection Inputs
            if (!gameManager.IsPaused) switch (e.keyCode)
            {
                case KeyCode.A:
                    SelectedMiniGameIndex = 0;
                    break;
                case KeyCode.S:
                    SelectedMiniGameIndex = 1;
                    break;
                case KeyCode.D:
                    SelectedMiniGameIndex = 2;
                    break;
                case KeyCode.Escape:
                    gameManager.IsPaused = true;
                    break;
                default:
                    //MiniGame Inputs
                    if (selectedMiniGame != null) selectedMiniGame.SendInput(e.keyCode);
                    break;
            }
            else switch (e.keyCode)
            {
                case KeyCode.Escape:
                    gameManager.IsPaused = false;
                    break;
            }
        }
    }

    public void RegisterGameObject(GameObject obj)
    {
        onClicks.AddRange(obj.GetComponentsInChildren<OnClickEvent>());
    }

    public void UnRegisterGameObject(GameObject obj)
    {
        List<OnClickEvent> objOnClicks = new List<OnClickEvent>(obj.GetComponentsInChildren<OnClickEvent>());
        onClicks.RemoveAll(x => objOnClicks.Contains(x));
    }

    
}
