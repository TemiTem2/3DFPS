using UnityEngine;

public class CloseWeapon : MonoBehaviour
{
   
    public string closeWeaponName;//���� ����

    //���� ����
    public bool isAxe;
    public bool isPickaxe;
    public bool isHand;

    public float range;//���ݹ���
    public float damage;//���ݷ�
    public float workSpeed;//�۾��ӵ�
    public float attackDelay;//���ݵ�����
    public float attackDelayA;//����Ȱ��ȭ ����������
    public float attackDelayB;//���ݺ�Ȱ��ȭ ����������

    public Animator anim;//�ִϸ��̼�
}
