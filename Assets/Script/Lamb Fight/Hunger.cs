using UnityEngine;
using UnityEngine.UI;
using PrimeTween;
public class Hunger : MonoBehaviour
{
    [SerializeField] Sprite full, half, empty;
    [SerializeField] Sprite fullVis, halfVis, emptyVis;
    string hungerName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void SetHunger(bool isFull, bool halfBool = false)
    {
        if (halfBool)
        {
            GetComponent<Image>().sprite = half;
            hungerName = "half";
            return;
        }
        GetComponent<Image>().sprite = isFull ? full : empty;
        hungerName = isFull ? "full" : "empty";
    }

}
