 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.Events;
 
 public class OnClickEvent : MonoBehaviour {

    public UnityEvent OnClick = new UnityEvent();
    public UnityEvent OnHover = new UnityEvent();

    void OnValidate() {
        var clickables = GetComponents<IOnClick>();
        for(int i = 0; i < clickables.Length; i++)
         OnClick.AddListener(clickables[i].OnClick);
    }

    public void Clicked() {
        Debug.Log("Clicked");
        OnClick.Invoke();
    }
}

public interface IOnClick {

    void OnClick();
}