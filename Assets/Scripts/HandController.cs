using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField]
    private Hand currentHand;//���� ��

    private bool isAttack = false;//���������� ����
    private bool isSwing = false;

    private RaycastHit hitinfo;

    void Update()
    {
        TryAttack();
    }

    private void TryAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            if (!isAttack)
            {
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        currentHand.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(currentHand.attackDelayA);
        isSwing = true;

        StartCoroutine(HitCoroutine());//���� Ȱ��ȭ ����

        yield return new WaitForSeconds(currentHand.attackDelayB);
        isSwing = false;

        yield return new WaitForSeconds(currentHand.attackDelay- currentHand.attackDelayA- currentHand.attackDelayB);

        isAttack = false;
    }

    IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                isSwing = false;
                Debug.Log(hitinfo.transform.name);//�浹
            }
            yield return null;
        }
    }

    private bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitinfo, currentHand.range))
        {
             return true;
        }
        return false;
    }
}
