using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplyTime : MonoBehaviour
{
    public static bool isMulti = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = $"x{PlayerPrefs.GetFloat("ChickenMulti")} : {ChickenAppear.timeLeft.ToString()}s";
        
        if (ChickenAppear.timeLeft <= 0)
        {
            if (isMulti)
                GameObject.FindWithTag("ChickenSpawner").GetComponent<ChickenAppear>().ChickenTimer(30,70);

            GetComponent<Text>().color = new Color(1,1,1,0);
            isMulti = false;
        }   
        else
        {
            isMulti = true;
            GetComponent<Text>().color = new Color(1,1,1,1);

        }
        
    }
}
