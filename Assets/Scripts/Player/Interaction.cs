using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TheDungeon.Item;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class Interaction : MonoBehaviour//상호작용 스크립트
{
    public float checkRate = 0.05f;//카메라 기준으로 레이 쏠 때 얼마나 자주 최신화를 할지 그 시간 주기를 정한거 0.05초마다 업데이트 되게 함
    private float lastCheckTime;//마지막으로 체크한 시간을 변수로 선언함, 위의 주기를 적용하기 위해 필요한 변수
    public float maxCheckDistance;//얼마나 멀리 있는걸 체크할지 정하는 변수
    public LayerMask layerMask;//어떤 레이어가 달려있는 게임오브젝트를 검출할건지 정하기 위한 변수

    public GameObject curSeeGameObject;//현재 바라보고 있는 게임오브젝트의 정보를 저장하는 변수
    private IInteractable curInteractable;//인터페이스를 저장하는 변수

    public GameObject curHoldObject;//
    public ItemObject curHoldObjectData;//아이템의 ItemData를 가져오기 위해 사용함

    public TextMeshProUGUI UIText;
    private Camera camera;
    private int throwPower=15;

    public static Action afterGetKey;

    private void Start()
    {
        camera = Camera.main;//카메라가 어떤건지 선언만 하지말고 넣어주는 과정 필요
    }

    private void Update()
    {

        if (Time.time - lastCheckTime > checkRate)//너무 자주 레이 쏘지마
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));//카메라 기준으로 백터3 기준으로 스크린 너비 반, 높이 반=카메라 정중앙에서 레이를 쏴 
            RaycastHit hit;//부딫혔을 때 레이랑 부딫힌 오브젝트의 정보를 담아주는 변수 선언

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))//Physics.Raycast 닿았는지 검사하는 함수
                   //위에 있는 레이를 쏴서 충돌한 오브젝트 정보 받아오는데, maxCheckDistance 거리 안의 내가 지정한 layerMask에 해당하는 오브젝트만 가져와
            {
                Debug.DrawRay(ray.origin, ray.direction * maxCheckDistance, Color.red,1f);//duration은 선이 나오는데 걸리는 시간
                if (hit.collider.gameObject != curSeeGameObject)//처음 발견했을 때만 최신화하는거 이 조건문이 없으면 볼 때마다 최신화해서 넣게 됨
                {
                    curSeeGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();//hit.collider에 붙어 있는 컴포넌트 중에서
                    //IInteractable 인터페이스를 구현한 컴포넌트를 찾아서 가져온다

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

        if (context.phase==InputActionPhase.Started&&curInteractable!=null)//보고 있는 오브젝트를 눌렀을 때
        {
            if (curHoldObject == null)//현재 들고 있는게 없으면 
            {
                curHoldObject = curSeeGameObject;//현재 잡은 오브젝트 저장
                curHoldObjectData = curHoldObject.GetComponent<ItemObject>();//현재 잡은 오브젝트의 데이터 저장
                HoldObjectPositionReset();//카메라에 오브젝트 뜨게 만들기

                curInteractable.Oninteract();//??

                if (curHoldObjectData.data.displayName == "Key")//위치조정하기
                {
                    afterGetKey?.Invoke();
                    
                    //( +=카메라 움직임 +=바닥무너짐 +=시간차UI) 
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
            curHoldObject.transform.localRotation = Quaternion.Euler(4.757f, -12.415f, 14.23f);//각도 넣어주는 함수
        }
        else if (curHoldObjectData.data.type == ItemType.Apple)
        {
            curHoldObject.transform.localPosition = new Vector3(4.14f, -2.2f, 3.88f);
            curHoldObject.transform.localRotation = Quaternion.identity;//회전값==(0,0,0);
        }
        else if (curHoldObjectData.data.displayName == "Rock")
        {
            curHoldObject.transform.localPosition = new Vector3(1f, -0.8f, 1f);
            curHoldObject.transform.localRotation = Quaternion.identity;
        }
    }

    public void OnInteractThrow(InputAction.CallbackContext context)//아이템 던지기
    {
        if (context.phase == InputActionPhase.Started && curHoldObject != null)//들고있는 오브젝트를 던지기 키를 입력할 때
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

    IEnumerator PowerUp()
    {
        throwPower = 100;
        Debug.Log("파워100");
        yield return new WaitForSeconds(10f);
        throwPower = 15;
        Debug.Log("돌아옴");
    }







}
