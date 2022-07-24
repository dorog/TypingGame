using UnityEngine;

public class Deadline : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public SpammingManager spammingManager;

    private void OnEnable()
    {
        boxCollider.size = ((RectTransform)gameObject.transform).rect.size;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Word>(out var word))
        {
            spammingManager.Remove(word);
        }
    }
}
