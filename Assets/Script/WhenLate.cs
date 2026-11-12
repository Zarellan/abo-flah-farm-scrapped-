using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenLate : MonoBehaviour
{


    DateTime date;
    [SerializeField]
    GameObject skulls;

    [SerializeField]
    GameObject[] toHide;

    SoundTrackScript music;
    // Start is called before the first frame update
    void Start()
    {

        music = GameObject.FindWithTag("SoundTrack").GetComponent<SoundTrackScript>();


        if (!PlayerPrefs.HasKey("dateAndTime"))
            CameEarly();
        else
            {
                if (DateTime.Now > DateTime.Parse(PlayerPrefs.GetString("dateAndTime")).AddDays(3))// if player didn't attend for 3 days
                {
                    print("is late");
                    StartCoroutine(Timer.timer(0.01f,()=> music.SetVolume(0)));

                    for (int i = 0;i < toHide.Length;i++)
                        toHide[i].SetActive(false);
                    

                    Hoe.canShove = false;
                }
                else
                {
                    CameEarly();
                }



            }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void CameEarly()
    {
        PlayerPrefs.SetString("dateAndTime",DateTime.Now.ToString()); // time will reset

        skulls.SetActive(false);
    }
}
