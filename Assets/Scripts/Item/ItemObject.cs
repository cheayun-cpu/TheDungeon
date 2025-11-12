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
        //플레이어 클래스에 아이템 데이터를 넘겨주니까 추가해줌, 플레이어 기준으로 플레이어가 상호작용한 아이템의 이벤트 정보를 알려주기 위해서 플레이어 클래스에 Action을 선언해둔거
       // CharacterManager.Instance.Player.itemData = data;//플레이어 클래스에 아이템 데이터를 저장
        
        gameObject.SetActive(false);//아이템 주웠으니까 맵에 있는건 삭제시키기

    }


}
