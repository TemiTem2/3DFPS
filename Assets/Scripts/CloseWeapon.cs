using UnityEngine;

public class CloseWeapon : MonoBehaviour
{
   
    public string closeWeaponName;//무기 구분

    //무기 유형
    public bool isAxe;
    public bool isPickaxe;
    public bool isHand;

    public float range;//공격범위
    public float damage;//공격력
    public float workSpeed;//작업속도
    public float attackDelay;//공격딜레이
    public float attackDelayA;//공격활성화 시점딜레이
    public float attackDelayB;//공격비활성화 시점딜레이

    public Animator anim;//애니메이션
}
