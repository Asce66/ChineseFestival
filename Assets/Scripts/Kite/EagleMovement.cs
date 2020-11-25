using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleMovement : MonoBehaviour
{
    public float moveSpeed = 60;

    private Vector2 direcation;
    public MapInstance mapInstance;

    void Start()
    {
        direcation = (GameObject.Find("Kite").transform.position- transform.position).normalized;
        if (mapInstance != null)
            moveSpeed += mapInstance.eagleSpeedIncr;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager_kite.isStop || GameManager_kite.useStop)
            return;
        Movement();
    }

    void Movement()
    {
        transform.Translate(direcation * moveSpeed * Time.deltaTime, Space.World);
    }


}
