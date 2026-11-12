using PrimeTween;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
public class ItemDrop : MonoBehaviour
{
    [SerializeField] GameObject droppedObject;
    public MinecraftItems item;
    public int quantity;
    bool canPickBlock = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Tween.LocalPositionY(droppedObject.transform, startValue: -0.146f, endValue: 0.16f, 2, Ease.InOutSine, cycles: -1, cycleMode: CycleMode.Yoyo);
        Tween.LocalRotation(droppedObject.transform, startValue: Quaternion.Euler(0, 0, 0), endValue: Quaternion.Euler(0, 180, 0), 1.4f, Ease.Linear, cycles: -1, cycleMode: CycleMode.Yoyo);
        if (item == null)
            item = new MinecraftItems("dirtBlock", "dirtBlock", "block", Resources.Load<Sprite>("Sprite/MinecraftItems/Dirt"), Resources.Load<Sprite>("Sprite/MinecraftItemPick/Dirt"), Resources.Load<TileBase>("Sprite/MinecraftTile/dirt"));
        droppedObject.GetComponent<SpriteRenderer>().sprite = item.itemImage;
        //quantity = 1;
        Tween.Delay(0.8f).OnComplete(() => canPickBlock = true);
        print(quantity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool itemReturned;
    void OnTriggerStay2D(Collider2D other)
    {
        if (canPickBlock && other.name == "FlahPlayer")
        {
            itemReturned = MinecraftInventory.instance.PickupItem(item, quantity,gameObject.GetComponent<ItemDrop>());
            if (itemReturned)
                Destroy(this.gameObject);
        }
    }
}
