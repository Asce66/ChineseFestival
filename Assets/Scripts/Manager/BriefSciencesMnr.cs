using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public enum InfoType
{
    ChunLian=3,
    RedEnvelope,
    DengMi,
    ZhiHe
}
public class BriefSciencesMnr : MonoBehaviour
{
    private static BriefSciencesMnr _ins;

    public static BriefSciencesMnr _Ins
    {
        get
        {
            return _ins;
        }
    }

    private Button btn;
    private Text text;
    Dictionary<InfoType, List<string>> infoDict=new Dictionary<InfoType, List<string>>();
    private int nowIndex;
    private InfoType nowInfoType;

    private void Awake()
    {       
        _ins = this;
        btn = GetComponentInChildren<Button>();
        text = GetComponentInChildren<Text>();
        DontDestroyOnLoad(gameObject);
        btn.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        btn.onClick.AddListener(NextInfo);
    }

    private void OnDisable()
    {
        btn.onClick.RemoveAllListeners();
    }

    public void ShowMiniGameInfo(int sceneID)
    {
        if (sceneID <= 2)
            return;
        InfoType infoType = (InfoType)sceneID;
        if (infoDict.ContainsKey(infoType) == false)
            LoadInfo(infoType);
        btn.gameObject.SetActive(true);
        text.text = infoDict[infoType][0];
        nowInfoType = infoType;
        nowIndex = 0;
        Time.timeScale = 0;
    }

    private void NextInfo()
    {
        nowIndex++;
        if(nowIndex>=infoDict[nowInfoType].Count)
        {
            btn.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            text.text = infoDict[nowInfoType][nowIndex];
        }
    }

    private void LoadInfo(InfoType infoType)
    {
        TextAsset ta = Resources.Load<TextAsset>("ActiveInfo/" + infoType.ToString());
        if (ta == null)
            Debug.LogError("文本资源路径错误 " + "ActiveInfo/" + infoType.ToString());
        string[] strs = ta.text.Split('&');
        infoDict.Add(infoType, new List<string>());
        for(int i=0;i<strs.Length;i++)
        {
            infoDict[infoType].Add(strs[i]);
        }
    }

}