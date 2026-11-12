using UnityEngine;
using UnityEngine.Tilemaps;

public class MinecraftInventory : MonoBehaviour
{
    public static MinecraftInventory instance;
    [SerializeField] GameObject hotBarMain;
    [SerializeField] GameObject dropBlockIns;
    GameObject[] hotBars;
    [SerializeField] GameObject hotBar;
    int choosedHotbar = 0;
    public MinecraftItems itemOwned;

    void Awake()
    {
        if (instance == null)
            instance = this;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ItemLoad();
        hotBars = new GameObject[9];
        for (int i = 0; i < 9; i++)
        {
            hotBars[i] = Instantiate(hotBarMain, transform);
            hotBars[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(-508.2f + (i * 59 * 2.1f), -473.2f);
            hotBars[i].name = "hotbar " + (i + 1);
            hotBars[i].GetComponent<MinecraftItem>().item = null;
            hotBar.transform.SetSiblingIndex(hotBar.transform.GetSiblingIndex() + 1);
            hotBars[i].GetComponent<MinecraftItem>().SetItem(new MinecraftItems("", "", "", Resources.Load<Sprite>("Sprite/MinecraftItems/empty"), Resources.Load<Sprite>("Sprite/MinecraftItems/empty"), null, 0, 0), false, 0);
        }
        hotBars[0].GetComponent<MinecraftItem>().SetItem(new MinecraftItems("woodenHoe", "woodenHoe", "weapon", Resources.Load<Sprite>("Sprite/MinecraftItems/hoe pix"), Resources.Load<Sprite>("Sprite/MinecraftItemPick/Wood hoe"), null, 0, 3));
        hotBars[1].GetComponent<MinecraftItem>().SetItem(new MinecraftItems("dirtBlock", "dirtBlock", "block", Resources.Load<Sprite>("Sprite/MinecraftItems/Dirt"), Resources.Load<Sprite>("Sprite/MinecraftItemPick/Dirt"), Resources.Load<TileBase>("Sprite/MinecraftTile/dirt"), 0, 0), false, 64);
        hotBars[2].GetComponent<MinecraftItem>().SetItem(new MinecraftItems("grassBlock", "grassBlock", "block", Resources.Load<Sprite>("Sprite/MinecraftItems/Grass"),Resources.Load<Sprite>("Sprite/MinecraftItemPick/Grass"),Resources.Load<TileBase>("Sprite/MinecraftTile/grass"), 0, 0), false, 64);
        hotBars[3].GetComponent<MinecraftItem>().SetItem(new MinecraftItems("rawChicken", "Raw Chicken", "consumeable", Resources.Load<Sprite>("Sprite/MinecraftItems/rawChick"),Resources.Load<Sprite>("Sprite/MinecraftItemPick/rawChick"),null, 4, 0), false, 64);
        //hotBars[9].GetComponent<MinecraftItem>().SetItem(new MinecraftItems("dirtBlock", "dirtBlock", "block", Resources.Load<Sprite>("Sprite/MinecraftItems/Dirt"), Resources.Load<Sprite>("Sprite/MinecraftItemPick/Dirt"), Resources.Load<TileBase>("Sprite/MinecraftTile/dirt"), 0, 0), false, 64);

        HotbarChoice();
        hotBarMain.SetActive(false);
    }
    void ItemLoad()
    {
        //MinecraftItemList.items.Add(new MinecraftItems("woodenHoe","woodenHoe","weapon",0,3));
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!MinecraftInvev.instance.inv.activeSelf)
                MinecraftInvev.instance.InventoryOpened(hotBars);
            else
                MinecraftInvev.instance.InventoryClosed();
        }
        for (int i = '1'; i <= '9'; i++)
        {
            char c = (char)i;
            if (Input.GetKeyDown(c.ToString()))
            {
                choosedHotbar = i - '1';
                HotbarChoice();
            }
        }

