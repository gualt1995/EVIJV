using UnityEngine;
using System.Collections;

[System.Serializable]
public class Done_Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerSearch : MonoBehaviour
{
    public float speed;
    public float xMin, xMax, yMin, yMax;

    void FixedUpdate()
    {
        Rigidbody m_Rigidbody = GetComponent<Rigidbody>();
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                //Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
                m_Rigidbody.velocity = transform.forward * speed;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                //Move the Rigidbody backwards constantly at the speed you define (the blue arrow axis in Scene view)
                m_Rigidbody.velocity = -transform.forward * speed;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                //Rotate the sprite about the Y axis in the positive direction
                transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * speed*10, Space.World);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //Rotate the sprite about the Y axis in the negative direction
                transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * speed*10, Space.World);
            }
        }
        //Vector3 movement = new Vector3(moveHorizontal,0.0f, moveVertical);
        //GetComponent<Rigidbody>().velocity = movement * speed;

        /*GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, xMin, xMax),
            Mathf.Clamp(GetComponent<Rigidbody>().position.y, yMin, yMax),
            GetComponent<Rigidbody>().position.z
        );*/

    }
}
