using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGamePos : MonoBehaviour {

	void Start ()
    {
        Move(1);
	}

    void Move(int pos)
    {
        transform.position = new Vector3((pos - 2) * 6, -2.5f, 0);
    }
}
