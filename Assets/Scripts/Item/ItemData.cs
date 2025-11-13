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
    [CreateAssetMenu(fileName = "Item", menuName = "New Item")]

    public class ItemData : ScriptableObject
    {
        [Header("info")]
        public string displayName;
        public string description;
        public ItemType type;
        public GameObject dropPrefab;
    }
}
