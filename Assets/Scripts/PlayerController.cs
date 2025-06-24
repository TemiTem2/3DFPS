using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float WalkSpeed;

    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private float cameraRotationLimit;
    private float CurrentcameraRotationX = 0f;

    [SerializeField]
    private Camera playerCamera;


    private Rigidbody myRb;
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        cameraRotation();
        CharacterRotation();
    }

    private void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");
        Vector3 moveHori = transform.right * moveDirX;
        Vector3 moveVerti = transform.forward * moveDirZ;

        Vector3 movement = (moveHori + moveVerti).normalized * WalkSpeed * Time.deltaTime;
        
        myRb.MovePosition(myRb.position + movement);
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
