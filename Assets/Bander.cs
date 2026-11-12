using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bander : MonoBehaviour
{
    [SerializeField] BanderPosition[] bander;

    [SerializeField] Item item;

    string[] speciality = {"make crops worth 3x","make your slaves works faster","just make him fit","done"};
    // Start is called before the first frame update
    void Start()
    {
        ChangeBander(PlayerPrefs.GetInt("level Bander")-1);

        if (PlayerPrefs.GetInt("level Bander") > 0)
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpecialsBander()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        ChangeBander(PlayerPrefs.GetInt("level Bander",0));

        switch(PlayerPrefs.GetInt("level Bander",0))
        {
            case 0:{
                item.CaveItem("Bander").description = "make your slaves works faster";
                PlayerPrefs.SetInt("Multiplier",PlayerPrefs.GetInt("Multiplier",1) + 2);
            }break;
			case 1:{
                item.CaveItem("Bander").description = "yeah just make him fit";
                // PlayerPrefs.SetInt("MultiplierSlave",PlayerPrefs.GetInt("MultiplierSlave",1) + 3);
            }break;
            case 2:{
                    
            }
            break;
        }
    }


    void ChangeBander(int level)
    {
        if (level < 0)
        {
        GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
        GetComponent<SpriteRenderer>().sprite = bander[level].sprite;

        transform.position = bander[level].position;

        transform.localScale = bander[level].scale;
        }


        item.CaveItem("Bander").description = speciality[level+1];
    }
}

[System.Serializable]
public class BanderPosition
{
    public Sprite sprite;
    public Vector2 position;

    public Vector2 scale;
}
