using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class ItemController : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab;
    //private List<int> item_Kite = new List<int>() { 0, 3 };
    //private List<int> item_redEnvelop = new List<int>() {1,3 };

    private List<int> itemIndex;
    private List<int> itemNum=new List<int>();
    private List<GameObject> items=new List<GameObject>();

    public delegate void ItemEffect();
    List<ItemEffect> itemEffects;

    public void Init(List<int>itemindex,List<ItemEffect>itemEffects)
    {
        this.itemIndex = itemindex;
        this.itemEffects = itemEffects;
        for(int i=0;i<this.itemIndex.Count;++i)
        {
            int cnt = PlayerData.Instance.GetItemNum(itemIndex[i]);
            GameObject go = Instantiate(itemPrefab,transform);
            items.Add(go);
            Sprite sp = Resources.Load<Sprite>("Goods/Goods1/Item" + itemIndex[i]);
            go.GetComponent<Image>().sprite = sp;
            itemNum.Add(cnt);
            go.GetComponentInChildren<Text>().text = cnt.ToString();
            int ind = i;
            go.GetComponent<Button>().onClick.AddListener(()=>UseItem(ind));
        }
    }

    IEnumerator ItemIceTime(Image mask)
    {
        float allTime = 3;
        float timer = 3;
        while(timer>0)
        {
            timer -= Time.deltaTime;
            mask.fillAmount = timer / allTime;
            yield return null;
        }
        mask.transform.parent.GetComponent<Button>().interactable = true;
    }

    void UseItem(int ind)
    {
        if (itemNum[ind] <= 0)
            return;
        --itemNum[ind];
        items[ind].GetComponentInChildren<Text>().text = itemNum[ind].ToString();
        PlayerData.Instance.ReduceItem(itemIndex[ind]);
        Image mask = items[ind].transform.Find("Mask").GetComponent<Image>();
        mask.fillAmount = 1;
        items[ind].GetComponent<Button>().interactable = false;
        StartCoroutine(ItemIceTime(mask));
        itemEffects[ind]();
    }

}
