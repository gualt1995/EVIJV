using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviour {

    private GameObject go;
    public Dictionary<string, List<GameObject>> planetDict;

    public GameObject space;

    public void setUiName(GameObject un)
    {
        go = un;
    }
    
    public GameObject getTargetPlanet()
    {
        return go;
    }

    public void Start()
    {
        planetDict = new Dictionary<string, List<GameObject>>();
        foreach (Transform child in space.transform)
        {
            planetDict.Add(child.name,new List<GameObject>());
        }
    }

    public void CreateRoute(GameObject start, GameObject end)
    {
        planetDict[start.name].Add(end);
        planetDict[end.name].Add(start);
    }

    public void DestroyRoute(GameObject start, GameObject end)
    {
        planetDict[start.name].Remove(end);
        planetDict[end.name].Remove(start);
    }
}
