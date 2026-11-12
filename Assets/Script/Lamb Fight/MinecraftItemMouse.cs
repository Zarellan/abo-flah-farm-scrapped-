using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MinecraftItemMouse : MonoBehaviour
{
    public MinecraftItems item;
    [SerializeField] GameObject itemImage;
    [SerializeField] TMP_Text quantityText;
    public int itemQuantity;
    public bool canPickUpItem;
    bool itemCooldown = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetItem(MinecraftItems item, int quantity = 1)
    {
        this.item = item;
        itemImage.GetComponent<Image>().sprite = item.itemImage;
        if (item.id != "")
            itemQuantity = quantity;
        QuantityReload();

    }
    public void QuantityReload()
    {
        if (item.id != "" && item.itemType != "weapon" && itemQuantity > 0)
            quantityText.text = itemQuantity.ToString();
        else
            quantityText.text = "";
    }
}
