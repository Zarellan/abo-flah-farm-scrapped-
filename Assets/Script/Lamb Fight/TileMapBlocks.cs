using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapBlocks : MonoBehaviour
{
    public static TileMapBlocks instance;
    Tilemap tilemap;
    [SerializeField] private TileBase tileToPlace;
    [SerializeField] GameObject dropBlockIns;
    GameObject breakBlock;
    bool isBreaking = false;
    Vector3Int selectedCellPos = new Vector3Int(-99999,-99999);
    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        breakBlock = GameObject.FindWithTag("BreakBlock");
        breakBlock.GetComponent<Animator>().Play("BreakingBlockB");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = tilemap.WorldToCell(worldPos);

        //Debug.Log($"Mouse is over cell {cellPos}");

        // Check if there's a tile there
        TileBase tile = tilemap.GetTile(cellPos);
        if (tile == null)
        {
            //Debug.Log("No tile at this position (empty space)");
        }
        else
        {
            //Debug.Log("Tile exists here!");
        }

        if (MinecraftInvev.instance.inv.activeSelf) return;
        if (tile == null && MinecraftInventory.instance.itemOwned.itemType == "block" && MinecraftInventory.instance.itemOwned.block != tilemap.GetTile(cellPos) && Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = 0f;

            tilemap.SetTile(cellPos, MinecraftInventory.instance.itemOwned.block);
            MinecraftInventory.instance.BlockUsed();
        }
        if (tile != null && Input.GetMouseButton(0))
        {
            if (selectedCellPos != cellPos)
            {
                Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorld.z = 0f;
                breakBlock.transform.position = cellPos + new Vector3(0.5f, 0.5f);
                breakBlock.GetComponent<Animator>().Play("BreakingBlockA", -1, 0);
                selectedCellPos = cellPos;
            }
            isBreaking = true;
            FlahMovement.instance.BreakingBlock();
            //tilemap.SetTile(cellPos, tileToPlace);
        }
        else if (isBreaking && (Input.GetMouseButtonUp(0) || tile == null))
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = 0f;
            breakBlock.transform.position = cellPos + new Vector3(0.5f, 0.5f);
            breakBlock.GetComponent<Animator>().Play("BreakingBlockB");
            selectedCellPos = new Vector3Int(-99999,-99999);
            isBreaking = false;
        }
    }

    public void BlockBrok()
    {
        GameObject dropBlock = Instantiate(dropBlockIns);
        dropBlock.transform.position = selectedCellPos;
        dropBlock.GetComponent<Rigidbody2D>().linearVelocityY += 4;
        switch (tilemap.GetTile(selectedCellPos).name)
        {
            case "dirt": dropBlock.GetComponent<ItemDrop>().item = new MinecraftItems("dirtBlock", "dirtBlock", "block", Resources.Load<Sprite>("Sprite/MinecraftItems/Dirt"), Resources.Load<Sprite>("Sprite/MinecraftItemPick/Dirt"), Resources.Load<TileBase>("Sprite/MinecraftTile/dirt"), 0, 0); break;
            case "grass":dropBlock.GetComponent<ItemDrop>().item = new MinecraftItems("grassBlock", "grassBlock", "block", Resources.Load<Sprite>("Sprite/MinecraftItems/Grass"),Resources.Load<Sprite>("Sprite/MinecraftItemPick/Grass"),Resources.Load<TileBase>("Sprite/MinecraftTile/grass"), 0, 0);break;
        }
        dropBlock.GetComponent<ItemDrop>().quantity = 1;
        tilemap.SetTile(selectedCellPos, tileToPlace);
    }
}
