using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticReset
{
    static StaticReset()
    {
        MultiplyTime.isMulti = false;
    }


    public static void ResetStatics()
    {
        MultiplyTime.isMulti = false;
    }
}
