using UnityEngine;

public class Rock : MonoBehaviour
{

    [SerializeField]
    private int hp;
    [SerializeField]
    private float destroyTime;
    [SerializeField]
    private SphereCollider col;
    [SerializeField]
    private GameObject go_rock; //돌
    [SerializeField]
    private GameObject go_debris;//부서진 돌
    [SerializeField]
    private GameObject go_effect_prefab;//채굴 이펙트
    [SerializeField]
    private GameObject go_rockitem_prefab;//아이템

    [SerializeField]
    private int count; //아이템 개수

    [SerializeField]
    private string strike_Sound;
    [SerializeField]
    private string destroy_Sound;

    public void Mining()
    {
        SoundManager.instance.PlaySE(strike_Sound);
        var clone = Instantiate(go_effect_prefab,col.bounds.center, Quaternion.identity);
        Destroy(clone, destroyTime);
        hp--;
        if (hp <= 0)
            Destruction();
    }
    private void Destruction()
    {
        SoundManager.instance.PlaySE(destroy_Sound);
        col.enabled = false;
        for (int i = 0; i <= count; i++)
        {
            Instantiate(go_rockitem_prefab, go_rock.transform.position, Quaternion.identity);
        }
        
        Destroy(go_rock);

        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);
    }
}
