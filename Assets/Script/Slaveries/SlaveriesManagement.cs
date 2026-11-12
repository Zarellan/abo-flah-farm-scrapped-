using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlaveriesManagement : MonoBehaviour
{


    [SerializeField]
    GameObject[] slaveries;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0;i<slaveries.Length;i++)
            slaveries[i].SetActive(slaveries[i].GetComponent<SlaverID>().idReq <= PlayerPrefs.GetInt("level Whip"));

    }
}
