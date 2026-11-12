using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemVar
{
    public string itemName; //for debug purpose
    
    public Sprite itemImage;
    public string title;
    public string description;

    public float cost;

    public float tax;

    public float taxInc;
    public int maxLevel;

    public bool saveable;

    public bool mustBeHard;


    [HideInInspector]
    public int level;

    public UnityEvent whenPurchased;


    public ItemVar()
    {

    }

    public ItemVar(string itemName,Sprite itemImage,string title,string description,float cost,float tax,float taxInc,int maxLevel,UnityAction call,bool saveable = true,bool mustBeHard = false)
    {
        this.itemName = itemName;
        this.itemImage = itemImage;
        this.title = title;
        this.description = description;
        this.cost = cost;
        this.tax = tax;
        this.taxInc = taxInc;
        this.maxLevel = maxLevel;
        AddEvent(call);
        this.saveable = saveable;
        this.mustBeHard = mustBeHard;
    }

    void AddEvent(UnityAction call)
    {
        whenPurchased = new UnityEngine.Events.UnityEvent();
        whenPurchased.AddListener(call);
    }


}
