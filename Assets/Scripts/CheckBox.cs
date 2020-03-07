using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    public GameObject checkReference;
    GameObject thisCheck;

    public void ActivateCheck()
    {
        thisCheck = Instantiate(checkReference, transform);
    }

    public void DisableCheck()
    {
        if (thisCheck != null)
        {
            Destroy(thisCheck);
        }
    }
}
