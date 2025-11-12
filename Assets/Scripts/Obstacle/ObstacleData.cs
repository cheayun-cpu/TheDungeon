using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Obstacle", menuName = "New Obstacle")]//밑에 입력할 수 있는 창을 만들어줌

public class ObstacleData : ScriptableObject
{
    [Header("info")]
    public string name;
    public int damage;
    public GameObject dropPrefab;
}



