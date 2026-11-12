using UnityEngine;
using PrimeTween;
using TMPro;
using UnityEngine.UI;

public class Arcade : MonoBehaviour
{
    [SerializeField] GameObject button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void OnButton()
	{
		if (button.activeSelf)
		{
            button.GetComponent<Button>().enabled = true;
            Tween.Scale(button.GetComponent<RectTransform>(),new Vector2(1,1),0.3f,Ease.OutCirc);
		}
	}

	public void OffButton()
	{
		if (button.activeSelf)
		{
            button.GetComponent<Button>().enabled = false;
            Tween.Scale(button.GetComponent<RectTransform>(),new Vector2(0,0),0.3f,Ease.InBack);
		}
	}
      public void TransitionToPinball()
      {
            GameObject.FindGameObjectWithTag("Transition").GetComponent<Transition>().TransScene("PinBall");
      }
}
