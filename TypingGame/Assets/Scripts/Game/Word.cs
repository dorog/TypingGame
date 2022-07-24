using UnityEngine;
using TMPro;

public class Word : MonoBehaviour
{
    private string _value;
    private float _speed;

    [SerializeField]
    private Rigidbody2D rig;
    [SerializeField]
    private TMP_Text valueText;

    public void Init(WordMeta word, float speed)
    {
        _value = word.Value;
        _speed = speed;

        valueText.text = word.Value;
    }

    public bool IsCorrect(string guess)
    {
        return _value == guess;
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = transform.position + (_speed * Time.fixedDeltaTime * Vector3.down);
        rig.MovePosition(newPosition);
    }
}
