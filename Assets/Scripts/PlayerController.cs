using System.Collections;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    //스피드 조정 변수
    [SerializeField]
    private float WalkSpeed;
    [SerializeField]
    private float RunSpeed;
    [SerializeField]
    private float crouchSpeed;
    private float applySpeed;


    [SerializeField]
    private float JumpForce;
    //상태변수
    private bool isWalk = false;
    private bool isRun = false;
    private bool isCrouch = false;
    private bool isGround = true;

    //움직임 체크 변수
    private Vector3 lastPos;
    //앉았을때 얼마나 앉을지 변수
    [SerializeField]
    private float crouchPosY;
    private float originalPosY;
    private float applyCrouchPosY;

    private CapsuleCollider capsuleCollider;

    //마우스 감도 및 카메라 회전 제한 변수
    [SerializeField]
    private float lookSensitivity;
    [SerializeField]
    private float cameraRotationLimit;
    private float CurrentcameraRotationX = 0f;

    [SerializeField]
    private Camera playerCamera;
    private GunController gunController;
    private Crosshair crosshair;


    private Rigidbody myRb;
    void Start()
    {
        myRb = GetComponent<Rigidbody>(); 
        capsuleCollider = GetComponent<CapsuleCollider>();
        gunController = FindAnyObjectByType<GunController>();
        crosshair = FindAnyObjectByType<Crosshair>();
        applySpeed = WalkSpeed;
        originalPosY = playerCamera.transform.localPosition.y;
        applyCrouchPosY = originalPosY;
    }

    // Update is called once per frame
    void Update()
    {
        Isground();
        TryJump();
        TryRun();
        TryCrouch();
        Move();
        MoveCheck();
        cameraRotation();
        CharacterRotation();
    }

    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)//점프 시도
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (isGround)
        {
            if(isCrouch)//앉은 상태에서 점프시 앉기 해제
            {
                Crouch();

            }
            myRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isGround = false;
        }
    }

    private void Isground()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
        crosshair.JumpAnimation(!isGround);
    }
    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (isCrouch)//앉은 상태에서 달리기시 앉기 해제
            {
                Crouch();
            }
            gunController.CancelFineSight();
            isRun = true;
            applySpeed = RunSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRun = false;
            applySpeed = WalkSpeed;
        }
        crosshair.RunningAnimation(isRun);
    }

    private void TryCrouch() 
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();            
        }
        StartCoroutine(CrouchCoroutine());
    }

    private void Crouch()
    {
        isCrouch = !isCrouch;
        crosshair.CrouchingAnimation(isCrouch);

        if (isCrouch)
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
            //capsuleCollider.height = 1f;   
        }
        else
        {
            applySpeed = WalkSpeed;
            applyCrouchPosY = originalPosY;
            //capsuleCollider.height = 2f;
        }
    }
    IEnumerator CrouchCoroutine()
    {
        float posY = playerCamera.transform.localPosition.y;
        int count = 0;
        while (posY !=applyCrouchPosY)
        {
            count++;
            posY = Mathf.Lerp(posY, applyCrouchPosY, 0.3f);
            playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, posY, playerCamera.transform.localPosition.z);
            yield return null;
            if (count > 15)
            {
                break;
            }
            
        }
        playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, applyCrouchPosY, playerCamera.transform.localPosition.z);
        yield return new WaitForSeconds(1f);
    }
    private void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");
        Vector3 moveHori = transform.right * moveDirX;
        Vector3 moveVerti = transform.forward * moveDirZ;

        Vector3 movement = (moveHori + moveVerti).normalized * applySpeed * Time.deltaTime;
        
        myRb.MovePosition(myRb.position + movement);
    }

    private void MoveCheck()
    {
        if (!isRun&&!isCrouch&&isGround)
        {
            if (Vector3.Distance(lastPos,transform.position) >= 0.001f)
            {
                isWalk = true;
            }
            else
            {
                isWalk = false;
            }
        }
        crosshair.WalkingAnimation(isWalk);
        lastPos = transform.position;
    }
    private void CharacterRotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * lookSensitivity;
        Vector3 rotationY = new Vector3(0f, mouseX, 0f);
        myRb.MoveRotation(myRb.rotation * Quaternion.Euler(rotationY));
    }
    private void cameraRotation()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y") * lookSensitivity;
        CurrentcameraRotationX -= xRotation;
        CurrentcameraRotationX = Mathf.Clamp(CurrentcameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        playerCamera.transform.localEulerAngles = new Vector3(CurrentcameraRotationX, 0f,0f);
    }
}
