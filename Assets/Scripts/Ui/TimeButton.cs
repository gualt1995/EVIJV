using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeButton : MonoBehaviour {

    public GameObject timeContol;
    private CanvasGroup canvasGroup;

    private bool timeIsShowm = true;

    public void Start()
    {
        canvasGroup = timeContol.GetComponent<CanvasGroup>();
        timeSwitch();
    }

    void timeSwitch()
    {
        if (timeIsShowm)
        {
            timeIsShowm = false;
            canvasGroup.alpha = 0f; //this makes everything transparent
            canvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
        }
        else
        {
            timeIsShowm = true;
            canvasGroup.alpha = 1f; //this makes everything transparent
            canvasGroup.blocksRaycasts = true; //this prevents the UI element to receive input events
        }
    }
}
