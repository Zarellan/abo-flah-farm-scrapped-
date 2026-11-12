using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScene : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void ToScene(string scene)
    {
        string[] sceneo = scene.Split(',');

        GameObject.FindWithTag("Transition").GetComponent<Transition>().TransScene(sceneo[0]/*,float.Parse(sceneo[1])*/);
    }
}
