using System;
using System.Collections;
using TheDungeon.Item;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curSeeGameObject;
    private IInteractable curInteractable;

    public GameObject curHoldObject;
    public ItemObject curHoldObjectData;

    public TextMeshProUGUI UIText;
    private Camera camera;
    private int throwPower=15;

    public static Action afterGetKey;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (Time.time - lastCheckTime > checkRate)//너무 자주 레이 쏘지마
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curSeeGameObject)
                {
                    curSeeGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                  
                    UIManager.uiManager.ShowDescriptionUI(curInteractable.GetInteractUI(curSeeGameObject));//현재 인식한 오브젝트를 넣어서 출력해
                }
            }

            else
            {
                curSeeGameObject = null;
                curInteractable = null;
                UIManager.uiManager.CloseDescriptionUI();
            }
        }
    }

    public void OnInteractInput(InputAction.CallbackContext context)//아이템 줍기
    {

        if (context.phase==InputActionPhase.Started&&curInteractable!=null)
        {
            if (curHoldObject == null)
            {
                curHoldObject = curSeeGameObject;
                curHoldObjectData = curHoldObject.GetComponent<ItemObject>();
                HoldObjectPositionReset();

                curInteractable.Oninteract();//??

                if (curHoldObjectData.data.displayName == "Key")
                {
                    afterGetKey?.Invoke();
                }

                curSeeGameObject = null;
                curInteractable = null;
                UIManager.uiManager.CloseDescriptionUI();
            }
            else
            {
                UIManager.uiManager.CantGetItem();
            }
        }
    }


    

    public void HoldObjectPositionReset()//주운 오브젝트 표시 및 위치 조정하기
    {
        Rigidbody rb = curHoldObject.GetComponent<Rigidbody>();
        Collider collider = curHoldObject.GetComponent<Collider>();
        rb.isKinematic = true;
        collider.isTrigger = true;

        Transform camTransform = Camera.main.transform;
        curHoldObject.transform.SetParent(camTransform);
        
        if (curHoldObjectData.data.displayName == "Key")
        {
            curHoldObject.transform.localPosition = new Vector3(4.52f, -2.11f, 4.22f);
            curHoldObject.transform.localRotation = Quaternion.Euler(4.757f, -12.415f, 14.23f);
        }
        else if (curHoldObjectData.data.type == ItemType.Apple)
        {
            curHoldObject.transform.localPosition = new Vector3(4.14f, -2.2f, 3.88f);
            curHoldObject.transform.localRotation = Quaternion.identity;
        }
        else if (curHoldObjectData.data.displayName == "Rock")
        {
            curHoldObject.transform.localPosition = new Vector3(1f, -0.8f, 1f);
            curHoldObject.transform.localRotation = Quaternion.identity;
        }
    }

    public void OnInteractThrow(InputAction.CallbackContext context)//아이템 던지기
    {
        if (context.phase == InputActionPhase.Started && curHoldObject != null)
        {
            Rigidbody rb = curHoldObject.GetComponent<Rigidbody>();
            Collider collider = curHoldObject.GetComponent<Collider>();
            rb.isKinematic = false;
            collider.isTrigger = false;

            curHoldObject.transform.SetParent(null);//상위하위 연결 끊기
            
            rb.AddForce(transform.forward * throwPower, ForceMode.Impulse);

            curHoldObject=null;

        }
    }

    public void OnInteractEat(InputAction.CallbackContext context)//아이템 먹기
    {
        if (context.phase == InputActionPhase.Started && curHoldObject != null)
        {
            curHoldObject.SetActive(false);

            if (curHoldObjectData.data.displayName== "GreenApple")
            {
                StartCoroutine(PowerUp());
                Debug.Log("파워업실행");
            }

            else if(curHoldObjectData.data.displayName == "Apple")
            {
                CharacterManager.Instance.PlayerController.isEatApple=true;
            }
            curHoldObject = null;
        }
    }

    IEnumerator PowerUp()//사과 아이템 먹은 후 효과
    {
        throwPower = 100;
        Debug.Log("파워100");
        yield return new WaitForSeconds(10f);
        throwPower = 15;
        Debug.Log("돌아옴");
    }







}
