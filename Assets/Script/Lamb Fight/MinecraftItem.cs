using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MinecraftItem : MonoBehaviour
{
    public MinecraftItems item;
    [SerializeField] GameObject itemImage;
    [SerializeField] TMP_Text quantityText;
    public int itemQuantity;
    public bool canPickUpItem;
    bool itemCooldown = true;
    bool singleBlockPlaced;
    [SerializeField] GameObject interc;
    [SerializeField] GameObject dropBlockIns;
    void Awake()
    {
    }
    int tempQuantity;
    void Update()
    {
        if (MinecraftInvev.instance.inv.activeSelf && interc != null)
        {
            if (IsPointerOverUIObject(GetComponent<Image>()))
                interc.GetComponent<Image>().color = new Color(1, 1, 1, 133.0f / 255f);
            else
            {
                interc.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
        }

        if (MinecraftInvev.instance.inv.activeSelf)
        {
            if (itemQuantity < 64 && item.itemType != "weapon" &&
            (item.id == "" || item.id == MinecraftInvev.instance.pickedFromMouse.id) &&
            itemCooldown &&
            MinecraftInvev.instance.pickedFromMouse.id != "" &&
            Input.GetMouseButtonDown(0) &&
            IsPointerOverUIObject(GetComponent<Image>()))
            {
                var mouseItem = GameObject.FindWithTag("MouseItem").GetComponent<MinecraftItemMouse>();
                int spaceLeft = 64 - itemQuantity;
                int quantityToMove = Mathf.Min(mouseItem.itemQuantity, spaceLeft);

                if (item.id == "" || item.id != MinecraftInvev.instance.pickedFromMouse.id)
                {
                    SetItem(MinecraftInvev.instance.pickedFromMouse, false, quantityToMove);
                }
                else
                {
                    AddItem(quantityToMove);
                }

                mouseItem.itemQuantity -= quantityToMove;

                if (mouseItem.itemQuantity <= 0)
                {
                    var emptyItem = new MinecraftItems("", "", "",
                        Resources.Load<Sprite>("Sprite/MinecraftItems/empty"),
                        Resources.Load<Sprite>("Sprite/MinecraftItems/empty"));

                    MinecraftInvev.instance.PickupItemMouse(emptyItem);
                    mouseItem.SetItem(emptyItem, 0);
                }
                else
                {
                    MinecraftInvev.instance.ReloadItemMouse();
                    mouseItem.QuantityReload();
                }

                itemCooldown = false;
            }

            if (itemQuantity < 64 && !singleBlockPlaced && item.itemType != "weapon" && (item.id == "" || item.id == MinecraftInvev.instance.pickedFromMouse.id) && itemCooldown && MinecraftInvev.instance.pickedFromMouse.id != "" && Input.GetMouseButton(1) && IsPointerOverUIObject(GetComponent<Image>()))
            {
                if (item.id != MinecraftInvev.instance.pickedFromMouse.id)
                    SetItem(MinecraftInvev.instance.pickedFromMouse, false, 1);
                else
                    AddItem(1);

                GameObject.FindWithTag("MouseItem").GetComponent<MinecraftItemMouse>().itemQuantity--;
                if (GameObject.FindWithTag("MouseItem").GetComponent<MinecraftItemMouse>().itemQuantity <= 0)
                {
                    MinecraftInvev.instance.PickupItemMouse(new MinecraftItems("", "", "", Resources.Load<Sprite>("Sprite/MinecraftItems/empty"), Resources.Load<Sprite>("Sprite/MinecraftItems/empty")));
                    GameObject.FindWithTag("MouseItem").GetComponent<MinecraftItemMouse>().SetItem(new MinecraftItems("", "", "", Resources.Load<Sprite>("Sprite/MinecraftItems/empty"), Resources.Load<Sprite>("Sprite/MinecraftItems/empty")), GameObject.FindWithTag("MouseItem").GetComponent<MinecraftItemMouse>().itemQuantity);
                }
                else
                {
                    MinecraftInvev.instance.ReloadItemMouse();
                    GameObject.FindWithTag("MouseItem").GetComponent<MinecraftItemMouse>().QuantityReload();
                }
                itemCooldown = false;
                singleBlockPlaced = true;
            }
            if (itemCooldown && MinecraftInvev.instance.pickedFromMouse.id == "" && item.id != "" && Input.GetMouseButtonDown(0) && IsPointerOverUIObject(GetComponent<Image>()))
            {
                MinecraftInvev.instance.PickupItemMouse(item);
                GameObject.FindWithTag("MouseItem").GetComponent<MinecraftItemMouse>().SetItem(item, itemQuantity);
                SetItem(new MinecraftItems("", "", "", Resources.Load<Sprite>("Sprite/MinecraftItems/empty"), Resources.Load<Sprite>("Sprite/MinecraftItems/empty"), null, 0, 0), false);
                itemQuantity = 0;

            }

            if (canPickUpItem && MinecraftInvev.instance.pickedFromMouse.id != "" && Input.GetKeyDown(KeyCode.Q))
            {
                GameObject dropBlock = Instantiate(dropBlockIns);
                dropBlock.transform.position = FlahMovement.instance.transform.position + new Vector3(0,1.5f);
                dropBlock.GetComponent<ItemDrop>().item = MinecraftInvev.instance.pickedFromMouse;
                dropBlock.GetComponent<ItemDrop>().quantity = GameObject.FindWithTag("MouseItem").GetComponent<MinecraftItemMouse>().itemQuantity;
                dropBlock.GetComponent<Rigidbody2D>().linearVelocityY += 4;
                dropBlock.GetComponent<Rigidbody2D>().linearVelocityX += FlahMovement.instance.GetComponent<SpriteRenderer>().flipX ? -4 : 4;
                MinecraftInventory.instance.HotbarChoice();
                QuantityReload();
                MinecraftInvev.instance.PickupItemMouse(new MinecraftItems("", "", "", Resources.Load<Sprite>("Sprite/MinecraftItems/empty"), Resources.Load<Sprite>("Sprite/MinecraftItems/empty")));
                GameObject.FindWithTag("MouseItem").GetComponent<MinecraftItemMouse>().SetItem(new MinecraftItems("", "", "", Resources.Load<Sprite>("Sprite/MinecraftItems/empty"), Resources.Load<Sprite>("Sprite/MinecraftItems/empty")), GameObject.FindWithTag("MouseItem").GetComponent<MinecraftItemMouse>().itemQuantity);
            }
            }
        itemCooldown = true;

        if (!IsPointerOverUIObject(GetComponent<Image>()) || Input.GetMouseButtonUp(1))
            singleBlockPlaced = false;
    }
    public void SetItem(MinecraftItems item, bool canPickUpItem = false, int quantity = 1)
    {
        this.item = item;
        itemImage.GetComponent<Image>().sprite = item.itemImage;
        this.canPickUpItem = canPickUpItem;
        if (item.id != "")
            itemQuantity = quantity;
        QuantityReload();
    }
    public void ThrowItem()
    {
        if (item.id == "") return;

        itemQuantity -= 1;
        itemImage.GetComponent<Image>().sprite = item.itemImage;
        if (itemQuantity <= 0)
        {
            SetItem(new MinecraftItems("", "", "", Resources.Load<Sprite>("Sprite/MinecraftItems/empty"), Resources.Load<Sprite>("Sprite/MinecraftItems/empty")));
        }
        QuantityReload();
    }
    public void AddItem(int quantity)
    {
        itemQuantity += quantity;
        QuantityReload();
    }
    public void QuantityReload()
    {
        if (item.id != "" && item.itemType != "weapon" && itemQuantity > 0)
            quantityText.text = itemQuantity.ToString();
        else
            quantityText.text = "";
    }
    bool IsPointerOverUIObject(Image targetButton)
    {
        // Raycast to check if the pointer is over the button
        return RectTransformUtility.RectangleContainsScreenPoint(
            targetButton.GetComponent<RectTransform>(),
            Input.mousePosition,
            Camera.main);
    }
}
