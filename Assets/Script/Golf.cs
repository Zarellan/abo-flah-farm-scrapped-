using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;
public class Golf : MonoBehaviour
{
    bool collected = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     if (entered)
        //         print("conf");
        // }


        if (!collected && MouseCollideUtil.CollisionDetectorMouse(Camera.main.ScreenToWorldPoint(Input.mousePosition), gameObject.transform, GetComponent<BoxCollider2D>()) && Input.GetMouseButtonDown(0))
        {
            PlayerPrefs.SetInt("pinBalls", PlayerPrefs.GetInt("pinBalls") + 1);
            NumberSpawner.instance.SpawnNumber(gameObject.transform);
            collected = true;
            Tween.Alpha(gameObject.GetComponent<SpriteRenderer>(), 0, 0.5f, Ease.Linear).OnComplete(() =>
            {
                Destroy(this.gameObject);
            });
            Tween.Scale(gameObject.transform, transform.localScale * 1.2f, 0.5f, Ease.Linear);
        }

    }

    
}
