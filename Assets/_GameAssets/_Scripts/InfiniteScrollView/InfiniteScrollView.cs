using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfiniteScrollView : MonoBehaviour , IScrollHandler,IDragHandler,IBeginDragHandler
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private InfiniteContent _infiniteContent;
    [SerializeField] private float elementHeight = 300;
    [SerializeField] private float spacing = 25;
    
    [SerializeField] private RectTransform _rectTransform;
    private bool isUpwards;
    private float upperBound;
    private float lowerBound;
    private Vector2 mousePos;
    
    public void Init()
    {
        _infiniteContent.ArrangeChildren(elementHeight, spacing);
        upperBound = _scrollRect.content.GetChild(0).position.y + spacing;
        lowerBound = _scrollRect.content.GetChild(_scrollRect.content.childCount - 1).position.y - spacing;
    }

    public int GetNecessaryElementCount()
    {
        return Mathf.CeilToInt(_rectTransform.rect.height / elementHeight);
    }
    
    public void OnScroll(PointerEventData eventData)
    {
        isUpwards = eventData.scrollDelta.y < 0;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        isUpwards = eventData.position.y > mousePos.y;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mousePos = eventData.position;
    }

    public void OnScrollValueChanged(Vector2 value)
    {
        if (isUpwards)
        {
            HandleUpwards();
        }
        else
        {
            HandleDownwards();
        }
    }

    private void HandleDownwards()
    {
        var downmostElement = _scrollRect.content.GetChild(_scrollRect.content.childCount - 1);
        if(downmostElement.position.y > lowerBound) return;

        var upmostElement = _scrollRect.content.GetChild(0);
        var upPos = upmostElement.localPosition;
        upPos.y = upmostElement.localPosition.y + elementHeight + spacing;
        downmostElement.localPosition = upPos;
        downmostElement.SetSiblingIndex(0);
    }

    private void HandleUpwards()
    {
        var upmostElement = _scrollRect.content.GetChild(0);
        if(upmostElement.position.y < upperBound) return;

        var downmostElement = _scrollRect.content.GetChild(_scrollRect.content.childCount - 1);
        var downPos = downmostElement.localPosition;
        downPos.y = downmostElement.localPosition.y - elementHeight - spacing;
        upmostElement.localPosition = downPos;
        upmostElement.SetSiblingIndex(_scrollRect.content.childCount - 1);
    }

}