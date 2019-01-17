using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planetgenerator : MonoBehaviour
{

    public GameObject Space;
    public GameObject Planet_Prefab;
    public int Nb_Planets;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<Nb_Planets; i++)
        {
            GameObject Planet = Instantiate(Planet_Prefab, Space.transform);
            //Planet.transform.position = transform.position;
        }
    }

    public void instanciate_planet()
    {

    }
}
