using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private GunController gunController;
    private Gun currentGun;

    [SerializeField]
    private GameObject go_BulletHUD;

    [SerializeField]
    private TextMeshProUGUI[] text_Bullet;

    private void Update()
    {
        CheckBullet();
    }

    private void CheckBullet()
    {
        currentGun = gunController.GetGun();
        text_Bullet[0].text = currentGun.carryBulletCount.ToString();
        text_Bullet[1].text = currentGun.reloadBulletCount.ToString();
        text_Bullet[2].text = currentGun.currentBulletCount.ToString();
    }
}
