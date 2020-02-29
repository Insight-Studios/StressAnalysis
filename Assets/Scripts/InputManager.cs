using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;

    public List<OnClickEvent> onClicks;

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
        // Do stuff
    }

    void Update ()
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
        }
    }

    public void RegisterGameObject(GameObject obj)
    {
        print(obj.GetComponentsInChildren<OnClickEvent>().Length);
        onClicks.AddRange(obj.GetComponentsInChildren<OnClickEvent>());
    }

    public void UnRegisterGameObject(GameObject obj)
    {
        List<OnClickEvent> objOnClicks = new List<OnClickEvent>(obj.GetComponentsInChildren<OnClickEvent>());
        onClicks.RemoveAll(x => objOnClicks.Contains(x));
    }

    
}
