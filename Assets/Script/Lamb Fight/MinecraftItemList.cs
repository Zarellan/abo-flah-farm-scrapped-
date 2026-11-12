using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class MinecraftItemList
{
    public static List<MinecraftItems> inventoryItems = new List<MinecraftItems>(); //hotbars doesn't count
}

public class MinecraftItems
{
    public string id;
    public string itemName;
    public string itemType; //consumeable, weapon, etc
    public Sprite itemImage;
    public Sprite itemImagePick;
    public TileBase block;
    public int heal; // if consumeable
    public float damage; // if weapon

    public MinecraftItems(string id, string itemName, string itemType, Sprite itemImage = null, Sprite itemImagePick = null, TileBase block = null, int heal = 1, float damage = 1)
    {
        this.id = id;
        this.itemName = itemName;
        this.itemType = itemType;
        this.itemImage = itemImage;
        this.itemImage = itemImagePick;
        this.block = block;
        this.heal = heal;
        this.damage = damage;
    }
}