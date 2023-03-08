using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InfiniteContent : MonoBehaviour
{
    [SerializeField] private float height;
    
    private RectTransform rectTransform;
    private RectTransform[] rtChildren;

    public void ArrangeChildren(float elementHeight, float spacing)
    {
        rectTransform = GetComponent<RectTransform>();
        rtChildren = new RectTransform[rectTransform.childCount];

        for (int i = 0; i < rectTransform.childCount; i++)
        {
            rtChildren[i] = rectTransform.GetChild(i) as RectTransform;
        }
        
        height = rectTransform.rect.height;

        SetPositions(elementHeight, spacing);
    }

    private void SetPositions(float elementHeight, float spacing)
    {
        float topY = height * 0.5f;
        float posOffset = elementHeight * 0.5f + spacing;
        for (int i = 0; i < rtChildren.Length; i++)
        {
            Vector2 childPos = rtChildren[i].localPosition;
            childPos.x = 0;
            childPos.y = topY - posOffset - (i - 1) * (elementHeight + spacing);
            rtChildren[i].localPosition = childPos;
        }
    }
}
