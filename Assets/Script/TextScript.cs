using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TextScript : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{

    
    [SerializeField]
    ButtonScript but;

    bool prevCol;
    // Start is called before the first frame update
    void Start()
    {
        prevCol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!but.collideAble && prevCol == true)
        {
            Tween.Color(gameObject.GetComponent<Text>(), RGBValue(152, 152, 152), 0.4f, Ease.OutCirc);
            prevCol = false;
            print("ss");
        }
        else if (but.collideAble && prevCol == false)
        {
            Tween.Color(gameObject.GetComponent<Text>(), Color.white, 0.4f, Ease.OutCirc);
            prevCol = true;
            print("ss2");
        }
            
        if (but.collideAble && Input.GetMouseButtonDown(0))
                Tween.Color(gameObject.GetComponent<Text>(), Color.white, 0.4f, Ease.OutCirc);
    }
    


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (but.collideAble)
        Tween.Color(gameObject.GetComponent<Text>(),Color.yellow,0.4f,Ease.OutCirc);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tween.Color(gameObject.GetComponent<Text>(),Color.white,0.4f,Ease.OutCirc);
    }


    Color RGBValue(float r,float g,float b)
    {
        return new Color(r/255.0f,g/255.0f,b/255.0f);
    }


}
