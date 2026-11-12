using UnityEngine;
using UnityEngine.UI;
using PrimeTween;
public class Heart : MonoBehaviour
{
    [SerializeField] Sprite full, half, empty;
    [SerializeField] Sprite fullVis, halfVis, emptyVis;
    string heartName;
    Tween vis1, vis2, vis3,vis4,vis5,vis6;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetHealth(bool isFull, bool halfBool = false)
    {
        if (halfBool)
        {
            GetComponent<Image>().sprite = half;
            heartName = "half";
            return;
        }
        GetComponent<Image>().sprite = isFull ? full : empty;
        heartName = isFull ? "full" : "empty";
    }
    public void VisualHeartel()
    {
        StopMethods();
        ShowVisual(true);
        vis1 = Tween.Delay(0.2f).OnComplete(()=>
        {
            ShowVisual(false);
            vis2 = Tween.Delay(0.2f).OnComplete(()=>
            {
                ShowVisual(true);
                vis3 = Tween.Delay(0.2f).OnComplete(()=>
                {
                    ShowVisual(false);
                    vis4 = Tween.Delay(0.2f).OnComplete(()=>
                    {
                        ShowVisual(true);
                        vis5 = Tween.Delay(0.2f).OnComplete(()=>
                        {
                            ShowVisual(false);
                        });
                    });
                });
            });
        });
    }

    void StopMethods()
    {
        if (vis1.isAlive)
            vis1.Stop();
        if (vis2.isAlive)
            vis2.Stop();
        if (vis3.isAlive)
            vis3.Stop();
        if (vis4.isAlive)
            vis4.Stop();
        if (vis5.isAlive)
            vis5.Stop();
    }
    public void ShowVisual(bool show)
    {
        if (show)
        {
            switch (heartName)
            {
                case "full": GetComponent<Image>().sprite = fullVis; break;
                case "half": GetComponent<Image>().sprite = halfVis; break;
                case "empty": GetComponent<Image>().sprite = emptyVis; break;
            }
        }
        else
        {
            switch (heartName)
            {
                case "full": GetComponent<Image>().sprite = full; break;
                case "half": GetComponent<Image>().sprite = half; break;
                case "empty": GetComponent<Image>().sprite = empty; break;
            }

        }
    }
}
