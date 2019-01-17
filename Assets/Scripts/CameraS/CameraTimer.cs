using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTimer : MonoBehaviour
{

    private GameObject Scam;
    private GameObject gameSwitcher;
    private float timer = 0.0f;
    public float lenght = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        Scam = GameObject.Find("ShipCamera");
        gameSwitcher = GameObject.Find("GameSwitcher");
    }

    // Update is called once per frame
    //TODO use coroutine instead
    void Update()
    {
        if (Scam.GetComponent<Camera>().enabled == true){
            timer += Time.deltaTime;
            if(timer > lenght)
            {
                //gameSwitcher.GetComponent<GameSwitcher>().ReturnToSpace(); ;
            }
        }
        else
        {
            timer = 0;
        }
    }
}
