using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour {

    public int thisNum;
    public float blinkSpeed = 0.3f;

    void OnMouseDown()
    {
        if (GetComponentInParent<Memory>())
        {
            if (thisNum == 20)
            {
                StartCoroutine(Blink());
                GetComponentInParent<Memory>().RestartSequence();
            }

            else { GetComponentInParent<Memory>().MouseInput(thisNum); }
        }

        else if (GetComponentInParent<MathGame>())
        {
            StartCoroutine(Blink());
            GetComponentInParent<MathGame>().CheckAnswer(thisNum);
        }

        else if (GetComponentInParent<NumberClick>())
        {
            StartCoroutine(Blink());
            GetComponentInParent<NumberClick>().CheckNum(thisNum);
        }
    }

    IEnumerator Blink()
    {
        GetComponent<TextMesh>().color = new Vector4(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, 0.7f);

        yield return new WaitForSeconds(blinkSpeed);

        GetComponent<TextMesh>().color = new Vector4(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, 1f);
    }
}
