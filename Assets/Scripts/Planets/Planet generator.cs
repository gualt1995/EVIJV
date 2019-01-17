using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour
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
            Random randm = new Random();
            int randx = Random.Range(-200, 200);
            int randy = Random.Range(-200, 200);
            Planet.transform.position = new Vector3(randx, randy, 0);
        }
    }
}
