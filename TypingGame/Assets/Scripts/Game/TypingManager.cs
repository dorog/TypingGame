using UnityEngine;
using TMPro;

public class TypingManager : MonoBehaviour
{
    [SerializeField]
    private SpammingManager spammingManager;
    [SerializeField]
    private TMP_InputField typingField;

    [SerializeField]
    private StateManager stateManager;

    private void Awake()
    {
        stateManager.SubscribeToGameStartAction(StartGame);
    }

    private void StartGame()
    {
        typingField.ActivateInputField();
    }

    public void FinishTyping()
    {
        string text = typingField.text;
        spammingManager.Guess(text);

        typingField.text = "";
        typingField.ActivateInputField();
    }
}
