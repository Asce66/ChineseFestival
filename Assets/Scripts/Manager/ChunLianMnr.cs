using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

struct DuiLian
{
    public string text;
    public int value;
}

public class ChunLianMnr : MonoBehaviour
{
    [SerializeField] Text coinText;
    public Text leftTxt, rigthTxt, topTxt;
    private List<DuiLian> strs = new List<DuiLian>();
    private List<DuiLian> topStrs = new List<DuiLian>();
    private int nowLeft, nowRight, nowTop;
    public Text resultTxt;
    string[] ranks = new string[] { "怎么说呢？新年你开心就好。\n哈哈......", "马马虎虎吧，但是不建议贴在自家门上", "格式标准是一副优秀的对联！！！" };
    private List<int> completedList;
    private int completeCnt = 0;

    private void Awake()
    {
        LoadData();
        ShuffRound(strs);
        ShuffRound(topStrs);
        SetDuiLian(leftTxt, strs[0].text);
        nowLeft = 0;
        SetDuiLian(rigthTxt, strs[1].text);
        nowRight = 1;
        topTxt.text = topStrs[0].text;
        nowTop = 0;
        PlayerData.Instance.CoinText = coinText;
        completedList = new List<int>();
        for (int i = 0; i < topStrs.Count; i++)
            completedList.Add(0);
    }

    public void ChangeLeft(int offset)
    {
        BtnClickAudio();
        nowLeft += offset;
        if (nowLeft < 0 || nowLeft >= strs.Count)
        {
            nowLeft -= offset;
            return;
        }
        SetDuiLian(leftTxt, strs[nowLeft].text);
    }

    public void ChangeRight(int offset)
    {
        BtnClickAudio();
        nowRight += offset;
        if (nowRight < 0 || nowRight >= strs.Count)
        {
            nowRight -= offset;
            return;
        }
        SetDuiLian(rigthTxt, strs[nowRight].text);
    }

    public void ChangeTop(int offset)
    {
        BtnClickAudio();
        nowTop += offset;
        if (nowTop < 0 || nowTop >= topStrs.Count)
        {
            nowTop -= offset;
            return;
        }
        topTxt.text = topStrs[nowTop].text;
    }

    void SetDuiLian(Text text, string str)
    {
        string ss = "";
        for (int i = 0; i < str.Length; i++)
        {
            ss += str[i];
            if (i < str.Length - 1)
                ss += "\n";
            text.text = ss;
        }
    }

    void LoadData()
    {
        TextAsset ta = Resources.Load<TextAsset>("ChunLianData");
        string[] temStr = ta.text.Split('\n');
        for (int i = 0; i < temStr.Length; i++)
        {
            if (temStr[i].Equals(""))
                continue;
            string[] data = temStr[i].Split(' ');
            DuiLian dl = new DuiLian();
            dl.text = data[0];
            dl.value = int.Parse(data[1]);
            if (int.Parse(data[2]) == 0)//必须要Parse再判断 要不只有最后一行判断成功
                topStrs.Add(dl);
            else
                strs.Add(dl);
        }
    }

    void ShuffRound(List<DuiLian> dilians)
    {
        for (int i = 0; i < dilians.Count; i++)
        {
            int temp = Random.Range(0, dilians.Count);
            DuiLian dl = dilians[i];
            dilians[i] = dilians[temp];
            dilians[temp] = dl;
        }
    }

    public void GetResult()
    {
        BtnClickAudio();
        int score = 0;
        if (strs[nowLeft].value + strs[nowRight].value == 0)
        {
            score++;
            if (strs[nowLeft].value == topStrs[nowTop].value)
                score++;
        }
        if (score == 0)
            PlayerData.Instance.ReduceCoin(-2);
        else if (score == 2&&completedList[nowTop]==0)
        {
            PlayerData.Instance.AddCoin(100);
            completedList[nowTop] = 1;
            ++completeCnt; 
            if (completeCnt == 2)
                PlayerData.Instance.AddAchieve(0, "一");
        }
            
        resultTxt.text = ranks[score];
    }

    public void BtnClickAudio()
    {
        AudioMnr._Ins.PlayBtnClickAudio();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
