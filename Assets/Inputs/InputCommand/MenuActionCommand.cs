using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActionCommand : Command
{
    bool isOpened = true;
    float timeScaleNow1;
    public override void Execute()
    {
        GameObject.Find("CanvasMenu").transform.GetChild(0).gameObject.SetActive(isOpened);
        if (isOpened)
        {
            timeScaleNow1 = Time.timeScale;
            Time.timeScale = 0;
            isOpened = false;

        }
        else
        {
            Time.timeScale = timeScaleNow1;
            isOpened = true;
        }
    }
}
