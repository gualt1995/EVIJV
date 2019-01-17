using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDestroyMenu : MonoBehaviour
{

    public void CloseMenu()
    {
        Destroy(transform.parent.gameObject);
        Camera.main.transform.position = new Vector3(0, 0, -200);
    }
}
