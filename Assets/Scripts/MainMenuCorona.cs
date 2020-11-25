using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class MainMenuCorona : MonoBehaviour, IDragHandler,IPointerEnterHandler
{
    float screenWidth, screenHeight;
    Vector2 temp;
    Animator am;
    bool isShow = false;
    float width;
    bool isMute=false;
    [SerializeField]
    Sprite muteSP, voiceSP;
    [SerializeField]
    Image voiceImg;
    
    void Start()
    {
        DontDestroyOnLoad(transform.parent.gameObject);
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        transform.Find("Img_ToMain").GetComponent<Button>().onClick.AddListener(ToMainMenu);
        transform.Find("Img_Mute").GetComponent<Button>().onClick.AddListener(
          () =>
          {
              AudioMnr._Ins.PlayBtnClickAudio();
             isMute= AudioMnr._Ins.AudioSwitch();
              if (isMute)
              {
                  voiceImg.sprite = muteSP;
              }
              else
                  voiceImg.sprite = voiceSP;
          }  );
        transform.Find("Img_Exit").GetComponent<Button>().onClick.AddListener(
            () =>
            {
                AudioMnr._Ins.PlayBtnClickAudio();
                Application.Quit();
            });
        SceneManager.LoadScene(1);
        am = GetComponent<Animator>();
        width = ((RectTransform)transform).rect.width;
    }

    void Update()
    {
        if(isShow)
        {
            if(transform.position.x- Input.mousePosition.x>width)
            {
                isShow = false;
                am.SetBool("IsShow", isShow);
            }
            else if(Vector2.Distance(transform.position,Input.mousePosition)>width*1.5f)
            {
                isShow = false;
                am.SetBool("IsShow", isShow);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {   
        temp = Input.mousePosition;
        temp.x = Mathf.Clamp(temp.x, 0, screenWidth);
        temp.y = Mathf.Clamp(temp.y, 0, screenHeight);
        transform.position = temp;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isShow = true;
        am.SetBool("IsShow", isShow);
    }

    public void ToMainMenu()
    {
        AudioMnr._Ins.PlayBtnClickAudio();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            GameObject.FindObjectOfType<MainMenu>().ToMainMenu();
            return;
        }
        SceneManager.LoadScene(1);
    }
}
