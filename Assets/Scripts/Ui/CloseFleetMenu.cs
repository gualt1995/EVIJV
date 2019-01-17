using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseFleetMenu : MonoBehaviour {

    public GameObject MainCamera;

    public void CloseMenu()
    {
        transform.parent.gameObject.transform.position = new Vector3(555,-10,0);
    }
}
