using UnityEngine;
using System.Collections.Generic;

public class SpammingManager : MonoBehaviour
{
    [SerializeField]
    private Word word;
    [SerializeField]
    private Transform spammingPoint;
    [SerializeField]
    private int maxWords = 10;
    [SerializeField]
    private ScoringManager scoringManager;

    private float _wordGameObjectWidth;

    private int _remainingWords;
    private readonly List<Word> spammedWords = new();

    private readonly WordMeta[] availableWordMetas = new WordMeta[]
    {
        new WordMeta() { Value = "abandon", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "ability", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "able", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "abortion", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "about", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "above", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "abroad", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "absence", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "absolute", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "absolutely", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "absorb", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "abuse", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "academic", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "accept", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "access", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "accident", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "accompany", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "accomplish", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "according", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "account", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "accurate", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "accuse", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "achieve", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "achievement", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "acid", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "acknowledge", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "acquire", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "across", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "act", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy },
        new WordMeta() { Value = "action", MinSpeed = 50, MaxSpeed = 200, Complexity = Complexity.easy }
    };

    private void Awake()
    {
        _wordGameObjectWidth = ((RectTransform)word.transform).rect.width;
    }

    private void OnEnable()
    {
        _remainingWords = maxWords;

        Invoke(nameof(SpamWord), 0);
    }

    private void SpamWord()
    {
        InstantiateWord();

        PrepareForNextSpamming();
    }

    private void InstantiateWord()
    {
        Vector3 spammingPosition = GetSpammingPosition();
        Word spammedWord = Instantiate(word, spammingPosition, Quaternion.identity, spammingPoint);

        WordMeta randomWord = SelectWord();
        spammedWord.Init(randomWord);

        spammedWords.Add(spammedWord);
    }

    private Vector3 GetSpammingPosition()
    {
        int halfOfTheScreenWidth = Screen.width / 2;

        float x = Random.Range(-halfOfTheScreenWidth + _wordGameObjectWidth, halfOfTheScreenWidth - _wordGameObjectWidth);

        Vector3 offset = new(x, 0, 0);

        return spammingPoint.position + offset;
    }

    private WordMeta SelectWord()
    {
        int randomWordMetaIndex = Random.Range(0, availableWordMetas.Length);
        return availableWordMetas[randomWordMetaIndex];
    }

    private void PrepareForNextSpamming()
    {
        _remainingWords--;

        if (_remainingWords > 0)
        {
            float timeBeforeTheNextSpamming = Random.Range(1, 4);
            Invoke(nameof(SpamWord), timeBeforeTheNextSpamming);
        }
    }

    public void Guess(string guess)
    {
        List<Word> correctlyTypedWords = new();
        foreach (var word in spammedWords)
        {
            if (word.IsCorrect(guess))
            {
                correctlyTypedWords.Add(word);

                Destroy(word.gameObject);
                scoringManager.Destroyed();
            }
        }

        spammedWords.RemoveAll(word => correctlyTypedWords.Contains(word));
    }

    public void Remove(Word word)
    {
        spammedWords.Remove(word);
        scoringManager.Missed();

        Destroy(word.gameObject);
    }
}
