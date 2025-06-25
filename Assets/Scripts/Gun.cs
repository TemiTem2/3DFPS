using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName; // �� �̸�
    public float range; // �� ��Ÿ�
    public float accuracy; // �� ��Ȯ��
    public float fireRate; // �� �߻� �ӵ�
    public float realoadTime; // �� ������ �ð�

    public int damage; // �� ������

    public int reloadBulletCount; // �� ź�� ��
    public int currentBulletCount; // ���� ź�� ��
    public int maxBulletCount; // �ִ� ���� ���� ����
    public int carryBulletCount; // ���� ���� ź�� ��

    public float retroActionForece; // �ݵ�
    public float retroActionFineSightForce; // ������ �ݵ� ����

    public Vector3 fineSightOriginPos;

    public Animator anim;

    public ParticleSystem muzzleFlash; // �ѱ� ȭ�� ȿ��

    public AudioClip fire_Sound; // �� �Ҹ�


}
