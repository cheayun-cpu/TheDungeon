using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TheDungeon.Item
    {
    [Serializable]

    public enum ItemType
    {
        Apple,
        NoApple
    }
    public class ItemDataConsumable
    {
        public float value;
    }

    [CreateAssetMenu(fileName = "Item", menuName = "New Item")]//밑에 입력할 수 있는 창을 만들어줌

    public class ItemData : ScriptableObject
    {
        [Header("info")]
        public string displayName;
        public string description;
        public ItemType type;
        public GameObject dropPrefab;
                
    }
}
