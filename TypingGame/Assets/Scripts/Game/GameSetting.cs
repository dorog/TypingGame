using UnityEngine;

[CreateAssetMenu(fileName = "GameSetting", menuName = "ScriptableObjects/GameSetting")]
public class GameSetting : ScriptableObject
{
    public int MaxWords;

    public float MinSpeed;
    public float MaxSpeed;

    public float MinTimeBetweenSpawns;
    public float MaxTimeBetweenSpawns;

    public Complexity Complexity;
}
