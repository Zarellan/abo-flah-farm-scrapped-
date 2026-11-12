using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopFunction : MonoBehaviour
{
    // Start is called before the first frame update


    void Awake()
    {
        gameObject.SetActive(true);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShopButtonPressed()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
