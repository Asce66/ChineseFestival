using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class ScenesMnr : MonoBehaviour
{
    private VideoPlayer vp;
    Dictionary<int, VideoClip> videoClipDict = new Dictionary<int, VideoClip>();
    private Text changeTxt;
    AsyncOperation ao;
    bool isok = false;
    bool isLoading = false;

    void Awake()
    {
        vp = GetComponent<VideoPlayer>();
        changeTxt = GetComponentInChildren<Text>();
        changeTxt.gameObject.SetActive(false);
        SceneManager.sceneLoaded +=(a,b)=>AudioMnr._Ins.SceneBGChange(a.buildIndex);
        SceneManager.sceneLoaded += (a, b) => BriefSciencesMnr._Ins.ShowMiniGameInfo(a.buildIndex);
    }

    private void Start()
    {
        int nextSceneID = PlayerPrefs.GetInt("PlayScene");
        if(videoClipDict.ContainsKey(nextSceneID)==false)
        {
            VideoClip vc = Resources.Load<VideoClip>("VideoClips/Scene" + nextSceneID);
            if (vc == null)
                Debug.LogError("VideoClip资源路径错误 " + "VideoClips/Scene" + nextSceneID);
            videoClipDict[nextSceneID] = vc;
        }
        vp.clip = videoClipDict[nextSceneID];
        vp.Play();
        ao = SceneManager.LoadSceneAsync(nextSceneID);
        ao.allowSceneActivation = false;
        StartCoroutine(LoadScene());
    }

    void Update()
    {
        if (isLoading && isok)
        {
            if (vp.isPlaying == false)
            {
                ao.allowSceneActivation = true;
            }
            else if (Input.anyKeyDown)
            {
                ao.allowSceneActivation = true;
            }
        }
    }

    IEnumerator LoadScene()
    {
        isLoading = true;
        isok = false;
        WaitForEndOfFrame wf = new WaitForEndOfFrame();
        int toProcess = 0, nowProcess = 0;
        while (ao.progress < 0.9f)
        {
            //print(ao.progress);
            toProcess = (int)(ao.progress * 100);
            while (nowProcess < toProcess)//一帧一帧增加进度,否则下一帧就直接加载完成了
            {
               // print(ao.progress);
                ++nowProcess;
                yield return wf;
            }
        }
        toProcess = 100;
        while (nowProcess < toProcess)
        {
           // print(ao.progress);
            ++nowProcess;
            yield return wf;
        }
        isok = true;
        changeTxt.gameObject.SetActive(true);
        changeTxt.DOFade(0, 1.5f).SetLoops(-1, LoopType.Yoyo);
    }
}
