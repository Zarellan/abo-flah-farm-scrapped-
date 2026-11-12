using UnityEngine;

public class DropBlockCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.name == GameObject.FindWithTag("FlahPlatf").name)
        {
            //MinecraftInventory.instance.PickupItem(item, quantity);
            //Destroy(this.gameObject);
            print("touche");
        }
    }
}
