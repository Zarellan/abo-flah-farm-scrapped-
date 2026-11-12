using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;
using System;

public class Hoe : MonoBehaviour
{
    // Start is called before the first frame update

    Tween hoeTween;
    Tween hoeTween2;

    Tween aboFlah;
    Tween aboFlah2;

    Tween aboflahDamage;


    ClickablePlace clickablePlace;


    [SerializeField]
    ParticleSystem part;
    [SerializeField]
    ParticleSystem partRare;



    [SerializeField]
    Sprite[] hoes;


    float multiply;

    GlobalAudio audioGlob;


    public static bool canShove = true;

    int cropRed = 0;

    public bool isRed = false;

    [SerializeField] GameObject background;

    [SerializeField] GameObject golfTest;

    [SerializeField] RectTransform numText;
    void Awake()
    {
        //PlayerPrefs.DeleteAll();
    }
    void Start()
    {

        hoeTween = new Tween();
        hoeTween2 = new Tween();


        aboFlah = new Tween();
        aboFlah2 = new Tween();

        clickablePlace = GameObject.FindWithTag("ClickablePlace").GetComponent<ClickablePlace>();

        if (PlayerPrefs.GetInt("cropsInc", 0) == 0)
        {
            PlayerPrefs.SetInt("cropsInc", 1);
        }

        audioGlob = GameObject.FindWithTag("GlobalAudio").GetComponent<GlobalAudio>();

        /*int num = 1000000;
        print((num.ToString().Length-1)/3);
        StartCoroutine(Timer.timer(2,()=>
        Tween.Custom(startValue:0,endValue:1000000,5,ease:Ease.Linear,onValueChange:newvalue => {

            //PlayerPrefs.SetInt("crops",Mathf.FloorToInt(newvalue));
        })));*/

        //StartCoroutine(Timer.timer(1, RedCrops));
        FirstGameStart();
    }
    void FirstGameStart()
    {
        if (PlayerPrefs.GetInt("MultiplierSlave", 0) == 0)
            PlayerPrefs.SetInt("MultiplierSlave", 1);

        if (PlayerPrefs.GetString("difficulty") == "hard")
            Tween.Delay(UnityEngine.Random.Range(30,80)).OnComplete(RedCrops);
    }
    // Update is called once per frame
    void Update()
    {
        if (MultiplyTime.isMulti)
            multiply = PlayerPrefs.GetFloat("ChickenMulti");
        else
            multiply = 1;
        
        int cropsCollectSpec = (PlayerPrefs.GetInt("cropsInc") * PlayerPrefs.GetInt("Multiplier",1) + 5000)* Convert.ToInt32(multiply);
        int cropsCollect = PlayerPrefs.GetInt("cropsInc") * PlayerPrefs.GetInt("Multiplier",1) * Convert.ToInt32(multiply);

        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && clickablePlace.canClick && !ShopButton.onShop && canShove)
        {
            if (!isRed)
            {
            if (UnityEngine.Random.Range(0.0f,1.0f) < PlayerPrefs.GetFloat("GoldChance")) 
            {
                partRare.Play();
                audioGlob.PlayAudio("prize");
                PlayerPrefs.SetInt("crops",PlayerPrefs.GetInt("crops") + cropsCollectSpec);
                PlayerPrefs.SetInt("cropsRecord",PlayerPrefs.GetInt("cropsRecord") + cropsCollectSpec);
            }
            else
            {
                PlayerPrefs.SetInt("crops",PlayerPrefs.GetInt("crops") + cropsCollect);
                PlayerPrefs.SetInt("cropsRecord",PlayerPrefs.GetInt("cropsRecord") + cropsCollect);
            }

            GetComponent<AudioSource>().Play();
            hoeTween.Stop();
            hoeTween2.Stop();
            hoeTween = Tween.Rotation(transform,new Vector3(0,0,-130),0.05f,Ease.Linear).OnComplete(HoeTween2)/*OnComplete(Omak() => hoeTween2 = Tween.Rotation(transform,new Vector3(0,0,-82),0.3f,Ease.Linear))*/;

            aboFlah.Stop();
            aboFlah2.Stop();
            aboFlah = Tween.ScaleY(GameObject.FindWithTag("Player").transform,0.80f,0.05f,Ease.Linear).OnComplete(FlahTween2);

            part.Play(); 
            }
            else
            {
                PlayerPrefs.SetInt("crops",Mathf.FloorToInt(PlayerPrefs.GetInt("crops") / 3));
                PlayerPrefs.SetInt("cropsRecord",Mathf.FloorToInt(PlayerPrefs.GetInt("crops") / 3));

                hoeTween.Stop();
                hoeTween2.Stop();
                hoeTween = Tween.Rotation(transform,new Vector3(0,0,-130),0.05f,Ease.Linear).OnComplete(HoeTween2)/*OnComplete(Omak() => hoeTween2 = Tween.Rotation(transform,new Vector3(0,0,-82),0.3f,Ease.Linear))*/;

                aboFlah.Stop();
                aboFlah2.Stop();
                aboFlah = Tween.ScaleY(GameObject.FindWithTag("Player").transform,0.80f,0.05f,Ease.Linear).OnComplete(FlahTween2);

                AudioUtil.PlayAudio("damage"+UnityEngine.Random.Range(1,2));
                
                GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>().color = new Color(255/255,155/255,155/255);
                if (aboflahDamage.isAlive)
                    aboflahDamage.Stop();
                aboflahDamage = Tween.Color(GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>(),Color.white,0.3f,Ease.Linear);

                if (PlayerPrefs.GetInt("crops") == 0)
                    SecretEnding();
            }   


        }
        
        GetComponent<SpriteRenderer>().sprite = hoes[PlayerPrefs.GetInt("level Hoe")];

            if (Input.GetKeyDown(KeyCode.Escape) && GameObject.FindWithTag("ShopHUD").GetComponent<ShopFunction>().enabled)
                GameObject.FindWithTag("ShopHUD").SetActive(false);

            //GameObject.FindWithTag("Transition").GetComponent<Transition>().TransScene("MainMenu",0.3f);
    }

    void HoeTween2()
    {
        hoeTween2 = Tween.Rotation(transform,new Vector3(0,0,-82),0.3f,Ease.OutCirc);
    }
    void FlahTween2()
    {
        aboFlah2 = Tween.ScaleY(GameObject.FindWithTag("Player").transform,0.86f,0.3f,Ease.OutCirc);
    }

    void OnMouseDown()
    {

    }

    void RedCrops()
    {
        if (cropRed < 3)
        {
            background.GetComponent<SpriteRenderer>().color = new Color(255/255,155/255,155/255);
            Tween.Color(background.GetComponent<SpriteRenderer>(),Color.white,0.3f,Ease.Linear);
            AudioUtil.PlayAudio("on beat");
            StartCoroutine(Timer.timer(0.5f,RedCrops));
        }
        else
        {
            background.GetComponent<SpriteRenderer>().color = new Color(255/255,155/255,155/255);
            StartCoroutine(Timer.timer(3f,RemoveRedCrops));
            isRed = true;
            cropRed = 0;
        }
        cropRed++;
    }

    void RemoveRedCrops()
    {
        Tween.Color(background.GetComponent<SpriteRenderer>(),Color.white,0.3f,Ease.Linear);
        isRed = false;
    }

    void SecretEnding()
    {
        SoundtrackUtil.GetAudio().Stop();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Cutscene");
    }

}
