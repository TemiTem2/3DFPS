using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Switch;

public class WeaponManager : MonoBehaviour
{
    //���ⱳü �ߺ� ����
    public static bool isChangeWeapon = false;

    [SerializeField]
    private float changeWeaponDelayTime;
    [SerializeField]
    private float changeWeaponEndDelayTime;

    //�������� ����
    [SerializeField]
    private Gun[] guns;
    [SerializeField]
    private CloseWeapon[] hands;
    [SerializeField]
    private CloseWeapon[] axes;
    [SerializeField]
    private CloseWeapon[] pickaxes;

    //���� ������ ���� �ϱ� ���� ��ųʸ�
    private Dictionary<string, Gun> gunDictionary = new Dictionary<string, Gun>();
    private Dictionary<string, CloseWeapon> handDictionary = new Dictionary<string, CloseWeapon>();
    private Dictionary<string, CloseWeapon> axeDictionary = new Dictionary<string, CloseWeapon>();
    private Dictionary<string, CloseWeapon> pickaxeDictionary = new Dictionary<string, CloseWeapon>();

    [SerializeField]
    private GunController gunController;
    [SerializeField]
    private HandController handController;
    [SerializeField]
    private AxeController axeController;
    [SerializeField]
    private PickaxeController pickaxeController;

    //���� ���� Ÿ��
    [SerializeField]
    private string currentWeaponType;

    //���� ����� �ִϸ��̼�
    public static Transform currentWeapon;
    public static Animator currentWeaponAnim;
    void Start()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            gunDictionary.Add(guns[i].gunName, guns[i]);
        }
        for (int i = 0; i < hands.Length; i++)
        {
            handDictionary.Add(hands[i].closeWeaponName, hands[i]);
        }
        for (int i = 0; i < axes.Length; i++)
        {
            axeDictionary.Add(axes[i].closeWeaponName, axes[i]);
        }
        for (int i = 0; i < pickaxes.Length; i++)
        {
            pickaxeDictionary.Add(pickaxes[i].closeWeaponName, pickaxes[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isChangeWeapon)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                StartCoroutine(ChangeWeaponCoroutine("Hand", "No_Item"));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                StartCoroutine(ChangeWeaponCoroutine("Gun", "SubMachineGun1"));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                StartCoroutine(ChangeWeaponCoroutine("Axe", "Axe"));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                StartCoroutine(ChangeWeaponCoroutine("Pickaxe", "Pickaxe"));
            }
        }
    }

    public IEnumerator ChangeWeaponCoroutine(string type,string name)
    {
        isChangeWeapon = true;
        currentWeaponAnim.SetTrigger("Weapon_Out");

        yield return new WaitForSeconds(changeWeaponDelayTime); //���� ��ü ������

        CancelPreWeaponAction();
        WeaponChange(type,name);

        yield return new WaitForSeconds(changeWeaponEndDelayTime);
        currentWeaponType = type; //���� ���� Ÿ�� ����
        isChangeWeapon = false;
    }

    private void CancelPreWeaponAction() //���� ���� �׼� ���
    {
        switch(currentWeaponType)
        {
            case "Gun":
                gunController.CancelFineSight();
                gunController.CancelReload();
                GunController.isActive = false;
                break;
            case "Hand":
                HandController.isActive = false;
                break;
            case "Axe":
                AxeController.isActive = false;
                break;
            case "Pickaxe":
                PickaxeController.isActive = false;
                break;
        }
    }

    private void WeaponChange(string type, string name) //���� ��ü
    {
        if (type == "Gun")
        {
            gunController.GunChange(gunDictionary[name]);
        }
        else if (type == "Hand")
        {
            handController.CloseWeaponChange(handDictionary[name]);
        }
        else if (type == "Axe")
        {
            axeController.CloseWeaponChange(axeDictionary[name]);
        }
        else if (type == "Pickaxe")
        {
            pickaxeController.CloseWeaponChange(pickaxeDictionary[name]);
        }
    }
}
