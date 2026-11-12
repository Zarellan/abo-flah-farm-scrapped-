using System;
using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class Item : MonoBehaviour
{
    public static string place = "farm";
    //[SerializeField] ItemVar[] item;

    [SerializeField] List<ItemVar> item = new List<ItemVar>();
    [SerializeField] List<ItemVar> itemCave = new List<ItemVar>();

    [SerializeField] Text itemTitle,itemDesc,itemcost,itemLevel;

    [SerializeField] GameObject buttonLeft,buttonRight;

    [SerializeField] Text leftButtonTxt,rightButtonTxt;

    [SerializeField] GameObject purchaseBut,image;

    int currentSection = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("level Whip") == 3)
        {
            item.Insert(3,new ItemVar("CaveDivers",Resources.Load<Sprite>("Sprite/CaveDiver"),"Cave Divers","Open cave place",11000,1.2f,0.3f,1,PurchaseTest,true,false));
        }


        for (int i=0;i<item.Count;i++)
        {
            ItemVar itemo = item[i];
            
            //SetItemStat(i,itemo.cost,itemo.tax,itemo.level);
            if (PlayerPrefs.GetString("difficulty") == "peaceful" && itemo.mustBeHard)
                item.Remove(item[i]);
            else
                RetrieveItemData(i);

        }
        place = "cave";
        for (int i=0;i<itemCave.Count;i++)
        {
            ItemVar itemo = itemCave[i];
            
            //SetItemStat(i,itemo.cost,itemo.tax,itemo.level);
            if (PlayerPrefs.GetString("difficulty") == "peaceful" && itemo.mustBeHard)
                itemCave.Remove(itemCave[i]);
            else
                RetrieveItemData(i);

        }
        place = "farm";
            //item.Add(new ItemVar("ss",Resources.Load<Sprite>("flah hitler"),"22","222",3,1.2f,0.3f,4,PurchaseTest,true,false));
            ChangedSection();


    }

    public ItemVar CaveItem(string name)
    {
        for (int i=0;i<itemCave.Count;i++)
        {
            if (itemCave[i].itemName == name)
                return itemCave[i];

        }

        return null;
    }
    

    // Update is called once per frame
    void Update()
    {

    }

    void PurchaseTest()
    {
        print("Worked");
    }
    

    public void ChangedSection()
    {
        EnsureCapacity(place);

        ItemVar itemo = ItemFor(place,currentSection);

        gameObject.name = itemo.itemName;
        itemTitle.text = itemo.title;
        itemDesc.text = itemo.description;
        if (itemo.level < itemo.maxLevel)
        {
            itemLevel.text = "Level: " + itemo.level.ToString();
            itemcost.text = "$ "+ FormatNumber.Format(Mathf.FloorToInt(itemo.cost));
            purchaseBut.SetActive(true);
        }
        else
        {
            itemLevel.text = "Level: Max";
            itemcost.text = "";
            purchaseBut.SetActive(false);
        }

        if(currentSection > 0)
            leftButtonTxt.text = "<";
        else
            leftButtonTxt.text = "ඞ";

        if  (currentSection < item.Count - 1)
            rightButtonTxt.text = ">";
        else
            rightButtonTxt.text = "ඞ";


        if (itemo.maxLevel > 1)
        {}
        else
        {
            itemLevel.text = "";
        }



        image.GetComponent<Image>().sprite = itemo.itemImage;

    }



    public void Purchased()
    {
        ItemVar itemo = ItemFor(place,currentSection);

        if (Mathf.Floor(itemo.cost) <= PlayerPrefs.GetInt("crops") && itemo.level < itemo.maxLevel)
        {
            if (itemo.saveable)
            PlayerPrefs.SetInt("crops",PlayerPrefs.GetInt("crops") - Convert.ToInt32(itemo.cost));

            itemo.whenPurchased.Invoke();

            itemo.cost *= itemo.tax;
            itemo.tax += itemo.taxInc;

            itemo.level += 1;
            ChangedSection();

            if (itemo.saveable)
            {
                SaveItemStat(currentSection,Mathf.Floor(itemo.cost),itemo.tax,itemo.level);
            }
            
        }
        
    }

    public void Left()
    {        
        ItemVar itemo = ItemFor(place,currentSection);

        if (currentSection > 0)
        {
            currentSection -= 1;
            Tween.Scale(buttonLeft.transform,startValue:new Vector2(1.25f,0.79f),endValue:Vector2.one,0.2f,Ease.OutExpo);

            ChangedSection();

        }

        
    }

    public void Right()
    {
        ItemVar itemo = ItemFor(place,currentSection);
        if (currentSection < Items(place).Count - 1)
        {
            currentSection += 1;
            Tween.Scale(buttonRight.transform,startValue:new Vector2(1.25f,0.79f),endValue:Vector2.one,0.2f,Ease.OutExpo);

            ChangedSection();
        }
    }

    public void EnsureCapacity(string place)
    {
        if (currentSection < 0)
        {
            currentSection = 0;
        }
        if (currentSection >= Items(place).Count - 1)
        {
            currentSection = Items(place).Count-1;
        }
    }

    public void SaveItemStat(int index,float cost,float tax,int level)
    {
        ItemVar itemo = ItemFor(place,index);
        //if (PrefExistFloat("cost"  + index))
        PlayerPrefs.SetFloat("cost " + itemo.itemName,cost);

        //if (PrefExistFloat("tax"  + index))
        PlayerPrefs.SetFloat("tax " + itemo.itemName,tax);

        //if (PrefExistInt("level"  + index))
        PlayerPrefs.SetInt("level " + itemo.itemName,level);

        print("level " + itemo.itemName);


    }


    public void RetrieveItemData(int index)
    {
        ItemVar itemo = ItemFor(place,index);

        if (PlayerPrefs.GetFloat("cost "+itemo.itemName) != 0)
        {
            itemo.cost = PlayerPrefs.GetFloat("cost "+itemo.itemName);
            itemo.tax = PlayerPrefs.GetFloat("tax "+itemo.itemName);
            itemo.level = PlayerPrefs.GetInt("level "+itemo.itemName);

            Debug.Log(itemo.itemName);
        }

    }

    bool PrefExistInt(string name)
    {
        return PlayerPrefs.GetInt(name,0) != 0;
    }
    bool PrefExistFloat(string name)
    {
        return PlayerPrefs.GetInt(name,0) != 0;
    }

    public void BuySlaveries()
    {  
        if (PlayerPrefs.GetInt("level Whip") == 2)
            item.Insert(3,new ItemVar("CaveDivers",Resources.Load<Sprite>("Sprite/CaveDiver"),"Cave Divers","Open cave place",11000,1.2f,0.3f,1,PurchaseTest,true,false));
    }



    public void IncreaseCropCollect(int s)
    {
        PlayerPrefs.SetInt("cropsInc",PlayerPrefs.GetInt("cropsInc") + s);
    }

    public void MultiplyCropCollect(int s)
    {
        PlayerPrefs.SetInt("cropsInc",PlayerPrefs.GetInt("cropsInc") * s);
    }
    

    public void ChickenMultiply()
    {
        

        if (PlayerPrefs.GetFloat("ChickenMulti",0) == 0)
        {
            PlayerPrefs.SetFloat("ChickenMulti",2);

            GameObject.FindWithTag("ChickenSpawner").GetComponent<ChickenAppear>().Purchased();
        }
        else
            PlayerPrefs.SetFloat("ChickenMulti",PlayerPrefs.GetFloat("ChickenMulti") + 0.5f);

        
    }

    public void PaperGold()
    {
        PlayerPrefs.SetFloat("GoldChance",PlayerPrefs.GetFloat("GoldChance")+0.01f);
    }

    public void Escape()
    {
        GameObject.FindWithTag("Transition").GetComponent<Transition>().TransScene("Escaped");
    }

    public void GetOut()
    {
        AudioUtil.PlayAudio("get out");
        StartCoroutine(Timer.timer(0.35f,Application.Quit));
    }

    public List<ItemVar> Items(string place)
    {
        switch(place.ToLower())
        {
            case "farm":return item;
            case "cave":return itemCave;
            default:return item;
        }
    }


    public ItemVar ItemFor(string place,int index)
    {
        switch(place.ToLower())
        {
            case "farm":return item[index];
            case "cave":return itemCave[index];
            default:return item[index];
        }
    }

}
