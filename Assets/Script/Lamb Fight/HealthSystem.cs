using PrimeTween;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public static HealthSystem instance;
    [SerializeField] GameObject[] healthHearts;
    [SerializeField] GameObject heartPrefab;
    int health = 20;
    int maxHealth = 20;
    Tween redColor;
    Tween vis1, vis2, vis3;
    void Awake()
    {
        if (instance == null)
            instance = this;
        healthHearts = new GameObject[10];
        for (int i = 0; i < 10; i++)
        {
            healthHearts[i] = Instantiate(heartPrefab, transform);
            healthHearts[i].GetComponent<RectTransform>().anchoredPosition += new Vector2(i * 50, 0);
            healthHearts[i].name = "heart " + (i + 1);
        }
        heartPrefab.SetActive(false);
    }
    void Start()
    {
        LoadHealths();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Damage(1);
        }
    }

    public void Damage(int damage)
    {
        health -= damage;
        Tween.Rotation(Camera.main.transform, startValue: Quaternion.Euler(0, 0, 5), endValue: Quaternion.Euler(0, 0, 0), 0.3f, Ease.OutCirc);
        FlahMovement.instance.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        if (redColor.isAlive)
            redColor.Stop();
        redColor = Tween.Delay(0.3f).OnComplete(() =>
        {
            FlahMovement.instance.GetComponent<SpriteRenderer>().color = Color.white;
        });
        for (int i = 0; i < 10; i++)
        {
            if (health % 2 == 1 && i == health / 2)
                healthHearts[i].GetComponent<Heart>().SetHealth(true, true);
            else
            {
                if (i < health / 2)
                    healthHearts[i].GetComponent<Heart>().SetHealth(true);
                else
                    healthHearts[i].GetComponent<Heart>().SetHealth(false);
            }
            healthHearts[i].GetComponent<Heart>().VisualHeartel();
        }
    }

    public void LoadHealths()
    {
        for (int i = 0; i < 10; i++)
        {
            if (health % 2 == 1 && i == health / 2)
                healthHearts[i].GetComponent<Heart>().SetHealth(true, true);
            else
            {
                if (i < health / 2)
                    healthHearts[i].GetComponent<Heart>().SetHealth(true);
                else
                    healthHearts[i].GetComponent<Heart>().SetHealth(false);
            }
        }
    }
}
