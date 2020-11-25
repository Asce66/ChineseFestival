using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementPanel : MonoBehaviour
{
    public List<GameObject> familyList;
    public MainMenu mainpanel;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
          CompeteAchieve();
          Show();
    }

    void Show()
    {
        foreach (int nowAchieve in PlayerData.Instance.nowAchieveList)
        {
            if(!familyList[nowAchieve].activeInHierarchy)
                familyList[nowAchieve].SetActive(true);
        }
    }

    void CompeteAchieve()
    {
        if(PlayerData.Instance.nowAchieveList.Count == 5)
        {
            PlayerData.Instance.AddAchieve(5, "六");
        }
    }
}
