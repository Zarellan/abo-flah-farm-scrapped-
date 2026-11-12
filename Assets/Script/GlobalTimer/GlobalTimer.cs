using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTimer : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("GamePlayed",PlayerPrefs.GetFloat("GamePlayed") + Time.deltaTime);

        IncreaseTime(Mathf.FloorToInt(PlayerPrefs.GetFloat("GamePlayed")));

    }



    void IncreaseTime(int timer)
    {
        int m = Mathf.FloorToInt(timer / 60 % 60);
        int s = Mathf.FloorToInt(timer % 60);
        int h = Mathf.FloorToInt(timer / (60*60));  



        string.Format("{0:00}:{1:00}:{2:00}",h,m,s);
        PlayerPrefs.SetString("GamePlayedTime",string.Format("{0:00}:{1:00}:{2:00}",h,m,s));

    }
}
