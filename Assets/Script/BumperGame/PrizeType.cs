using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PrizeType : MonoBehaviour
{
    [SerializeField] public Prize[] prize;
    [HideInInspector] public int rng;
    [SerializeField] TMP_Text text;

    void Awake()
    {

        rng = Random.Range(0,prize.Length);

        text.text = prize[rng].prizeName;
        
    }

    void Update()
    {
        
    }


    [System.Serializable]
    public class Prize
    {
        public string prizeName;
        public UnityEvent prize;
    }

}
