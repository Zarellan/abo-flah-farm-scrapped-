using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InfoScript : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    GameObject info;
    [SerializeField]
    TMP_Text infoTextObject;
    [SerializeField]
    string infoText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoTextObject.text = infoText;
        Tween.UIAnchoredPosition(info.GetComponent<RectTransform>(),new Vector2(575.41f,0),0.3f,Ease.OutCirc);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Tween.Position(GameObject.FindWithTag("LeafArrow").transform,new Vector2(7.76f,-3.36f),0.4f,Ease.OutCirc);
        //Tween.Rotation(GameObject.FindWithTag("LeafArrow").transform,new Vector3(0,0,-90),0.4f,Ease.OutCirc);
        Tween.UIAnchoredPosition(info.GetComponent<RectTransform>(),new Vector2(1406,0),0.3f,Ease.InSine);

    }

}
