using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager_kite : MonoBehaviour
{
    public static bool isStop = false;
    public static bool useStop = false;
    public GameObject scGo;
    public GameObject failedPanel;
    public GameObject skinPanel;

    private MapInstance mapIns;
    private GameObject kite;
    private GameObject[] eagleArray;

    [SerializeField] Text coinText;
    [SerializeField] ScoreChange scoreChange;
    public const int KiteAchieveScore = 300;//达成成就需要的分数
    private List<int> levelScore;//每一级需要的分数
    private int bgIndex;//现在背景的下标
    private bool isActive = true;       //SkinPanel是否显示
    [SerializeField] List<Sprite> bgSprites;
    [SerializeField]SpriteRenderer bgSR;

    [SerializeField] ItemController itemController;

    private void Start()
    {
        bgIndex = 0;
        levelScore = new List<int>() { 500, 1000 };
        mapIns = GameObject.Find("Map").GetComponent<MapInstance>();
        kite = GameObject.Find("Kite");
        PlayerData.Instance.CoinText = coinText;
        isStop = true;

        List<int> itemInd = new List<int>() { 0, 3 };
        List<ItemController.ItemEffect> itemEffects = new List<ItemController.ItemEffect>() { EffectStopTime, EffectShield };
        itemController.Init(itemInd, itemEffects);
    }

    void EffectStopTime()
    {
        useStop = true;
        StartCoroutine(Stop());
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(3);
        useStop = false;
    }

    void EffectShield()
    {
        kite.GetComponent<KiteMovement>().isShield = true;
        StartCoroutine(Stop());
    }

    IEnumerator Shield()
    {
        yield return new WaitForSeconds(3);
        kite.GetComponent<KiteMovement>().isShield = false;
    }

    private void Update()
    {
        if (failedPanel.activeSelf)
        {
            scGo.SetActive(false);
            //Time.timeScale = 0;
            isStop = true;
        }
        if (bgIndex >= bgSprites.Count)
            return;
        if(scoreChange.score>=levelScore[bgIndex])
        {
            bgSR.sprite = bgSprites[bgIndex];
            ++bgIndex;          
        }
        CompeteAchieve();

    }

    private void SetEagleList()
    {
        eagleArray = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void DestroyEagle()
    {
        foreach (GameObject eagle in eagleArray)
        {
            Destroy(eagle);
        }
    }

    public void OnReplayButton()
    {
        failedPanel.SetActive(false);
        kite.GetComponent<KiteMovement>().Recover();
        Debug.Log(kite.transform.eulerAngles);
        ChangeSkinPanel();
        scGo.SetActive(true);
        mapIns.ResetTime();
        SetEagleList();
        DestroyEagle();
        scGo.GetComponent<ScoreChange>().ResetScore();
        //Time.timeScale = 1;
    }

    public void OnExitButton()
    {
        failedPanel.SetActive(false);
        //Time.timeScale = 1;
        isStop = false;
        SceneManager.LoadScene(1);
    }
    //完成成就
    void CompeteAchieve()
    {
        if(scoreChange.score > KiteAchieveScore)
        {
            PlayerData.Instance.AddAchieve(2, "三");
        }
    }

    public void ChangeSkinPanel()
    {
        skinPanel.SetActive(!isActive);
        isActive = !isActive;
    }

    public void OnCloseButton()
    {
        ChangeSkinPanel();
        isStop = false;
    }
}
