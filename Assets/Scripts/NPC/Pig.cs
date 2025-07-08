using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Pig : MonoBehaviour
{
    [SerializeField] private string animalName;
    [SerializeField] private int hp;

    [SerializeField] private float walkSpeed;

    private Vector3 direction;
    private bool isWalking ;
    private bool isAction;
    [SerializeField] private float walkTime;
    [SerializeField] private float waitTime;
    private float currentTime;

    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private BoxCollider boxcol;

    private void Start()
    {
        currentTime = waitTime;
        isAction = true;
    }
    private void Update()
    {
        ElapseTime();
        Move();
        Rotation(); ;
    }

    private void Move()
    {
        if (isWalking)
        {
            rigid.MovePosition(transform.position + transform.forward * walkSpeed * Time.deltaTime);
        }

    }

    private void Rotation()
    {
        if (isWalking)
        {
            Vector3 rotation = Vector3.Lerp(transform.eulerAngles, direction, 0.01f);
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
        isAction = true;
        anim.SetBool("Walking", isWalking);
        direction.Set(0, Random.Range(0f,360f), 0);
        RandomAction();
    }

    private void RandomAction()
    {
        
        int _random = Random.Range(0, 4);

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
        anim.SetBool("Walking", isWalking);
    }
}
