using UnityEngine;

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
