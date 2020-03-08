using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;
    public GameObject selectedMiniGameIcon;

    List<OnClickEvent> onClicks = new List<OnClickEvent>();
    MiniGameBase selectedMiniGame;
    Vector3 prevMousePos;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
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
            }

            if (selectedMiniGame != Hit.collider.GetComponentInParent<MiniGameBase>() && Hit.collider.GetComponentInParent<MiniGameBase>() && prevMousePos != Input.mousePosition)
            {
                selectedMiniGameIcon.SetActive(true);
                selectedMiniGame = Hit.collider.GetComponentInParent<MiniGameBase>();
                selectedMiniGameIcon.transform.position = new Vector3(selectedMiniGame.gameObject.GetComponent<Transform>().position.x, selectedMiniGameIcon.transform.position.y, selectedMiniGameIcon.transform.position.z);
            }
        }
        prevMousePos = Input.mousePosition;
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            //MiniGame Inputs
            if (selectedMiniGame != null && Input.GetKeyDown(e.keyCode)) 
            switch (e.keyCode)
            {
                case KeyCode.Alpha0:
                case KeyCode.Keypad0:
                    selectedMiniGame.ReceiveInput(0);
                    break;
                case KeyCode.Alpha1:
                case KeyCode.Keypad1:
                    selectedMiniGame.ReceiveInput(1);
                    break;
                case KeyCode.Alpha2:
                case KeyCode.Keypad2:
                    selectedMiniGame.ReceiveInput(2);
                    break;
                case KeyCode.Alpha3:
                case KeyCode.Keypad3:
                    selectedMiniGame.ReceiveInput(3);
                    break;
                case KeyCode.Alpha4:
                case KeyCode.Keypad4:
                    selectedMiniGame.ReceiveInput(4);
                    break;
                case KeyCode.Alpha5:
                case KeyCode.Keypad5:
                    selectedMiniGame.ReceiveInput(5);
                    break;
                case KeyCode.Alpha6:
                case KeyCode.Keypad6:
                    selectedMiniGame.ReceiveInput(6);
                    break;
                case KeyCode.Alpha7:
                case KeyCode.Keypad7:
                    selectedMiniGame.ReceiveInput(7);
                    break;
                case KeyCode.Alpha8:
                case KeyCode.Keypad8:
                    selectedMiniGame.ReceiveInput(8);
                    break;
                case KeyCode.Alpha9:
                case KeyCode.Keypad9:
                    selectedMiniGame.ReceiveInput(9);
                    break;
                case KeyCode.Return:
                case KeyCode.KeypadEnter:
                    selectedMiniGame.ReceiveInput(-1);
                    break;
                case KeyCode.Backspace:
                case KeyCode.Delete:
                    selectedMiniGame.ReceiveInput(-2);
                    break;
            }

            //MiniGame Selection Inputs
            if (Input.GetKeyDown(e.keyCode))
            {
                switch (e.keyCode)
                {
                    case KeyCode.LeftArrow:
                    case KeyCode.A:
                        if (selectedMiniGameIcon.transform.position.x == -6)
                        {
                            selectedMiniGameIcon.transform.position += 12 * Vector3.right;
                        }
                        else
                        {
                            selectedMiniGameIcon.transform.position += 6 * Vector3.left;
                        }
                        break;

                    case KeyCode.RightArrow:
                    case KeyCode.D:
                        if (selectedMiniGameIcon.transform.position.x == 6)
                        {
                            selectedMiniGameIcon.transform.position += 12 * Vector3.left;
                        }
                        else
                        {
                            selectedMiniGameIcon.transform.position += 6 * Vector3.right;
                        }
                        break;
                }
                selectedMiniGameIcon.SetActive(true);
                selectedMiniGame = GameManager.instance.miniGames[Mathf.RoundToInt(selectedMiniGameIcon.transform.position.x / 6 + 1)];
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
