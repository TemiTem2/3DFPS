using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] protected string animalName;
    [SerializeField] protected int hp;

    [SerializeField] protected float walkSpeed;
    [SerializeField] protected float runSpeed;
    [SerializeField] protected float turningSpeed;
    protected float applySpeed;

    protected Vector3 direction;
    protected bool isWalking;
    protected bool isRunning;
    protected bool isAction;
    protected bool isDead;
    [SerializeField] protected float walkTime;
    [SerializeField] protected float waitTime;
    [SerializeField] protected float runTime;
    protected float currentTime;

    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected BoxCollider boxcol;
    protected AudioSource audioSource;
    [SerializeField] protected AudioClip[] sound_Normal;
    [SerializeField] protected AudioClip sound_hurt;
    [SerializeField] protected AudioClip sound_dead;


    protected void Start()
    {
        currentTime = waitTime;
        isAction = true;
        audioSource = GetComponent<AudioSource>();
    }
    protected void Update()
    {
        if (!isDead)
        {
            ElapseTime();
            Move();
            Rotation();
        }

    }

    protected void Move()
    {
        if (isWalking || isRunning)
        {
            rigid.MovePosition(transform.position + transform.forward * applySpeed * Time.deltaTime);
        }

    }

    protected void Rotation()
    {
        if (isWalking || isRunning)
        {
            Vector3 rotation = Vector3.Lerp(transform.eulerAngles, new Vector3(0, direction.y, 0), turningSpeed);
            rigid.MoveRotation(Quaternion.Euler(rotation));
        }
    }
    protected void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                ReSet();
            }
        }
    }

    protected virtual void ReSet()
    {
        isWalking = false;
        isRunning = false;
        isAction = true;
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        direction.Set(0, Random.Range(0f, 360f), 0);
        applySpeed = walkSpeed;
    }




    protected void TryWalk()
    {
        currentTime = walkTime;
        isWalking = true;
        applySpeed = walkSpeed;
        anim.SetBool("Walking", isWalking);
    }
    

    public virtual void Damage(int _damaga, Vector3 _targetPos)
    {
        if (!isDead)
        {
            hp -= _damaga;
            if (hp <= 0)
            {
                Dead();
                return;
            }
            PlaySE(sound_hurt);
            anim.SetTrigger("Hurt");
        }

    }
    protected void Dead()
    {
        PlaySE(sound_dead);
        anim.SetTrigger("Dead");
        isWalking = false;
        isRunning = false;
        isDead = true;
    }

    protected void PlayRandomSound()
    {
        int _random = Random.Range(0, sound_Normal.Length);
        PlaySE(sound_Normal[_random]);
    }
    protected void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }
}
