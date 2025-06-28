using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{

    [SerializeField]
    private Gun currentGun; // 총 정보

    private float currentFireRate; // 연사 계산

    //상태변수
    private bool isReload = false;
    private bool isFineSightMode = false;

    [SerializeField]
    private Vector3 originPos;

    private AudioSource audioSource;

    private RaycastHit hitinfo;

    [SerializeField]
    private Camera thecam;

    [SerializeField]
    private GameObject hit_effect_prefab;
    private void Start()
    {
        originPos = Vector3.zero; // 초기 위치 설정
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        GunFireRateCalc();
        TryFire();
        TryReload();
        TryFineSight();
    }

    private void GunFireRateCalc() //연사속도 계산
    {
        if (currentGun.fireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
    }

    private void TryFire()//발사시도
    {
        
        if (Input.GetButton("Fire1") && currentFireRate <= 0&& isReload == false)
        {
            Fire();
        }
    }

    private void Fire()//발사 전 계산
    {
        if (!isReload)
        {
            if (currentGun.currentBulletCount > 0)
                Shoot();
            else
            {
                CancleFIneSight();
                StartCoroutine(ReloadCoroutine());
            }
        }
    }

    private void Shoot()//발사 후 계산
    {
        currentGun.currentBulletCount--; // 총알 감소
        currentFireRate = currentGun.fireRate;
        PlaySE(currentGun.fire_Sound);
        currentGun.muzzleFlash.Play(); // 총구 화염 효과 재생
        Hit();
        //총기반동 코루틴 실행
        StopAllCoroutines();
        StartCoroutine(RetroActionCoroutine());

    }

    private void Hit()
    {
        if(Physics.Raycast(thecam.transform.position, thecam.transform.forward, out hitinfo, currentGun.range))
        {
            var clone = Instantiate(hit_effect_prefab, hitinfo.point, Quaternion.LookRotation(hitinfo.normal)); // 히트 이펙트 생성
            Destroy(clone, 2f);
        }
    }

    IEnumerator RetroActionCoroutine() //반동
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

    private void PlaySE(AudioClip clip) //사운드 재생
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void TryReload() // 재장전 시도
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReload && currentGun.currentBulletCount < currentGun.reloadBulletCount)   
        {
            CancleFIneSight();
            StartCoroutine(ReloadCoroutine());
        }
    }

    IEnumerator ReloadCoroutine() //재장전
    {
        if (currentGun.carryBulletCount > 0) 
        {
            isReload = true;

            currentGun.anim.SetTrigger("Reload");

            currentGun.carryBulletCount += currentGun.currentBulletCount;
            currentGun.currentBulletCount = 0;

            yield return new WaitForSeconds(currentGun.realoadTime); // 재장전 시간 대기

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
            Debug.Log("탄약이 없습니다.");
        }
    }

    private void TryFineSight()//정조준 시도
    {
        if (Input.GetButtonDown("Fire2")&&!isReload)
        {
            FineSight();
        }
    }

    public void CancleFIneSight() //정조준 취소
    {
        if (isFineSightMode)
        {
            FineSight();
        }
    }

    private void FineSight()//정조준 로직
    {
        isFineSightMode = !isFineSightMode;
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

    IEnumerator FineSightActivateCoroutine() //정조준 활성화
    {
        while (currentGun.transform.localPosition != currentGun.fineSightOriginPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.2f);
            yield return null;
        }
    }

    IEnumerator FineSightDeactivateCoroutine() //정조준 비활성화
    {
        while (currentGun.transform.localPosition != originPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.2f);
            yield return null;
        }
    }
}
