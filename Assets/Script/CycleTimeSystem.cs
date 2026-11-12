using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;
public class CycleTimeSystem : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] sprites;


    SoundTrackScript soundTrack;


    void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {

        if (!PlayerPrefs.HasKey("isNight"))
            PlayerPrefs.SetString("isNight","false");

        soundTrack = GameObject.FindWithTag("SoundTrack").GetComponent<SoundTrackScript>();

        StartCoroutine(Timer.timer(1,IncSec));

        StartCoroutine(Timer.timer(0.0001f,TimeIs));// due to layering instance problem

    }

    // Update is called once per frame
    void Update()
    {

    }


    float RGBValue(float value)
    {
        return value / 255f;
    }


    void IncMin()
    {
        PlayerPrefs.SetInt("minuteCycle",PlayerPrefs.GetInt("minuteCycle") + 1);
        PlayerPrefs.SetInt("secondCycle",0);
        

        if (PlayerPrefs.GetInt("minuteCycle") == 6)
            SetTime();



    }

    void SetTime()
    {
        if (PlayerPrefs.GetString("isNight") == "false")
            {
                Night();
                PlayerPrefs.SetString("isNight","true");
            }
        else if (PlayerPrefs.GetString("isNight") == "true")
            {
                Day();
                PlayerPrefs.SetString("isNight","false");
            }

        PlayerPrefs.SetInt("minuteCycle",0);
    }

    void TimeIs()
    {
        if (PlayerPrefs.GetString("isNight") == "false")
            {
                if (PlayerPrefs.GetString("CopyRight") == "false")
                    soundTrack.PlayMusic("sans");
                else
                    soundTrack.PlayMusic("you gotta move");



                for (int i = 0;i < sprites.Length;i++)
                    sprites[i].color = new Color(RGBValue(255),RGBValue(255),RGBValue(255));
            }
        else if (PlayerPrefs.GetString("isNight") == "true")
            {
                soundTrack.PlayMusic("Start a cul");

                for (int i = 0;i < sprites.Length;i++)
                    sprites[i].color = new Color(RGBValue(84),RGBValue(79),RGBValue(128));

            }
    }

    void IncSec()
    {
        PlayerPrefs.SetInt("secondCycle",PlayerPrefs.GetInt("secondCycle") + 1);
        if (PlayerPrefs.GetInt("secondCycle") == 60)
            IncMin();

        StartCoroutine(Timer.timer(1,IncSec));
    }

    void Day()
    {
        if (PlayerPrefs.GetString("CopyRight") == "true")
            soundTrack.PlayMusic("you gotta move");
        else
            soundTrack.PlayMusic("sans");
        
        for (int i = 0;i < sprites.Length;i++)
            Tween.Color(sprites[i],new Color(RGBValue(255),RGBValue(255),RGBValue(255)),1,Ease.Linear);

    }

    void Night()
    {

        soundTrack.PlayMusic("Start a cul");
        
        for (int i = 0;i < sprites.Length;i++)
            Tween.Color(sprites[i],new Color(RGBValue(84),RGBValue(79),RGBValue(128)),1,Ease.Linear);

    }


}
