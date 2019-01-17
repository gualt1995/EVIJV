using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetData : MonoBehaviour {

    public string planetName = "Name_here";
    public float speed = 1;
    public bool upgrade1;
    public bool upgrade2;
    public bool upgrade3;

    public GameObject cam;
    public GameObject planetMenu;

    private Transform target;
    //private Vector3 velocity = Vector3.zero;

    public void cameraZoom()
    {

        Vector3 cam_pos = transform.position;
        cam_pos.x = transform.position.x;
        cam_pos.z = transform.position.z-50;
        //cam.transform.position = Vector3.Lerp(cam.transform.position, transform.position, 0.2f);
        cam.transform.position = cam_pos;
        GameObject newMenu = Instantiate(planetMenu, transform.parent);
        newMenu.transform.position = transform.position;
        //transform.position = Vector3.SmoothDamp(transform.position, cam_pos, ref velocity, smoothTime);

    }


    //TODO smothly move the camera thoward the planet
    /*private void Update()
    {
        Vector3 cam_pos = transform.position;
        cam_pos.x = cam_pos.x + 50;
        cam_pos.z = cam_pos.z - 220;
        transform.position = Vector3.SmoothDamp(transform.position, cam_pos, ref velocity, smoothTime);
    }

    /*{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    */
    void Update()
    {
        // Define a target position above and behind the target transform
        //Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, -10));
        transform.RotateAround(Vector3.zero, Vector3.back,speed * Time.deltaTime);
        // Smoothly move the camera towards that target position
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}

