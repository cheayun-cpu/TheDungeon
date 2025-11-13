using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle", menuName = "New Obstacle")]

public class ObstacleData : ScriptableObject
{
    [Header("info")]
    public string name;
    public int damage;
    public GameObject dropPrefab;
}



