using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using PrimeTween;
using UnityEngine;

public class Chicken : MonoBehaviour
{

    [SerializeField]
    Sprite rawChicken;

    bool inside = false;

    float timer;

    GlobalAudio audioGlop;

    int health = 3;

    [SerializeField]
    AudioClip[] hurtSounds;


    bool isDead = false;

    float direction;

    float speed;
    // Start is called before the first frame update
    void Start()
    {
        audioGlop = GameObject.FindWithTag("GlobalAudio").GetComponent<GlobalAudio>();

        if (transform.position.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            direction = 1;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            direction = -1;
        }

        speed = Random.Range(1,4);

        GetComponent<SpriteRenderer>().color = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>().color;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
            transform.position = new Vector2(transform.position.x + speed * direction * Time.deltaTime, transform.position.y);


        if (inside && Input.GetMouseButtonDown(0) && !isDead)
        {
            print("damaged");
            timer = 0;
            GetComponent<SpriteRenderer>().color = new Color(RGBValue(255f), RGBValue(50f), RGBValue(50f));

            health -= 1;
            GetComponent<AudioSource>().clip = hurtSounds[Random.Range(0, hurtSounds.Length)];
            GetComponent<AudioSource>().Play();

        }

        timer += Time.deltaTime;

        IsDead();

        TimerFinished();

        if (!inside && MouseCollideUtil.CollisionDetectorMouse(Camera.main.ScreenToWorldPoint(Input.mousePosition)
                , gameObject.transform, GetComponent<BoxCollider2D>()))
            inside = true;
        else if (inside && !MouseCollideUtil.CollisionDetectorMouse(Camera.main.ScreenToWorldPoint(Input.mousePosition)
                , gameObject.transform, GetComponent<BoxCollider2D>()))
            inside = false;

    }


    void OnMouseEnter()
    {
        inside = true;
    }

    void OnMouseExit()
    {
        inside = false;
    }

    void TimerFinished()
    {
        if (timer > 0.20 && !isDead)
            GetComponent<SpriteRenderer>().color = Color.white;
    }

    

    float RGBValue(float value)
    {
        return value / 255f;
    }


    void IsDead()
    {
        if (health <= 0 && !isDead)
        {
            isDead = true;

            Tween.Rotation(transform,new Vector3(0,0,-90),0.6f,Ease.OutSine);

            StartCoroutine(Timer.timer(1,Freshed));

            PlayerPrefs.SetInt("chickenKilled",PlayerPrefs.GetInt("chickenKilled")+1);

        }
    }

    void Freshed()
    {
        Vector2 playerPos =  GameObject.FindWithTag("Player").transform.position;

        transform.eulerAngles = new Vector3(0,0,0);
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<SpriteRenderer>().sprite = rawChicken;
        Tween.Position(transform,new Vector2(playerPos.x,playerPos.y),0.7f,Ease.InSine).OnComplete(()=>{Destroy(gameObject);audioGlop.PlayAudio("eat");StartBonus();});
    }

    void StartBonus()
    {
        GameObject.FindWithTag("ChickenSpawner").GetComponent<ChickenAppear>().StartTime();
    }

    bool CollisionDetector(Transform pla1,  Transform pla2)
    {
        return pla2.position.x + pla2.localScale.x >= pla1.position.x &&
            pla2.position.x <= pla1.position.x + pla1.localScale.x &&
            pla2.position.y + pla2.localScale.y >= pla1.position.y &&
            pla2.position.y <= pla1.position.y + pla1.localScale.y;
    }

}
