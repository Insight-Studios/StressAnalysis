using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public List<OnClickEvent> onClicks;

    void Start()
    {
        if(instance == null) 
            instance = this;
        else
            throw new System.Exception("Singleton instantiated twice.");

        ReloadOnClicks();
    }

    // Remarkably unreliable
    public void ReloadOnClicks() {
        onClicks = new List<OnClickEvent>(FindObjectsOfType<OnClickEvent>());
    }

    void Update () {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        if (Physics.Raycast(ray, out Hit))
        {
            if (Input.GetMouseButtonDown(0)) {
                for (int i = 0; i < onClicks.Count; i++) {
                    if (Hit.collider.gameObject == onClicks[i].gameObject) {
                        onClicks[i].Clicked();
                        break;
                    }
                }
            }
        }
    }

    
}
