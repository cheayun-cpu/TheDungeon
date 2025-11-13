using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheDungeon.Item;

public interface IInteractable
{
    public string GetInteractUI(GameObject interactObject);//화면에 띄워줄 UI
    public void Oninteract();//상호작용할 때의 함수
}


public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractUI(GameObject interactObject)//설명 UI 띄운거
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    public void Oninteract()//줍는 상호작용만 작동
    {
       //
    }


}
