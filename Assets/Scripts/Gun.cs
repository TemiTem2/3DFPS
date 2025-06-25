using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName; // 총 이름
    public float range; // 총 사거리
    public float accuracy; // 총 정확도
    public float fireRate; // 총 발사 속도
    public float realoadTime; // 총 재장전 시간

    public int damage; // 총 데미지

    public int reloadBulletCount; // 총 탄약 수
    public int currentBulletCount; // 현재 탄약 수
    public int maxBulletCount; // 최대 소유 가능 개수
    public int carryBulletCount; // 현재 소지 탄약 수

    public float retroActionForece; // 반동
    public float retroActionFineSightForce; // 정조준 반동 세기

    public Vector3 fineSightOriginPos;

    public Animator anim;

    public ParticleSystem muzzleFlash; // 총구 화염 효과

    public AudioClip fire_Sound; // 총 소리


}
