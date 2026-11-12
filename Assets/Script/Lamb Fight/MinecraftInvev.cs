using PrimeTween;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class MinecraftInvev : MonoBehaviour
{
    public static MinecraftInvev instance;
    [SerializeField] GameObject hotBarMain;
    public GameObject inv;
    GameObject[] hotBars;
    public GameObject[] inventory;

    public MinecraftItems pickedFromMouse;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        pickedFromMouse = new MinecraftItems("", "", "", Resources.Load<Sprite>("Sprite/MinecraftItems/empty"), Resources.Load<Sprite>("Sprite/MinecraftItems/empty"), null, 0, 0);
        hotBars = new GameObject[9];
        for (int i = 0; i < 9; i++)
        {
            hotBars[i] = Instantiate(hotBarMain, inv.transform);
            hotBars[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(-469.4f + (i * (100 + 17)), -189f);
            hotBars[i].name = "hotbar " + (i + 1);
            hotBars[i].GetComponent<MinecraftItem>().item = null;
            hotBars[i].GetComponent<MinecraftItem>().SetItem(new MinecraftItems("", "", "", Resources.Load<Sprite>("Sprite/MinecraftItems/empty"), Resources.Load<Sprite>("Sprite/MinecraftItems/empty"), null, 0, 0), true);
            GameObject.FindWithTag("MouseItem").transform.SetSiblingIndex(GameObject.FindWithTag("MouseItem").transform.GetSiblingIndex() + 1);
        }
        inventory = new GameObject[9 * 3];
        float yPosInv = 183;
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = Instantiate(hotBarMain, inv.transform);
            inventory[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(-469.4f + ((i % 9) * (100 + 17)), yPosInv);
            inventory[i].name = "inventory " + (i + 1);
            inventory[i].GetComponent<MinecraftItem>().item = null;
            inventory[i].GetComponent<MinecraftItem>().SetItem(new MinecraftItems("dirtBlock", "dirtBlock", "block", Resources.Load<Sprite>("Sprite/MinecraftItems/Dirt"), Resources.Load<Sprite>("Sprite/MinecraftItemPick/Dirt"), Resources.Load<TileBase>("Sprite/MinecraftTile/dirt"), 0, 0), false, 64);
            GameObject.FindWithTag("MouseItem").transform.SetSiblingIndex(GameObject.FindWithTag("MouseItem").transform.GetSiblingIndex() + 1);
            if (inventory[i].name == "inventory 10" || inventory[i].name == "inventory 19")
                inventory[i].GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 117);
            if (i != 0 && i % 9 == 0)
                yPosInv -= 117;
        }
        inventory[1].GetComponent<MinecraftItem>().SetItem(new MinecraftItems("dirtBlock", "dirtBlock", "block", Resources.Load<Sprite>("Sprite/MinecraftItems/Dirt"), Resources.Load<Sprite>("Sprite/MinecraftItemPick/Dirt"), Resources.Load<TileBase>("Sprite/MinecraftTile/dirt"), 0, 0), false, 64);
        inventory[2].GetComponent<MinecraftItem>().SetItem(new MinecraftItems("dirtBlock", "dirtBlock", "block", Resources.Load<Sprite>("Sprite/MinecraftItems/Dirt"), Resources.Load<Sprite>("Sprite/MinecraftItemPick/Dirt"), Resources.Load<TileBase>("Sprite/MinecraftTile/dirt"), 0, 0), false, 56);
        inventory[3].GetComponent<MinecraftItem>().SetItem(new MinecraftItems("dirtBlock", "dirtBlock", "block", Resources.Load<Sprite>("Sprite/MinecraftItems/Dirt"), Resources.Load<Sprite>("Sprite/MinecraftItemPick/Dirt"), Resources.Load<TileBase>("Sprite/MinecraftTile/dirt"), 0, 0), false, 60);
        inventory[4].GetComponent<MinecraftItem>().SetItem(new MinecraftItems("woodenHoe", "woodenHoe", "weapon", Resources.Load<Sprite>("Sprite/MinecraftItems/hoe pix"), Resources.Load<Sprite>("Sprite/MinecraftItemPick/Wood hoe"), null, 0, 3));
        inventory[5].GetComponent<MinecraftItem>().SetItem(new MinecraftItems("woodenHoe", "woodenHoe", "weapon", Resources.Load<Sprite>("Sprite/MinecraftItems/hoe pix"), Resources.Load<Sprite>("Sprite/MinecraftItemPick/Wood hoe"), null, 0, 3));

        hotBarMain.SetActive(false);
        InventoryClosed();
    }
    void Update()
    {
        if (inv.activeSelf)
        {
            RectTransform mouseItemRect = GameObject.FindWithTag("MouseItem").GetComponent<RectTransform>();
            RectTransform canvasRect = mouseItemRect.GetComponentInParent<Canvas>().GetComponent<RectTransform>();

            Vector2 localPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            Input.mousePosition,
            Camera.main,
            out localPos
            );


            mouseItemRect.anchoredPosition = localPos;

        }
    }
    bool refreshOpen = true;
    public void InventoryOpened(GameObject[] hotBarInventory)
    {
        inv.SetActive(true);
        for (int i = 0; i < hotBarInventory.Length; i++)
        {
            hotBars[i].GetComponent<MinecraftItem>().SetItem(hotBarInventory[i].GetComponent<MinecraftItem>().item, true, hotBarInventory[i].GetComponent<MinecraftItem>().itemQuantity);
        }
        if (refreshOpen)
        {
            PickupItemMouse(new MinecraftItems("", "", "", Resources.Load<Sprite>("Sprite/MinecraftItems/empty"), Resources.Load<Sprite>("Sprite/MinecraftItems/empty")));
            GameObject.FindWithTag("MouseItem").GetComponent<MinecraftItemMouse>().SetItem(new MinecraftItems("", "", "", Resources.Load<Sprite>("Sprite/MinecraftItems/empty"), Resources.Load<Sprite>("Sprite/MinecraftItems/empty")), GameObject.FindWithTag("MouseItem").GetComponent<MinecraftItemMouse>().itemQuantity);
            refreshOpen = false;
        }
    }
    public void ReloadInventory(GameObject[] hotBarInventory)
    {
        if (inv.activeSelf)
        {
            for (int i = 0; i < hotBarInventory.Length; i++)
            {
                hotBars[i].GetComponent<MinecraftItem>().SetItem(hotBarInventory[i].GetComponent<MinecraftItem>().item, true,hotBarInventory[i].GetComponent<MinecraftItem>().itemQuantity);
            }
        }
    }
    public void PickupItemMouse(MinecraftItems item)
    {
        pickedFromMouse = item;
        GameObject.FindWithTag("MouseItem").GetComponent<Image>().sprite = item.itemImage;
        Tween.Delay(0.01f).OnComplete(() =>
        {
            MinecraftInventory.instance.ReloadInventory(hotBars);
            MinecraftInventory.instance.HotbarChoice();
        });
        
    }
    public void ReloadItemMouse()
    {
        Tween.Delay(0.01f).OnComplete(() =>
        {
            MinecraftInventory.instance.ReloadInventory(hotBars);
            MinecraftInventory.instance.HotbarChoice();
        });
    }
    public void InventoryClosed()
    {
        inv.SetActive(false);
    }

}
