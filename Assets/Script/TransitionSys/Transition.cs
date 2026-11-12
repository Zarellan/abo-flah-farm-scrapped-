using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField] Material transitionObj;

    [SerializeField] GameObject black;

    string sceneNameToLoad;
    AsyncOperation operationScene;
    // Start is called before the first frame update
    void Start()
    {

        DontDestroyOnLoad(this);
        GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (operationScene != null)
        {

        }
    }


    void StartTrans()
    {
        Tween.Custom(0, 120.31f, duration: 0.8f,ease:Ease.InQuart, onValueChange: newVal => {

            transitionObj.SetFloat("_hello",newVal);

            StartCoroutine(Timer.timer(0.6f,()=>{
                Tween.Alpha(black.GetComponent<Image>(),1,0.2f,Ease.Linear);
            }));
        });
        Tween.Custom(0, 1f, duration: 0.8f,ease:Ease.InQuart, onValueChange: newVal => {

            transitionObj.SetFloat("_rotation",newVal);
        });

        StartCoroutine(Timer.timer(2,TweenOut));
    }

    void TweenOut()
    {
        Tween.Alpha(black.GetComponent<Image>(),0,0.2f,Ease.Linear);
    
        Tween.Custom(120.31f, 0f, duration: 0.8f,ease:Ease.OutCirc, onValueChange: newVal => {

            transitionObj.SetFloat("_hello",newVal);

        });
        Tween.Custom(1, 0f, duration: 0.8f,ease:Ease.OutCirc, onValueChange: newVal => {

            transitionObj.SetFloat("_rotation",newVal);
        });
    }


    public void TransScene(string sceneName)
    {
        //Tween.UIFillAmount(transition,1,duration,Ease.OutSine);
        Tween.Custom(0, 120.31f, duration: 0.8f,ease:Ease.InQuart, onValueChange: newVal => {

            transitionObj.SetFloat("_hello",newVal);

        });
        Tween.Custom(0, 1f, duration: 0.8f,ease:Ease.InQuart, onValueChange: newVal => {

            transitionObj.SetFloat("_rotation",newVal);
        });
        StartCoroutine(Timer.timer(0.6f,()=>{
            Tween.Alpha(black.GetComponent<Image>(),1,0.2f,Ease.Linear);
        }));

        StartCoroutine(Timer.timer(0.8f + 0.15f,()=> SceneChange(sceneName)));



    }

    void SceneChange(string sceneName)
    {
        operationScene = SceneManager.LoadSceneAsync(sceneName);
        operationScene.allowSceneActivation = false;
        SceneManager.LoadScene(sceneName);
        operationScene.allowSceneActivation = true;
    
        Tween.Custom(120.31f, 0f, duration: 0.8f,ease:Ease.OutCirc, onValueChange: newVal => {

            transitionObj.SetFloat("_hello",newVal);

        });
        Tween.Custom(1, 0f, duration: 0.8f,ease:Ease.OutCirc, onValueChange: newVal => {

            transitionObj.SetFloat("_rotation",newVal);
        });

        Tween.Alpha(black.GetComponent<Image>(),0,0.2f,Ease.Linear);
    }

}
