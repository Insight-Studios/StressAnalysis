using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;
    public GameObject selectedMiniGameIcon;
    public GameObject exitGame;

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
            selectedMiniGameIcon.transform.Translate(selectedMiniGame.transform.position.x - selectedMiniGameIcon.transform.position.x, selectedMiniGame.transform.position.y - selectedMiniGameIcon.transform.position.y, 0);
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
            selectedMiniGameIcon.transform.Translate(selectedMiniGame.transform.position.x - selectedMiniGameIcon.transform.position.x, selectedMiniGame.transform.position.y - selectedMiniGameIcon.transform.position.y, 0);
        }
    }
    int selectedMiniGameIndex;

    Vector3 prevMousePos;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            throw new System.Exception("Singleton instantiated twice.");
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
            if (e.keyCode == KeyCode.Escape)
            {
                exitGame.SetActive(!exitGame.activeSelf);
            }
            if ((e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) && exitGame.activeSelf)
            {
                Debug.LogWarning("Exit Game");
                Application.Quit();
            }

            if (exitGame.activeSelf)
            {
                return;
            }

            //MiniGame Inputs
            if (selectedMiniGame != null) selectedMiniGame.SendInput(e.keyCode);

            //MiniGame Selection Inputs
            switch (e.keyCode)
            {
                case KeyCode.LeftArrow:
                case KeyCode.A:
                    SelectedMiniGameIndex -= 1;
                    break;

                case KeyCode.RightArrow:
                case KeyCode.D:
                    SelectedMiniGameIndex += 1;
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
