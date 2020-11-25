using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class KiteMovement : MonoBehaviour
{
    public float moveSpeed = 50;
    public GameObject failedPanel;
    [SerializeField] float xLimit = 200, yLimit = 125;
    [SerializeField] float rotateSpeed = 5;
    public bool isShield = false;
    public ScoreChange score;

    void Update()
    {
        if (GameManager_kite.isStop == false)
        {
            Movement();
        }
    }

    void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if(h!=0||v!=0)
        {
            transform.Translate(new Vector2(h, v) * moveSpeed * Time.deltaTime, Space.World);
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);
            pos.y = Mathf.Clamp(pos.y, -yLimit, yLimit);
            transform.position = pos;
            transform.rotation = Quaternion.Euler(0,h * moveSpeed * rotateSpeed, 0);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            if(isShield)
            {
                Destroy(collision.gameObject);
            }
            else
                failedPanel.SetActive(true);
        }else if (collision.gameObject.tag.Equals("Prop"))
        {
            score.ChangeScore(50);
            Destroy(collision.gameObject);
        }
    }

    public void Recover()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = new Vector3(0, -125, 0);
    }
}
