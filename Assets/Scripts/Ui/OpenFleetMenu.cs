using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenFleetMenu : MonoBehaviour {

    public GameObject FleetMenu;
    public GameObject MainCamera;

    public void InstatiateMenu()
    {
        FleetMenu.transform.localPosition = new Vector3(455, -10, 0);
    }
}
