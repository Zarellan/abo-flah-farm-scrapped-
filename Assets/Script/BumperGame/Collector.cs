using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] PrizeType prizeType;
    [SerializeField] ParticleSystem particle;
    [SerializeField] SpriteRenderer shineSprite;
    void OnTriggerEnter2D(Collider2D col)
    {
        prizeType.prize[prizeType.rng].prize.Invoke();
        particle.Play();
        Tween.Alpha(shineSprite,1,0.5f,Ease.Linear).OnComplete(()=>
        {
            Tween.Delay(2).OnComplete(()=>
            {
                Tween.Alpha(shineSprite, 0, 0.5f, Ease.Linear);
            });
        });
    }

}
