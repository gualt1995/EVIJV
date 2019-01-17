using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour {

    public Dictionary<string, Vector2> positions;
    public Dictionary<string,Ship> data;
    public GameObject shipContent;
    public GameObject ShipMenu;

    //private ShipContent sc;

    public void Start()
    {
        /*positions = new Dictionary<string, Vector2>();
        data = new Dictionary<string, Ship>();
        shipContent = GameObject.Find("ShipContent");

        addShip("sp1", new Ship("sp1", 10f, 2, 5f), new Vector2(10000, 10000));
        addShip("sp2", new Ship("sp2", 15f, 2, 5f), new Vector2(10000, 10000));
        addShip("sp3", new Ship("sp3", 10f, 3, 7f), new Vector2(10000, 10000));
        addShip("sp4", new Ship("sp3", 9f, 2, 1f), new Vector2(10000, 10000));

    }

    public void addShip(string name, Ship ship,Vector2 position)
    {
        RectTransform rtmenu = shipContent.GetComponent<RectTransform>();
        RectTransform rtship = ShipMenu.GetComponent<RectTransform>();
        positions.Add(name, position);
        data.Add(name, ship);
        GameObject sm = Instantiate(ShipMenu, shipContent.transform);
        RectTransform srct = sm.GetComponent<RectTransform>();
        srct.anchorMin = new Vector2(0.5f,1);
        srct.anchorMax = new Vector2(0.5f, 1);
        srct.pivot = new Vector2(0.5f, 0.5f);
        srct.localPosition = new Vector2(0,-1*((srct.rect.height/2)+(srct.rect.height*(data.Count-1))));
        rtmenu.sizeDelta = new Vector2(rtmenu.rect.width, rtship.rect.height * data.Count);
        sm.GetComponent<ShipMenuData>().myship = ship;
        sm.GetComponent<ShipMenuData>().Datachanged();*/



    }

}
