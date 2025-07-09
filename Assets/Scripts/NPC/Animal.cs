using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    [SerializeField] protected string animalName;
    [SerializeField] protected int hp;

    [SerializeField] protected float walkSpeed;
    [SerializeField] protected float runSpeed;

    protected Vector3 destination;
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
    protected NavMeshAgent navAgent;


    protected void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
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
        }

    }

    protected void Move()
    {
        if (isWalking || isRunning)
        {
            //rigid.MovePosition(transform.position + transform.forward * applySpeed * Time.deltaTime);
            navAgent.destination = transform.position + destination;
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
        navAgent.ResetPath();
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        destination.Set(Random.Range(-0.2f,0.2f),0, Random.Range(0.5f, 1f));
        navAgent.speed = walkSpeed;
    }




    protected void TryWalk()
    {
        currentTime = walkTime;
        isWalking = true;
        navAgent.speed = walkSpeed;
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
