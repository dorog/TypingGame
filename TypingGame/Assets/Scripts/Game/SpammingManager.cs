using UnityEngine;
using System.Collections.Generic;

public class SpammingManager : MonoBehaviour
{
    [SerializeField]
    private Word word;
    [SerializeField]
    private Transform spammingPoint;
    [SerializeField]
    private ScoringManager scoringManager;

    [SerializeField]
    private StateManager stateManager;

    private float _wordGameObjectWidth;

    private GameSetting _gameSetting;

    private int _remainingWords;
    private int _diedWords = 0;
    private readonly List<Word> _spammedWords = new();

    private readonly WordMeta[] availableWordMetas = new WordMeta[]
    {
        new WordMeta() { Value = "abandon", Complexity = Complexity.easy },
        new WordMeta() { Value = "ability", Complexity = Complexity.easy },
        new WordMeta() { Value = "able", Complexity = Complexity.easy },
        new WordMeta() { Value = "abortion", Complexity = Complexity.easy },
        new WordMeta() { Value = "about", Complexity = Complexity.easy },
        new WordMeta() { Value = "above", Complexity = Complexity.easy },
        new WordMeta() { Value = "abroad", Complexity = Complexity.easy },
        new WordMeta() { Value = "absence", Complexity = Complexity.easy },
        new WordMeta() { Value = "absolute", Complexity = Complexity.easy },
        new WordMeta() { Value = "absolutely", Complexity = Complexity.easy },
        new WordMeta() { Value = "absorb", Complexity = Complexity.easy },
        new WordMeta() { Value = "abuse", Complexity = Complexity.easy },
        new WordMeta() { Value = "academic", Complexity = Complexity.easy },
        new WordMeta() { Value = "accept", Complexity = Complexity.easy },
        new WordMeta() { Value = "access", Complexity = Complexity.easy },
        new WordMeta() { Value = "accident", Complexity = Complexity.easy },
        new WordMeta() { Value = "accompany", Complexity = Complexity.easy },
        new WordMeta() { Value = "accomplish", Complexity = Complexity.easy },
        new WordMeta() { Value = "according", Complexity = Complexity.easy },
        new WordMeta() { Value = "account", Complexity = Complexity.easy },
        new WordMeta() { Value = "accurate", Complexity = Complexity.easy },
        new WordMeta() { Value = "accuse", Complexity = Complexity.easy },
        new WordMeta() { Value = "achieve", Complexity = Complexity.easy },
        new WordMeta() { Value = "achievement", Complexity = Complexity.easy },
        new WordMeta() { Value = "acid", Complexity = Complexity.easy },
        new WordMeta() { Value = "acknowledge", Complexity = Complexity.easy },
        new WordMeta() { Value = "acquire", Complexity = Complexity.easy },
        new WordMeta() { Value = "across", Complexity = Complexity.easy },
        new WordMeta() { Value = "act", Complexity = Complexity.easy },
        new WordMeta() { Value = "action", Complexity = Complexity.easy }
    };

    private void Awake()
    {
        _wordGameObjectWidth = ((RectTransform)word.transform).rect.width;

        stateManager.SubscribeToGameStartAction(StartGame);
    }

    public void SetGameSettings(GameSetting gameSetting)
    {
        _gameSetting = gameSetting;
    }

    private void StartGame()
    {
        _remainingWords = _gameSetting.MaxWords;
        _diedWords = 0;

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
        float speed = Random.Range(_gameSetting.MinSpeed, _gameSetting.MaxSpeed);
        spammedWord.Init(randomWord, speed);

        _spammedWords.Add(spammedWord);
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
            float timeBeforeTheNextSpamming = Random.Range(_gameSetting.MinTimeBetweenSpawns, _gameSetting.MaxTimeBetweenSpawns);
            Invoke(nameof(SpamWord), timeBeforeTheNextSpamming);
        }
    }

    public void Guess(string guess)
    {
        List<Word> correctlyTypedWords = CollectCorrectlyTypedWords(guess);

        RemoveCorrectlyTypedWords(correctlyTypedWords);

        CheckEndStatements();
    }

    private List<Word> CollectCorrectlyTypedWords(string guess)
    {
        List<Word> correctlyTypedWords = new();
        foreach (var word in _spammedWords)
        {
            if (word.IsCorrect(guess))
            {
                correctlyTypedWords.Add(word);
            }
        }

        return correctlyTypedWords;
    }

    private void RemoveCorrectlyTypedWords(List<Word> correctlyTypedWords)
    {
        foreach(var correctlyTypedWord in correctlyTypedWords)
        {
            Destroy(correctlyTypedWord.gameObject);

            scoringManager.Destroyed();

            _spammedWords.Remove(correctlyTypedWord);
        }

        _diedWords += correctlyTypedWords.Count;
    }

    public void Delete(Word word)
    {
        Destroy(word.gameObject);
        scoringManager.Missed();

        _spammedWords.Remove(word);
        _diedWords++;

        CheckEndStatements();
    }

    private void CheckEndStatements()
    {
        if (_diedWords >= _gameSetting.MaxWords)
        {
            stateManager.EndGame();
        }
    }
}
