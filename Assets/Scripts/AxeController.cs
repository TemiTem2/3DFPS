using System.Collections;
using UnityEngine;

public class AxeController : CloseWeaponController
{
    public static bool isActive = true;

    private void Start()
    {
        WeaponManager.currentWeapon = currentCloseWeapon.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentCloseWeapon.anim;
    }
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
                Debug.Log(hitinfo.transform.name);//Ãæµ¹
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
