using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SlideBooks : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    //改变content的Left值来实现翻书效果
    //每次翻一页
    ScrollRect scrollRect;
    RectTransform content;
    RectTransform canvasRect;
    int currentIndex = 0, itemCount;
    float mouseBeginPosY,
        offsetTop,
        cellHeight,
        spacing,//单元格之间的距离
        oneItemDis,//移动一个单元格需要鼠标滑动的距离
        oneItemPosX,//移动一个单元content的LocalPositionY的值需要+-的值
        contentPosY = 0,//滑动鼠标后,要到达的LocalPositionY值
        initPosY;//初始时y坐标

    float contentLen;//记录content的原始长度

    [SerializeField]Image bgImg;
    [SerializeField]Sprite []bgSprite;

    private void Awake()
    {

        scrollRect = GetComponent<ScrollRect>();
        content = scrollRect.content;
        initPosY = content.localPosition.y;
        canvasRect = GameObject.Find("Canvas").transform as RectTransform;

        itemCount = content.childCount;
        GridLayoutGroup gridLayoutGroup = GetComponentInChildren<GridLayoutGroup>();
        offsetTop = gridLayoutGroup.padding.top;
        cellHeight = gridLayoutGroup.cellSize.y;
        spacing = gridLayoutGroup.spacing.y;
        oneItemDis = cellHeight / 2 + offsetTop;
        oneItemPosX = cellHeight + spacing;
        scrollRect.inertia = false;//必须禁止这个
        contentLen = content.sizeDelta.x;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRect, new Vector2
            (Input.mousePosition.x, Input.mousePosition.y), null, out pos);
        mouseBeginPosY = pos.y;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRect, new Vector2
            (Input.mousePosition.x, Input.mousePosition.y), null, out pos);
        float offsetY = mouseBeginPosY - pos.y;
        if (offsetY > 0)//右滑
            PageChange(-1);
        else
            PageChange(1);
    }

    /// <param name="currentIndexOffset"> 页码偏移量(单滑,只有-1与1) 1:向右滑动  -1:向左滑动</param>
    public void PageChange(int currentIndexOffset)
    {
        currentIndex += currentIndexOffset;
        if (currentIndex > (itemCount - 1))
        {
            currentIndex--;
            return;
        }
        if (currentIndex < 0)
        {
            currentIndex++;
            return;
        }
        AudioMnr._Ins.PlayPaging();
        float offsetPos =currentIndexOffset * oneItemPosX;
        contentPosY += offsetPos;
        content.DOLocalMoveY(contentPosY, 0.5f).SetEase(Ease.OutQuint).OnComplete(()=> { bgImg.sprite = bgSprite[currentIndex]; });     
    }

    public void ResetPos()
    {
        currentIndex = 0;
        contentPosY = 0;
        content.localPosition = new Vector2(content.localPosition.x, initPosY);
    }

}
