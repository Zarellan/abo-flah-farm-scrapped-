using PrimeTween;
using UnityEngine;

public class Bumber : MonoBehaviour
{
    Vector2 defScale;
    bool canTouch = true; // avoid spam touches
    void Start()
    {
        defScale = transform.localScale;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (canTouch && col.collider.name == "Ball")
        {
            print("touched");
            AudioUtil.PlayAudio("pinball");
            Tween.Scale(transform, startValue: defScale.x * 1.3f, defScale.x, 0.3f, Ease.OutCirc).OnComplete(()=>
            {
                canTouch = true;
            });

            canTouch = false;
        }
    }
}
