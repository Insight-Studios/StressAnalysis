using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private OnClickEvent[] onClicks;

    void Start()
    {
        onClicks = FindObjectsOfType<OnClickEvent>();
    }

    void Update () {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        if (Physics.Raycast(ray, out Hit))
        {
            if (Input.GetMouseButtonDown(0)) {
                for (int i = 0; i < onClicks.Length; i++) {
                    if (Hit.collider.gameObject == onClicks[i].gameObject) {
                        onClicks[i].Clicked();
                        break;
                    }
                }
            }
        }
    }

    
}
