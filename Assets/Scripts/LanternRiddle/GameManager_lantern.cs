using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager_lantern : MonoBehaviour
{
    public static GameManager_lantern _ins;
    public GameObject mysertyBG;
    public InputField input;
    public Text riddle;
    public Text tip;
    public Text answer;

    private Lantern lantern;
    private int successCount = 0;

    [SerializeField] Text coinText;
    [SerializeField] ItemController itemController;

    private void Awake()
    {
        _ins = this;
        PlayerData.Instance.CoinText = coinText;
        List<int> itemIndex = new List<int>() { 2 };
        List<ItemController.ItemEffect> itemEffects = new List<ItemController.ItemEffect>() { UseSuccessProp };
        itemController.Init(itemIndex, itemEffects);
    }

    // Update is called once per frame
    void Update()
    {
        if (mysertyBG.activeSelf == true)
        {
            input.enabled = true;
            if (input.text.Equals(lantern.GetAnswer()) && lantern.flag == 1)
            {
                lantern.ChangeLight();
                Success();
            }

            else if (lantern.flag != 1)
            {
                input.enabled = false;
                ShowAnswer();
                ShowTip();
            }
        }
        else
        {
            Detection();
        }
    }

    void Detection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Lantern" && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                lantern = hit.collider.gameObject.GetComponent<Lantern>();
                mysertyBG.SetActive(true);
                ShowRiddle();
            }
        }
    }

    void Success()
    {
        successCount++;
        ShowAnswer();
        ShowSuccess();
        lantern.ChangeState();
        CompeteAchieve();
    }

    void ShowRiddle()
    {
        riddle.text = lantern.GetMystery();
    }

    void ShowSuccess()
    {
        tip.color = Color.green;
        tip.text = "正确";
    }

    void ShowTip()
    {
        if (!tip.text.Equals("正确"))
        {
            tip.color = Color.black;
            tip.text = lantern.GetTip();
        }
    }

    void ShowAnswer()
    {
        answer.text = "答案：" + lantern.GetAnswer();
    }

    void mReset()
    {
        riddle.text = "";
        tip.text = "";
        answer.text = "";
    }

    public void OnButtonTip()
    {
        ShowTip();
    }

    public void OnButtonGiveup()
    {
        ShowTip();
        ShowAnswer();
        input.enabled = false;
        lantern.ChangeState();
    }

    public void OnButtonClose()
    {
        input.text = "";
        mReset();
        mysertyBG.SetActive(false);
    }

    public void UseSuccessProp()
    {
        if (lantern != null && lantern.flag == 1)
        {
            Success();
        }
    }

    void CompeteAchieve()
    {
        if (successCount > 2)
        {
            PlayerData.Instance.AddAchieve(3, "四");
        }
    }
}