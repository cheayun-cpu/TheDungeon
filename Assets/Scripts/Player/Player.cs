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
       public GameObject holdItem;


    private void Awake()
    {
        CharacterManager.Instance.Player = this;
    }

        
}
