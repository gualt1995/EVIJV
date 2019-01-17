using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePause : MonoBehaviour {

    public void timePause()
    {
        Time.timeScale = 0f;
    }
}
