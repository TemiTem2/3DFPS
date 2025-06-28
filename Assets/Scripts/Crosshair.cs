using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    private float gunAccuracy;//ÃÑ Á¤È®µµ

    [SerializeField]
    private GameObject go_CrosshairHUD;
    [SerializeField]
    private GunController gunController;

    public void WalkingAnimation(bool flag)
    {
        WeaponManager.currentWeaponAnim.SetBool("Walk", flag);
        anim.SetBool("Walking", flag);
    }
    public void RunningAnimation(bool flag)
    {
        WeaponManager.currentWeaponAnim.SetBool("Run", flag);
        anim.SetBool("Running", flag);
    }
    public void JumpAnimation(bool flag)
    {
        anim.SetBool("Running", flag);
    }
    public void CrouchingAnimation(bool flag)
    {
        anim.SetBool("Crouching", flag);
    }
    public void FineSightAnimation(bool flag)
    {
        anim.SetBool("FineSight", flag);
    }
    public void FireAnimation()
    {
        if (anim.GetBool("Walking"))
        {
            anim.SetTrigger("Walk_Fire");
        }
        else if (anim.GetBool("Crouching"))
        {
            anim.SetTrigger("Crouch_Fire");
        }
        else
        {
            anim.SetTrigger("Idle_Fire");
        }
    }

    public float GetGunAccuracy()
    {
        if (anim.GetBool("Walking"))
        {
            gunAccuracy = 0.08f; 
        }
        else if (anim.GetBool("Crouching"))
        {
            gunAccuracy = 0.02f;
        }
        else if (gunController.GetFinesightMode())
        {
            gunAccuracy = 0.001f;
        }
        else
        {
            gunAccuracy = 0.04f;
        }

        return gunAccuracy;
    }
}
