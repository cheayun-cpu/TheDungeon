using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public int playerHp = 3;
    
    int damage;

    public void TakeDamage()
    {
        playerHp -= damage;
        UIManager.uiManager.ChangeHealthUI(playerHp);
        Debug.Log($"{damage}를 입었습니다. 현재 체력:{playerHp}");
        CheckDie();
    }

    public void CheckDie()
    {
        if(playerHp<=0)
        {
            playerHp = 0;
            GameManager.Instance.isDie=true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ObstacleObject obstacle = other.GetComponentInParent<ObstacleObject>();
        if (other.CompareTag("Arrow")|| other.CompareTag("Ax"))
        {
            Debug.Log($"장애물에 충돌했습니다");
            damage = obstacle.data.damage;
            TakeDamage();
        }
    }

}
