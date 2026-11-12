using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class TransferSystem : MonoBehaviour
{
    Camera cam;

    [SerializeField] Item item;

    [SerializeField] Button transferButton;

    [SerializeField] GameObject overlappedPlace;

    [SerializeField] GameObject fakeTimer;

    Tween fakeTimerTween;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        PrimeTweenConfig.warnEndValueEqualsCurrent = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.C))
        //     TransferTo("cave");
        // else if (Input.GetKeyDown(KeyCode.F))
        //     TransferTo("farm");


        if (PlayerPrefs.GetInt("level CaveDivers") > 0 && IsPointerOverUIObject(overlappedPlace))
        {
            ButtonAppear();
        }
        
    }

    public void TransferButton()
    {
        if (Item.place == "farm")
            TransferTo("cave");
        else
            TransferTo("farm");
    }

    public void ButtonAppear()
    {
        Tween.UIAnchoredPositionY(transferButton.GetComponent<RectTransform>(),-450,0.3f,Ease.OutSine);

        fakeTimerTween.Stop();
        fakeTimerTween = Tween.PositionX(fakeTimer.transform,startValue:0,endValue:1,1.5f).OnComplete(()=>{

            Tween.UIAnchoredPositionY(transferButton.GetComponent<RectTransform>(),-630,0.3f,Ease.InSine);
        });

        

    }
    

    private void TransferTo(string transfer)
    {
        switch(transfer.ToLower())
        {
            case "farm":Farm();break;
            case "cave":Cave();break;
            default:Farm();break;
        }
    }


    void Farm()
    {
        Item.place = "farm";
        item.ChangedSection();
        Tween.Position(cam.transform,new Vector3(0,0,-10),0.2f,Ease.InOutCirc);

        int direct = 1;
        //Tween.UIAnchoredPositionY(transferButton.GetComponent<RectTransform>(),-450*direct,0.2f,Ease.InOutCirc);
        Tween.ScaleX(transferButton.GetComponent<RectTransform>(),1*direct,0.2f,Ease.InOutCirc);



        SoundtrackUtil.PlayMusic("you gotta move");
    }

    void Cave()
    {
        Item.place = "cave";
        item.ChangedSection();
        Tween.Position(cam.transform,new Vector3(0,-10,-10),0.2f,Ease.InOutCirc);

        int direct = -1;
        //Tween.UIAnchoredPositionY(transferButton.GetComponent<RectTransform>(),-450*direct,0.2f,Ease.InOutCirc);
        Tween.ScaleX(transferButton.GetComponent<RectTransform>(),1*direct,0.2f,Ease.InOutCirc);


        SoundtrackUtil.PlayMusic("undertale hotel");
    }


    bool IsPointerOverUIObject(GameObject targetButton)
    {
        // Raycast to check if the pointer is over the button
        return RectTransformUtility.RectangleContainsScreenPoint(
            targetButton.GetComponent<RectTransform>(),
            Input.mousePosition,
            Camera.main);
    }

}
