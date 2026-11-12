using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PrimeTween;
using System;
using System.Xml.Serialization;

public class Options : MonoBehaviour
{
    //options
    [SerializeField]
    Toggle discord;
    [SerializeField]
    Toggle copyright;
    [SerializeField]
    Toggle fullScr;
    [SerializeField]
    Button deleteData;

    //objects
    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    GameObject option;

    
    [SerializeField]
    GameObject logo;


    //mainMenu objects
    [SerializeField]
    SpriteRenderer sign;

    [SerializeField]
    GameObject startBut;

    [SerializeField]
    GameObject optionBut;
    [SerializeField]
    GameObject creditsBut;

    Transition sceneManagement;

    [SerializeField] GameObject peaceful,hard,extreme,chooseDiff;

    //bool for option interaction
    bool options = false;

    bool prepDelete = false;

    void Awake()
    {

        if (!PlayerPrefs.HasKey("CopyRight"))
            PlayerPrefs.SetString("CopyRight","true");
        

        if (!PlayerPrefs.HasKey("DiscordRPC"))
            PlayerPrefs.SetString("DiscordRPC","true");

        if (!PlayerPrefs.HasKey("FullScreenSav"))
            PlayerPrefs.SetString("FullScreen","true");


        copyright.isOn = BoolPref("CopyRight");
        discord.isOn = BoolPref("DiscordRPC");
        fullScr.isOn = BoolPref("FullScreenSav");


        print(BoolPref("FullScreenSav"));



        //gameObject.SetActive(false);

        print(option.GetComponent<RectTransform>().anchoredPosition.x);

        sceneManagement = GameObject.FindWithTag("Transition").GetComponent<Transition>();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartIt()
    {
        if (PlayerPrefs.GetInt("crops") <= 0)
            AskForDifficulty();
        else
            sceneManagement.TransScene("Farm");
    }


    
    public void AskForDifficulty()
    {
        Tween.UIAnchoredPositionY(mainMenu.GetComponent<RectTransform>(),-1620,0.3f,Ease.InBack);
        Tween.PositionX(GameObject.FindWithTag("LeafArrow").transform,20,0.3f,Ease.InSine);

        Tween.UIAnchoredPositionX(peaceful.GetComponent<RectTransform>(),-510,0.3f,Ease.OutBack);
        Tween.UIAnchoredPositionY(hard.GetComponent<RectTransform>(),-18,0.3f,Ease.OutBack);
        Tween.UIAnchoredPositionX(extreme.GetComponent<RectTransform>(),510,0.3f,Ease.OutBack);
        Tween.UIAnchoredPositionY(chooseDiff.GetComponent<RectTransform>(),380,0.3f,Ease.OutBack);

    }
    
    public void DifficultyChoice(string diff)
    {

            PlayerPrefs.SetString("difficulty",diff);
            sceneManagement.TransScene("Farm");
        

        peaceful.GetComponent<Button>().interactable = false;
        hard.GetComponent<Button>().interactable = false;
        extreme.GetComponent<Button>().interactable = false;

    }


    public void SetRPC()
    {
        if (!discord.isOn)
            PlayerPrefs.SetString("DiscordRPC","false");
        else
            PlayerPrefs.SetString("DiscordRPC","true");

    }


    public void SetCopyright()
    {
        if (!copyright.isOn)
            PlayerPrefs.SetString("CopyRight","false");
        else
            PlayerPrefs.SetString("CopyRight","true");
    }


    bool BoolPref(string name)
    {
        if (PlayerPrefs.GetString(name) == "false")
            return false;
        else
            return true;
    }


    public void BackToLeafPos()
    {
        Tween.Position(GameObject.FindWithTag("LeafArrow").transform,new Vector2(7.76f,-3.36f),0.4f,Ease.OutCirc);
        Tween.Rotation(GameObject.FindWithTag("LeafArrow").transform,new Vector3(0,0,-90),0.4f,Ease.OutCirc);

        if (!options)
        {
            Tween.UIAnchoredPosition(option.GetComponent<RectTransform>(),new Vector2(0,0),0.4f,Ease.OutCirc);
            Tween.Scale(mainMenu.GetComponent<RectTransform>(),new Vector2(0.85f,0.85f),0.4f,Ease.OutCirc);
            Tween.Color(sign,new Color(RGBValue(170),RGBValue(170),RGBValue(170)),0.4f,Ease.OutCirc);

            Tween.UIAnchoredPosition(logo.GetComponent<RectTransform>(),new Vector2(-609,900),0.4f,Ease.OutCirc);


            InteractedOptions(true);
        }
        else
        {
            Tween.UIAnchoredPosition(option.GetComponent<RectTransform>(),new Vector2(-1920,0),0.4f,Ease.OutCirc);
            Tween.Scale(mainMenu.GetComponent<RectTransform>(),new Vector2(1,1),0.4f,Ease.OutCirc);
            Tween.Color(sign,new Color(RGBValue(255),RGBValue(255),RGBValue(255)),0.4f,Ease.OutCirc);
            Tween.UIAnchoredPosition(logo.GetComponent<RectTransform>(),new Vector2(-609,228),0.4f,Ease.OutCirc);

            InteractedOptions(false);

        }
    }

    public void SetFullScreen()
    {
        if (!fullScr.isOn)
            PlayerPrefs.SetString("FullScreenSav","false");
        else
            PlayerPrefs.SetString("FullScreenSav","true");



        FullScreen(fullScr.isOn);
    }

    public void DeleteData()
    {
        Text textData = deleteData.GetComponentInChildren<Text>().GetComponent<Text>();
        if (!prepDelete)
        {
            prepDelete = true;
            textData.text = "you sure ?";
        }
        else
        {
            PlayerPrefs.DeleteAll();
            StaticReset.ResetStatics();

            textData.color = Color.red;
            Tween.Color(textData,Color.white,0.4f,Ease.OutSine);

            prepDelete = false;
            textData.text = "Delete data";
        }

    }


    void FullScreen(bool cond)
    {
        if (cond)
        {
            Screen.fullScreen = true;
        }
        else
        {
            int width = Convert.ToInt32(Screen.mainWindowDisplayInfo.width/1.5f);
            int height = Convert.ToInt32(Screen.mainWindowDisplayInfo.height/1.5f);
            Screen.SetResolution(width,height,false);
        }

    }


    float RGBValue(float value)
    {
        return value / 255f;
    }


    void InteractedOptions(bool cond)
    {
        startBut.GetComponent<Button>().interactable = !cond;
        optionBut.GetComponent<Button>().interactable = !cond;
        creditsBut.GetComponent<Button>().interactable = !cond;

        startBut.GetComponent<ButtonScript>().collideAble = !cond;
        optionBut.GetComponent<ButtonScript>().collideAble = !cond;
        creditsBut.GetComponent<ButtonScript>().collideAble = !cond;


        options = cond;

    }


    public void Credit()
    {
        sceneManagement.TransScene("Credits");
    }


    Color RGBValue(float r,float g,float b)
    {
        return new Color(r/255,g/255,b/255);
    }

    
}
