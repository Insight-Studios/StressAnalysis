using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;

    private List<OnClickEvent> onClicks = new List<OnClickEvent>();
    private MiniGameBase focus;

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

    void Start()
    {

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
                focus = Hit.collider.GetComponentInParent<MiniGameBase>();
                print(focus);
            }
        }
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            if (focus != null && Input.GetKeyDown(e.keyCode)) 
                switch (e.keyCode)
                {
                    case KeyCode.Alpha0:
                    case KeyCode.Keypad0:
                        focus.ReceiveInput(0);
                        break;
                    case KeyCode.Alpha1:
                    case KeyCode.Keypad1:
                        focus.ReceiveInput(1);
                        break;
                    case KeyCode.Alpha2:
                    case KeyCode.Keypad2:
                        focus.ReceiveInput(2);
                        break;
                    case KeyCode.Alpha3:
                    case KeyCode.Keypad3:
                        focus.ReceiveInput(3);
                        break;
                    case KeyCode.Alpha4:
                    case KeyCode.Keypad4:
                        focus.ReceiveInput(4);
                        break;
                    case KeyCode.Alpha5:
                    case KeyCode.Keypad5:
                        focus.ReceiveInput(5);
                        break;
                    case KeyCode.Alpha6:
                    case KeyCode.Keypad6:
                        focus.ReceiveInput(6);
                        break;
                    case KeyCode.Alpha7:
                    case KeyCode.Keypad7:
                        focus.ReceiveInput(7);
                        break;
                    case KeyCode.Alpha8:
                    case KeyCode.Keypad8:
                        focus.ReceiveInput(8);
                        break;
                    case KeyCode.Alpha9:
                    case KeyCode.Keypad9:
                        focus.ReceiveInput(9);
                        break;
                    case KeyCode.Return:
                    case KeyCode.KeypadEnter:
                        focus.ReceiveInput(-1);
                        break;
                    case KeyCode.Backspace:
                        focus.ReceiveInput(-2);
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
