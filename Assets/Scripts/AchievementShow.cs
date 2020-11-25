using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementShow : MonoBehaviour
{
    public static AchievementShow _ins;
    private bool canMove = false;

    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.parent.gameObject);
        _ins = this;
        text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Show();
        }
        else
        {
            transform.localPosition = Vector2.Lerp(transform.position, new Vector2(0, 610), 1f);
        }
    }

    public void Show()
    {
        transform.localPosition = Vector2.Lerp(transform.position, new Vector2(0, 480), 1f);
        StartCoroutine(Back());
    }

    IEnumerator Back()
    {
        yield return new WaitForSeconds(3);
        canMove = false;
    }

    public void SetDes(string str)
    {
        text.text = str;
        canMove = true;
    }
}
