using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAppear : MonoBehaviour
{
    [SerializeField]
    GameObject chicken;

    public static int timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("level Chicken") > 0)
        {
            StartCoroutine(Timer.timer(Random.Range(40,80),ChickenTime));
        }
        //ChickenTime();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartTime()
    {
        timeLeft = 30;
        StartCoroutine(Timer.timer(1,Decres));
    }

    void Decres()
    {
        if (timeLeft <=0)
            return;

        timeLeft-=1;
        StartCoroutine(Timer.timer(1,Decres));
    }

    public void ChickenTimer(float min,float max)
    {
        StartCoroutine(Timer.timer(Random.Range(min,max),ChickenTime));
    }


    void ChickenTime()
    {
        if (Random.Range(0,2)==0)
            //Instantiate(chicken,new Vector2(-10,-3.59f),Quaternion.identity);
            Instantiate(chicken,new Vector2(-10,-3.59f),Quaternion.identity,this.transform);
        else
            //Instantiate(chicken,new Vector2(10,-3.59f),Quaternion.identity);
            Instantiate(chicken,new Vector2(10,-3.59f),Quaternion.identity,this.transform);

        StartCoroutine(Timer.timer(Random.Range(30,80),ChickenTime));
    }



    public void Purchased()
    {
        ChickenTime();
    }
    
}
