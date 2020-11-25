using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public enum AchieveEnum
{
    ChunLian = 0,
    RedEnvelope = 1,
    Lantern,
    Kite
}

public enum ItemEnum
{
    TimeStop = 0,//时间暂停
    DoubleCoins,
    vise_free,
    shield
}

public class PlayerData
{
    private static PlayerData _ins;
    public static PlayerData Instance
    {
        get
        {
            if (_ins == null)
                _ins = new PlayerData();
            return _ins;
        }
    }

    private int coin;
    private AudioClip getCoinClip;
    private AudioClip reduceCoinClip;
    private Text coinText;
    public Text CoinText
    {
        get { return coinText; }
        set { coinText = value; coinText.text = coin.ToString(); }
    }

    public List<int> nowAchieveList;
    //是否购买了成就
    public bool isBuyAchieve = false;

    //道具&皮肤
    public Dictionary<int, int> itemDic = new Dictionary<int, int>();
    public HashSet<int> skins = new HashSet<int>();

    private PlayerData()
    {
        coin = 10000;
        nowAchieveList = new List<int>();
        getCoinClip = Resources.Load<AudioClip>("AudioClips/getCoinClip");
        reduceCoinClip = Resources.Load<AudioClip>("AudioClips/reduceCoinClip");

    }

    public void AddItem(int item, int cnt = 1)
    {
        if (itemDic.ContainsKey(item) == false)
            itemDic[item] = cnt;
        else
            itemDic[item] += cnt;
    }

    public bool ReduceItem(int item)
    {
        if (itemDic.ContainsKey(item) == false || itemDic[item] == 0)
            return false;
        itemDic[item]--;
        return true;
    }

    public void AddAchieve(int achieveInd, string num)
    {
        if (!nowAchieveList.Contains(achieveInd))
        {
            nowAchieveList.Add(achieveInd);
            AchievementShow._ins.SetDes("成就达成！获得第" + num + "块拼图");
        }
    }

    public void AddCoin(int num)
    {
        if (num < 0)
            return;
        coin += num;
        if (getCoinClip != null)
        {
            AudioSource.PlayClipAtPoint(getCoinClip, Vector3.zero);
        }
        if (coinText != null)
            coinText.text = coin.ToString();
    }

    public bool ReduceCoin(int coinNum)
    {
        if (coinNum < 0)
            return false;
        if (coin < coinNum)
            return false;
        coin -= coinNum;
        if (reduceCoinClip != null)
        {
            AudioSource.PlayClipAtPoint(reduceCoinClip, Vector3.zero);
        }
        if (coinText != null)
            coinText.text = coin.ToString();
        return true;
    }


    public bool HasSkin(int skinIndex)
    {
        return skins.Contains(skinIndex);
    }

    public void AddSkin(int skinIndex)
    {
        if (!skins.Contains(skinIndex))
        {
            skins.Add(skinIndex);
        }

    }

    public int GetItemNum(int itemIndex)
    {
        if (itemDic.ContainsKey(itemIndex))
            return itemDic[itemIndex];
        return 0;
    }

}
