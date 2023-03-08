using UnityEngine;

public class ScrollViewElement : MonoBehaviour
{
    public int Index;
    public float Height;
    public RectTransform rectTransform;
    
    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Height = rectTransform.rect.height;
    }
    
    public void ChangeSiblingIndex(int index)
    {
        Index = index;
        rectTransform.SetSiblingIndex(index);
    }
    [EditorButton]
    public void PrintPosition()
    {
        print("rectPos:" + GetComponent<RectTransform>().rect.position);
        print("pos:" + GetComponent<RectTransform>().position);
        print("anch:" + GetComponent<RectTransform>().position);
    }
}
