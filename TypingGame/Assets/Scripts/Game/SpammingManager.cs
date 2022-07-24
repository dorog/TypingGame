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

    private readonly Dictionary<Complexity, List<WordMeta>> _allWords = new()
    {
        { Complexity.easy, new List<WordMeta>() },
        { Complexity.medium, new List<WordMeta>() },
        { Complexity.hard, new List<WordMeta>() }
    };

    private List<WordMeta> _availableWordMetas;

    private void Awake()
    {
        _wordGameObjectWidth = ((RectTransform)word.transform).rect.width;

        stateManager.SubscribeToGameStartAction(StartGame);

        InitWords();
    }

    private void InitWords()
    {
        TextAsset wordsJson = Resources.Load<TextAsset>("eng_words");

        LanguageWords languageWords = JsonUtility.FromJson<LanguageWords>(wordsJson.text);
        foreach (var wordMeta in languageWords.wordMetas)
        {
            _allWords[wordMeta.Complexity].Add(wordMeta);
        }
    }

    public void SetGameSettings(GameSetting gameSetting)
    {
        _gameSetting = gameSetting;
    }

    private void StartGame()
    {
        _remainingWords = _gameSetting.MaxWords;
        _availableWordMetas = _allWords[_gameSetting.Complexity];
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
        int randomWordMetaIndex = Random.Range(0, _availableWordMetas.Count);
        return _availableWordMetas[randomWordMetaIndex];
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
