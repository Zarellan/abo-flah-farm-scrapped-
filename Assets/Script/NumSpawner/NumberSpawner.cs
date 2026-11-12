using UnityEngine;

public class NumberSpawner : MonoBehaviour
{
    public static NumberSpawner instance;

    [SerializeField] GameObject number1;
    void Start()
    {
        if (instance == null)
            instance = this;
    }
    public void SpawnNumber(Transform position)
    {
        GameObject numSp = Instantiate(number1, transform, false);
        WorldToUiUtil.SetPos(numSp.GetComponent<RectTransform>(),position);
    }
}
