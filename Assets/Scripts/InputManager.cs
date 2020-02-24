using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    private OnClickEvent[] onClicks;

    void Start()
    {
        onClicks = FindObjectsOfType<OnClickEvent>();
    }

    // Update is called once per frame
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
        Debug.Log(Hit.collider);
    }

    
}
