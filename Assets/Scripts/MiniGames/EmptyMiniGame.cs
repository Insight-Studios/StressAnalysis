using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyMiniGame : MiniGameBase
{

    [SerializeField] private GameObject ooze, active_light, sewage;

    [SerializeField] private float sewageMinPercent, sewageSpeedProp;

    private float timeTillEmpty, initTimeTillEmpty;
    private bool isEmptying;


    protected override void MiniGameStart()
    {
        ooze.SetActive(false);
        active_light.SetActive(false);
        sewage.transform.localScale = new Vector3(8, 0, 1);
        timeTillEmpty = Lifetime * sewageSpeedProp;
        initTimeTillEmpty = timeTillEmpty;
        isEmptying = false;
    }

    protected override void MiniGameUpdate()
    {
        if (!isEmptying)
        {
            timeTillEmpty = Mathf.Max(timeTillEmpty-Time.deltaTime, 0);
            sewage.transform.localScale = new Vector3(8, 8*(1 - timeTillEmpty/initTimeTillEmpty), 1);

            if (sewage.transform.localScale.y/8 > sewageMinPercent)
            {
                active_light.SetActive(true);
            }
        }
        else
        {
            sewage.transform.localScale = sewage.transform.localScale + 8 * Time.deltaTime * Vector3.down;
            if (sewage.transform.localScale.y <= 0)
            {
                isEmptying = false;
                ooze.SetActive(false);
                timeTillEmpty = initTimeTillEmpty;
            }
        }
    }

    public override void SendInput(KeyCode key)
    {
        if (!isEmptying && sewage.transform.localScale.y/8 > sewageMinPercent)
        {
            isEmptying = true;
            ooze.SetActive(true);
            active_light.SetActive(false);
            Score++;
        }
    }
}
