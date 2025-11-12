using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheDungeon.Item;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerController controller;

    public GameObject curHoldObject;
    public ItemObject curHoldObjectData;
    // Dictionary<ItemType,ItemData>inventory = new Dictionary<ItemType,ItemData>();  
    //여러 아이템을 사용하는 경우

    //public Action OnAddItem;
    public GameObject holdItem;


    private void Awake()
    {
        CharacterManager.Instance.Player = this;
    }

    private void isMyHand()
    {
        if(curHoldObjectData.data.type == ItemType.Key) //들고 있는게 열쇠 아이템이면
        {
            
        }
    //private void OnEnable()
    //{
    //    CharacterManager.Instance.Player.OnAddItem += AddApple;
    //}

    //private void OnDisable()
    //{
    //    CharacterManager.Instance.Player.OnAddItem -= AddApple;
    //}
    }


    

    
   

}
