using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    // Start is called before the first frame update

    Button button;

    
    GameObject shop;

    public static bool onShop = false;

    [SerializeField] GameObject mainShop;
    void Start()
    {
        
        button = GetComponent<Button>();
        shop = GameObject.FindWithTag("ShopHUD");
        //shop.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUIObject(button))
            {
                OnButtonPressed();
            }
        }

    }


    bool IsPointerOverUIObject(Button targetButton)
    {
        // Raycast to check if the pointer is over the button
        return RectTransformUtility.RectangleContainsScreenPoint(
            targetButton.GetComponent<RectTransform>(),
            Input.mousePosition,
            Camera.main);
    }

    void OnButtonPressed()
    {
        
        /*if (!shop.activeSelf)
        {
            shop.SetActive(true);
            onShop = true;
        }
        else
        {
            shop.SetActive(false);
            onShop = false;

        }*/

        onShop = onShop?onShop = false:onShop = true;

        if (!onShop)
            Tween.UIAnchoredPositionY(mainShop.GetComponent<RectTransform>(),1080,0.4f,Ease.InCubic);
        else
            Tween.UIAnchoredPositionY(mainShop.GetComponent<RectTransform>(),0,0.4f,Ease.OutBack);


    }

}
