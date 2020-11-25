using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_redEnvelope : MonoBehaviour
{
    private static GameManager_redEnvelope _instanceRed;
    public static GameManager_redEnvelope Instance
    {
        get { return _instanceRed; }
    }

    public GameObject[] goPrefabs;
    public int[] moneys;
    public float[] probabilities;
    public RectTransform poolTransform;
    public Player_redEnvelope player;
    public int poolCount = 20;
    public float time = 1;
    public float xPosition = 0;
    public float yPosition = 0;
    public float propTime = 3f;

    private float timer = 0;
    private List<GameObject> goList = new List<GameObject>();
    private List<Sprite> spriteList = new List<Sprite>();

    [SerializeField] Text coinText;
    [SerializeField] ItemController itemController;

    private void Awake()
    {
        _instanceRed = this;
        InitSprite();
        InitPool();
        PlayerData.Instance.CoinText = coinText;
        List<int> Itemindex = new List<int>() { 1, 3 };
        List<ItemController.ItemEffect> itemEffects = new List<ItemController.ItemEffect>() { UseDoubleProp, UseInvincibleProp };
        itemController.Init(Itemindex, itemEffects);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        Play();
    }
    //运行
    void Play()
    {
        if (timer > time)
        {
            timer = 0;
            GameObject go = GetGameObject();
            if (go != null)
            {
                go.transform.localPosition = new Vector2(Random.Range(-xPosition, xPosition), yPosition);
            }
        }
    }
    //构造图片、道具链表以减少每次更改对象图片时的性能消耗
    void InitSprite()
    {
        for (int i = 0; i < goPrefabs.Length; i++)
        {
            spriteList.Add(goPrefabs[i].GetComponent<Image>().sprite);
        }
    }
    //创建对象池
    void InitPool()
    {
        for (int i = 0; i < poolCount; i++)
        {
            GameObject go = GameObject.Instantiate(goPrefabs[0]);
            go.AddComponent<BoxCollider2D>();
            go.AddComponent<Prop>();
            go.SetActive(false);
            go.transform.SetParent(poolTransform);
            goList.Add(go);
        }
    }

    //获得对象
    public GameObject GetGameObject()
    {
        for (int i = 0; i < poolCount; i++)
        {
            if (!goList[i].activeInHierarchy)
            {
                int rand = Random.Range(0, 100);
                for (int j = 0; j < probabilities.Length - 1; j++)
                {
                    if (rand >= probabilities[j] && rand <= probabilities[j + 1])
                        rand = j;
                }
                goList[i].SetActive(true);
                goList[i].GetComponent<Image>().sprite = spriteList[rand];
                goList[i].GetComponent<Prop>().Money = moneys[rand];
                return goList[i];
            }
        }
        return null;
    }
    //回收对象
    public void DestroyGameObject(GameObject go)
    {
        go.SetActive(false);
    }
    //使用双倍金币道具
    public void UseDoubleProp()
    {
        player.SetScale(2);
        StartCoroutine(Use());
    }
    IEnumerator Use()
    {
        yield return new WaitForSeconds(propTime);
        player.SetScale(1);
    }
    //使用无敌道具
    public void UseInvincibleProp()
    {
        player.SetScale(0);
        StartCoroutine(Use());
    }

}