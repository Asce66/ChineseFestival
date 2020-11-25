using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float moveSpeed = 10f;

    void Update()
    {
        if (GameManager_kite.isStop || GameManager_kite.useStop)
            return;
        Movement();
    }

    void Movement()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }
}
