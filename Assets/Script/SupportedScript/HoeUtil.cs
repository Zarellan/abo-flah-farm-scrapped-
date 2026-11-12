using UnityEngine;

public static class HoeUtil
{
    public static Hoe Hoe()
    {
        return GameObject.FindWithTag("Hoe").GetComponent<Hoe>();
    }
}
