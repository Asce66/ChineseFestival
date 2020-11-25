using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private float speed = 20f;

    private float max = -217f;
    private float min = -225.5f;

    private void FixedUpdate()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && transform.position.z <= max)
        {
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime), Space.World);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0 && transform.position.z >= min)
        {
            transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime), Space.World);
        }
    }
}
