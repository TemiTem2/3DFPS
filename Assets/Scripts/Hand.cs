using UnityEngine;

public class Hand : MonoBehaviour
{
   
    public string handName;//손 구분
    public float range;//공격범위
    public float damage;//공격력
    public float workSpeed;//작업속도
    public float attackDelay;//공격딜레이
    public float attackDelayA;//공격활성화 시점딜레이
    public float attackDelayB;//공격비활성화 시점딜레이

    public Animator anim;//애니메이션
}
