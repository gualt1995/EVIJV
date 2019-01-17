using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipPosition : MonoBehaviour
{

    public GameObject Planet;
    private GameObject startPlanet;
    private GameObject gameSwitcher;
    public float P_switch = 0.1f;
    public bool inMovement;
    public Slider slider;
    private float speed;
    private float elapsed = 0f;

    void Start()
    {
        speed = 50f;
        inMovement = false;
        Planet = GameObject.Find("Start_Planet");
        gameSwitcher = GameObject.Find("GameSwitcher");
    }

    void Update()
    {
        if (inMovement)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Planet.transform.position, step);
            var dir = Planet.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            float totalDistance = Vector3.Distance(Planet.transform.position, startPlanet.transform.position);
            float shipDistance = Vector3.Distance(Planet.transform.position, transform.position);
            GameSwitcher gs = gameSwitcher.GetComponent<GameSwitcher>();
            elapsed += Time.deltaTime;
            if (elapsed >= 1f)
            {
                elapsed = elapsed % 1f;
                if (Random.Range(0.0f, 1.0f) < P_switch)
                {
                    gs.TestGameSwitch();
                }
            }
            slider.value =  1 - shipDistance / totalDistance;
            if (Vector3.Distance(Planet.transform.position, this.transform.position)< 0.5){
                inMovement = false;
            }
        }
        else
        {
            slider.value = 0;
            transform.position = Planet.transform.position;
        }
    }

    public void setPlanet(GameObject planet)
    {
        if (!inMovement)
        {
            startPlanet = this.Planet;
            inMovement = true;
            this.Planet = planet;
        }
    }

}
