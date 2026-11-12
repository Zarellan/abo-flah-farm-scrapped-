using System;
using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Escaped : MonoBehaviour
{
    [SerializeField]
    TMP_Text escapedText;
    [SerializeField]
    TMP_Text cropsCollected;
    [SerializeField]
    TMP_Text chickenKilled;
    [SerializeField]
    TMP_Text gamePlayDuration;
    [SerializeField]
    Button credit;
    [SerializeField]
    Button mainMenu;

    int cropsNumber;
    float number = 0;
    
    //tween
    Tween cropScale;


    GlobalAudio sound;



    Transition sceneManagement;
    // Start is called before the first frame update
    void Start()
    {
        Tween.UIAnchoredPosition(escapedText.GetComponent<RectTransform>(),new Vector2(0,342),0.4f,Ease.OutSine);

        StartCoroutine(Timer.timer(1,()=> Tween.UIAnchoredPosition(cropsCollected.GetComponent<RectTransform>(),new Vector2(-460.44f,-183.41f),0.4f,Ease.OutCirc)));
        StartCoroutine(Timer.timer(1.5f,CountCrops));



        sound = GameObject.FindWithTag("GlobalAudio").GetComponent<GlobalAudio>();



        sceneManagement = GameObject.FindWithTag("Transition").GetComponent<Transition>();
    }

    // Update is called once per frame
    void Update()
    {
        //number += Time.deltaTime / 1.5f;
        //cropsCollected.text = "Crops collected: "+ Mathf.Floor(Mathf.Lerp(0,3000,number));

    }

    void CountCrops()
    {
        if (number <= 1)
        {
        number += Time.deltaTime * 10;
        cropsNumber = Mathf.FloorToInt(Mathf.Lerp(0,PlayerPrefs.GetInt("cropsRecord"),number));


        cropsCollected.text = "Crops collected: "+ cropsNumber;

        StartCoroutine(Timer.timer(0.03f,CountCrops));

        cropsCollected.transform.localScale = new Vector2(0.95f,0.95f);

        cropScale.Stop();
        cropScale = Tween.Scale(cropsCollected.transform,new Vector2(1,1),0.4f,Ease.OutCirc);

        
        sound.PlayAudio("scrollMenu",0.7f);
        }
        else
        {

            cropsCollected.GetComponent<PlayParticle>().PlayPartic();
            sound.PlayAudio("confirmMenu");

            StartCoroutine(Timer.timer(1,()=> Tween.UIAnchoredPosition(chickenKilled.GetComponent<RectTransform>(),new Vector2(-460.44f,-316),0.4f,Ease.OutCirc)));
            StartCoroutine(Timer.timer(1.5f,CountChicken));

        }
    }


    void CountChicken()
    {
        int chickenKills = PlayerPrefs.GetInt("chickenKilled",0);

        
        chickenKilled.text = "Chicken Killed: " + (chickenKills > 0? chickenKills: "pacifist");

        chickenKilled.GetComponent<PlayParticle>().PlayPartic();
        sound.PlayAudio("confirmMenu");

        chickenKilled.transform.localScale = new Vector2(0.95f,0.95f);

        Tween.Scale(chickenKilled.transform,new Vector2(1,1),0.4f,Ease.OutCirc);

        StartCoroutine(Timer.timer(1,()=> Tween.UIAnchoredPosition(gamePlayDuration.GetComponent<RectTransform>(),new Vector2(-386.9501f,-435),0.4f,Ease.OutCirc)));
        StartCoroutine(Timer.timer(1.5f,GameDuration));

    }


    void GameDuration()
    {

        gamePlayDuration.text = "GamePlay Duration: " + PlayerPrefs.GetString("GamePlayedTime");

        gamePlayDuration.GetComponent<PlayParticle>().PlayPartic();
        sound.PlayAudio("confirmMenu");

        gamePlayDuration.transform.localScale = new Vector2(0.95f,0.95f);

        Tween.Scale(gamePlayDuration.transform,new Vector2(1,1),0.4f,Ease.OutCirc);

        StartCoroutine(Timer.timer(1.5f,ButtonAppears));

    }

    void ButtonAppears()
    {
        Tween.UIAnchoredPositionX(credit.GetComponent<RectTransform>(),581.24f,0.4f,Ease.OutCirc);
        Tween.UIAnchoredPositionX(mainMenu.GetComponent<RectTransform>(),581.24f,0.4f,Ease.OutCirc);
    }


    string FormatNumber(int num)
    {
        if (num > 1000000)
        {
            return (num / 1000000).ToString() + " M";
        }
        else if (num > 1000)
        {
            return (num / 1000).ToString() + " K";
        }
        else
        {
            return num.ToString();
        }
        
            
        

    }



    public void MainMenu()
    {
        sceneManagement.TransScene("MainMenu");
    }

    public void Credit()
    {
        sceneManagement.TransScene("Credits");
    }

    
}
