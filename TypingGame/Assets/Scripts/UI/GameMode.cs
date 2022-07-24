using TMPro;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField]
    private GameSetting gameSetting;
    [SerializeField]
    private TMP_Text descriptionText;
    [SerializeField]
    private UiManager uiManager;

    private void Awake()
    {
        descriptionText.text = $"({gameSetting.MaxWords} words, " +
            $"{gameSetting.MinSpeed}-{gameSetting.MaxSpeed} s/spawn, " +
            $"{gameSetting.MinTimeBetweenSpawns}-{gameSetting.MaxTimeBetweenSpawns} px/s";
    }

    public void StartGame()
    {
        uiManager.StartGame(gameSetting);
    }
}
