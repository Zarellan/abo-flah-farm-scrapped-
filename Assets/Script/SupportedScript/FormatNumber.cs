using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FormatNumber
{

    static string[] symbols = {"","k","M","B"};
    public static string Format(int num)
    {
        /*if (num >= 1000000)
        {
            return (Mathf.Floor(num / 1_000_000.0f * 100) / 100).ToString() + "M";
        }
        else if (num >= 1_000) // 1 thousand or more
        {
            return (Mathf.Floor(num / 1_000.0f * 100) / 100).ToString() +"K";
        }
        else
        {
            return num.ToString();
        }*/


        return (Mathf.Floor(num / (1 * Mathf.Pow(1000,(num.ToString().Length-1)/3)) * 100) / 100).ToString() + symbols[(num.ToString().Length-1)/3];
        
    }
        


    
}
