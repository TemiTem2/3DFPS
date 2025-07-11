using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HandController : CloseWeaponController
{
    public static bool isActive = false;
    void Update()
    {
        if (isActive)
            TryAttack();
    }


    protected override IEnumerator HitCoroutine()
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

    public override void CloseWeaponChange(CloseWeapon closeweapon)
    {
        base.CloseWeaponChange(closeweapon);
        isActive = true;
    }
}
