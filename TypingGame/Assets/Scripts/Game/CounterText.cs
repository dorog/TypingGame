using TMPro;
using UnityEngine;

public class CounterText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text counterText;

    public int Counter { get; private set; }

    public void SetCounter(int value)
    {
        Counter = value;
        SetText();
    }

    public void IncreaseCount()
    {
        Counter++;
        SetText();
    }

    public void DecreaseCount()
    {
        Counter--;
        SetText();
    }

    private void SetText()
    {
        counterText.text = Counter.ToString();
    }
}
