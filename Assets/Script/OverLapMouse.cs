using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OverLapMouse : MonoBehaviour
{
    [SerializeField]
    UnityEvent entered,exit;

    [SerializeField,Tooltip("will it keep update the information ? (used for 1 statement only)")] bool update;

    bool enter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (update)
            {
                if (enter)
                    entered.Invoke();
                else
                    exit.Invoke();

            }

    }

    void OnMouseEnter()
    {
        entered.Invoke();
        enter = true;
        
    }

    void OnMouseExit()
    {
        exit.Invoke();
        enter = false;
    }
}
