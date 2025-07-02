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
    private GameObject go_rock;
    [SerializeField]
    private GameObject go_debris;
    [SerializeField]
    private GameObject go_effect_prefab;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip effectClip;
    [SerializeField]
    private AudioClip effectClip2;
    public void Mining()
    {
        audioSource.clip = effectClip;
        audioSource.Play();
        var clone = Instantiate(go_effect_prefab,col.bounds.center, Quaternion.identity);
        Destroy(clone, destroyTime);
        hp--;
        if (hp <= 0)
            Destruction();
    }
    private void Destruction()
    {
        audioSource.clip = effectClip2;
        audioSource.Play();
        col.enabled = false;
        Destroy(go_rock);

        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);
    }
}
