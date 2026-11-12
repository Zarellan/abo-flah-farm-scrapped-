using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ScaleOnLap : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    float defaultX,defaultY;
    // Start is called before the first frame update
    void Start()
    {
        defaultX = GetComponent<RectTransform>().localScale.x;
        defaultY = GetComponent<RectTransform>().localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tween.Scale(gameObject.GetComponent<RectTransform>(),new Vector2(defaultX * 1.1f,defaultY * 1.1f),0.4f,Ease.OutCirc);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tween.Scale(gameObject.GetComponent<RectTransform>(),new Vector2(defaultX,defaultY),0.4f,Ease.OutCirc);
    }

}
