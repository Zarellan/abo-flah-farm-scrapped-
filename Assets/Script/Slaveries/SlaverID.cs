using System;
using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlaverID : MonoBehaviour
{
	[SerializeField]
	public int idReq;

	[SerializeField]
	float speed;

	float speed2;

	Tween slaveTween;
	Tween slaveTween2;

	Tween slaveHoeTween;
	Tween slaveHoeTween2;

	float defaultScaleY;

	float defaultHoeRotationZ;


	[SerializeField]
	int increase,price;

	int level = 0;

	[SerializeField] float tax,taxInc;

	[SerializeField]
	GameObject sprite,hoe,button;
	[SerializeField] TMP_Text textPrice;
	
	[SerializeField]
	Sprite[] hoes;

	[SerializeField] Sprite superSprite;

	[SerializeField] Renderer mat;


	// Start is called before the first frame update
	void Start()
	{
		//StartCoroutine(Timer.timer(speed,Harvest));

		defaultScaleY = sprite.transform.localScale.y;


		defaultHoeRotationZ = hoe.transform.rotation.z;

		InitializeSlavery("increase","int",increase);
		InitializeSlavery("level","int",level);
		InitializeSlavery("price","int",price);
		InitializeSlavery("tax","float",tax);
		InitializeSlavery("taxInc","float",taxInc);

		textPrice.text = PlayerPrefs.GetInt("Slaver "+gameObject.name +" "+ "price").ToString() + "$";


		hoe.GetComponent<SpriteRenderer>().sprite = hoes[PlayerPrefs.GetInt("Slaver "+gameObject.name +" "+ "level")];

		if (hoes.Length-1 == PlayerPrefs.GetInt("Slaver "+gameObject.name +" "+ "level"))
			MaxLevel();


	}

	void OnEnable()
	{
		defaultScaleY = sprite.transform.localScale.y;

		speed2 = speed / PlayerPrefs.GetInt("MultiplierSlave");
		if (Hoe.canShove)
		StartCoroutine(Timer.timer(speed2,Harvest));
	}


	// Update is called once per frame
	public void Update()
	{
		//gameObject.SetActive(idReq <= PlayerPrefs.GetInt("level1"));

		GetComponentInChildren<SpriteRenderer>().color = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>().color;
	
		speed2 = speed / PlayerPrefs.GetInt("MultiplierSlave");
	}

	public void Harvest()
	{
		if (!HoeUtil.Hoe().isRed)
		{
			GetComponent<AudioSource>().Play();

			PlayerPrefs.SetInt("crops",PlayerPrefs.GetInt("crops") + (PlayerPrefs.GetInt("Slaver "+gameObject.name +" "+ "increase") * PlayerPrefs.GetInt("MultiplierSlave",1)));


			slaveTween.Stop();
			slaveTween.Stop();
			slaveTween = Tween.ScaleY(sprite.transform,defaultScaleY  / 1.1f,0.05f,Ease.Linear).OnComplete(SlaveTween2)/*OnComplete(Omak() => hoeTween2 = Tween.Rotation(transform,new Vector3(0,0,-82),0.3f,Ease.Linear))*/;

			slaveHoeTween.Stop();
			slaveHoeTween2.Stop();
			slaveHoeTween = Tween.Rotation(hoe.transform,new Vector3(0,0,-130),0.05f,Ease.Linear).OnComplete(SlaveHoeTween2)/*OnComplete(Omak() => hoeTween2 = Tween.Rotation(transform,new Vector3(0,0,-82),0.3f,Ease.Linear))*/;
		}
		StartCoroutine(Timer.timer(speed2,Harvest));
	}

	public void SlaveTween2()
	{
		slaveTween2 = Tween.ScaleY(sprite.transform,defaultScaleY,0.3f,Ease.OutCirc);
	}

	public void SlaveHoeTween2()
	{
		slaveHoeTween2 = Tween.Rotation(hoe.transform,new Vector3(0,0,-82),0.3f,Ease.OutCirc);
	}


	public void Purchased()
	{
		if (PlayerPrefs.GetInt("Slaver "+gameObject.name+" "+"price") <= PlayerPrefs.GetInt("crops") && hoes.Length-1 > PlayerPrefs.GetInt("Slaver "+gameObject.name +" "+ "level"))
		{
			PlayerPrefs.SetInt("Slaver "+gameObject.name +" "+ "increase",Mathf.FloorToInt(PlayerPrefs.GetInt("Slaver "+gameObject.name +" "+ "increase")*2.5f));

			PlayerPrefs.SetInt("Slaver "+gameObject.name +" "+ "level",PlayerPrefs.GetInt("Slaver "+gameObject.name +" "+ "level")+1);

			PlayerPrefs.SetInt("crops",PlayerPrefs.GetInt("crops") - PlayerPrefs.GetInt("Slaver "+gameObject.name +" "+ "price"));

			PlayerPrefs.SetInt("Slaver "+gameObject.name +" "+ "price",Mathf.FloorToInt(PlayerPrefs.GetInt("Slaver "+gameObject.name +" "+ "price") * PlayerPrefs.GetFloat("Slaver "+gameObject.name +" "+ "tax")));

			PlayerPrefs.SetFloat("Slaver "+gameObject.name +" "+ "tax",PlayerPrefs.GetFloat("Slaver "+gameObject.name +" "+ "tax") * PlayerPrefs.GetFloat("Slaver "+gameObject.name +" "+ "taxInc"));

			textPrice.text = PlayerPrefs.GetInt("Slaver "+gameObject.name +" "+ "price").ToString()+ "$";

			hoe.GetComponent<SpriteRenderer>().sprite = hoes[PlayerPrefs.GetInt("Slaver "+gameObject.name +" "+ "level")];

			if (hoes.Length-1 == PlayerPrefs.GetInt("Slaver "+gameObject.name +" "+ "level"))
			{
				Tween.Custom(1, 0, duration: 1, onValueChange: newVal => {
					mat.material.SetFloat("_Opacity",newVal);
				});
					AudioUtil.PlayAudio("metal audio");

				MaxLevel();
			}
			
		}
	}



		//PlayerPrefs.GetInt("Slaver "+gameObject.name +" "+ "increase");
		//PlayerPrefs.GetInt("Slaver "+gameObject.name +" "+ "price");

		//PlayerPrefs.GetFloat("Slaver "+gameObject.name +" "+ "tax");
		//PlayerPrefs.GetFloat("Slaver "+gameObject.name +" "+ "taxInc");
		


	

	void InitializeSlavery(string saveType,string dataType,float defaultNumber)
	{
		switch(dataType.ToLower())
		{
			case "int":PlayerPrefs.SetInt("Slaver "+gameObject.name +" "+ saveType,PlayerPrefs.GetInt("Slaver "+gameObject.name +" "+ saveType,(int)defaultNumber));;break;
			case "float":PlayerPrefs.SetFloat("Slaver "+gameObject.name + " " + saveType,PlayerPrefs.GetFloat("Slaver "+gameObject.name +" "+ saveType,defaultNumber));break;
		}

		
	}

	void MaxLevel()
	{
		button.SetActive(false); 
		sprite.GetComponent<SpriteRenderer>().sprite = superSprite;
	}

	public void OnButton()
	{
		if (button.activeSelf)
		{
		button.GetComponent<Button>().enabled = true;
		Tween.UIAnchoredPositionY(button.GetComponent<RectTransform>(),279,0.3f,Ease.OutCirc);
		Tween.Scale(button.GetComponent<RectTransform>(),new Vector2(1,1),0.3f,Ease.OutCirc);
		}
	}

	public void OffButton()
	{
		if (button.activeSelf)
		{
		button.GetComponent<Button>().enabled = false;
		Tween.UIAnchoredPositionY(button.GetComponent<RectTransform>(),0,0.3f,Ease.InBack);
		Tween.Scale(button.GetComponent<RectTransform>(),new Vector2(0,0),0.3f,Ease.InBack);
		}
	}
}
