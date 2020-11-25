using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_redEnvelope : MonoBehaviour
{
    public float moveSpeed = 1;
    public float xPositionLimit = 250;
    
    private int money;
    public int Money
    {
        get { return money; }
    }
    private int scale = 1;
    private int moneyCount = 0;         //获得金钱的数量

    private void FixedUpdate()
    {
        Move();
    }
    //移动
    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        if (transform.localPosition.x > xPositionLimit)
            transform.localPosition = new Vector2(xPositionLimit, transform.localPosition.y);
        else if (transform.localPosition.x < -xPositionLimit)
            transform.localPosition = new Vector2(-xPositionLimit, transform.localPosition.y);

        transform.Translate(Vector2.right * x * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        money = collision.gameObject.GetComponent<Prop>().Money;    //带符号

        ChangeMoney();

        GameManager_redEnvelope.Instance.DestroyGameObject(collision.gameObject);
    }

    void ChangeMoney()
    {
        if (scale != 0)
        {
            money = money * scale;  //没使用无敌道具
        }
        else if (money < 0)      //使用无敌道具
        {
            money = 0;
        }
        CompeteAchieve();
        if (money >= 0)
        {
            PlayerData.Instance.AddCoin(money);
        }
        else
        {
            PlayerData.Instance.ReduceCoin(-money);
        }
    }

    public void SetScale(int scale)
    {
        this.scale = scale;
    }

    void CompeteAchieve()
    {
        moneyCount += money;
        if(moneyCount > 500)
        {
            PlayerData.Instance.AddAchieve(1, "二");
        }
    }
}
