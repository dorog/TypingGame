using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField]
    private int secondsBeforeStart;
    [SerializeField]
    private CounterText counterText;
    [SerializeField]
    private StateManager stateManager;

    private void OnEnable()
    {
        counterText.SetCounter(secondsBeforeStart);
        counterText.gameObject.SetActive(true);

        Invoke(nameof(Count), 1);
    }

    private void Count()
    {
        counterText.DecreaseCount();

        if (counterText.Counter > 0)
        {
            Invoke(nameof(Count), 1);
        }
        else
        {
            counterText.gameObject.SetActive(false);
            stateManager.StartGame();
        }
    }
}
