using UnityEngine;
public class PlayerCondition : MonoBehaviour
{
    public int playerHp = 3;
    int damage;

    public void TakeDamage()
    {
        playerHp -= damage;
        UIManager.uiManager.ChangeHealthUI(playerHp);
        UIManager.uiManager.ShowBloodScreen();
        CheckDie();
    }

    public void CheckDie()
    {
        if(playerHp<=0)
        {
            playerHp = 0;
            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ObstacleObject obstacle = other.GetComponentInParent<ObstacleObject>();
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log($"장애물에 충돌했습니다");
            damage = obstacle.data.damage;
            TakeDamage();
        }
    }
}
