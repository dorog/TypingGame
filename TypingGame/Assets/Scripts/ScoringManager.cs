using TMPro;
using UnityEngine;

public class ScoringManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text destroyedText;
    private int destroyedCounter = 0;

    [SerializeField]
    private TMP_Text missedText;
    private int missedCounter = 0;

    public void Destroyed()
    {
        Increase(ref destroyedCounter, destroyedText);
    }

    public void Missed()
    {
        Increase(ref missedCounter, missedText);
    }

    private void Increase(ref int counter, TMP_Text tmpText)
    {
        counter++;
        tmpText.text = counter.ToString();
    }
}
