using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InfiniteContent : MonoBehaviour
{
    public void ArrangeChildren(float elementHeight, float spacing)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        RectTransform[] rtChildren = new RectTransform[rectTransform.childCount];

        for (int i = 0; i < rectTransform.childCount; i++)
        {
            rtChildren[i] = rectTransform.GetChild(i) as RectTransform;
        }
        
        float topY = rectTransform.rect.height * .5f;
        float posOffset = elementHeight * .5f + spacing;
        
        for (int i = 0; i < rtChildren.Length; i++)
        {
            Vector2 childPos = rtChildren[i].localPosition;
            childPos.x = 0;
            childPos.y = topY - posOffset - (i - 1) * (elementHeight + spacing);// first one for upper hidden position
            rtChildren[i].localPosition = childPos;
        }
    }
}
