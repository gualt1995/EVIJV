using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDragger : MonoBehaviour {


    public void dragship()
    {
        GameObject fm = gameObject.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.gameObject;
        CloseFleetMenu cfm = fm.GetComponentInChildren<CloseFleetMenu>();
        Debug.Log(cfm.name);
        cfm.CloseMenu();
        GameObject spaceship = GameObject.Find("SpaceShip");
        spaceship.transform.position = Input.mousePosition;
    }
}
