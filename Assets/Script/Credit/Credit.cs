using UnityEngine;

public class Credit : MonoBehaviour
{

    [SerializeField]
    CreditVar[] credits;

    [SerializeField]
    GameObject prefab;
    [SerializeField]
    GameObject prefab2;

    [SerializeField]
    float arrowSpeed;
    [SerializeField]
    float scrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0;i<credits.Length;i++)
        {
            if (credits[i].sprite != null)
            {
                prefab = Instantiate(prefab,this.transform);
                prefab.name = credits[i].name;
                prefab.GetComponent<CreditChild>().text = credits[i].name;
                prefab.GetComponent<CreditChild>().img = credits[i].sprite;
                prefab.GetComponent<CreditChild>().altImg = credits[i].altSprite;
                prefab.GetComponent<CreditChild>().urlLink = credits[i].urlLink;
            }
            else
            {
                prefab2 = Instantiate(prefab2,this.transform);
                prefab2.name = credits[i].name;
                prefab2.GetComponent<CreditChild>().text = credits[i].name;
                prefab2.GetComponent<CreditChild>().urlLink = credits[i].urlLink;
            }
            //prefab.name = name;

        }

        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x,280 * credits.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // there was actually simpler solution but now it's late :\
            GetComponent<RectTransform>().position = new Vector2(transform.position.x,transform.position.y + Input.GetAxis("Mouse ScrollWheel") * scrollSpeed);
        

        if (Input.GetKey(KeyCode.DownArrow))
            ChangePos(GetPos("x"),GetPos("y") - arrowSpeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.UpArrow))
            ChangePos(GetPos("x"),GetPos("y") + arrowSpeed * Time.deltaTime);
        

        
    }

    void ChangePos(float x,float y)
    {
        GetComponent<RectTransform>().position = new Vector2(x,y);
    }

    float GetPos(string type)
    {
        if (type == "x")
            return GetComponent<RectTransform>().position.x;
        else if (type == "y")
            return GetComponent<RectTransform>().position.y;

        return GetComponent<RectTransform>().position.x;
    }
}

[System.Serializable]
public class CreditVar
{
    public string name;
    public Sprite sprite;
    public Sprite altSprite;

    public string urlLink;

}


