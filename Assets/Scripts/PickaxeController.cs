using System.Collections;
using UnityEngine;

public class PickaxeController : CloseWeaponController
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
                if (hitinfo.transform.tag == "Rock")
                {
                    hitinfo.transform.GetComponent<Rock>().Mining();
                }
                if (hitinfo.transform.tag == "NPC")
                {
                    SoundManager.instance.PlaySE("Animal_Hit");
                    hitinfo.transform.GetComponent<Pig>().Damage(1,transform.position);
                }
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
