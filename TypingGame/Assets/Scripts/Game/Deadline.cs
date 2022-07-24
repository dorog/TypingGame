using UnityEngine;

public class Deadline : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D boxCollider;
    [SerializeField]
    private SpammingManager spammingManager;

    private void OnEnable()
    {
        boxCollider.size = ((RectTransform)gameObject.transform).rect.size;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Word>(out var word))
        {
            spammingManager.Delete(word);
        }
    }
}
