using UnityEngine;
using TMPro;

public class TypingManager : MonoBehaviour
{
    [SerializeField]
    private SpammingManager spammingManager;
    [SerializeField]
    private TMP_InputField typingField;

    private void OnEnable()
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
