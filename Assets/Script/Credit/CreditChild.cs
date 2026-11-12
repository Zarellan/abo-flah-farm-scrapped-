using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreditChild : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public Sprite img;
    [SerializeField]
    public Sprite altImg;
    [SerializeField]
    public string text;

    [SerializeField]
    GameObject textObj;


    public string urlLink;


    private bool isInside;
    // Start is called before the first frame update
    void Start()
    {



        if (img != null)
        GetComponentInChildren<Image>().sprite = img;

        GetComponentInChildren<TMP_Text>().text = text;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isInside && urlLink != "")
            Application.OpenURL(urlLink);
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (altImg != null)
        GetComponentInChildren<Image>().sprite = altImg;

        isInside = true;

    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (img != null)
            GetComponentInChildren<Image>().sprite = img;

        isInside = false;
    }

}
