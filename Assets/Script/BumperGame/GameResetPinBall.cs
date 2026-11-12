using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

public class GameResetPinBall : MonoBehaviour
{
    [SerializeField] PinBall pinBallSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D col)
    {

        Tween.Delay(1.95f).OnComplete(pinBallSystem.GameBegin);

    }

}
