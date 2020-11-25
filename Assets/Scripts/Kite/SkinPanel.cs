using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinPanel : MonoBehaviour
{
    public GameObject item;
    public GameObject kite;

    StorePanel store = new StorePanel();
    List<Sprite> spriteList;
    List<int> hasSkin = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        Load();
        add();
    }

    // Update is called once per frame
    void Update()
    {
        Add();
    }

    void Add()
    {
        foreach (int id in PlayerData.Instance.skins)
        {
            if (!hasSkin.Contains(id))
            {
                hasSkin.Add(id);
                GameObject go = GameObject.Instantiate<GameObject>(item);
                go.transform.parent = transform;
                Image ima = go.GetComponent<Image>();
                ima.sprite = spriteList[id];
                go.GetComponent<Button>().onClick.AddListener(() =>
                {
                    kite.GetComponent<SpriteRenderer>().sprite = ima.sprite;
                }
                );
            }
        }
    }

    void Load()
    {
        spriteList = new List<Sprite>( Resources.LoadAll<Sprite>("Goods/Goods0"));
    }

    void add()
    {
        item.GetComponent<Button>().onClick.AddListener(() =>
        {
            kite.GetComponent<SpriteRenderer>().sprite = item.GetComponent<Image>().sprite;
        }
        );
    }
}
