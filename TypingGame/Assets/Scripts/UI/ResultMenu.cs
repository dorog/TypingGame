using TMPro;
using UnityEngine;

public class ResultMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_Text destroyedResult;
    [SerializeField]
    private TMP_Text missedResult;

    public void UpdatePoints(int destroyedCount, int missedCount)
    {
        destroyedResult.text = destroyedCount.ToString();
        missedResult.text = missedCount.ToString();
    }
}
