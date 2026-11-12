using UnityEngine;

public static class ItemAction
{
    public static void ItemAct(MinecraftItems idName)
    {
        switch (idName.id)
        {
            case "woodenHoe": { FlahMovement.instance.Attack(); } break;
            case "rawChicken":{ FlahMovement.instance.Eating(idName.heal); }break;
        }
    }
}
