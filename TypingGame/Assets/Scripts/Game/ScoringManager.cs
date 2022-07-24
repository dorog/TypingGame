using UnityEngine;

public class ScoringManager : MonoBehaviour
{
    [SerializeField]
    private CounterText destroyedText;

    [SerializeField]
    private CounterText missedText;

    [SerializeField]
    private UiManager uiManager;
    [SerializeField]
    private StateManager stateManager;

    private void Awake()
    {
        stateManager.SubscribeToGameEndAction(EndGame);
    }

    public void Destroyed()
    {
        destroyedText.IncreaseCount();
    }

    public void Missed()
    {
        missedText.IncreaseCount();
    }

    private void EndGame()
    {
        uiManager.ShowResult(destroyedText.Counter, missedText.Counter);

        destroyedText.SetCounter(0);
        missedText.SetCounter(0);
    }
}
