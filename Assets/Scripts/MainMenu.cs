using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject[] panels;
    public GameObject mainMenu;
    private GameObject nowPanel;
    [SerializeField] Text coinText;


    private void Start()
    {
        PlayerData.Instance.CoinText = coinText;

    }

    public void ChooseFestival(int festivalID)
    {
        AudioMnr._Ins.PlayBtnClickAudio();
        mainMenu.SetActive(false);
        nowPanel= panels[festivalID];
        nowPanel.SetActive(true);
    }

    public void ToMainMenu()
    {
        if(nowPanel!=null)
             nowPanel.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ChangeScene(int sceneID)
    {
        AudioMnr._Ins.PlayBtnClickAudio();
        AudioMnr._Ins.PauseBGAudio();
        PlayerPrefs.SetInt("PlayScene", sceneID);
        SceneManager.LoadScene(2);
    }
}
