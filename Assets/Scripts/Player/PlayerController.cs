
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed=5;
    private Vector2 inputMove;
    private float jumpPower=80;
    public LayerMask groundLayerMask;

    [Header("Look")]
    
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    private Rigidbody rb;

   
    private void Awake()
    {
        rb=GetComponent<Rigidbody>();
    }

    private void Start()
    {
       
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    private void FixedUpdate()
    {
        Move();
        
    }


    private void Move()
    {
        Vector3 dir = transform.forward * inputMove.y + transform.right * inputMove.x;
        dir *= moveSpeed;
        dir.y=rb.velocity.y;

        rb.velocity=dir;
    } 

    void CameraLook()
    {
        camCurXRot -= mouseDelta.y * lookSensitivity;//방향 정하는 방식을 반대로 하고 싶으면 여기다가 -를 넣는게 더 나을 것
        camCurXRot=Mathf.Clamp(camCurXRot,minXLook,maxXLook);
        cameraContainer.localEulerAngles = new Vector3(camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }
    public void OnMove(InputAction.CallbackContext context)//입력된 현재상태 불러오기
    {

        
        if (context.phase==InputActionPhase.Performed)//phase 분기점을 의미함
        {
            inputMove = context.ReadValue<Vector2>();
        }
        else if(context.phase==InputActionPhase.Canceled)
        {
            inputMove = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)//입력된 현재상태 불러오기
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
       // Debug.Log("점프 입력");
       // Debug.Log($"{IsGrounded()},{context.phase}");

        if (context.phase==InputActionPhase.Started&&IsGrounded())
        {
            rb.AddForce(Vector2.up*jumpPower,ForceMode.Impulse);
        }
    }



    public void OnThrow(InputAction.CallbackContext context)
    {

        //손에 든거 다 던져
    }
   


    bool IsGrounded()//바닥을 밟고 있는지 체크
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (transform.right  * 0.2f) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (-transform.right  * 0.2f) + (transform.up * 0.05f), Vector3.down)
        };
        
        //transform.position + (transform.forward * 0.2f) 플레이어 앞뒤좌우0.2m씩 위치에서 레이 쏠 것

        //+ (transform.up * 0.01f)레이를 발바닥보다 약간 위쪽에서 시작하게 합니다.

        //이유는, 발바닥과 바닥 콜라이더가 정확히 붙어있으면 레이가 즉시 충돌로 인식되지 않는 경우가 있어서 살짝 띄워 쏘는 겁니다. (시작점 위치 0.01~0.05f 정도가 적당)

        for (int i = 0; i < rays.Length; i++)
        {
            //Debug.DrawRay(rays[i].origin, rays[i].direction, Color.red);//duration은 선이 나오는데 걸리는 시간
            if (Physics.Raycast(rays[i],1f, groundLayerMask))//groundLayerMask레이어에 속하는 0.5m 범위 내의 그라운드를 감지해
            {
                return true;
            }
        }
        return false;
    }

    private void AdrenalineMode()
    {
        moveSpeed = 10;
        jumpPower = 100;
    }





    private void OnCollisionStay(Collision other)
    {
        if (IsGrounded()&& other.gameObject.CompareTag("SuperJump"))
        {
            jumpPower = 200;
        }

        else if (IsGrounded() && other.gameObject.CompareTag("SuperJumpFake"))
        {
            rb.AddForce(Vector3.up * 100, ForceMode.Impulse);
        }
        else if(other.gameObject.CompareTag("Arrow"))
        {
            UIManager.uiManager.ShowBloodScreen();

        }



    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("SuperJump"))
        {
            jumpPower = 80;
        }
    }

}
