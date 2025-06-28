using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public static bool isActive = true;

    [SerializeField]
    private Gun currentGun; // �� ����

    private float currentFireRate; // ���� ���

    //���º���
    private bool isReload = false;
    public bool isFineSightMode = false;

    [SerializeField]
    private Vector3 originPos;

    private AudioSource audioSource;

    private RaycastHit hitinfo;

    [SerializeField]
    private Camera thecam;
    private Crosshair crosshair;

    [SerializeField]
    private GameObject hit_effect_prefab;
    private void Start()
    {
        originPos = Vector3.zero; // �ʱ� ��ġ ����
        audioSource = GetComponent<AudioSource>();
        crosshair = FindAnyObjectByType<Crosshair>();

        WeaponManager.currentWeapon = currentGun.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentGun.anim;
    }
    void Update()
    {
        if (isActive) { 
        GunFireRateCalc();
        TryFire();
        TryReload();
        TryFineSight();
        }
    }

    private void GunFireRateCalc() //����ӵ� ���
    {
        if (currentGun.fireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
    }

    private void TryFire()//�߻�õ�
    {
        
        if (Input.GetButton("Fire1") && currentFireRate <= 0&& isReload == false)
        {
            Fire();
        }
    }

    private void Fire()//�߻� �� ���
    {
        if (!isReload)
        {
            if (currentGun.currentBulletCount > 0)
                Shoot();
            else
            {
                CancelFineSight();
                StartCoroutine(ReloadCoroutine());
            }
        }
    }

    private void Shoot()//�߻� �� ���
    {
        crosshair.FireAnimation(); // �߻� �ִϸ��̼� ����
        currentGun.currentBulletCount--; // �Ѿ� ����
        currentFireRate = currentGun.fireRate;
        PlaySE(currentGun.fire_Sound);
        currentGun.muzzleFlash.Play(); // �ѱ� ȭ�� ȿ�� ���
        Hit();
        //�ѱ�ݵ� �ڷ�ƾ ����
        StopAllCoroutines();
        StartCoroutine(RetroActionCoroutine());

    }

    private void Hit()
    {
        if(Physics.Raycast(thecam.transform.position, 
            thecam.transform.forward+ new Vector3(
            Random.Range(-crosshair.GetGunAccuracy()-currentGun.accuracy, crosshair.GetGunAccuracy() + currentGun.accuracy), 
            Random.Range(-crosshair.GetGunAccuracy() - currentGun.accuracy, crosshair.GetGunAccuracy() + currentGun.accuracy),
            0)
            , out hitinfo, currentGun.range))
        {
            var clone = Instantiate(hit_effect_prefab, hitinfo.point, Quaternion.LookRotation(hitinfo.normal)); // ��Ʈ ����Ʈ ����
            Destroy(clone, 2f);
        }
    }

    IEnumerator RetroActionCoroutine() //�ݵ�
    {
        Vector3 recoilBack=new Vector3(currentGun.retroActionForece,originPos.y,originPos.z);
        Vector3 retroActionRecoilBack = new Vector3(currentGun.retroActionFineSightForce, currentGun.fineSightOriginPos.y, currentGun.fineSightOriginPos.z);

        if (!isFineSightMode)
        {
            currentGun.transform.localPosition = originPos;

            while(currentGun.transform.localPosition.x <= currentGun.retroActionForece -0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, recoilBack, 0.4f);
                yield return null;
            }

            while (currentGun.transform.localPosition.x != originPos.x)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.1f);
                yield return null;
            }

        }
        else
        {
            currentGun.transform.localPosition = currentGun.fineSightOriginPos;

            while (currentGun.transform.localPosition.x <= currentGun.retroActionFineSightForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, retroActionRecoilBack, 0.4f);
                yield return null;
            }

            while (currentGun.transform.localPosition.x != currentGun.fineSightOriginPos.x)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.1f);
                yield return null;
            }
        }
    }

    private void PlaySE(AudioClip clip) //���� ���
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void TryReload() // ������ �õ�
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReload && currentGun.currentBulletCount < currentGun.reloadBulletCount)   
        {
            CancelFineSight();
            StartCoroutine(ReloadCoroutine());
        }
    }

    public void CancelReload()
    {
        if (isReload)
        {
            StopAllCoroutines();
            isReload = false;
        }
    }

    IEnumerator ReloadCoroutine() //������
    {
        if (currentGun.carryBulletCount > 0) 
        {
            isReload = true;

            currentGun.anim.SetTrigger("Reload");

            currentGun.carryBulletCount += currentGun.currentBulletCount;
            currentGun.currentBulletCount = 0;

            yield return new WaitForSeconds(currentGun.realoadTime); // ������ �ð� ���

            if (currentGun.carryBulletCount >= currentGun.reloadBulletCount)
            {
                currentGun.currentBulletCount = currentGun.reloadBulletCount;
                currentGun.carryBulletCount -= currentGun.reloadBulletCount;
            }
            else
            {
                currentGun.currentBulletCount = currentGun.carryBulletCount;
                currentGun.carryBulletCount = 0;
            }

            isReload = false;
        }
        else
        {
            Debug.Log("ź���� �����ϴ�.");
        }
    }

    private void TryFineSight()//������ �õ�
    {
        if (Input.GetButtonDown("Fire2")&&!isReload)
        {
            FineSight();
        }
    }

    public void CancelFineSight() //������ ���
    {
        if (isFineSightMode)
        {
            FineSight();
        }
    }

    private void FineSight()//������ ����
    {
        isFineSightMode = !isFineSightMode;
        crosshair.FineSightAnimation(isFineSightMode);
        currentGun.anim.SetBool("FineSightMode",isFineSightMode);

        if (isFineSightMode)
        {
            StopAllCoroutines();
            StartCoroutine(FineSightActivateCoroutine());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(FineSightDeactivateCoroutine());
        }
    }

    IEnumerator FineSightActivateCoroutine() //������ Ȱ��ȭ
    {
        while (currentGun.transform.localPosition != currentGun.fineSightOriginPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.2f);
            yield return null;
        }
    }

    IEnumerator FineSightDeactivateCoroutine() //������ ��Ȱ��ȭ
    {
        while (currentGun.transform.localPosition != originPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.2f);
            yield return null;
        }
    }

    public Gun GetGun() //���� �� ���� ��ȯ
    {
        return currentGun;
    }

    public bool GetFinesightMode()
    {
        return isFineSightMode;
    }

    public void GunChange(Gun gun)
    {
        if(WeaponManager.currentWeapon !=null)
            WeaponManager.currentWeapon.gameObject.SetActive(false); // ���� ���� ��Ȱ��ȭ

        currentGun = gun; // ���ο� �� ����
        WeaponManager.currentWeapon = currentGun.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentGun.anim;


        currentGun.transform.localPosition = Vector3.zero;
        currentGun.gameObject.SetActive(true);
        isActive = true;
    }
}
