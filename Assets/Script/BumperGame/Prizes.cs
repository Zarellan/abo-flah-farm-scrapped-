using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prizes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CropsPrizeMultiply(float multiplication)
    {
        PlayerPrefs.SetInt("crops",Mathf.FloorToInt(PlayerPrefs.GetInt("crops") * multiplication));

    }
}
