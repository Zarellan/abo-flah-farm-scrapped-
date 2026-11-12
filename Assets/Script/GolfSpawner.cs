using UnityEngine;
using PrimeTween;
public class GolfSpawner : MonoBehaviour
{
    [SerializeField] GameObject golf;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.GetInt("level Golf") > 0)
            GolfTimer();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        //     MoveGolf();
    }
    public void StartGolf() //begin when purchased
    {
        if (PlayerPrefs.GetInt("level Golf") < 1)
            MoveGolf();
    }
    void MoveGolf()
    {
        GameObject golf2 = Instantiate(golf, new Vector2(transform.position.x + Random.Range(-1.0f, 5.0f), transform.position.y), Quaternion.identity);
        golf2.name = "golf";
        AudioUtil.PlayAudio("golf hit");
        Tween.Position(golf2.transform, new Vector2(golf2.transform.position.x + 0.5f, -3.95f), 0.2f, Ease.InSine).OnComplete(() =>
        {

            Tween.PositionX(golf2.transform, golf2.transform.position.x + 2.0f, 0.6f, Ease.OutCirc);
            Tween.Rotation(golf2.transform, new Vector3(0, 0, -120), 0.6f, Ease.OutCirc);

            Tween.PositionY(golf2.transform, golf2.transform.position.y + 0.5f, 0.1f, Ease.OutCirc).OnComplete(() =>
            {
                Tween.PositionY(golf2.transform, golf2.transform.position.y - 0.5f, 0.1f, Ease.InSine).OnComplete(() =>
                {
                    Tween.PositionY(golf2.transform, golf2.transform.position.y + 0.35f, 0.1f, Ease.OutCirc).OnComplete(() =>
                    {
                        Tween.PositionY(golf2.transform, golf2.transform.position.y - 0.35f, 0.1f, Ease.InSine);
                    });
                });
            });

        });
        GolfTimer();
    }
    void GolfTimer()
    {
        Tween.Delay(Random.Range(20, 40)).OnComplete(() =>
        {
            MoveGolf();
        });
    }
}
