using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;
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
            for (int i = 0; i < GameManager.instance.miniGames.Length; i++)
            {
                if (GameManager.instance.miniGames[i] == value)
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
            selectedMiniGameIcon.transform.position = (Vector3) GameManager.instance.miniGamePositions[selectedMiniGameIndex] - Vector3.back;
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
            if (value >= GameManager.instance.miniGames.Length)
                value = 0;
            else if (value < 0)
                value = GameManager.instance.miniGames.Length - 1;
            selectedMiniGameIndex = value;
            selectedMiniGame = GameManager.instance.miniGames[value];
            selectedMiniGameIcon.SetActive(true);
            selectedMiniGameIcon.transform.position = (Vector3) GameManager.instance.miniGamePositions[value] - Vector3.back;
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
            if (!GameManager.instance.IsPaused) switch (e.keyCode)
            {
                case KeyCode.A:
                    SelectedMiniGameIndex = 0;
                    break;
                case KeyCode.S:
                        if (GameManager.instance.miniGames[1] != null)
                        {
                            SelectedMiniGameIndex = 1;
                        }
                    break;
                case KeyCode.D:
                        if (GameManager.instance.miniGames[2] != null)
                        {
                            SelectedMiniGameIndex = 2;
                        }
                        break;
                case KeyCode.Escape:
                    GameManager.instance.IsPaused = true;
                    break;
                default:
                    //MiniGame Inputs
                    if (selectedMiniGame != null) selectedMiniGame.SendInput(e.keyCode);
                    break;
            }
            else switch (e.keyCode)
            {
                case KeyCode.Escape:
                    GameManager.instance.IsPaused = false;
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
