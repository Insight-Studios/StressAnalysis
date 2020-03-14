using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OnClickEvent), typeof(Collider))]
public class MiniGameButton : MonoBehaviour, IOnClick
{
    public MiniGameBase owner;
    public KeyCode key;

    void Awake() {
        if(owner == null) {
            owner = GetComponentInParent<MiniGameBase>();
        }
    }

    public void OnClick()
    {
        if (owner.enabled)
            owner.SendInput(key);
            InputManager.instance.SelectedMiniGame = owner;
    }
}
