using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    private float minMoveSpeed = 0.5f;
    private float maxMoveSpeed = 5f;

    private float moveSpeed = 1;

    private int money = 0;
    public int Money
    {
        set; get;
    }

    private void Start()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Debug.Log(moveSpeed);
        transform.Translate(Vector2.down * moveSpeed, Space.Self);
    }
}