        if (!MinecraftInvev.instance.inv.activeSelf && itemOwned.id != "" && Input.GetKeyDown(KeyCode.Q))
        {
            GameObject dropBlock = Instantiate(dropBlockIns);
            dropBlock.transform.position = FlahMovement.instance.transform.position + new Vector3(0,1.5f);
            dropBlock.GetComponent<ItemDrop>().item = hotBars[choosedHotbar].GetComponent<MinecraftItem>().item;
            dropBlock.GetComponent<ItemDrop>().quantity = 1;
            hotBars[choosedHotbar].GetComponent<MinecraftItem>().ThrowItem();
            dropBlock.GetComponent<Rigidbody2D>().linearVelocityY += 4;
            dropBlock.GetComponent<Rigidbody2D>().linearVelocityX += FlahMovement.instance.GetComponent<SpriteRenderer>().flipX ? -4 : 4;
            HotbarChoice();
        }
    }

    public void BlockUsed()
    {
        hotBars[choosedHotbar].GetComponent<MinecraftItem>().itemQuantity -= 1;
        hotBars[choosedHotbar].GetComponent<MinecraftItem>().QuantityReload();
        if (hotBars[choosedHotbar].GetComponent<MinecraftItem>().itemQuantity <= 0)
        {
            hotBars[choosedHotbar].GetComponent<MinecraftItem>().SetItem(new MinecraftItems("", "", "", Resources.Load<Sprite>("Sprite/MinecraftItems/empty"), Resources.Load<Sprite>("Sprite/MinecraftItems/empty"), null, 0, 0));
            HotbarChoice();
        }
    }

    public void ReloadInventory(GameObject[] hotBarInventory)
    {
        for (int i = 0; i < hotBars.Length; i++)
        {
            hotBars[i].GetComponent<MinecraftItem>().SetItem(hotBarInventory[i].GetComponent<MinecraftItem>().item, false,hotBarInventory[i].GetComponent<MinecraftItem>().itemQuantity);
        }
    }
    bool itemAdded;
    public bool PickupItem(MinecraftItems item, int quantity,ItemDrop drop)
    {
        itemAdded = false;
        int quantit = quantity;

        // 1. Stack into hotbar
        for (int i = 0; i < hotBars.Length; i++)
        {
            var slot = hotBars[i].GetComponent<MinecraftItem>();
            if (slot.item.id == item.id && slot.itemQuantity < 64 && item.itemType != "weapon")
            {
                while (slot.itemQuantity < 64 && quantit > 0)
                {
                    slot.AddItem(1);
                    quantit--;
                }
                if (quantit <= 0)
                {
                    itemAdded = true;
                    break;
                }
            }
        }

        // 2. Stack into inventory
        if (!itemAdded && quantit > 0)
        {
            for (int i = 0; i < MinecraftInvev.instance.inventory.Length; i++)
            {
                var slot = MinecraftInvev.instance.inventory[i].GetComponent<MinecraftItem>();
                if (slot.item.id == item.id && slot.itemQuantity < 64 && item.itemType != "weapon")
                {
                    while (slot.itemQuantity < 64 && quantit > 0)
                    {
                        slot.AddItem(1);
                        quantit--;
                    }
                    if (quantit <= 0)
                    {
                        itemAdded = true;
                        break;
                    }
                }
            }
        }

        // 3. Place into empty hotbar slot
        if (!itemAdded && quantit > 0)
        {
            for (int i = 0; i < hotBars.Length; i++)
            {
                var slot = hotBars[i].GetComponent<MinecraftItem>();
                if (slot.item.id == "")
                {
                    int toAdd = Mathf.Min(quantit, 64);
                    slot.SetItem(item, false, toAdd);
                    quantit -= toAdd;
                    itemAdded = true;
                    if (quantit <= 0) break;
                }
            }
        }

        // 4. Place into empty inventory slot
        if (!itemAdded && quantit > 0)
        {
            for (int i = 0; i < MinecraftInvev.instance.inventory.Length; i++)
            {
                var slot = MinecraftInvev.instance.inventory[i].GetComponent<MinecraftItem>();
                if (slot.item.id == "")
                {
                    int toAdd = Mathf.Min(quantit, 64);
                    slot.SetItem(item, false, toAdd);
                    quantit -= toAdd;
                    itemAdded = true;
                    if (quantit <= 0) break;
                }
            }
        }

        // Final updates
        QueueItems(item);
        if (itemAdded)
        {
            MinecraftInvev.instance.ReloadInventory(hotBars);
            HotbarChoice();
            return true;
        }
        else
        {
            drop.GetComponent<ItemDrop>().quantity = quantit;
            return false;
        }
    }
    void QueueItems(MinecraftItems it)
    {
        for (int i = 0; i < hotBars.Length; i++)
        {
            if (it.id == hotBars[i].GetComponent<MinecraftItem>().item.id)
            {
                for (int j = 0; j < hotBars.Length; j++)
                {
                    if (it.id == hotBars[j].GetComponent<MinecraftItem>().item.id && hotBars[i].GetComponent<MinecraftItem>().itemQuantity > hotBars[j].GetComponent<MinecraftItem>().itemQuantity)
                    {
                        ItemSwap(hotBars[j].GetComponent<MinecraftItem>(), hotBars[i].GetComponent<MinecraftItem>());
                    }
                }
            }
        }
        for (int i = 0; i < MinecraftInvev.instance.inventory.Length; i++)
        {
            if (it.id == MinecraftInvev.instance.inventory[i].GetComponent<MinecraftItem>().item.id)
            {
                for (int j = 0; j < MinecraftInvev.instance.inventory.Length; j++)
                {
                    if (it.id == MinecraftInvev.instance.inventory[j].GetComponent<MinecraftItem>().item.id && MinecraftInvev.instance.inventory[i].GetComponent<MinecraftItem>().itemQuantity > MinecraftInvev.instance.inventory[j].GetComponent<MinecraftItem>().itemQuantity)
                        ItemSwap(MinecraftInvev.instance.inventory[j].GetComponent<MinecraftItem>(),MinecraftInvev.instance.inventory[i].GetComponent<MinecraftItem>());
                }
            }
        }
    }

    void ItemSwap(MinecraftItem it1, MinecraftItem it2)
    {
        MinecraftItem itTemp = it1;
        it1 = it2;
        it2 = itTemp;

        int tempQty = it1.itemQuantity;
        it1.itemQuantity = it2.itemQuantity;
        it2.itemQuantity = tempQty;

        it1.QuantityReload();
        it2.QuantityReload();

    }
    public void HotbarChoice()
    {
        hotBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-508.2f + (choosedHotbar * 59 * 2.1f), hotBar.GetComponent<RectTransform>().anchoredPosition.y);
        itemOwned = hotBars[choosedHotbar].GetComponent<MinecraftItem>().item;
        FlahMovement.instance.pickedItem.GetComponent<SpriteRenderer>().sprite = itemOwned.itemImage;
    }
}
