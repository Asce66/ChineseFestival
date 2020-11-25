using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorePanel : MonoBehaviour
{
    public MainMenu mainPanel;
    [SerializeField] GameObject goodsPrefab;
    [SerializeField] Text CoinText;
    private List<GameObject> childs = new List<GameObject>();
    public Dictionary<int, List<Sprite>> goodsSpDict = new Dictionary<int, List<Sprite>>();
    private int nowSelectedInd = -1;

    //第一个皮肤/道具/拼图的价格
    private int[] firstItemPrices = new int[3] { 600, 400, 8888 };
    //皮肤/道具/拼图递增价格
    private int[] itemIncreasePrices = new int[3] { 100, 80, 0 };

    private void OnEnable()
    {
        ChangeChildList(1);
        PlayerData.Instance.CoinText = CoinText;
    }

    private void CheckNumText(Text text, int index)
    {
        if (nowSelectedInd == 1)
        {
            if (PlayerData.Instance.itemDic.ContainsKey(index) && PlayerData.Instance.itemDic[index] > 0)
            {
                if (text.enabled == false)
                {
                    text.enabled = true;
                }

                text.text = PlayerData.Instance.itemDic[index].ToString();
            }
            else
                   if (text.enabled)
                text.enabled = false;
        }
        else
            text.enabled = false;
    }

    private void ChangeChilds()
    {
        if (goodsSpDict.ContainsKey(nowSelectedInd) == false)
        {
            Sprite[] sp = Resources.LoadAll<Sprite>("Goods/Goods" + nowSelectedInd);
            goodsSpDict[nowSelectedInd] = new List<Sprite>(sp);
        }

        int position = 0;
        List<Sprite> sprites = goodsSpDict[nowSelectedInd];
        //把以前的元素换样式
        for (; position < sprites.Count && position < childs.Count; ++position)
        {
            if (childs[position].activeSelf == false)
                childs[position].SetActive(true);
            childs[position].GetComponentInChildren<Image>().sprite = sprites[position];
            Text numText = childs[position].transform.Find("Nums").GetComponent<Text>();
            CheckNumText(numText, position);
            childs[position].transform.Find("Coin_Text").GetComponent<Text>().text =
                (firstItemPrices[nowSelectedInd] + itemIncreasePrices[nowSelectedInd] * position).ToString();

            Button btn = childs[position].GetComponentInChildren<Button>();
            btn.onClick.RemoveAllListeners();
            int ind = position;//必须要用ind暂存position 否则后面position就继续增大了,传进去的参数就错了
            btn.onClick.AddListener(() => Purchase(childs[ind], ind));
            btn.interactable = !(nowSelectedInd == 0 && PlayerData.Instance.HasSkin(position));
        }
        //隐藏多的元素
        for (; position < childs.Count; ++position)
        {
            childs[position].SetActive(false);
        }
        //生成不足的元素
        for (; position < sprites.Count; ++position)
        {
            GameObject go = Instantiate(goodsPrefab, transform);
            childs.Add(go);
            go.GetComponentInChildren<Image>().sprite = sprites[position];
            Text numText = go.transform.Find("Nums").GetComponent<Text>();
            CheckNumText(numText, position);
            go.transform.Find("Coin_Text").GetComponent<Text>().text =
                (firstItemPrices[nowSelectedInd] + itemIncreasePrices[nowSelectedInd] * position).ToString();

            Button btn = go.GetComponentInChildren<Button>();
            btn.onClick.RemoveAllListeners();
            int ind = position;
            btn.onClick.AddListener(() => Purchase(go, ind));
            btn.interactable = !(nowSelectedInd == 0 && PlayerData.Instance.HasSkin(position));
        }
    }

    public void ChangeChildList(int index)
    {
        if (nowSelectedInd == index)
            return;
        nowSelectedInd = index;
        ChangeChilds();
    }

    private void AfterBuyAchieve()
    {

    }

    private void Purchase(GameObject go, int index)
    {
        if (nowSelectedInd == 2 && PlayerData.Instance.isBuyAchieve)
            return;
        int needPrice = firstItemPrices[nowSelectedInd] + itemIncreasePrices[nowSelectedInd] * index;
        if (PlayerData.Instance.ReduceCoin(needPrice))
        {
            switch (nowSelectedInd)
            {
                case 0:
                    PlayerData.Instance.AddSkin(index);
                    go.GetComponentInChildren<Button>().interactable = false;
                    break;
                case 1:
                    PlayerData.Instance.AddItem(index);
                    CheckNumText(go.transform.Find("Nums").GetComponent<Text>(), index);
                    break;
                case 2:
                    PlayerData.Instance.isBuyAchieve = true;
                    AfterBuyAchieve();
                    PlayerData.Instance.AddAchieve(4, "五");
                    break;
                default:
                    break;
            }
        }

    }

    public void OnExitButton()
    {
        mainPanel.ToMainMenu();
    }

}
