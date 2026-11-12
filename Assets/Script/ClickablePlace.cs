using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickablePlace : MonoBehaviour
{
    // Start is called before the first frame update

    [HideInInspector]
    public bool canClick;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnMouseEnter()
    {
        canClick = true;
    }

    void OnMouseExit()
    {
        canClick = false;
    }

    public void overro()
    {
        print("jeuu");
    }
}
