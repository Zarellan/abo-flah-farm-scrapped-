using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class PinBall : MonoBehaviour
{
    public static PinBall instance;
    GameObject camera;

    bool ballThrown = true;

    Tween ballTween;

    public static bool canQuit = true;

    public GameObject quitButton;

    void Awake()
    {
        if (instance == null)
            instance = this;

        quitButton = GameObject.FindWithTag("QuitButtonPinball");
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindWithTag("MainCamera");
        Item.place = "farm";

        GameBegin();


    }

    public void GameBegin()
    {
        transform.position = new Vector2(0, 3.72f);
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        BringBackQuit();
        if (PlayerPrefs.GetInt("pinBalls", 0) > 0)
        {
            ballTween = Tween.PositionX(transform, startValue: -5.55f, endValue: 4.25f, 0.3f, Ease.Linear, cycleMode: CycleMode.Yoyo, cycles: -1);
            ballThrown = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsPointerOverUIObject(quitButton.GetComponent<Button>()) && Input.GetMouseButtonDown(0) && !ballThrown)
        {
            ballTween.Stop();
            ballThrown = true;
            GetComponent<Rigidbody2D>().gravityScale = 1;
            PlayerPrefs.SetInt("pinBalls", PlayerPrefs.GetInt("pinBalls", 0) - 1);
            canQuit = false;
            quitButton.GetComponent<Button>().interactable = false;
            Tween.UIAnchoredPositionX(quitButton.GetComponent<RectTransform>(), 1120, 0.3f, Ease.InCirc);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            ToTheFarm();
    }


    void FixedUpdate()
    {
        Vector3 velocity = Vector3.zero;
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, new Vector3(-0.6f, Mathf.Clamp(transform.position.y, -9.29f, -0.2f), -10), ref velocity, 0.03f);
    }


    public void ToTheFarm()
    {
        if (canQuit)
        {
            ballThrown = true; // not actually thrown
            GameObject.FindWithTag("Transition").GetComponent<Transition>().TransScene("Farm");
        }
    }
    public void bringBackQuit()
    {
        canQuit = true;
        quitButton.GetComponent<Button>().interactable = true;
        Tween.UIAnchoredPositionX(quitButton.GetComponent<RectTransform>(), 800, 0.3f, Ease.OutCirc);
    }
    public static void BringBackQuit()
    {
        instance.bringBackQuit();
    }
    void OnCollisionEnter2D(Collision2D col)
    {

    }
    
    bool IsPointerOverUIObject(Button targetButton)
    {
        // Raycast to check if the pointer is over the button
        return RectTransformUtility.RectangleContainsScreenPoint(
            targetButton.GetComponent<RectTransform>(),
            Input.mousePosition,
            Camera.main);
    }
}
