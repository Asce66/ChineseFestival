using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreChange : MonoBehaviour
{
    private Text scoreText;

    private float timer = 0;
    private float scoreTime = 1;

    public int score { get; private set; }

    void Start()
    {
        score = 0;
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }

    void Update()
    {
        if (!GameManager_kite.isStop || GameManager_kite.useStop)
        {
            timer += Time.deltaTime;
            if (timer > scoreTime)
            {
                score += 10;
                timer = 0;
            }
        }
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = "0";
        timer = 0;
    }

    public void ChangeScore(int s)
    {
        score += s;
    }
}
