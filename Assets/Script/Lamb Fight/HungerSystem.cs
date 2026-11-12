using UnityEngine;

public class HungerSystem : MonoBehaviour
{
    public static HungerSystem instance;
    [SerializeField] GameObject[] hungerMeats;
    [SerializeField] GameObject hungerPrefab;
    int hunger = 20;
    int maxhunger = 20;
    public int saturation = 100;
    int maxSaturation = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
            instance = this;
        hungerMeats = new GameObject[10];
        for (int i = 0; i < 10; i++)
        {
            hungerMeats[i] = Instantiate(hungerPrefab, transform);
            hungerMeats[i].GetComponent<RectTransform>().anchoredPosition -= new Vector2(i * 50, 0);
            hungerMeats[i].name = "hunger " + (i + 1);
        }
        hungerPrefab.SetActive(false);
        LoadHungers();
        Consume(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Consume(1);
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            Consume(-1);
        }
    }
    public void SaturationConsume(int cons)
    {
        saturation -= cons;
        if (saturation <= 0)
        {
            saturation = 100;
            Consume(1);
        }
    }
    public void Consume(int hung)
    {
        hunger -= hung;
        hunger = Mathf.Clamp(hunger,0,20);
        for (int i = 0; i < 10; i++)
        {
            if (hunger % 2 == 1 && i == hunger / 2)
                hungerMeats[i].GetComponent<Hunger>().SetHunger(true, true);
            else
            {
                if (i < hunger / 2)
                    hungerMeats[i].GetComponent<Hunger>().SetHunger(true);
                else
                    hungerMeats[i].GetComponent<Hunger>().SetHunger(false);
            }
            //hungerMeats[i].GetComponent<Heart>().VisualHeartel();
        }
    }

    public void LoadHungers()
    {
        for (int i = 0; i < 10; i++)
        {
            if (hunger % 2 == 1 && i == hunger / 2)
                hungerMeats[i].GetComponent<Hunger>().SetHunger(true, true);
            else
            {
                if (i < hunger / 2)
                    hungerMeats[i].GetComponent<Hunger>().SetHunger(true);
                else
                    hungerMeats[i].GetComponent<Hunger>().SetHunger(false);
            }
        }
    }
}
