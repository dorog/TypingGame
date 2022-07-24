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
                                $"{gameSetting.MinTimeBetweenSpawns}-{ gameSetting.MaxTimeBetweenSpawns} s/spawn, " +
                                $"{gameSetting.MinSpeed}-{gameSetting.MaxSpeed} px/s";
    }

    public void StartGame()
    {
        uiManager.StartGame(gameSetting);
    }
}
