using UnityEngine;

public class CharacterManager : MonoBehaviour//현재 플레이어를 전역적으로 저장하고 외부에서 참조할 수 있게 함
{
    private static CharacterManager instance;//전역 1개, 외부접근 불가로 변수선언해준거고

    public static CharacterManager Instance//외부접근용(가져오기만 가능)
    {
        get 
        {
            if (instance == null)//인스턴스가 없으면 새로 생성하는 방어코드
            {
                instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return instance; 
        }
    }

   
    public Player Player 
    {get; set;}

    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance=this;//오브젝트 집어넣고
            DontDestroyOnLoad(gameObject);

        }
        else if(instance != this)//나 자신이 아닌 중복개체만
        {
            Destroy(gameObject);
        }
    }


}
