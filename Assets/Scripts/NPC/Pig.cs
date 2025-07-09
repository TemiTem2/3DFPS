using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Pig : MonoBehaviour
{
    [SerializeField] private string animalName;
    [SerializeField] private int hp;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    private float applySpeed;

    private Vector3 direction;
    private bool isWalking ;
    private bool isRunning;
    private bool isAction;
    private bool isDead;
    [SerializeField] private float walkTime;
    [SerializeField] private float waitTime;
    [SerializeField] private float runTime;
    private float currentTime;

    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private BoxCollider boxcol;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] sound_pig;
    [SerializeField] private AudioClip sound_pig_hurt;
    [SerializeField] private AudioClip sound_pig_dead;

    private void Start()
    {
        currentTime = waitTime;
        isAction = true;
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (!isDead) 
        {
            ElapseTime();
            Move();
            Rotation();
        }
        
    }

    private void Move()
    {
        if (isWalking||isRunning)
        {
            rigid.MovePosition(transform.position + transform.forward * applySpeed * Time.deltaTime);
        }

    }

    private void Rotation()
    {
        if (isWalking||isRunning)
        {
            Vector3 rotation = Vector3.Lerp(transform.eulerAngles,new Vector3(0, direction.y,0), 0.01f);
            rigid.MoveRotation(Quaternion.Euler(rotation));
        }
    }
    private void ElapseTime()
    {
        if (isAction) 
        { 
            currentTime -= Time.deltaTime;
            if(currentTime <= 0)
            {
                ReSet();
            }
        }
    }

    private void ReSet()
    {
        isWalking = false;
        isRunning = false;
        isAction = true;
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        direction.Set(0, Random.Range(0f,360f), 0);
        applySpeed = walkSpeed;
        RandomAction();
    }

    private void RandomAction()
    {
        
        int _random = Random.Range(0, 4);
        PlayRandomSound();

        if (_random == 0)
        {
            Wait();
        }
        if (_random == 1)
        {
            Eat();
        }
        if (_random == 2)
        {
            Peek();
        }
        if (_random == 3)
        {
            TryWalk();
        }
    }

    private void Wait()
    {
        currentTime = waitTime;
        Debug.Log("´ë±â");
    }
    private void Eat()
    {
        currentTime = waitTime;
        anim.SetTrigger("Eat");
    }
    private void Peek()
    {
        currentTime = waitTime;
        anim.SetTrigger("Peek");
    }
    private void TryWalk()
    {
        currentTime = walkTime;
        isWalking = true;
        applySpeed = walkSpeed;
        anim.SetBool("Walking", isWalking);
    }
    private void Run(Vector3 _targetPos)
    {
        direction = Quaternion.LookRotation(transform.position- _targetPos).eulerAngles;

        currentTime = runTime;
        applySpeed = runSpeed;
        isWalking = false;
        isRunning = true;
        anim.SetBool("Running", isRunning);
    }

    public void Damage(int _damaga,Vector3 _targetPos)
    {
        if (!isDead)
        {
            hp -= _damaga;
            if (hp <= 0)
            {
                Dead();
                return;
            }
            PlaySE(sound_pig_hurt);
            anim.SetTrigger("Hurt");
            Run(_targetPos);
        }
        
    }
    private void Dead()
    {
        PlaySE(sound_pig_dead);
        anim.SetTrigger("Dead");
        isWalking = false;
        isRunning = false;
        isDead = true;
    }

    private void PlayRandomSound()
    {
        int _random = Random.Range(0, sound_pig.Length);
        PlaySE(sound_pig[_random]);
    }
    private void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }
}
