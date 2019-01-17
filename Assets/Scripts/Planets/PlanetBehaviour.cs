using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehaviour : MonoBehaviour
{
    private float speed = 2;

    public void onClick()
    {
        GameObject ship = GameObject.Find("SpaceShip");
        ShipPosition shipPositionScript = ship.GetComponent<ShipPosition>();
        shipPositionScript.setPlanet(this.gameObject);
    }

    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.back, speed * Time.deltaTime);
    }
}
