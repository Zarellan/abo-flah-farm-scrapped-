using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepFollow : MonoBehaviour
{
    [SerializeField]
    GameObject obj;

    [SerializeField]
    string follow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (follow == "x")
            transform.position = new Vector2(obj.transform.position.x,transform.position.y);
        else if (follow == "y")
            transform.position = new Vector2(transform.position.x,obj.transform.position.y);
        else
            transform.position = new Vector2(obj.transform.position.x,obj.transform.position.y);

    }
}
