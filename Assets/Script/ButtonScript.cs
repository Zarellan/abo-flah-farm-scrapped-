using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ButtonScript : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{


    [SerializeField]
    float incBy;

    public bool collideAble = true;
    public bool leafAbe = true;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Button>() != null)
            collideAble = GetComponent<Button>().interactable;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CollidableBool(bool value)
    {
        collideAble = value;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (collideAble && leafAbe)
        {
            Tween.Position(GameObject.FindWithTag("LeafArrow").transform, new Vector2(transform.position.x + incBy, transform.position.y), 0.4f, Ease.OutCirc);
            Tween.Rotation(GameObject.FindWithTag("LeafArrow").transform, new Vector3(0, 0, 0), 0.4f, Ease.OutCirc);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Tween.Position(GameObject.FindWithTag("LeafArrow").transform,new Vector2(7.76f,-3.36f),0.4f,Ease.OutCirc);
        //Tween.Rotation(GameObject.FindWithTag("LeafArrow").transform,new Vector3(0,0,-90),0.4f,Ease.OutCirc);

    }


    Color RGBValue(float r,float g,float b)
    {
        return new Color(r/255,g/255,b/255);
    }


}
