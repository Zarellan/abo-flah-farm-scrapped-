using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafArrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2(transform.localScale.x,Mathf.Sin(Time.time * 2)/1.3f);
    }
}
