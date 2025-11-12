using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TheDungeon.Item
    {
    public enum ItemType//아이템 타입 종류 정의
    {
        Key,
        Equip,
        Rock,
        food
    }

    public enum ConsumableType//소비템 타입 종류 정리
    {
        Health,
        stamina
    }

    [Serializable]
    public class ItemDataConsumable
    {
        public ConsumableType type;
        public float value;
    }

    [CreateAssetMenu(fileName = "Item", menuName = "New Item")]//밑에 입력할 수 있는 창을 만들어줌

    public class ItemData : ScriptableObject
    {
        [Header("info")]
        public string displayName;
        public string description;
        public ItemType type;
        public Sprite icon;
        public GameObject dropPrefab;

        [Header("Stacking")]
        public bool canStack;
        public int maxStackAmount;

        [Header("Consumable")]
        public ItemDataConsumable[] consumables;

    }
}
