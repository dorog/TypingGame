using System;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private Action _gameStarted;
    private Action _gameEnded;

    public void SubscribeToGameStartAction(Action action)
    {
        _gameStarted += action;
    }

    public void SubscribeToGameEndAction(Action action)
    {
        _gameEnded += action;
    }

    public void StartGame()
    {
        _gameStarted?.Invoke();
    }

    public void EndGame()
    {
        _gameEnded?.Invoke();
    }
}
