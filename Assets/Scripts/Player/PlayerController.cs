using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed=10;
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
    public bool isEatApple=false;

    private void Awake()
    {
        rb=GetComponent<Rigidbody>();
        CharacterManager.Instance.PlayerController = this;
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
        camCurXRot -= mouseDelta.y * lookSensitivity;
        camCurXRot=Mathf.Clamp(camCurXRot,minXLook,maxXLook);
        cameraContainer.localEulerAngles = new Vector3(camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase==InputActionPhase.Performed)
        {
            inputMove = context.ReadValue<Vector2>();
        }
        else if(context.phase==InputActionPhase.Canceled)
        {
            inputMove = Vector2.zero;
        }
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase==InputActionPhase.Started&&IsGrounded())
        {
            rb.AddForce(Vector2.up*jumpPower,ForceMode.Impulse);
        }
    }
    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (transform.right  * 0.2f) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (-transform.right  * 0.2f) + (transform.up * 0.05f), Vector3.down)
        };
        
        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i],1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }
    private void AdrenalineMode()
    {
        moveSpeed = 40;
        jumpPower = 200;
    }

    public IEnumerator AdrenalineStart()
    {
        AdrenalineMode();

        //if (isEatApple)
        //{
        //    yield return new WaitForSeconds(10f);
        //    UIManager.uiManager.ShowAdrenalineTimer_EatApple();
        //}
        //else
        //{
        //    yield return new WaitForSeconds(5f);
        //    UIManager.uiManager.ShowAdrenalineTimer();
        //} 구현 못함

        yield return new WaitForSeconds(10f);

        moveSpeed = 10;
        jumpPower = 80;
        Debug.Log("효과 끝!");
    }

    IEnumerator DlayFourSeconds()
    {
        yield return new WaitForSeconds(3f);

        StartCoroutine(AdrenalineStart());
    }

    private void RunAdrenalineCorutine()
    {
        StartCoroutine(DlayFourSeconds());
    }
    
    private void OnEnable()
    {
        Interaction.afterGetKey += RunAdrenalineCorutine;
    }

    private void OnDisable()
    {
        Interaction.afterGetKey -= RunAdrenalineCorutine;
    }
    private void OnCollisionStay(Collision other)
    {
        if (IsGrounded() && other.gameObject.CompareTag("SuperJumpFake"))
        {
            rb.AddForce(Vector3.up * 800, ForceMode.Impulse);
        }
    }
}
