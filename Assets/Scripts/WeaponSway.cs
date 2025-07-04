using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    //초기
    private Vector3 originPos;
    //현재 위치
    private Vector3 currentPos;
    //최대 변화량
    [SerializeField]
    private Vector3 limitPos;
    [SerializeField]
    private Vector3 finsightlimitPos;
    [SerializeField]
    private Vector3 smoothSway;
    [SerializeField]
    private GunController gunController;
    void Start()
    {
        originPos = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Inventory.inventoryActivated)
            TrySway();
    }

    private void TrySway()
    {
        if(Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)
        {
            Swaying();
           
        }
        else
        {
            BackToOriginPos();
        }
    }

    private void Swaying()
    {
        float moveX = Input.GetAxisRaw("Mouse X");
        float moveY = Input.GetAxisRaw("Mouse Y");
        if (!gunController.isFineSightMode)
        {
            currentPos.Set(Mathf.Clamp(Mathf.Lerp(currentPos.x, -moveX, smoothSway.x), -limitPos.x, limitPos.x),
                        Mathf.Clamp(Mathf.Lerp(currentPos.y, -moveY, smoothSway.x), -limitPos.y, limitPos.y),
                        originPos.z);       
        }
        else
        {
            currentPos.Set(Mathf.Clamp(Mathf.Lerp(currentPos.x, -moveX, smoothSway.y), -finsightlimitPos.x, finsightlimitPos.x),
                        Mathf.Clamp(Mathf.Lerp(currentPos.y, -moveY, smoothSway.y), -finsightlimitPos.y, finsightlimitPos.y),
                        originPos.z);;
        }
        transform.localPosition = currentPos;
    }

    private void BackToOriginPos()
    {
        Vector3.Lerp(currentPos, originPos, smoothSway.x);
        transform.localPosition = currentPos;
    }
}
