using System;
using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TransitionOld : MonoBehaviour
{

    Image transition;

    // Start is called before the first frame update
    void Start()
    {
        transition = GetComponentInChildren<Image>();
        DontDestroyOnLoad(this);

        GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }


    public void TransScene(string sceneName,float duration)
    {
        Tween.UIFillAmount(transition,1,duration,Ease.OutSine);
        StartCoroutine(Timer.timer(duration + 0.1f,()=> SceneChange(sceneName,duration)));
    }

    void SceneChange(string sceneName,float duration)
    {
        SceneManager.LoadScene(sceneName);
        StartCoroutine(Timer.timer(duration,()=> Tween.UIFillAmount(transition,0,duration,Ease.InSine)));

    }
}
