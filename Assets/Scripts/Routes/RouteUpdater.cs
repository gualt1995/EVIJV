using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteUpdater : MonoBehaviour {

    private GameObject endPlanet;
    private LineRenderer lr;
    private bool initialized = false;


	public void initialize (GameObject ep) {
        endPlanet = ep;
        lr = gameObject.GetComponent<LineRenderer>();
        initialized = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (initialized)
        {
            lr.SetPosition(1, transform.InverseTransformPoint(
                endPlanet.transform.position));
        }
    }
}
