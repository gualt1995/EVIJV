using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour {


    public float mouseSensitivity = 1.0f;
    public float targetZoomInMax = -150f;
    public float targetZoomOutMax = -400f;

    private bool cameraSlideOk = true;
    private bool cameraMouvement = true;
    private Vector3 lastPosition;


	public void SetCameraScrollOk(bool cso)
    {
        cameraSlideOk = cso;
    }
    public void SetCameraMovement(bool csm)
    {
        cameraMouvement = csm;
    }

	// Update is called once per frame
	void Update () {
    //TODO reglez les foncton de zoom (notamenet le claping de la camera)
    // Mouse wheel moving forward
    if (Input.GetAxis("Mouse ScrollWheel") > 0f && transform.position.z < targetZoomInMax) // forward 
        {
            transform.position += Vector3.forward * 10;
    }

        // Mouse wheel moving backward
    if (Input.GetAxis("Mouse ScrollWheel") < 0f && transform.position.z > targetZoomOutMax)
    {
            transform.position += Vector3.back * 10;
        }
    
        if (cameraMouvement)
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, -270f, 270f);
            pos.y = Mathf.Clamp(pos.y, -270f, 270f);
            transform.position = pos;
            if (Input.GetMouseButtonDown(0))
            {
                lastPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 delta = Input.mousePosition - lastPosition;
                if (cameraSlideOk)
                {
                    transform.Translate(-delta.x * mouseSensitivity, -delta.y * mouseSensitivity, 0);
                }
                else
                {
                    if (Input.mousePosition.y >= Screen.height * 0.95) {
                        transform.Translate(Vector3.up * mouseSensitivity *2 , Space.World);
                    }
                    if (Input.mousePosition.y <= Screen.height * 0.05) {
                        transform.Translate(Vector3.down * mouseSensitivity*2 , Space.World); } 
                    if (Input.mousePosition.x >= Screen.width * 0.95) {
                        transform.Translate(Vector3.right  * mouseSensitivity*2, Space.World); }
                    if (Input.mousePosition.x <= Screen.width * 0.05) {
                        transform.Translate(Vector3.left * mouseSensitivity*2, Space.World); }

                }
                lastPosition = Input.mousePosition;
            }
        }
    }
}

